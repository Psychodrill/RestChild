using System;
using System.Linq;
using System.Text;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class TourExtensions
    {
        public static int GetRestManCount(this Tour tour)
        {
            if (tour.TypeOfRestId == (long) TypeOfRestEnum.SpecializedСamp &&
                (tour.StateId == StateMachineStateEnum.Tour.Formed ||
                 tour.StateId == StateMachineStateEnum.Tour.ToFormationFromFormed))
            {
                var listsOfChildrenCount = tour.ListOfChilds.Where(i =>
                        !i.IsDeleted && (i.StateId == StateMachineStateEnum.Limit.List.Formed ||
                                         i.StateId == StateMachineStateEnum.Limit.List.IncludedInTour ||
                                         i.StateId == StateMachineStateEnum.Limit.List.IncludedPayment))
                    .Sum(i => i.Childs.Count(c => !c.IsDeleted));

                return listsOfChildrenCount;
            }

            if (tour.TypeOfRest?.Parent?.ParentId == (long) TypeOfRestEnum.ChildRest
                || tour.TypeOfRest?.ParentId == (long) TypeOfRestEnum.ChildRest
                || tour.TypeOfRestId == (long) TypeOfRestEnum.ChildRest
                || tour.TypeOfRest?.Parent?.ParentId == (long) TypeOfRestEnum.RestWithParents
                || tour.TypeOfRest?.ParentId == (long) TypeOfRestEnum.RestWithParents
                || tour.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents)
            {
                var childrenRestCount = tour.RequestsSingle
                    .Where(i => !i.IsDeleted && i.StatusId == (int) StatusEnum.CertificateIssued)
                    .Sum(i => i.Child.Count(c => !c.IsDeleted));

                if (tour.TypeOfRest?.Parent?.ParentId == (long) TypeOfRestEnum.ChildRest
                    || tour.TypeOfRest?.ParentId == (long) TypeOfRestEnum.ChildRest
                    || tour.TypeOfRestId == (long) TypeOfRestEnum.ChildRest)
                {
                    return childrenRestCount;
                }

                if (tour.TypeOfRest?.Parent?.ParentId == (long) TypeOfRestEnum.RestWithParents
                    || tour.TypeOfRest?.ParentId == (long) TypeOfRestEnum.RestWithParents
                    || tour.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents)
                {
                    var attendantsCount = tour.RequestsSingle
                        .Where(i => !i.IsDeleted && i.StatusId == (int) StatusEnum.CertificateIssued)
                        .Sum(i => i.Attendant.Count(j => !j.IsDeleted));
                    attendantsCount += tour.RequestsSingle.Count(i =>
                        !i.IsDeleted && i.StatusId == (int) StatusEnum.CertificateIssued &&
                        (i.Applicant?.IsAccomp ?? false));
                    return childrenRestCount + attendantsCount;
                }
            }

            return 0;
        }

        /// <summary>
        ///     Сравнение значений для истории изменений размещения
        /// </summary>
        public static string Compare(this Tour source, Tour target, IUnitOfWork uw)
        {
            var res = new StringBuilder();
            if (source.IsActive != target.IsActive)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'активность размещения' старое значение: '{source.IsActive}', новое значение: '{target.IsActive}'</li>");
            }

            if (source.TypeOfRestId != target.TypeOfRestId)
            {
                var serviceName = uw.GetById<TypeOfRest>(target.TypeOfRestId)?.Name;
                res.AppendLine(
                    $"<li>Изменено поле 'цель обращения' старое значение: '{source.TypeOfRest?.Name}', новое значение: '{serviceName}'</li>");
            }

            if (source.YearOfRestId != target.YearOfRestId)
            {
                var serviceName = uw.GetById<YearOfRest>(target.YearOfRestId)?.Name;
                res.AppendLine(
                    $"<li>Изменено поле 'год размещения' старое значение: '{source.YearOfRest?.Name}', новое значение: '{serviceName}'</li>");
            }

            if (source.RestrictionGroupId != target.RestrictionGroupId)
            {
                var serviceName = uw.GetById<RestrictionGroup>(target.RestrictionGroupId)?.Name;
                res.AppendLine(
                    $"<li>Изменено поле 'группа ограничения' старое значение: '{source.RestrictionGroup?.Name}', новое значение: '{serviceName}'</li>");
            }

            if (source.HotelsId != target.HotelsId)
            {
                var serviceName = uw.GetById<Hotels>(target.HotelsId)?.Name;
                res.AppendLine(
                    $"<li>Изменено поле 'место отдыха' старое значение: '{source.Hotels?.Name}', новое значение: '{serviceName}'</li>");
            }

            if (source.DateIncome != target.DateIncome)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'дата начала' старое значение: '{source.DateIncome?.ToString("dd.MM.yyyy")}', новое значение: '{target.DateIncome?.ToString("dd.MM.yyyy")}'</li>");
            }

            if (source.DateOutcome != target.DateOutcome)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'дата окончания' старое значение: '{source.DateOutcome?.ToString("dd.MM.yyyy")}', новое значение: '{target.DateOutcome?.ToString("dd.MM.yyyy")}'</li>");
            }

            if (source.TimeOfRestId != target.TimeOfRestId)
            {
                var serviceName = uw.GetById<TimeOfRest>(target.TimeOfRestId)?.Name;
                res.AppendLine(
                    $"<li>Изменено поле 'время отдыха' старое значение: '{source.TimeOfRest?.Name}', новое значение: '{serviceName}'</li>");
            }

            if (source.StartBooking != target.StartBooking)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'дата начала записи' старое значение: '{source.StartBooking?.ToString("dd.MM.yyyy HH:mm")}', новое значение: '{target.StartBooking?.ToString("dd.MM.yyyy HH:mm")}'</li>");
            }

            if (source.EndBooking != target.EndBooking)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'дата окончания записи' старое значение: '{source.EndBooking?.ToString("dd.MM.yyyy HH:mm")}', новое значение: '{target.EndBooking?.ToString("dd.MM.yyyy HH:mm")}'</li>");
            }


            if (source.LimitOnVedomstvoId != target.LimitOnVedomstvoId)
            {
                var oldRes = source.LimitOnVedomstvoId  != null ? $"{source.LimitOnVedomstvo?.Organization?.Name} ({source.LimitOnVedomstvo?.TypeOfLimitList?.Name})": "не выбрано";

                var newRes = "не выбрано";
                if (target.LimitOnVedomstvoId != null)
                {
                    var vedom = uw.GetById<LimitOnVedomstvo>(target.LimitOnVedomstvoId);
                    newRes = $"{vedom?.Organization?.Name}({vedom?.TypeOfLimitList?.Name})";
                }

                res.AppendLine(
                    $"<li>Изменено поле 'ОИВ' старое значение: '{oldRes}', новое значение: '{newRes}'</li>");
            }

            if (source.CorpusNumber != target.CorpusNumber)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'номера корпусов' старое значение: '{source.CorpusNumber}', новое значение: '{target.CorpusNumber}'</li>");
            }

            if (source.ForMultipleStageCompany != target.ForMultipleStageCompany)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'размещение для многоэтапной кампании' старое значение: '{source.ForMultipleStageCompany.FormatEx()}', новое значение: '{target.ForMultipleStageCompany.FormatEx()}'</li>");
            }

            if (source.SubjectOfRestId != target.SubjectOfRestId)
            {
                var oldName = source.SubjectOfRest?.Name;
                if (string.IsNullOrEmpty(oldName))
                {
                    oldName = "не выбрано";
                }
                var newName =  uw.GetById<SubjectOfRest>(target.SubjectOfRestId)?.Name;
                if (string.IsNullOrEmpty(newName))
                {
                    newName = "не выбрано";
                }
                res.AppendLine(
                    $"<li>Изменено поле 'тематика смены' старое значение: '{oldName}', новое значение: '{newName}'</li>");
            }

            if (source.Descr != target.Descr)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'дополнительная информация' старое значение: '{source.Descr}', новое значение: '{target.Descr}'</li>");
            }

            if (source.ContractId != target.ContractId)
            {
                var oldOrg = uw.GetById<Organization>(source.Contract?.SupplierId);

                var targetContract = uw.GetById<Contract>(target.ContractId);
                var newOrg = uw.GetById<Organization>(targetContract?.SupplierId);

                var oldRes = source.ContractId != null ? $"{source.Contract?.SignNumber} ({oldOrg?.ShortName ?? oldOrg?.Name})"  : "не выбрано";
                var newRes = target.ContractId != null ? $"{targetContract?.SignNumber} ({newOrg?.ShortName ?? newOrg?.Name})"  : "не выбрано";

                res.AppendLine($"<li>Изменено поле 'контракт' старое значение: '{oldRes}', новое значение: '{newRes}'</li>");
            }

            if (source.TourPrice != target.TourPrice)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'стоимость для ребёнка' старое значение: '{String.Format("{0:0.00}", source.TourPrice)}', новое значение: '{String.Format("{0:0.00}", target.TourPrice)}'</li>");
            }

            if (source.TourPriceAttendant != target.TourPriceAttendant)
            {
                res.AppendLine(
                    $"<li>Изменено поле 'стоимость для взрослого' старое значение:'{String.Format("{0:0.00}", source.TourPriceAttendant)}', новое значение: '{String.Format("{0:0.00}", target.TourPriceAttendant)}'</li>");
            }

            if ((source.Volumes?.Any() ?? false) || (target.Volumes?.Any() ?? false))
            {
                var sourceTypeOfRoomsIds = source.Volumes?.Where(x => x.TourId == source.Id && x.TypeOfRoomsId != null)
                    .Select(a => a.TypeOfRoomsId).ToList();
                var targetTypeOfRoomsIds = target.Volumes?.Where(x => x.TypeOfRoomsId != null)
                    .Select(a => a.TypeOfRoomsId).ToList();

                var oldNotInNewTypeOfRooms = sourceTypeOfRoomsIds.Except(targetTypeOfRoomsIds).ToList();
                var newNotInOldTypeOfRooms = targetTypeOfRoomsIds.Except(sourceTypeOfRoomsIds).ToList();
                var inBothTypeOfRooms = sourceTypeOfRoomsIds.Intersect(targetTypeOfRoomsIds).ToList();

                foreach  (var tv in oldNotInNewTypeOfRooms.Select(i => source.Volumes?.Where(a => a.TypeOfRoomsId == i).FirstOrDefault()).Where(ss => ss != null))
                {
                    var tor = uw.GetById<TypeOfRooms>(tv?.TypeOfRoomsId);
                    res.AppendLine(
                        $"<li>В списке размещений удалено поле: '{  tor?.Name }', кол-во номеров:  '{tv?.CountRooms }'</li>");
                }
                foreach  (var tv in newNotInOldTypeOfRooms.Select(i => target.Volumes?.Where(a => a.TypeOfRoomsId == i).FirstOrDefault()).Where(ss => ss != null))
                {
                    var tor = uw.GetById<TypeOfRooms>(tv?.TypeOfRoomsId);
                    res.AppendLine(
                        $"<li>В список размещений добавлено поле: '{  tor?.Name }', кол-во номеров:  '{tv?.CountRooms }'</li>");
                }
                foreach (var i in inBothTypeOfRooms)
                {
                    var tvOld = source.Volumes?.Where(a => a.TypeOfRoomsId == i).FirstOrDefault();
                    var tvNew = target.Volumes?.Where(a => a.TypeOfRoomsId == i).FirstOrDefault();
                    if (tvOld?.CountRooms != tvNew?.CountRooms)
                    {
                        res.AppendLine( $"<li> В размещении '{tvOld.TypeOfRooms.Name}' изменено поле 'Кол-во номеров', старое значение: '{tvOld?.CountRooms}', новое значение: '{tvNew?.CountRooms}'</li>");
                    }

                    if (tvOld?.CountPlace != tvNew?.CountPlace)
                    {
                        res.AppendLine( $"<li> В размещении '{tvOld.TypeOfRooms.Name}' изменено поле 'Кол-во мест', старое значение: '{tvOld?.CountPlace}', новое значение: '{tvNew?.CountPlace}'</li>");
                    }
                }

                if(source.Volumes.Count == 1 && target.Volumes.Count == 1)
                {
                    var oldCountPlace = source.Volumes.FirstOrDefault().CountPlace;
                    var newCountPlace = target.Volumes.FirstOrDefault().CountPlace;
                    if (oldCountPlace != newCountPlace)
                    {
                        res.AppendLine($"<li> Изменено поле 'Кол-во мест', старое значение: '{oldCountPlace}', новое значение: '{newCountPlace}'</li>");
                    }
                }
            }

            return res.ToString();
        }
    }
}
