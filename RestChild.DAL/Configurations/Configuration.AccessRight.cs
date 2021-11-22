using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        private static long startIndex = 290;

        /// <summary>
        ///     Права
        /// </summary>
        private static void AccessRight(Context context)
        {
            context.AccessRight.AddOrUpdate(s => s.Id,
                new AccessRight
                {
                    Id = 1,
                    Code = AccessRightEnum.AccountManage,
                    Name = "Управление пользователями"
                },
                new AccessRight
                {
                    Id = 2,
                    Code = AccessRightEnum.PlaceOfRestManage,
                    Name = "Управление регионами отдыха"
                },
                new AccessRight
                {
                    Id = 3,
                    Code = AccessRightEnum.VocabularyManage,
                    Name = "Управление справочниками"
                },
                new AccessRight
                {
                    Id = 4,
                    Code = AccessRightEnum.RequestManage,
                    Name = "Управление заявления на первую заявочную кампанию"
                },
                new AccessRight
                {
                    Id = 5,
                    Code = AccessRightEnum.Status.ToSend,
                    Name = "Заявление. Перевод в статус Подано. Заявка находится на рассмотрении"
                },
                new AccessRight
                {
                    Id = 6,
                    Code = AccessRightEnum.Status.ToReject,
                    Name = "Заявление. Перевод в статус Услуга оказана. Отказ в предоставлении услуги"
                },
                new AccessRight
                {
                    Id = 7,
                    Code = AccessRightEnum.Status.ToRejectNormal,
                    Name =
                        "Заявление. Перевод в статус Услуга оказана. Отказ в предоставлении услуги из статуса ожидания прихода заявителя"
                },
                new AccessRight
                {
                    Id = 8,
                    Code = AccessRightEnum.Status.ToStoped,
                    Name = "Заявление. Перевод в статус Приостановлено. Запрос на отзыв по инициативе заявителя"
                },
                new AccessRight
                {
                    Id = 9,
                    Code = AccessRightEnum.Status.CertificateIssued,
                    Name = "Перевод в статус Включить в реестр"
                },
                new AccessRight
                {
                    Id = 10,
                    Code = AccessRightEnum.Status.ApplicantCome,
                    Name =
                        "Заявление. Перевод в статус Формирование результата предоставления услуги"
                },
                new AccessRight
                {
                    Id = 11,
                    Code = AccessRightEnum.Status.ToWaitApplicant,
                    Name = "Заявление. Перевод в статус Приостановлено. Ожидание прихода заявителя"
                },
                new AccessRight
                {
                    Id = 12,
                    Code = AccessRightEnum.Status.EditInWaitApplicant,
                    Name = "Заявление. Перевод в статус Сведения предоставлены заявителем"
                },
                new AccessRight
                {
                    Id = 13,
                    Code = AccessRightEnum.RequestView,
                    Name = "Заявление. Просмотр заявлений"
                },
                new AccessRight
                {
                    Id = 14,
                    Code = AccessRightEnum.InteragencyRequestManage,
                    Name = "Работа с межведомственным запросом. Управление"
                },
                new AccessRight
                {
                    Id = 15,
                    Code = AccessRightEnum.EditAfterRegistration,
                    Name = "Редактирование заявления после регистрации"
                },
                new AccessRight
                {
                    Id = 16,
                    Code = AccessRightEnum.Organization.View,
                    Name = "Организации. Просмотр списка"
                },
                new AccessRight
                {
                    Id = 17,
                    Code = AccessRightEnum.Organization.Edit,
                    Name = "Организации. Редактирование"
                },
                new AccessRight
                {
                    Id = 18,
                    Code = AccessRightEnum.Limits.LimitToOiv,
                    Name = "Назначение и согласование квот ОИВ"
                },
                new AccessRight
                {
                    Id = 19,
                    Code = AccessRightEnum.Limits.LimitByOrganization,
                    Name = "Назначение и согласование квот организациям",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 20,
                    Code = AccessRightEnum.Limits.LimitChildInOrganization,
                    Name = "Составление списков детей по организациям",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 21,
                    Code = AccessRightEnum.Limit.Oiv.Formation,
                    Name = "Списки. ОИВ. Переход в статус Формирование"
                },
                new AccessRight
                {
                    Id = 22,
                    Code = AccessRightEnum.Limit.Oiv.Brought,
                    Name = "Списки. ОИВ. Переход в статус Доведены до ведомства/ Формирование квот по учреждениям"
                },
                new AccessRight
                {
                    Id = 23,
                    Code = AccessRightEnum.Limit.Oiv.BroughtToOrganization,
                    Name = "Списки. ОИВ. Переход в статус доведены до организаций"
                },
                new AccessRight
                {
                    Id = 24,
                    Code = AccessRightEnum.Limit.Organization.Formation,
                    Name = "Списки. Учреждение. Переход в статус Формирование"
                },
                new AccessRight
                {
                    Id = 25,
                    Code = AccessRightEnum.Limit.Organization.Brought,
                    Name = "Списки. Учреждение. Переход в статус Доведены до организации/ Формирование списков"
                },
                new AccessRight
                {
                    Id = 26,
                    Code = AccessRightEnum.Limit.Organization.BroughtToCompletion,
                    Name =
                        "Списки. Учреждение. Переход в статус Доведены до организации/ Формирование списков (из статуса на доработку)"
                },
                new AccessRight
                {
                    Id = 27,
                    Code = AccessRightEnum.Limit.Organization.ToApprove,
                    Name = "Списки. Учреждение. Переход в статус Отправлена на утверждение в ОИВ"
                },
                new AccessRight
                {
                    Id = 28,
                    Code = AccessRightEnum.Limit.Organization.Approved,
                    Name = "Списки. Учреждение. Переход в статус Утверждено"
                },
                new AccessRight
                {
                    Id = 29,
                    Code = AccessRightEnum.Limit.Organization.OnCompletion,
                    Name = "Списки. Учреждение. Переход в статус На доработку"
                },
                new AccessRight
                {
                    Id = 30,
                    Code = AccessRightEnum.Limit.Organization.Confirmed,
                    Name = "Списки. Учреждение. Переход в статус подтверждено ДКгМ"
                },
                new AccessRight
                {
                    Id = 31,
                    Code = AccessRightEnum.Limit.Organization.ApprovedToCompetiotion,
                    Name = "Списки. Учреждение. Отправка на доработку из статуса утверждено (ДКгМ)"
                },
                new AccessRight
                {
                    Id = 32,
                    Code = AccessRightEnum.Limit.List.Formation,
                    Name = "Списки. Список учреждения. Переход в статус Формирование"
                },
                new AccessRight
                {
                    Id = 33,
                    Code = AccessRightEnum.Limit.List.Formed,
                    Name = "Списки. Список учреждения. Переход в статус Сформирован"
                },
                new AccessRight
                {
                    Id = 34,
                    Code = AccessRightEnum.Limit.List.IncludedPayment,
                    Name = "Списки. Список учреждения. Переход в статус сведения об оплате внесены"
                },
                new AccessRight
                {
                    Id = 35,
                    Code = AccessRightEnum.Limit.Oiv.OnCompletion,
                    Name = "Списки. ОИВ. Возврат доведенных квот."
                },
                new AccessRight
                {
                    Id = 38,
                    Code = AccessRightEnum.Hotel.Manage,
                    Name = "Управление оздоровительными организациями"
                },
                new AccessRight
                {
                    Id = 39,
                    Code = AccessRightEnum.Hotel.View,
                    Name = "Просмотр оздоровительных организаций",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 40,
                    Code = AccessRightEnum.ToursManage,
                    Name = "Размещение. Управление размещениями"
                },
                new AccessRight
                {
                    Id = 41,
                    Code = AccessRightEnum.ToursView,
                    Name = "Размещение. Просмотр размещений"
                },
                new AccessRight
                {
                    Id = 43,
                    Code = AccessRightEnum.Tour.ToTourForm,
                    Name = "Размещение. Формирование размещения"
                },
                new AccessRight
                {
                    Id = 44,
                    Code = AccessRightEnum.Tour.TourPay,
                    Name = "Размещение. Оплата размещения"
                },
                new AccessRight
                {
                    Id = 45,
                    Code = AccessRightEnum.Hotel.ForAprove,
                    Name = "Оздоровительная организация. Отправка на утверждение"
                },
                new AccessRight
                {
                    Id = 46,
                    Code = AccessRightEnum.Hotel.Approved,
                    Name = "Оздоровительная организация. Утверждение"
                },
                new AccessRight
                {
                    Id = 47,
                    Code = AccessRightEnum.Hotel.ForRework,
                    Name = "Оздоровительная организация. Отправка на доработку"
                },
                new AccessRight
                {
                    Id = 48,
                    Code = AccessRightEnum.Hotel.OnReworking,
                    Name = "Оздоровительная организация. Доработка"
                },
                new AccessRight
                {
                    Id = 49,
                    Code = AccessRightEnum.Counselor.ForAprove,
                    Name = "Отправка информации о вожатом на утверждение"
                },
                new AccessRight
                {
                    Id = 50,
                    Code = AccessRightEnum.Counselor.Approved,
                    Name = "Утверждение информации о вожатом"
                },
                new AccessRight
                {
                    Id = 51,
                    Code = AccessRightEnum.Counselor.ForRework,
                    Name = "Отправка информации о вожатом на доработку"
                },
                new AccessRight
                {
                    Id = 52,
                    Code = AccessRightEnum.Counselor.OnReworking,
                    Name = "Доработка информации о вожатом"
                },
                new AccessRight
                {
                    Id = 53,
                    Code = AccessRightEnum.CounselorsManage,
                    Name = "Управление вожатыми"
                },
                new AccessRight
                {
                    Id = 54,
                    Code = AccessRightEnum.Contract.Register,
                    Name = "Договор. Зарегистрировать контракт"
                },
                new AccessRight
                {
                    Id = 55,
                    Code = AccessRightEnum.Contract.Archive,
                    Name = "Договор. Перевести контракт в архив"
                },
                new AccessRight
                {
                    Id = 56,
                    Code = AccessRightEnum.Contract.Manage,
                    Name = "Договор. Управление договорами"
                },
                new AccessRight
                {
                    Id = 57,
                    Code = AccessRightEnum.BoutManage,
                    Name = "Управление заездами"
                },
                new AccessRight
                {
                    Id = 58,
                    Code = AccessRightEnum.Bout.Forming,
                    Name = "Формирование заезда"
                },
                new AccessRight
                {
                    Id = 59,
                    Code = AccessRightEnum.CityView,
                    Name = "Просмотр городов"
                },
                new AccessRight
                {
                    Id = 60,
                    Code = AccessRightEnum.CityManage,
                    Name = "Управление городами"
                },
                new AccessRight
                {
                    Id = 61,
                    Code = AccessRightEnum.PartyManage,
                    Name = "Управление отрядами"
                },
                new AccessRight
                {
                    Id = 62,
                    Code = AccessRightEnum.AdministratorTour.Manage,
                    Name = "Управление администраторами смен"
                },
                new AccessRight
                {
                    Id = 63,
                    Code = AccessRightEnum.Tour.TourForm,
                    Name = "Размещение. Утверждение"
                },
                new AccessRight
                {
                    Id = 64,
                    Code = AccessRightEnum.Report.TourReport,
                    Name = "Отчет. Блоки мест"
                },
                new AccessRight
                {
                    Id = 65,
                    Code = AccessRightEnum.Report.ServiceStatistics,
                    Name = "Отчет. Просмотр статистики оказания государственной услуги"
                },
                new AccessRight
                {
                    Id = 66,
                    Code = AccessRightEnum.Tour.TourEdit,
                    Name = "Размещение. Редактирование"
                },
                new AccessRight
                {
                    Id = 67,
                    Code = AccessRightEnum.Status.ToRejectFromCertificateIssued,
                    Name = "Заявление. В отказ из путевка выдана"
                },
                new AccessRight
                {
                    Id = 68,
                    Code = AccessRightEnum.Report.StatisticReport,
                    Name = "Отчет. Статистика обработки заявлений"
                },
                new AccessRight
                {
                    Id = 69,
                    Code = AccessRightEnum.DirectoryFlightsManage,
                    Name = "Рейс. Управление рейсами",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 70,
                    Code = AccessRightEnum.DirectoryFlights.Form,
                    Name = "Рейс. Переход в статус Сформирован"
                },
                new AccessRight
                {
                    Id = 71,
                    Code = AccessRightEnum.DirectoryFlights.Edit,
                    Name = "Рейс. Переход в статус Формирование"
                },
                new AccessRight
                {
                    Id = 72,
                    Code = AccessRightEnum.Transport.Form,
                    Name = "Транспорт. Переход в статус Сформирован"
                },
                new AccessRight
                {
                    Id = 73,
                    Code = AccessRightEnum.Transport.Edit,
                    Name = "Транспорт. Переход в статус Формирование"
                },
                new AccessRight
                {
                    Id = 74,
                    Code = AccessRightEnum.TransportInfoManage,
                    Name = "Транспорт. Управление транспортом"
                },
                new AccessRight
                {
                    Id = 75,
                    Code = AccessRightEnum.RetryRequestInBaseRegistry,
                    Name = "Заявление. Повторный запрос в базовый регистр"
                },
                new AccessRight
                {
                    Id = 76,
                    Code = AccessRightEnum.AddonRequest,
                    Name = "Заявление. Подача заявления на дополнительные места"
                },
                new AccessRight
                {
                    Id = 77,
                    Code = AccessRightEnum.Status.ToReadyToPay,
                    Name = "Заявление. Перевод в статус готово к оплате"
                },
                new AccessRight
                {
                    Id = 78,
                    Code = AccessRightEnum.Bout.NotComeInPlaceOfRestForAllBouts,
                    Name = "Заезд. Изменение признака \"Не явился в место отдыха\" отдыхающего в любом заезде"
                },
                new AccessRight
                {
                    Id = 79,
                    Code = AccessRightEnum.Bout.AdministratorTour,
                    Name = "Заезд. Администратор смены"
                },
                new AccessRight
                {
                    Id = 80,
                    Code = AccessRightEnum.Party.Forming,
                    Name = "Отряд. Формирование"
                },
                new AccessRight
                {
                    Id = 81,
                    Code = AccessRightEnum.Party.Edit,
                    Name = "Отряд. Начать редактирование"
                },
                new AccessRight
                {
                    Id = 82,
                    Code = AccessRightEnum.Bout.Edit,
                    Name = "Заезд. Начать редактирование"
                },
                new AccessRight
                {
                    Id = 83,
                    Code = AccessRightEnum.Limit.Organization.ConfirmedToApproved,
                    Name = "Списки. Учреждение. Вернуть на утверждение"
                },
                new AccessRight
                {
                    Id = 84,
                    Code = AccessRightEnum.Limit.List.EditInAllStates,
                    Name = "Списки. Редактирование в любом статусе"
                },
                new AccessRight
                {
                    Id = 85,
                    Code = AccessRightEnum.Report.SpecializedCampsReport,
                    Name = "Отчет. Отдыхающие в профильных лагерях"
                },
                new AccessRight
                {
                    Id = 86,
                    Code = AccessRightEnum.AddonService.View,
                    Name = "Доп. услуги. Просмотр",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 87,
                    Code = AccessRightEnum.AddonService.Edit,
                    Name = "Доп. услуги. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 88,
                    Code = AccessRightEnum.AddonService.ToFormed,
                    Name = "Доп. услуги. В статус сформировано",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 89,
                    Code = AccessRightEnum.AddonService.ToForming,
                    Name = "Доп. услуги. В статус редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 90,
                    Code = AccessRightEnum.Transport.SetNotNeedTicketReason,
                    Name = "Транспорт. Указание причины отказа от билета"
                },
                new AccessRight
                {
                    Id = 91,
                    Code = AccessRightEnum.RequestWithoutBookingDate,
                    Name = "Заявление. Подача заявления на блок мест на котором нет приема."
                },
                new AccessRight
                {
                    Id = 92,
                    Code = AccessRightEnum.AddonServiceInRequest,
                    Name = "Заявление. Услуги",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 93,
                    Code = AccessRightEnum.Bout.Confirmed,
                    Name = "Заезд. Подтверждение заезда"
                },
                new AccessRight
                {
                    Id = 94,
                    Code = AccessRightEnum.Transport.SetNotNeedTicket,
                    Name = "Транспорт. Отказ от билета"
                },
                new AccessRight
                {
                    Id = 95,
                    Code = AccessRightEnum.ExcludeChild,
                    Name = "Заявление. Исключение ребёнка"
                },
                new AccessRight
                {
                    Id = 96,
                    Code = AccessRightEnum.RemoveDraft,
                    Name = "Заявление. Удаление черновика"
                }
                ,
                new AccessRight
                {
                    Id = 97,
                    Code = AccessRightEnum.CounselorTask.View,
                    Name = "Задачи вожатым. Просмотр задач"
                },
                new AccessRight
                {
                    Id = 98,
                    Code = AccessRightEnum.CounselorTask.Edit,
                    Name = "Задачи вожатым. Редактирование и создание задач"
                },
                new AccessRight
                {
                    Id = 99,
                    Code = AccessRightEnum.CounselorTask.ToApproved,
                    Name = "Задачи вожатым. В статус на согласовании"
                },
                new AccessRight
                {
                    Id = 100,
                    Code = AccessRightEnum.CounselorTask.ToDelivered,
                    Name = "Задачи вожатым. В статус поставлена"
                },
                new AccessRight
                {
                    Id = 101,
                    Code = AccessRightEnum.CounselorTask.ToSolved,
                    Name = "Задачи вожатым. В статус решена"
                },
                new AccessRight
                {
                    Id = 102,
                    Code = AccessRightEnum.CounselorTask.ToCompletion,
                    Name = "Задачи вожатым. В статус на доработку"
                },
                new AccessRight
                {
                    Id = 103,
                    Code = AccessRightEnum.CounselorTask.EditDeliveredTask,
                    Name = "Задачи вожатым. Редактирование всех поставленных задач"
                },
                new AccessRight
                {
                    Id = 104,
                    Code = AccessRightEnum.CounselorTask.DeleteTask,
                    Name = "Задачи вожатым. Удаление задач"
                }
                , new AccessRight
                {
                    Id = 105,
                    Code = AccessRightEnum.CounselorTask.ViewAll,
                    Name = "Задачи вожатым. Просмотр всех задач (оператор Мосгортур)"
                },
                new AccessRight
                {
                    Id = 106,
                    Code = AccessRightEnum.Tour.ToFormationFromFormed,
                    Name = "Размещение. В формирование из сформирован."
                },
                new AccessRight
                {
                    Id = 62,
                    Code = AccessRightEnum.AdministratorTour.Manage,
                    Name = "Администратор смен. Управление"
                },
                new AccessRight
                {
                    Id = 107,
                    Code = AccessRightEnum.AdministratorTour.View,
                    Name = "Администратор смен. Просмотр"
                },
                new AccessRight
                {
                    Id = 108,
                    Code = AccessRightEnum.AdministratorTour.CreateAccount,
                    Name = "Администратор смен. Создание пользователей"
                },
                new AccessRight
                {
                    Id = 109,
                    Code = AccessRightEnum.AdministratorTour.ToEdit,
                    Name = "Администратор смен. В редактирование"
                },
                new AccessRight
                {
                    Id = 110,
                    Code = AccessRightEnum.AdministratorTour.ToFormed,
                    Name = "Администратор смен. В сформирован"
                },
                new AccessRight
                {
                    Id = 111,
                    Code = AccessRightEnum.AddonService.ToArchive,
                    Name = "Доп. услуги. Перевод в архив",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 112,
                    Code = AccessRightEnum.CounselorTask.ToUnreaded,
                    Name = "Задачи вожатым. Пометить задачу как не прочитанную"
                },
                new AccessRight
                {
                    Id = 113,
                    Code = AccessRightEnum.CounselorTask.ToReaded,
                    Name = "Задачи вожатым. Пометить задачу как прочитанную"
                },
                new AccessRight
                {
                    Id = 114,
                    Code = AccessRightEnum.CommercialTour.Request,
                    Name = "Ком. путевки. Список заявок",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 115,
                    Code = AccessRightEnum.CommercialTour.ProductView,
                    Name = "Ком. путевки. Продукты. Просмотр",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 116,
                    Code = AccessRightEnum.CommercialTour.ProductEdit,
                    Name = "Ком. путевки. Продукты. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 36,
                    Code = AccessRightEnum.Counselor.RequestDecline,
                    Name = "Вожатый. Отказ в заявлении."
                },
                new AccessRight
                {
                    Id = 117,
                    Code = AccessRightEnum.CounselorTest.View,
                    Name = "Вожатые. Тестирование. Просмотр."
                },
                new AccessRight
                {
                    Id = 118,
                    Code = AccessRightEnum.CounselorTest.Edit,
                    Name = "Вожатые. Тестирование. Редактирование."
                },
                new AccessRight
                {
                    Id = 119,
                    Code = AccessRightEnum.CounselorTest.ToEdit,
                    Name = "Вожатые. Тестирование. В статус редактирование."
                },
                new AccessRight
                {
                    Id = 120,
                    Code = AccessRightEnum.CounselorTest.ToFormed,
                    Name = "Вожатые. Тестирование. В статус формирование."
                }
                ,
                new AccessRight
                {
                    Id = 121,
                    Code = AccessRightEnum.TrainingCounselors.View,
                    Name = "Вожатые. Группы обучения. Просмотр."
                },
                new AccessRight
                {
                    Id = 122,
                    Code = AccessRightEnum.TrainingCounselors.Edit,
                    Name = "Вожатые. Группы обучения. Редактирование."
                },
                new AccessRight
                {
                    Id = 123,
                    Code = AccessRightEnum.TrainingCounselors.ToEdit,
                    Name = "Вожатые. Группы обучения. В статус редактирование."
                },
                new AccessRight
                {
                    Id = 124,
                    Code = AccessRightEnum.TrainingCounselors.ToFormed,
                    Name = "Вожатые. Группы обучения. В статус сформирован."
                },
                new AccessRight
                {
                    Id = 125,
                    Code = AccessRightEnum.TrainingCounselors.ToEducationFinished,
                    Name = "Вожатые. Группы обучения. В обучение завершено."
                },
                new AccessRight
                {
                    Id = 126,
                    Code = AccessRightEnum.PedParty.PedPartyManage,
                    Name = "Педотряды. Редактирование"
                },
                new AccessRight
                {
                    Id = 127,
                    Code = AccessRightEnum.PedParty.PedPartyToEdit,
                    Name = "Педотряды. В статус редактирование."
                },
                new AccessRight
                {
                    Id = 128,
                    Code = AccessRightEnum.PedParty.PedPartyToFormed,
                    Name = "Педотряды. В статус сформирован."
                },
                new AccessRight
                {
                    Id = 130,
                    Code = AccessRightEnum.PedParty.PedPartyView,
                    Name = "Педотряды. Просмотр."
                },
                new AccessRight
                {
                    Id = 131,
                    Code = AccessRightEnum.Limit.Oiv.GatheringRequirements,
                    Name = "Списки. ОИВ. Возврат в статус сбор потребностей"
                },
                new AccessRight
                {
                    Id = 132,
                    Code = AccessRightEnum.Limit.Request.View,
                    Name = "Профильники. Заявки. Просмотр заявок",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 133,
                    Code = AccessRightEnum.Limit.Request.Edit,
                    Name = "Профильники. Заявки. Редактирование"
                },
                new AccessRight
                {
                    Id = 134,
                    Code = AccessRightEnum.Limit.Request.ToApprove,
                    Name = "Профильники. Заявки. На утверждение"
                },
                new AccessRight
                {
                    Id = 135,
                    Code = AccessRightEnum.Limit.Request.Approve,
                    Name = "Профильники. Заявки. Подтвердить"
                },
                new AccessRight
                {
                    Id = 136,
                    Code = AccessRightEnum.Limit.Request.Decline,
                    Name = "Профильники. Заявки. Отказать"
                },
                new AccessRight
                {
                    Id = 137,
                    Code = AccessRightEnum.Calculation.View,
                    Name = "Начисление. Просмотр",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 138,
                    Code = AccessRightEnum.Calculation.ToCancelled,
                    Name = "Начисление. Аннулировать",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 139,
                    Code = AccessRightEnum.Calculation.ToPaid,
                    Name = "Начисление. Ввести сведения об оплате",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 140,
                    Code = AccessRightEnum.Payments.View,
                    Name = "Платежи. Просмотр",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 141,
                    Code = AccessRightEnum.Payments.LinkToCalculation,
                    Name = "Платежи. Связывать с начислениями",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 142,
                    Code = AccessRightEnum.TrainingCounselors.SetEducationFinished,
                    Name = "Вожатые. Группы обучения. Установка признака обучение успешно завершено."
                },
                new AccessRight
                {
                    Id = 143,
                    Code = AccessRightEnum.Counselor.OnRequest,
                    Name = "Вожатые. В статус заявка на вожатого"
                },
                new AccessRight
                {
                    Id = 144,
                    Code = AccessRightEnum.Bout.Closed,
                    Name = "Заезд. Закрыть заезд"
                },
                new AccessRight
                {
                    Id = 145,
                    Code = AccessRightEnum.Bout.FromConfirmedToFormed,
                    Name = "Заезд. Из подтверждено в сформировано"
                },
                new AccessRight
                {
                    Id = 146,
                    Code = AccessRightEnum.Bout.FromClosedToConfirmed,
                    Name = "Заезд. Из закрыто в подтверждено"
                },
                new AccessRight
                {
                    Id = 147,
                    Code = AccessRightEnum.Transport.View,
                    Name = "Транспорт. Просмотр транспорта",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 148,
                    Code = AccessRightEnum.Contract.View,
                    Name = "Договор. Просмотр и выбор договоров",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 149,
                    Code = AccessRightEnum.Bout.Counselor,
                    Name = "Просмотр заездов вожатым"
                },
                new AccessRight
                {
                    Id = 150,
                    Code = AccessRightEnum.AnalyticReports.BenefitRestChildByAgeAndSex,
                    Name = "Отчет. Индивидуальный отдых. Льготники по возрасту и полу"
                },
                new AccessRight
                {
                    Id = 151,
                    Code = AccessRightEnum.AnalyticReports.BenefitFamilyRestByAgeAndSex,
                    Name = "Отчет. Семейный отдых. Льготники по возрасту и полу"
                },
                new AccessRight
                {
                    Id = 152,
                    Code = AccessRightEnum.AnalyticReports.BenefitRestChildByCategoryAndDistrict,
                    Name = "Отчет. Индивидуальный отдых. Льготники по категориям льгот и округам"
                },
                new AccessRight
                {
                    Id = 153,
                    Code = AccessRightEnum.AnalyticReports.BenefitFamilyRestByCategoryAndDistrict,
                    Name = "Отчет. Семейный отдых.  Льготники по категориям льгот и округам"
                },
                new AccessRight
                {
                    Id = 154,
                    Code = AccessRightEnum.AnalyticReports.BenefitRestChildByBoutCompleteness,
                    Name = "Отчет. Индивидуальный отдых. Недозаезды"
                },
                new AccessRight
                {
                    Id = 155,
                    Code = AccessRightEnum.AnalyticReports.BenefitFamilyRestByBoutCompleteness,
                    Name = "Отчет. Семейный отдых. Недозаезды"
                },
                new AccessRight
                {
                    Id = 156,
                    Code = AccessRightEnum.AnalyticReports.ByTransportServices,
                    Name = "Отчет. Оказание транспортных услуг"
                },
                new AccessRight
                {
                    Id = 157,
                    Code = AccessRightEnum.AnalyticReports.ByResidenceServices,
                    Name = "Отчет. Оказание услуг по проживанию"
                },
                new AccessRight
                {
                    Id = 158,
                    Code = AccessRightEnum.AnalyticReports.SpecializedCampsByOrganizations,
                    Name = "Отчет. Профильные лагеря. Востребованность по ОИВ"
                },
                new AccessRight
                {
                    Id = 159,
                    Code = AccessRightEnum.AnalyticReports.SpecializedCampsByVedomstvo,
                    Name = "Отчет. Профильные лагеря. Востребованность по учреждениям"
                },
                new AccessRight
                {
                    Id = 160,
                    Code = AccessRightEnum.AnalyticReports.SpecializedCampsByAgeAndRegions,
                    Name = "Отчет. Профильные лагеря. Распределение по году рождения и регионам"
                },
                new AccessRight
                {
                    Id = 161,
                    Code = AccessRightEnum.Request.ToDraft,
                    Name = "Ком. путевки. Заявки. В редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 162,
                    Code = AccessRightEnum.Request.ToCertificateIssued,
                    Name = "Ком. путевки. Заявки. В оплачена",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 163,
                    Code = AccessRightEnum.Request.ToOnApprove,
                    Name = "Ком. путевки. Заявки. В запрошено",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 164,
                    Code = AccessRightEnum.Request.ToReadyToPay,
                    Name = "Ком. путевки. Заявки. В готово для оплаты",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 165,
                    Code = AccessRightEnum.Request.ToReject,
                    Name = "Ком. путевки. Заявки. В аннулирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 166,
                    Code = AccessRightEnum.CommercialTour.MayPriceEdit,
                    Name = "Ком. путевки. Заявки. Можно редактировать цену",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 167,
                    Code = AccessRightEnum.CommercialTour.MayApprove,
                    Name = "Ком. путевки. Заявки. Можно подтверждать",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 168,
                    Code = AccessRightEnum.CommercialTour.RequestEdit,
                    Name = "Ком. путевки. Заявки. Можно редактировать",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 169,
                    Code = AccessRightEnum.Request.ToReadyToPayFull,
                    Name = "Ком. путевки. Заявки. В готово для доплаты",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 170,
                    Code = AccessRightEnum.Request.ToOnWorkig,
                    Name = "Ком. путевки. Заявки. На в работе",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 171,
                    Code = AccessRightEnum.Status.ToOperatorCheck,
                    Name = "Заявления. Перевод в статус На исполнении. Сбор сведений"
                },
                new AccessRight
                {
                    Id = 172,
                    Code = AccessRightEnum.Contract.ViewCommercial,
                    Name = "Договор. Просмотр и выбор договоров (коммерция)",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 173,
                    Code = AccessRightEnum.Contract.ToEdit,
                    Name = "Договор. Перевод в статус редактирование"
                },
                new AccessRight
                {
                    Id = 174,
                    Code = AccessRightEnum.Status.ToCancelByApplicant,
                    Name = "Заявления. В статус отозвано"
                },
                new AccessRight
                {
                    Id = 175,
                    Code = AccessRightEnum.Status.ToRegistrationDecline,
                    Name = "Заявления. В статус отказ в регистрации",
                    GroupCode = "-"
                },
                new AccessRight
                {
                    Id = 176,
                    Code = AccessRightEnum.Tour.WorkWithServices,
                    Name = "Размещение. Внос услуг в размещение",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 177,
                    Code = AccessRightEnum.TradeUnionList.View,
                    Name = "Профсоюзные списки. Просмотр списков",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 178,
                    Code = AccessRightEnum.TradeUnionList.Edit,
                    Name = "Профсоюзные списки. Редактирование"
                },
                new AccessRight
                {
                    Id = 179,
                    Code = AccessRightEnum.TradeUnionList.ToApproved,
                    Name = "Профсоюзные списки. В статус утверждено"
                },
                new AccessRight
                {
                    Id = 180,
                    Code = AccessRightEnum.TradeUnionList.ToDeclined,
                    Name = "Профсоюзные списки. В статус отклонено"
                },
                new AccessRight
                {
                    Id = 181,
                    Code = AccessRightEnum.TradeUnionList.ToEdit,
                    Name = "Профсоюзные списки. В статус редактирование из статуса отклонено"
                },
                new AccessRight
                {
                    Id = 182,
                    Code = AccessRightEnum.TradeUnionList.ToEditFromAll,
                    Name = "Профсоюзные списки. В статус редактирование из других статусов"
                },
                new AccessRight
                {
                    Id = 183,
                    Code = AccessRightEnum.TradeUnionList.ToFinish,
                    Name = "Профсоюзные списки. В статус сведения о заехавших внесены"
                },
                new AccessRight
                {
                    Id = 184,
                    Code = AccessRightEnum.TradeUnionList.ToOnAproving,
                    Name = "Профсоюзные списки. В статус на утверждение"
                },
                new AccessRight
                {
                    Id = 185,
                    Code = AccessRightEnum.CommercialPart,
                    Name = "Коммерческая часть. Управление пользователями",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 186,
                    Code = AccessRightEnum.Hotel.EditName,
                    Name = "Оздоровительная организация. Изменение наименования"
                },
                new AccessRight
                {
                    Id = 187,
                    Code = AccessRightEnum.TypeOfServiceExcursion,
                    Name = "Доп. услуги. Тип 3 Экскурсия. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 188,
                    Code = AccessRightEnum.TypeOfServiceVisa,
                    Name = "Доп. услуги. Тип 4 Виза. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 189,
                    Code = AccessRightEnum.TypeOfServiceTransferAero,
                    Name = "Доп. услуги. Тип 5 Транспорт (Авиа). Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 190,
                    Code = AccessRightEnum.TypeOfServiceInsurance,
                    Name = "Доп. услуги. Тип 10 Страховка. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 191,
                    Code = AccessRightEnum.TypeOfServiceAddonPlace,
                    Name = "Доп. услуги. Тип 11 Дополнительное место к льготной путевке. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 192,
                    Code = AccessRightEnum.TypeOfServiceTransferTrain,
                    Name = "Доп. услуги. Тип 12 Транспорт (ЖД). Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 193,
                    Code = AccessRightEnum.TypeOfServiceTransferAuto,
                    Name = "Доп. услуги. Тип 13 Транспорт (Авто). Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 194,
                    Code = AccessRightEnum.TypeOfServiceOther,
                    Name = "Доп. услуги. Тип 999 Прочее. Редактирование",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 195,
                    Code = AccessRightEnum.AnalyticReports.RestWithChildTypeOfRooms,
                    Name = "Отчет. Востребованность номеров в совместном отдыхе"
                },
                new AccessRight
                {
                    Id = 196,
                    Code = AccessRightEnum.RequestEditTypeViolation,
                    Name = "Заявление. Редактирование Видов нарушений"
                },
                new AccessRight
                {
                    Id = 197,
                    Code = AccessRightEnum.CommercialTour.InternalPriceView,
                    Name = "Ком. путевки. Заявки. Просмотр себестоимости",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 198,
                    Code = AccessRightEnum.Hotel.PricesView,
                    Name = "Оздоровительная организация. Просмотр матрицы цен",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 199,
                    Code = AccessRightEnum.Hotel.PricesEdit,
                    Name = "Оздоровительная организация. Редактирование матрицы цен",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 200,
                    Code = AccessRightEnum.Payments.LoadPayments,
                    Name = "Платежи. Загрузка из облачной бухгалтерии",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 201,
                    Code = AccessRightEnum.Request.EditPersonInOtherState,
                    Name = "Ком. путевки. Заявки. Редактирование отдыхающих во всех статусах",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 202,
                    Code = AccessRightEnum.Request.ToDenial,
                    Name = "Ком. путевки. Заявки. В статус отказано",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 203,
                    Code = AccessRightEnum.Request.ToPayed,
                    Name = "Ком. путевки. Заявки. В статус оплачено",
                    GroupCode = AccessRightEnum.CommercialPart
                },
                new AccessRight
                {
                    Id = 210,
                    Code = AccessRightEnum.Status.FcToCertificateIssued,
                    Name = "Многоэтапная кампания. В статус услуга оказана"
                },
                new AccessRight
                {
                    Id = 211,
                    Code = AccessRightEnum.Status.FcToDecisionIsMade,
                    Name = "Многоэтапная кампания. В статус решение принято"
                },
                new AccessRight
                {
                    Id = 212,
                    Code = AccessRightEnum.Status.FcToDecisionMaking,
                    Name = "Многоэтапная кампания. В статус принятие решения(админ)"
                },
                new AccessRight
                {
                    Id = 259,
                    Code = AccessRightEnum.Status.FcToDecisionMakingCovid,
                    Name =
                        "Многоэтапная кампания. В статус принятие решения(дополнительная кампания 2020, администратор)"
                },
                new AccessRight
                {
                    Id = 213,
                    Code = AccessRightEnum.Status.FcToRanging,
                    Name = "Многоэтапная кампания. В статус ранжирование (админ)"
                },
                new AccessRight
                {
                    Id = 214,
                    Code = AccessRightEnum.Status.FcToReject,
                    Name = "Многоэтапная кампания. В статус отказано"
                },
                new AccessRight
                {
                    Id = 215,
                    Code = AccessRightEnum.Status.FcToWaitApplicant,
                    Name = "Многоэтапная кампания. В статус вызов для подтверждения родства"
                },
                new AccessRight
                {
                    Id = 216,
                    Code = AccessRightEnum.Status.FcToWaitApplicantMoney,
                    Name = "Многоэтапная кампания. В статус вызов для подтверждения реквизитов"
                },
                new AccessRight
                {
                    Id = 217,
                    Code = AccessRightEnum.Status.FcToIncludedInList,
                    Name = "Многоэтапная кампания. Включено в список(админ)"
                },
                new AccessRight
                {
                    Id = 218,
                    Code = AccessRightEnum.Status.FcToMoneyAccountAccepted,
                    Name = "Многоэтапная кампания. Реквизиты счета для перечисления подтверждены"
                },
                new AccessRight
                {
                    Id = 219,
                    Code = AccessRightEnum.Status.FcFinishWorkWithRequest,
                    Name = "Многоэтапная кампания. Завершить обработку заявления"
                },
                new AccessRight
                {
                    Id = 220,
                    Code = AccessRightEnum.Limit.ViewOrphan,
                    Name = "Списки. Работа с сиротами"
                },
                new AccessRight
                {
                    Id = 221,
                    Code = AccessRightEnum.Limit.ViewProfile,
                    Name = "Списки. Работа с профильниками"
                },
                new AccessRight
                {
                    Id = 222,
                    Code = AccessRightEnum.Status.FcRepareRequest,
                    Name = "Многоэтапная кампания. Восстановить заявку"
                },
                new AccessRight
                {
                    Id = 223,
                    Code = AccessRightEnum.ManageExchangeBaseRegistry,
                    Name = "Управление обменом с базовым регистром (включение отключение рассылки)"
                },
                new AccessRight
                {
                    Id = 224,
                    Code = AccessRightEnum.Status.FcNotComeOnMoney,
                    Name = "Многоэтапная кампания. Отправить уведомление заявителю по уточнению выплаты (не пришел)"
                },
                new AccessRight
                {
                    Id = 225,
                    Code = AccessRightEnum.MosgorturScheduleBookingView,
                    Name = "(МОСГОРТУР) Реестр записи на приём. Просмотр."
                },
                new AccessRight
                {
                    Id = 226,
                    Code = AccessRightEnum.Security.Login,
                    Name = "(ИБ) Администратор информационной безопасности"
                },
                new AccessRight
                {
                    Id = 227,
                    Code = AccessRightEnum.Security.SecuritySettingsView,
                    Name = "(ИБ) Настройки безопасности. Просмотр."
                },
                new AccessRight
                {
                    Id = 228,
                    Code = AccessRightEnum.Security.SecuritySettingsEdit,
                    Name = "(ИБ) Настройки безопасности. Редактирование."
                },
                new AccessRight
                {
                    Id = 229,
                    Code = AccessRightEnum.Security.JournalEntrance,
                    Name = "(ИБ) Журнала входов в систему. Просмотр."
                },
                new AccessRight
                {
                    Id = 230,
                    Code = AccessRightEnum.Security.JournalProceses,
                    Name = "(ИБ) Журнал процессов и программ. Просмотр."
                },
                new AccessRight
                {
                    Id = 231,
                    Code = AccessRightEnum.Security.JournalRoles,
                    Name = "(ИБ) Журнал изменений ролей пользователя. Просмотр."
                },
                new AccessRight
                {
                    Id = 232,
                    Code = AccessRightEnum.Security.JournalSessions,
                    Name = "(ИБ) Журнал сеансов (сессий). Просмотр."
                },
                new AccessRight
                {
                    Id = 233,
                    Code = AccessRightEnum.Security.StopSessions,
                    Name = "(ИБ) Принудительная остановка сессии"
                },
                new AccessRight
                {
                    Id = 234,
                    Code = AccessRightEnum.Security.JournalSecurity,
                    Name = "(ИБ) Журнал уведомлений безопасности. Просмотр."
                },
                new AccessRight
                {
                    Id = 235,
                    Code = AccessRightEnum.MosgorturScheduleBookingCancel,
                    Name = "(МОСГОРТУР) Реестр записи на приём. Аннулирование."
                },
                new AccessRight
                {
                    Id = 236,
                    Code = AccessRightEnum.MosgorturScheduleBookingCreate,
                    Name = "(МОСГОРТУР) Реестр записи на приём. Создание."
                },
                new AccessRight
                {
                    Id = 237,
                    Code = AccessRightEnum.MosgorturWorkingDaysView,
                    Name = "(МОСГОРТУР) Управление рабочими днями. Просмотр."
                },
                new AccessRight
                {
                    Id = 238,
                    Code = AccessRightEnum.MosgorturWorkingDaysEdit,
                    Name = "(МОСГОРТУР) Управление рабочими днями. Управление."
                },
                new AccessRight
                {
                    Id = 239,
                    Code = AccessRightEnum.MosgorturBookingTargetsView,
                    Name = "(МОСГОРТУР) Цели обращения. Просмотр."
                },
                new AccessRight
                {
                    Id = 240,
                    Code = AccessRightEnum.MosgorturBookingTargetsEdit,
                    Name = "(МОСГОРТУР) Цели обращения. Управление."
                },
                new AccessRight
                {
                    Id = 241,
                    Code = AccessRightEnum.ZAGSIntegration,
                    Name = "Управление интеграцией с ЗАГС"
                },
                new AccessRight
                {
                    Id = 242,
                    Code = AccessRightEnum.DTSZNIntegration,
                    Name = "Управление интеграцией с ДТСЗН"
                },
                new AccessRight
                {
                    Id = 243,
                    Code = AccessRightEnum.ChildTransfer,
                    Name = "Управление переносом заездов"
                },
                new AccessRight
                {
                    Id = 244,
                    Code = AccessRightEnum.Security.IteractionsWithOutSystems,
                    Name = "(ИБ) Журнал взаимодействий с ИС. Просмотр."
                },
                new AccessRight
                {
                    Id = 245,
                    Code = AccessRightEnum.Orphans.Main,
                    Name = "(Сироты) Основной доступ",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 246,
                    Code = AccessRightEnum.Orphans.PupilGroup,
                    Name = "(Сироты) Работа с группами (потребностями)",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 247,
                    Code = AccessRightEnum.Orphans.PupilGroupForm,
                    Name = "(Сироты) Сформировать группу (потребность) приюта"
                },
                new AccessRight
                {
                    Id = 248,
                    Code = AccessRightEnum.Orphans.PupilGroupOnAgreement,
                    Name = "(Сироты) Отправить на согласование группу (потребность) приюта"
                },
                new AccessRight
                {
                    Id = 249,
                    Code = AccessRightEnum.Orphans.PupilGroupAgree,
                    Name = "(Сироты) Согласовать группу (потребность) приюта"
                },
                new AccessRight
                {
                    Id = 250,
                    Code = AccessRightEnum.Orphans.PupilGroupApprove,
                    Name = "(Сироты) Утвердить группу (потребность) приюта"
                },
                new AccessRight
                {
                    Id = 251,
                    Code = AccessRightEnum.Orphans.PupilGroupEdit,
                    Name = "(Сироты) Редактировать группу (потребность) приюта"
                },
                new AccessRight
                {
                    Id = 252,
                    Code = AccessRightEnum.Orphans.PupilGroupDelete,
                    Name = "(Сироты) Удалить группу (потребность) приюта"
                },
                new AccessRight
                {
                    Id = 253,
                    Code = AccessRightEnum.Orphans.PupilGroupList,
                    Name = "(Сироты) Работа со списками (группы отправки) приюта",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 254,
                    Code = AccessRightEnum.Orphans.PupilGroupListEdit,
                    Name = "(Сироты) Редактировать список (группу отправки) приюта"
                },
                new AccessRight
                {
                    Id = 255,
                    Code = AccessRightEnum.Orphans.PupilGroupListForm,
                    Name = "(Сироты) Сформировать список (группу отправки) приюта"
                },
                new AccessRight
                {
                    Id = 256,
                    Code = AccessRightEnum.Orphans.PupilGroupListApprove,
                    Name = "(Сироты) Утвердить список (группу отправки) приюта"
                },
                new AccessRight
                {
                    Id = 257,
                    Code = AccessRightEnum.Orphans.PupilGroupListDelete,
                    Name = "(Сироты) Удалить список (группу отправки) приюта"
                },
                new AccessRight
                {
                    Id = 258,
                    Code = AccessRightEnum.AnalyticReports.EGISO,
                    Name = "Отчет ЕГИСО"
                },
                new AccessRight
                {
                    Id = 265,
                    Code = AccessRightEnum.RequestTo10753,
                    Name = "Заявление. Многоэтапная кампания. Отказать по 1075.3."
                },
                new AccessRight
                {
                    Id = 266,
                    Code = AccessRightEnum.NewBout.View,
                    Name = "Мобильное приложение. Заезды. Управление и просмотр статистики"
                },
                new AccessRight
                {
                    Id = 267,
                    Code = AccessRightEnum.Task.View,
                    Name = "Мобильное приложение. Статистика по заданиям."
                },
                new AccessRight
                {
                    Id = 268,
                    Code = AccessRightEnum.Gift.View,
                    Name = "Мобильное приложение. Управление подарками."
                },
                new AccessRight
                {
                    Id = 269,
                    Code = AccessRightEnum.GiftReserved.View,
                    Name = "Мобильное приложение. Выдача подарков."
                },
                new AccessRight
                {
                    Id = 270,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.View,
                    Name = "Мониторинг. Сведения о численности детей. Просмотр формы",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 271,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.EventRecive,
                    Name = "Мониторинг. Сведения о численности детей. Получение уведомлений",
                },
                new AccessRight
                {
                    Id = 272,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.Edit,
                    Name = "Мониторинг. Сведения о численности детей. Работа с формой",
                },
                new AccessRight
                {
                    Id = 273,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                    Name = "Мониторинг. Сведения о численности детей. Отправить на согласование",
                },
                new AccessRight
                {
                    Id = 274,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.Agree,
                    Name = "Мониторинг. Сведения о численности детей. Согласовать",
                },
                new AccessRight
                {
                    Id = 275,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.Approve,
                    Name = "Мониторинг. Сведения о численности детей. Утвердить",
                },
                new AccessRight
                {
                    Id = 276,
                    Code = AccessRightEnum.Monitoring.ChildrenNumberInformation.ToEdit,
                    Name = "Мониторинг. Сведения о численности детей. Отправить на доработку",
                },
                new AccessRight
                {
                    Id = 277,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.View,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Просмотр формы",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 278,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.EventRecive,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Получение уведомлений",
                },
                new AccessRight
                {
                    Id = 279,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.Edit,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Работа с формой",
                },
                new AccessRight
                {
                    Id = 280,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.OnAgreement,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Отправить на согласование",
                },
                new AccessRight
                {
                    Id = 281,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.Agree,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Согласовать",
                },
                new AccessRight
                {
                    Id = 282,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.Approve,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Утвердить",
                },
                new AccessRight
                {
                    Id = 283,
                    Code = AccessRightEnum.Monitoring.FinanceInformation.ToEdit,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании. Отправить на доработку",
                },
                new AccessRight
                {
                    Id = 284,
                    Code = AccessRightEnum.Monitoring.SmallLeisureInfoData.View,
                    Name = "Мониторинг. Сведения о малых формах занятости детей. Просмотр формы",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = 285,
                    Code = AccessRightEnum.Monitoring.SmallLeisureInfoData.EventRecive,
                    Name = "Мониторинг. Сведения о малых формах занятости детей. Получение уведомлений",
                },
                new AccessRight
                {
                    Id = 286,
                    Code = AccessRightEnum.Monitoring.SmallLeisureInfoData.Edit,
                    Name = "Мониторинг. Сведения о малых формах занятости детей. Работа с формой",
                },
                new AccessRight
                {
                    Id = 287,
                    Code = AccessRightEnum.Monitoring.SmallLeisureInfoData.OnApproving,
                    Name = "Мониторинг. Сведения о малых формах занятости детей. Отправить на утверждение",
                },
                new AccessRight
                {
                    Id = 288,
                    Code = AccessRightEnum.Monitoring.SmallLeisureInfoData.Approve,
                    Name = "Мониторинг. Сведения о малых формах занятости детей. Утвердить",
                },
                new AccessRight
                {
                    Id = 289,
                    Code = AccessRightEnum.Monitoring.SmallLeisureInfoData.ToEdit,
                    Name = "Мониторинг. Сведения о малых формах занятости детей. Отправить на доработку",
                },
                new AccessRight
                {
                    Id = startIndex,
                    Code = AccessRightEnum.Monitoring.ReestrWork,
                    Name = "Мониторинг. Работа с реестрами",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Monitoring.EventSent,
                    Name = "Мониторинг. Отправка уведомлений",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Monitoring.CompleteFormDownload,
                    Name = "Мониторинг. Выгрузка сводных форм",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.ReplacingAccompanying,
                    Name = "Заявление. Замена сопровождающего",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Organization.GBUView,
                    Name = "Организации. Просмотр списка ГБУ",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Organization.GBUEdit,
                    Name = "Организации. Редактирование ГБУ",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Organization.CampView,
                    Name = "Организации. Просмотр списка лагерей всех регионов РФ",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Organization.CampEdit,
                    Name = "Организации. Редактирование лагеря",
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.AnalyticReports.RoomsFund,
                    Name = "Отчет. Номерной фонд"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Orphans.PupilGroupEditFromApprove,
                    Name = "(Сироты) Редактировать группу (потребность) приюта (из статуса утверждено)"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Orphans.MainRemoveRestorePupil,
                    Name = "(Сироты) Удаление/восстановление воспитанников учреждения социальной защиты"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Orphans.MainRemoveRestoreCollaborator,
                    Name = "(Сироты) Удаление/восстановление сотрудников учреждения социальной защиты"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.Orphans.MainViolationsInformationEntering,
                    Name = "(Сироты) Внесение сведений о нарушениях"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.TradeUnionCashback.ListView,
                    Name = "(Кэшбек) Списки претендентов на получение кэшбэка. Просмотр",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.TradeUnionCashback.ListEdit,
                    Name = "(Кэшбек) Списки претендентов на получение кэшбэка. Редактирование",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.TradeUnionCashback.RegistryView,
                    Name = "(Кэшбек) Реестр претендентов на получение кэшбэка. Просмотр",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.TradeUnionCashback.RegistryExportCSV,
                    Name = "(Кэшбек) Реестр претендентов на получение кэшбэка. Экспорт CSV",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.GiftReserved.MassCancel,
                    Name = "Мобильное приложение. Сброс подарков",
                    ForOrganization = true
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.InteragencyRequestDelete,
                    Name = "Работа с межведомственным запросом. Удаление"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.AnalyticReports.NotRespondedRequests,
                    Name = "Отчет. Не отвеченные запросы"
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturScheduleBookingViewClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Реестр записи на приём. Просмотр."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturScheduleBookingCancelClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Реестр записи на приём. Аннулирование."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturScheduleBookingCreateClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Реестр записи на приём. Создание."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturWorkingDaysViewClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Управление рабочими днями. Просмотр."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturWorkingDaysEditClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Управление рабочими днями. Управление."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturBookingTargetsViewClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Цели обращения. Просмотр."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturBookingTargetsEditClientDepartment,
                    Name = "(МОСГОРТУР) Отдел по работе с клиентами. Цели обращения. Управление."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturScheduleBookingViewBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Реестр записи на приём. Просмотр."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturScheduleBookingCancelBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Реестр записи на приём. Аннулирование."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturScheduleBookingCreateBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Реестр записи на приём. Создание."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturWorkingDaysViewBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Управление рабочими днями. Просмотр."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturWorkingDaysEditBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Управление рабочими днями. Управление."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturBookingTargetsViewBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Цели обращения. Просмотр."
                },
                new AccessRight
                {
                    Id = ++startIndex,
                    Code = AccessRightEnum.MosgorturBookingTargetsEditBookingDepartment,
                    Name = "(МОСГОРТУР) Отдел бронирования. Цели обращения. Управление."
                }
            );

            SetEidAndLastUpdateTicks(context.AccessRight.ToList());
            context.SaveChanges();
        }
    }
}
