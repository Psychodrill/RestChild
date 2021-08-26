using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using OfficeOpenXml;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Перенос размещений
    /// </summary>
    public static class ChildTransferQueue
    {
        /// <summary>
        ///     в процессе
        /// </summary>
        private static bool inProgress;

        /// <summary>
        ///     блокировщик
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        ///     статус
        /// </summary>
        private static ProgressStatus status;

        /// <summary>
        ///     тело загружаемого файла
        /// </summary>
        private static byte[] file;

        /// <summary>
        ///     процесс 
        /// </summary>
        public static bool InProgress =>
            inProgress && status != null && status.LastUpdate > DateTime.Now.AddSeconds(-40);

        /// <summary>
        ///     исполняемый метод
        /// </summary>
        public static string Execute(byte[] File)
        {
            lock (locker)
            {
                if (InProgress)
                {
                    if (status != null && status.LastUpdate > DateTime.Now.AddMinutes(-3))
                    {
                        return "Процесс занят";
                    }
                }

                file = File;
                inProgress = true;
                status = new ProgressStatus {Message = "Message", Step = 1, Steps = 100};


                var th = new Thread(Do);
                th.Start();
            }

            return null;
        }

        /// <summary>
        ///     установить статус
        /// </summary>
        private static void SetStatus(ProgressStatus status)
        {
            lock (locker)
            {
                if (status != null && ChildTransferQueue.status != null)
                {
                    if (status.IsError)
                    {
                        ChildTransferQueue.status.IsError = true;
                    }
                    else
                    {
                        ChildTransferQueue.status.Step = status.Step;
                        ChildTransferQueue.status.Steps = status.Steps;
                    }

                    ChildTransferQueue.status.Message = status.Message;
                    ChildTransferQueue.status.LastUpdate = DateTime.Now;
                }
            }
        }

        /// <summary>
        ///     получить текущий статус
        /// </summary>
        public static ProgressStatus GetStatus()
        {
            return status ?? new ProgressStatus {IsError = true, Message = "Процесс не запущен"};
        }

        /// <summary>
        ///     получить обработанные данные из загруженного excel документа
        /// </summary>
        private static IDictionary<string, List<REQS>> GetDataFromExcel(byte[] file)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = new MemoryStream(file))
                {
                    pck.Load(stream);
                }

                var result = new Dictionary<string, List<REQS>>();
                foreach (var ws in pck.Workbook.Worksheets.ToList())
                {
                    var startRow = 1;
                    if (!int.TryParse(ws.Cells[startRow, 2].Text, out var numzav) ||
                        !int.TryParse(ws.Cells[startRow, 43].Text, out var idzaezd))
                    {
                        startRow++;
                    }

                    var columnsCount = ws.Dimension.End.Column;
                    var row = new List<REQS>();
                    for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        row.Add(new REQS
                        {
                            RequestNumber = ws.Cells[rowNum, 1].Text, NewTourId = long.Parse(ws.Cells[rowNum, 43].Text)
                        });
                    }

                    result.Add(ws.Name, row.Distinct(new DistinctItemComparer()).ToList());
                }

                return result;
            }
        }

        /// <summary>
        ///     перевести размещения
        /// </summary>
        private static void Do()
        {
            try
            {
                using (var uv = new UnitOfWork())
                {
                    SetStatus(new ProgressStatus
                        {IsError = false, Steps = 100, Step = 0, Message = "Открытие файла данных..."});
                    var reqs = GetDataFromExcel(file);
                    file = null;

                    var errors = new List<string>();

                    var steps = reqs.Count * reqs.Values.Sum(sx => sx.Count()) + 2;
                    SetStatus(new ProgressStatus
                    {
                        IsError = false, Steps = steps, Step = 1,
                        Message = $"Файл данных открыт. Извлечено {steps} строк."
                    });

                    var index = 2;

                    foreach (var ws in reqs)
                    {
                        SetStatus(new ProgressStatus
                        {
                            IsError = false, Steps = steps, Step = index,
                            Message = $"Завершено на {Math.Round((double) (index * 100 / steps))}%;"
                        });
                        index++;

                        //Thread.Sleep(3500);

                        var psteps = 0;

                        try
                        {
                            psteps = ws.Value.Count();

                            using (var tran = uv.GetTransactionScope())
                            {
                                for (var i = 0; i < ws.Value.Count(); i++)
                                {
                                    SetStatus(new ProgressStatus
                                    {
                                        IsError = false, Steps = steps, Step = index,
                                        Message = $"Завершено на {Math.Round((double) (index * 100 / steps))}%;"
                                    });
                                    index++;
                                    psteps--;
                                    var rr = ws.Value[i];

                                    rr.Id = uv.GetSet<Request>().Where(ss => ss.RequestNumber == rr.RequestNumber).Select(ss => ss.Id).First();
                                    var tour = uv.GetSet<Tour>().First(ss => ss.Id == rr.NewTourId);

                                    rr.HotelsId = tour.HotelsId;
                                    rr.SubjectOfRestId = tour.SubjectOfRestId;
                                    rr.PlaceOfRestId = tour.Hotels.PlaceOfRestId;

                                    ws.Value[i] = rr;
                                }

                                var bookings = new List<Bookings>();
                                foreach (var row in ws.Value)
                                {
                                    var booking_f = uv.GetSet<Domain.Booking>().FirstOrDefault(ss => !ss.Canceled && ss.RequestId == row.Id);

                                    var tourVolume_t = booking_f.TourVolume.TypeOfRoomsId != null ?
                                        uv.GetSet<TourVolume>().FirstOrDefault(ss => ss.TourId == row.NewTourId && ss.TypeOfRooms.CountBasePlace == booking_f.TourVolume.TypeOfRooms.CountBasePlace)
                                        : uv.GetSet<TourVolume>().FirstOrDefault(ss => ss.TourId == row.NewTourId && ss.TypeOfRoomsId == null);

                                    bookings.Add(new Bookings
                                    {
                                        Id = booking_f?.Id ?? 0,
                                        RequestId = booking_f?.RequestId,
                                        OldTv = booking_f?.TourVolumeId,
                                        CountRooms = booking_f?.CountRooms ?? 0,
                                        CountPlace = (booking_f?.CountPlace ?? 0) + (booking_f?.CountAttendants ?? 0),
                                        NewTv = tourVolume_t?.Id
                                    });
                                }

                                if (ws.Value.Any(ss => bookings.All(sx => sx.RequestId != ss.Id)))
                                {
                                    errors.Add($"Ошибка: [{ws.Key}] таблица для переноса мест не корректна");
                                    continue;
                                }

                                if (bookings.Any(ss => ss.OldTv == null || ss.NewTv == null || ss.RequestId == null))
                                {
                                    errors.Add($"Ошибка: [{ws.Key}] таблица для переноса мест не корректна");
                                    continue;
                                }

                                foreach (var row in ws.Value)
                                {
                                    var r = uv.GetSet<Request>().First(ss => ss.Id == row.Id);
                                    r.EidSendStatus = 1;
                                    r.HotelsId = row.HotelsId;
                                    r.SubjectOfRestId = row.SubjectOfRestId;
                                    r.TourId = row.NewTourId;
                                    uv.SaveChanges();
                                }

                                foreach (var b in bookings)
                                {
                                    var r = uv.GetSet<Domain.Booking>().First(ss => ss.Id == b.Id);
                                    r.EidSendStatus = 1;
                                    r.TourVolumeId = b.NewTv.Value;
                                    uv.SaveChanges();
                                }

                                foreach (var b in bookings.GroupBy(p => p.NewTv).Select(ss =>
                                    new
                                    {
                                        Id = ss.Key, CountPlace = ss.Sum(sx => sx.CountPlace),
                                        CountRooms = ss.Sum(sx => sx.CountRooms)
                                    }))
                                {
                                    var r = uv.GetSet<TourVolume>().First(ss => ss.Id == b.Id);
                                    r.EidSendStatus = 1;
                                    r.CountBusyPlace = (r.CountBusyPlace ?? 0) + b.CountPlace;
                                    r.CountBusyRooms = (r.CountBusyRooms ?? 0) + b.CountRooms;
                                    uv.SaveChanges();
                                }

                                foreach (var b in bookings.GroupBy(p => p.OldTv).Select(ss =>
                                    new
                                    {
                                        Id = ss.Key, CountPlace = ss.Sum(sx => sx.CountPlace),
                                        CountRooms = ss.Sum(sx => sx.CountRooms)
                                    }))
                                {
                                    var r = uv.GetSet<TourVolume>().First(ss => ss.Id == b.Id);
                                    r.EidSendStatus = 1;
                                    if ((r.CountBusyPlace ?? 0) > b.CountPlace)
                                    {
                                        r.CountBusyPlace -= b.CountPlace;
                                    }
                                    else
                                    {
                                        r.CountBusyPlace = 0;
                                    }

                                    if ((r.CountBusyRooms ?? 0) > b.CountRooms)
                                    {
                                        r.CountBusyRooms -= b.CountRooms;
                                    }
                                    else
                                    {
                                        r.CountBusyRooms = 0;
                                    }

                                    uv.SaveChanges();
                                }

                                tran.Complete();
                            }
                        }
                        catch
                        {
                            errors.Add($"Ошибка: {ws.Key} не распознанная ошибка");

                            SetStatus(new ProgressStatus
                            {
                                IsError = false, Steps = steps, Step = index + psteps,
                                Message = $"Завершено на {Math.Round((double) ((index + psteps) * 100 / steps))}%"
                            });
                        }
                    }

                    SetStatus(new ProgressStatus
                    {
                        IsError = false, Steps = steps, Step = steps,
                        Message =
                            $"Завершено на 100%{(errors.Any() ? $"; Возникшие ошибки: <br/>{string.Join("<br/>", errors)}" : string.Empty)}"
                    });
                }
            }
            catch (Exception ex)
            {
                file = null;

                SetStatus(new ProgressStatus
                    {IsError = true, Message = "Во время загрузки произошел сбой: " + ex.Message});
            }
        }

        /// <summary>
        ///     изначальные данные
        /// </summary>
        private struct REQS
        {
            /// <summary>
            ///     идентификатор заявления
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            ///     идентификатор размещения откуда осуществляем перенос
            /// </summary>
            public long OldTourId { get; set; }

            /// <summary>
            ///     идентификатор заявления текстовый
            /// </summary>
            public string RequestNumber { get; set; }

            /// <summary>
            ///     идентификатор размещения куда осуществляем перенос
            /// </summary>
            public long NewTourId { get; set; }

            /// <summary>
            ///     идентификатор гостиницы
            /// </summary>
            public long? HotelsId { get; set; }

            /// <summary>
            ///     идентификатор тематики смены
            /// </summary>
            public long? SubjectOfRestId { get; set; }

            /// <summary>
            ///     идентификатор региона
            /// </summary>
            public long? PlaceOfRestId { get; set; }
        }

        /// <summary>
        ///     бронирование
        /// </summary>
        private struct Bookings
        {
            /// <summary>
            ///     идентификатор
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            ///     заявление
            /// </summary>
            public long? RequestId { get; set; }

            /// <summary>
            ///     идентификатор номерного фонда (старый)
            /// </summary>
            public long? OldTv { get; set; }

            /// <summary>
            ///     идентификатор номерного фонда (новый)
            /// </summary>
            public long? NewTv { get; set; }

            /// <summary>
            ///     количество мест
            /// </summary>
            public int CountPlace { get; set; }

            /// <summary>
            ///     количество комнат
            /// </summary>
            public int CountRooms { get; set; }
        }

        /// <summary>
        ///     компоратор для результирующих данных из загруженнного файла 
        /// </summary>
        private class DistinctItemComparer : IEqualityComparer<REQS>
        {
            public bool Equals(REQS x, REQS y)
            {
                return string.Equals(x.RequestNumber, y.RequestNumber, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode(REQS obj)
            {
                return obj.RequestNumber.GetHashCode();
            }
        }
    }


    /// <summary>
    ///     Контроллер переноса размещений
    /// </summary>
    public class ChildTransferController : BaseController
    {
        /// <summary>
        ///     Главная страница
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Запуск процесса переноса
        /// </summary>
        public ActionResult StartProcess()
        {
            if (!Security.HasRight(AccessRightEnum.ChildTransfer))
            {
                return RedirectToAvalibleAction();
            }


            if (Request.Files.Count != 1)
            {
                return View("Index", (object) "Ошибка загрузки файла");
            }

            var file = Request.Files[0];

            byte[] data;
            using (var target = new MemoryStream())
            {
                file.InputStream.CopyTo(target);
                data = target.ToArray();
            }

            var result = ChildTransferQueue.Execute(data);

            if (string.IsNullOrWhiteSpace(result))
            {
                return RedirectToAction("Index");
            }

            return View("Index", (object) result);
        }

        /// <summary>
        ///     Получить статус процесса переноса
        /// </summary>
        public ActionResult GetStatus(Guid Index)
        {
            if (!Security.HasRight(AccessRightEnum.ChildTransfer))
            {
                return RedirectToAvalibleAction();
            }

            return Json(ChildTransferQueue.GetStatus(), JsonRequestBehavior.AllowGet);
        }
    }
}
