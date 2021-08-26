using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     времена отдыха
    /// </summary>
    [DataContract(Name = "timeOfRest")]
    public class TimeOfRest
    {
        public TimeOfRest()
        {
        }

        public TimeOfRest(TimeOfRest source, bool deepCopy = false)
        {
            Id = source.Id;
            TimeOfRestId = source.TimeOfRestId;
            Start = source.Start;
            End = source.End;
            OpenBooking = source.OpenBooking;
            CloseBooking = source.CloseBooking;

            Rooms = deepCopy ? source.Rooms.Select(r => new RoomShedule(r)).ToList() : source.Rooms;
        }

        public TimeOfRest(TimeOfRest source, int main, int attendant, bool withBookingDate, bool findAdditional)
        {
            Id = source.Id;
            TimeOfRestId = source.TimeOfRestId;
            Start = source.Start;
            End = source.End;
            OpenBooking = source.OpenBooking;
            CloseBooking = source.CloseBooking;

            if (withBookingDate && (OpenBooking > DateTime.Now || CloseBooking < DateTime.Now))
            {
                LeftPlaces = 0;
                return;
            }

            if (withBookingDate)
            {
                CloseBooking = null;
            }

            if (Start < DateTime.Now)
            {
                LeftPlaces = 0;
                return;
            }

            if (attendant == 0)
            {
                LeftPlaces = main <= source.LeftPlaces ? source.LeftPlaces : 0;
            }
            else
            {
                LeftPlaces = 0;
                if (source.Rooms != null)
                {
                    Rooms = source.Rooms.Select(r => new RoomShedule(r)).ToList();
                    SetLeft(main, attendant, findAdditional);
                    Rooms = null;
                }
            }
        }

        /// <summary>
        ///     открытие бронирования
        /// </summary>
        [IgnoreDataMember]
        public DateTime? OpenBooking { get; set; }

        /// <summary>
        ///     закрытие бронирования
        /// </summary>
        [DataMember(EmitDefaultValue = false, Name = "cb")]
        public DateTime? CloseBooking { get; set; }


        /// <summary>
        ///     отель к которому привязан заезд
        /// </summary>
        [IgnoreDataMember]
        public Hotel Hotel { get; set; }

        /// <summary>
        ///     нужны записи
        /// </summary>
        [IgnoreDataMember]
        public bool NeedAdd { get; set; }

        /// <summary>
        ///     заезд
        /// </summary>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        ///     Время отдыха
        /// </summary>
        [DataMember(Name = "timeOfRestId")]
        public long TimeOfRestId { get; set; }

        /// <summary>
        ///     дата заезда
        /// </summary>
        [DataMember(Name = "start")]
        public DateTime Start { get; set; }

        /// <summary>
        ///     дата выезда
        /// </summary>
        [DataMember(Name = "end")]
        public DateTime End { get; set; }

        /// <summary>
        ///     осталось мест.
        /// </summary>
        [DataMember(Name = "leftPlaces")]
        public int LeftPlaces { get; set; }

        /// <summary>
        ///     кол-во комнат.
        /// </summary>
        [IgnoreDataMember]
        public int CountRooms { get; set; }


        /// <summary>
        ///     Номера в заезде
        /// </summary>
        [DataMember(Name = "rooms")]
        public List<RoomShedule> Rooms { get; set; }

        /// <summary>
        ///     запросы на бронирование
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<Guid, BookingRequest> Bookings { get; set; }

        /// <summary>
        ///     расчет размещения.
        /// </summary>
        private void SetLeft(int main, int attendant, bool findAdditional)
        {
            var dict = Rooms.Where(p => p.Left > 0).GroupBy(p => p.Place)
                .ToDictionary(p => p.Key, v => v.Sum(t => t.Left));
            LeftPlaces = 0;

            if (dict.Count <= 0)
            {
                return;
            }

            var total = main + attendant;

            var minRooms = int.MaxValue;

            if (!findAdditional)
            {
                var from = total / attendant + total % attendant;
                for (var i = from; i <= total; i++)
                {
                    var first = i;
                    var second = total - i;
                    if ((!dict.ContainsKey(first) || !dict.ContainsKey(second)) && second == 0 &&
                        !dict.ContainsKey(first))
                    {
                        continue;
                    }

                    if (second == 0)
                    {
                        LeftPlaces += dict[first];
                        minRooms = 1;
                    }
                    else if (dict.ContainsKey(first) && dict.ContainsKey(second) && first != second)
                    {
                        LeftPlaces += Math.Min(dict[first], dict[second]);
                        if (minRooms > 2)
                        {
                            minRooms = 2;
                        }
                    }
                    else if (dict.ContainsKey(first) && dict[first] > 1 && first == second)
                    {
                        LeftPlaces += dict[first] / 2;
                        if (minRooms > 2)
                        {
                            minRooms = 2;
                        }
                    }
                }
            }
            else
            {
                if (LeftPlaces == 0 && attendant > 2)
                {
                    var vars = CalculateVariants(attendant, total);
                    LeftPlaces = vars.Count;
                    if (vars.Count > 0)
                    {
                        minRooms = vars.Select(v => v.Places.Select(p => p.CountRooms ?? 0).DefaultIfEmpty().Sum())
                            .DefaultIfEmpty().Min();
                    }
                }
            }

            CountRooms = minRooms;
        }


        /// <summary>
        ///     расчет вариантов размещения.
        /// </summary>
        public List<Location> GetVariationPlacement(int main, int? attendant, bool? withBookingDate)
        {
            if (Rooms == null || !Rooms.Any())
            {
                LeftPlaces = 0;
                return null;
            }

            if ((withBookingDate ?? true) && (OpenBooking > DateTime.Now || CloseBooking < DateTime.Now))
            {
                LeftPlaces = 0;
                return null;
            }

            var dict = Rooms.Where(p => p.Left > 0).GroupBy(p => p.Place)
                .ToDictionary(p => p.Key, v => v.Sum(t => t.Left));

            if (dict.Count <= 0)
            {
                LeftPlaces = 0;
                return null;
            }

            var res = new List<Location>();

            var total = main + (attendant ?? 0);

            var from = total / (attendant ?? 1) + total % (attendant ?? 1);

            var keysAdd = new Dictionary<string, int>();

            for (var i = from; i <= total; i++)
            {
                var first = i;
                var second = total - i;
                if (second == 0)
                {
                    var rooms = Rooms.Where(p => p.Left > 0 && p.Place == first).ToList();
                    res.AddRange(rooms.Select(room => new Location
                        {Places = new List<Rooms> {new Rooms(room) {CountRooms = 1}}}));
                }
                else if (dict.ContainsKey(first) && dict.ContainsKey(second))
                {
                    if (first == second)
                    {
                        var rooms = Rooms.Where(p => p.Left > 1 && p.Place == first).ToList();
                        res.AddRange(rooms.Select(room => new Location
                            {Places = new List<Rooms> {new Rooms(room) {CountRooms = 2}}}));
                    }

                    var firstRooms = Rooms.Where(p => p.Left > 0 && p.Place == first).ToList();
                    var secondRooms = Rooms.Where(p => p.Left > 0 && p.Place == second).ToList();
                    foreach (var firstRoom in firstRooms)
                    {
                        foreach (var secondRoom in secondRooms)
                        {
                            var key =
                                $"{Math.Min(firstRoom.Id, secondRoom.Id)}_{Math.Max(firstRoom.Id, secondRoom.Id)}";

                            if (firstRoom.Id != secondRoom.Id && !keysAdd.ContainsKey(key))
                            {
                                keysAdd.Add(key, 0);
                                res.Add(new Location
                                {
                                    Places = new List<Rooms>
                                        {new Rooms(firstRoom) {CountRooms = 1}, new Rooms(secondRoom) {CountRooms = 1}}
                                });
                            }
                        }
                    }
                }
            }

            if (!res.Any() && attendant > 2)
            {
                res.AddRange(CalculateVariants(attendant, total));
            }

            return res;
        }

        private List<Location> CalculateVariants(int? attendant, int total)
        {
            var res = new List<Location>();
            // получили варианты по номерам
            var vars = GetVariantRooms(total, attendant ?? 0);
            if (vars.Any())
            {
                var dictKey = new HashSet<string>();
                foreach (var v in vars)
                {
                    var items = v.GroupedDictionary(d => d.Id, d => d);
                    var places = new List<Rooms>();
                    var key = "";
                    foreach (var k in items.Keys.OrderBy(d => d).ToArray())
                    {
                        var roomSchedules = items[k];
                        var roomSchedule = roomSchedules.FirstOrDefault();
                        if (roomSchedule == null)
                        {
                            key = null;
                            break;
                        }

                        var r = new Rooms(roomSchedule) {CountRooms = roomSchedules.Count};
                        places.Add(r);

                        if (roomSchedule.Left < r.CountRooms)
                        {
                            key = null;
                            break;
                        }

                        key += $"({r.Id}/{r.CountRooms})";
                    }

                    if (!string.IsNullOrWhiteSpace(key) && !dictKey.Contains(key))
                    {
                        res.Add(new Location {Places = places});
                        dictKey.Add(key);
                    }
                }
            }

            return res;
        }

        /// <summary>
        ///     получить варианты
        /// </summary>
        private IList<RoomShedule[]> GetVariantRooms(int totalSum, int maxCount, RoomShedule[] prefix = null)
        {
            var result = new List<RoomShedule[]>();

            var prefixSum = prefix?.Select(s => s.Place).Sum() ?? 0;
            var resultFind = false;

            foreach (var item in Rooms.Where(r => r.Left > 0 && r.Place + prefixSum <= totalSum)
                .OrderByDescending(r => r.Place).ToArray())
            {
                var i = prefix?.ToList() ?? new List<RoomShedule>();
                i.Add(item);

                var sum = prefixSum + item.Place;
                if (sum == totalSum)
                {
                    result.Add(i.ToArray());
                }

                if (i.Count < maxCount && sum < totalSum && !resultFind)
                {
                    result.AddRange(GetVariantRooms(totalSum, maxCount, i.ToArray()));
                }

                if (result.Any())
                {
                    resultFind = true;
                }
            }

            return result;
        }
    }
}
