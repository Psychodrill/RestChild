using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Действия машины статусов
        /// </summary>
        private static void StateMachineAction(Context context)
        {
            var index = 0;
            context.StateMachineAction.AddOrUpdate(t => t.Id,
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.List.Formed,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.Limit.List.Formed,
                    ActionName = "Завершить формирование списка",
                    ActionCode = AccessRightEnum.Limit.List.Formed,
                    Description = "Завершить формирование списка?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.List.Formation,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.Limit.List.Formation,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Limit.List.Formation,
                    Description = "Внести изменения в сформированный список?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.List.IncludedInTour,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.Limit.List.IncludedInTour,
                    ActionName = "Включить в блок мест",
                    ActionCode = AccessRightEnum.Limit.List.IncludedInTour,
                    IsSystemAction = true,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.List.IncludedPayment,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.Limit.List.IncludedPayment,
                    ActionName = "Сведения об оплате внесены",
                    ActionCode = AccessRightEnum.Limit.List.IncludedPayment,
                    Description = "Ввод сведений об оплате завершен?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.Formation,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.Formation,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Limit.Organization.Formation,
                    IsSystemAction = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.Brought,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.Brought,
                    ActionName = "Довести квоту до учреждения",
                    ActionCode = AccessRightEnum.Limit.Organization.Brought,
                    Description = "Вы действительно хотите довести размер квоты до учреждения?",
                    IsSystemAction = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.BroughtToCompetiotion,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.Brought,
                    ActionName = "Редактировать списки",
                    ActionCode = AccessRightEnum.Limit.Organization.BroughtToCompletion,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.ToApprove,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.ToApprove,
                    ActionName = "Отправить на утверждение",
                    ActionCode = AccessRightEnum.Limit.Organization.ToApprove,
                    Description =
                        "После отправки списка на утверждение изменение списка будет невозможно. Отправить на утверждение в ОИВ?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.Approved,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Limit.Organization.Approved,
                    NeedSign = false,
                    Description = "Утвердить сформированный список?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.OnCompletion,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.OnCompletion,
                    ActionName = "На доработку",
                    ActionCode = AccessRightEnum.Limit.Organization.OnCompletion,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.ApprovedToCompetiotion,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.OnCompletion,
                    ActionName = "На доработку",
                    Description = "Отправить утвержденный список на доработку?",
                    ActionCode = AccessRightEnum.Limit.Organization.ApprovedToCompetiotion,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.Confirmed,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.Confirmed,
                    ActionName = "Подтвердить",
                    Description = "Подтвердить утвержденный список?",
                    ActionCode = AccessRightEnum.Limit.Organization.Confirmed,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Oiv.Brought,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    ToStateId = StateMachineStateEnum.Limit.Oiv.Brought,
                    ActionName = "Подтвердить",
                    ActionCode = AccessRightEnum.Limit.Oiv.Brought,
                    Description = "Вы действительно хотите довести размер квоты до ОИВ?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Oiv.BroughtToOrganization,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    ToStateId = StateMachineStateEnum.Limit.Oiv.BroughtToOrganization,
                    ActionName = "Довести квоты до учреждения",
                    ActionCode = AccessRightEnum.Limit.Oiv.BroughtToOrganization,
                    Description = "Вы действительно хотите довести размер квоты до учреждения?",
                    IsSystemAction = true,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Oiv.GatheringRequirements,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    ToStateId = StateMachineStateEnum.Limit.Oiv.GatheringRequirements,
                    ActionName = "Продолжить сбор потребностей",
                    ActionCode = AccessRightEnum.Limit.Oiv.GatheringRequirements,
                    Description = "Вы действительно хотите продолжить сбор потребностей?",
                    IsSystemAction = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Oiv.Formation,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    ToStateId = StateMachineStateEnum.Limit.Oiv.Formation,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Limit.Oiv.Formation,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Oiv.OnCompletion,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    ToStateId = StateMachineStateEnum.Limit.Oiv.Brought,
                    ActionName = "Вернуть на редактирование",
                    ActionCode = AccessRightEnum.Limit.Oiv.OnCompletion,
                    Description = "Вернуть доведенные квоты на редактирование?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Tour.ToFormed,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    ToStateId = StateMachineStateEnum.Tour.ToFormed,
                    ActionName = "Сформировать",
                    Description = "Вы действительно хотите сформировать?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Tour.ToTourForm,
                    LastUpdateTick = ++index
                },
                //new StateMachineAction
                //{
                //	Id = StateMachineStateEnum.Tour.Paid,
                //	StateMachineId = (long)StateMachineEnum.TourState,
                //	ToStateId = StateMachineStateEnum.Tour.Paid,
                //	ActionName = "Завершить ввод сведений об оплате",
                //	Description = "Вы действительно хотите завершить ввод сведений об оплате?",
                //	IsSystemAction = true,
                //	ActionCode = AccessRightEnum.Tour.TourPay
                //},
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Tour.Formed,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    ToStateId = StateMachineStateEnum.Tour.Formed,
                    ActionName = "Утвердить",
                    Description = "Вы действительно хотите утвердить?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.Tour.TourForm,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Tour.Formation,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    ToStateId = StateMachineStateEnum.Tour.Formation,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.Tour.TourEdit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Hotel.ForApprove,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    ToStateId = StateMachineStateEnum.Hotel.ForApprove,
                    ActionName = "Отправить на утверждение",
                    Description = "Вы действительно хотите отправить место отдыха на утверждение?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Hotel.ForAprove,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Hotel.Approved,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    ToStateId = StateMachineStateEnum.Hotel.Approved,
                    ActionName = "Утвердить",
                    Description = "Вы действительно хотите утвердить место отдыха?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Hotel.Approved,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Hotel.ForRework,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    ToStateId = StateMachineStateEnum.Hotel.ForRework,
                    ActionName = "Отправить на доработку",
                    Description = "Вы действительно хотите отправить место отдыха на доработку?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Hotel.ForRework,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Hotel.OnReworking,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    ToStateId = StateMachineStateEnum.Hotel.OnReworking,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите начать редактирование?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Hotel.OnReworking,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Counselor.ForApprove,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    ToStateId = StateMachineStateEnum.Counselor.ForApprove,
                    ActionName = "Отправить на утверждение",
                    Description = "Вы действительно хотите отправить информацию о вожатом на утверждение?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Counselor.ForAprove,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Counselor.Approved,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    ToStateId = StateMachineStateEnum.Counselor.Approved,
                    ActionName = "Утвердить",
                    Description = "Вы действительно хотите утвердить информацию о вожатом?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Counselor.Approved,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Counselor.ForRework,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    ToStateId = StateMachineStateEnum.Counselor.ForRework,
                    ActionName = "Отправить на доработку",
                    Description = "Вы действительно хотите отправить информацию о вожатом на доработку?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Counselor.ForRework,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Counselor.OnReworking,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    ToStateId = StateMachineStateEnum.Counselor.OnReworking,
                    ActionName = "Редактирование",
                    Description = "Вы действительно хотите начать редактирование?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Counselor.OnReworking,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Contract.Active,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    ToStateId = StateMachineStateEnum.Contract.Active,
                    ActionName = "Зарегистрировать",
                    Description = "Вы действительно хотите зарегистрировать контракт?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Contract.Register,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Contract.Archive,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    ToStateId = StateMachineStateEnum.Contract.Archive,
                    ActionName = "Перевести в архив",
                    Description = "Вы действительно хотите перевести контракт в архив?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Contract.Archive,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Contract.New,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    ToStateId = StateMachineStateEnum.Contract.New,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите перевести контракт в редактирование?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Contract.ToEdit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Bout.Formed,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    ToStateId = StateMachineStateEnum.Bout.Formed,
                    ActionName = "Сформировать заезд",
                    Description = "Вы действительно хотите сформировать заезд?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Bout.Forming,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Party.Formed,
                    StateMachineId = (long) StateMachineEnum.PartyState,
                    ToStateId = StateMachineStateEnum.Party.Formed,
                    ActionName = "Сформировать",
                    Description = "Вы действительно хотите сформировать отряд?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Party.Forming,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Transport.Formed,
                    StateMachineId = (long) StateMachineEnum.TransportState,
                    ToStateId = StateMachineStateEnum.Transport.Formed,
                    ActionName = "Сформировать",
                    ActionCode = AccessRightEnum.Transport.Form,
                    Description = "Завершить формирование?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Transport.Forming,
                    StateMachineId = (long) StateMachineEnum.TransportState,
                    ToStateId = StateMachineStateEnum.Transport.Forming,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Transport.Edit,
                    Description = "Начать редактирование?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Party.Forming,
                    StateMachineId = (long) StateMachineEnum.PartyState,
                    ToStateId = StateMachineStateEnum.Party.Forming,
                    ActionName = "Редактировать",
                    Description = "Начать редактирование?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Party.Edit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Bout.Editing,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    ToStateId = StateMachineStateEnum.Bout.Editing,
                    ActionName = "Редактировать",
                    Description = "Начать редактирование?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Bout.Edit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Limit.Organization.ConfirmedToApproved,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    ToStateId = StateMachineStateEnum.Limit.Organization.ToApprove,
                    ActionName = "Вернуть на утверждение",
                    ActionCode = AccessRightEnum.Limit.Organization.ConfirmedToApproved,
                    Description = "Вернуть список на утверждение в ОИВ?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTask.Approved,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    ToStateId = StateMachineStateEnum.CounselorTask.Approved,
                    ActionName = "Отправить на согласование",
                    ActionCode = AccessRightEnum.CounselorTask.ToApproved,
                    Description = "Отправить на согласование?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTask.Solved,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    ToStateId = StateMachineStateEnum.CounselorTask.Solved,
                    ActionName = "Задача решена",
                    ActionCode = AccessRightEnum.CounselorTask.ToSolved,
                    Description = "Задача решена?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTask.Completion,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    ToStateId = StateMachineStateEnum.CounselorTask.Completion,
                    ActionName = "На доработку",
                    ActionCode = AccessRightEnum.CounselorTask.ToCompletion,
                    Description = "Отправить на доработку?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTask.Delivered,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    ToStateId = StateMachineStateEnum.CounselorTask.Delivered,
                    ActionName = "Исправить",
                    ActionCode = AccessRightEnum.CounselorTask.ToDelivered,
                    Description = "Исправить результаты исполнения?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.DirectoryFlights.Formed,
                    StateMachineId = (long) StateMachineEnum.DirectoryFlightsState,
                    ToStateId = StateMachineStateEnum.DirectoryFlights.Formed,
                    ActionName = "Сформировать рейс",
                    ActionCode = AccessRightEnum.DirectoryFlights.Form,
                    Description = "Завершить формирование рейса?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.DirectoryFlights.Forming,
                    StateMachineId = (long) StateMachineEnum.DirectoryFlightsState,
                    ToStateId = StateMachineStateEnum.DirectoryFlights.Forming,
                    ActionName = "Редактировать рейс",
                    ActionCode = AccessRightEnum.DirectoryFlights.Edit,
                    Description = "Начать редактирование рейса?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.AddonService.Formed,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    ToStateId = StateMachineStateEnum.AddonService.Formed,
                    ActionName = "Сформировать",
                    ActionCode = AccessRightEnum.AddonService.ToFormed,
                    Description = "Завершить формирование?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.AddonService.Forming,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    ToStateId = StateMachineStateEnum.AddonService.Forming,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.AddonService.ToForming,
                    Description = "Начать редактирование?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.AddonService.Archive,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    ToStateId = StateMachineStateEnum.AddonService.Archive,
                    ActionName = "В архив",
                    ActionCode = AccessRightEnum.AddonService.ToArchive,
                    Description = "Отправить в архив?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Counselor.Request,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    ToStateId = StateMachineStateEnum.Counselor.Request,
                    ActionName = "Заявка",
                    Description = "Зарегистрировать заявку на вожатого?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Counselor.OnRequest,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Counselor.RequestDecline,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    ToStateId = StateMachineStateEnum.Counselor.RequestDecline,
                    ActionName = "Отказать",
                    Description = "Вы действительно хотите отказать соискателю?",
                    IsSystemAction = false,
                    ActionCode = AccessRightEnum.Counselor.RequestDecline,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Tour.ToFormationFromFormed,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    ToStateId = StateMachineStateEnum.Tour.Formation,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.Tour.ToFormationFromFormed,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.AdministratorTour.Formed,
                    StateMachineId = (long) StateMachineEnum.AdministratorTour,
                    ToStateId = StateMachineStateEnum.AdministratorTour.Formed,
                    ActionName = "Сформировать",
                    Description = "Вы действительно хотите сформировать?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.AdministratorTour.ToFormed,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.AdministratorTour.Editing,
                    StateMachineId = (long) StateMachineEnum.AdministratorTour,
                    ToStateId = StateMachineStateEnum.AdministratorTour.Editing,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.AdministratorTour.ToEdit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTask.Readed,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    ToStateId = StateMachineStateEnum.CounselorTask.Readed,
                    ActionName = "Прочитано",
                    Description = "Изменить статус сообщения в прочитано?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.CounselorTask.ToReaded,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTask.Sended,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    ToStateId = StateMachineStateEnum.CounselorTask.Sended,
                    ActionName = "Не прочитано",
                    Description = "Изменить статус сообщения в не прочитано?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.CounselorTask.ToUnreaded,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTest.Formed,
                    StateMachineId = (long) StateMachineEnum.CounselorTest,
                    ToStateId = StateMachineStateEnum.CounselorTest.Formed,
                    ActionName = "Сформировать",
                    Description = "Вы действительно хотите сформировать?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.CounselorTest.ToFormed,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.CounselorTest.Editing,
                    StateMachineId = (long) StateMachineEnum.CounselorTest,
                    ToStateId = StateMachineStateEnum.CounselorTest.Editing,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.CounselorTest.ToEdit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TrainingCounselors.Formed,
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors,
                    ToStateId = StateMachineStateEnum.TrainingCounselors.Formed,
                    ActionName = "Сформировать",
                    Description = "Вы действительно хотите сформировать?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.TrainingCounselors.ToFormed,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TrainingCounselors.Editing,
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors,
                    ToStateId = StateMachineStateEnum.TrainingCounselors.Editing,
                    ActionName = "Редактировать",
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.TrainingCounselors.ToEdit,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TrainingCounselors.EducationFinished,
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors,
                    ToStateId = StateMachineStateEnum.TrainingCounselors.EducationFinished,
                    ActionName = "Обучение завершено",
                    Description = "Вы действительно хотите установить признак об окончании обучения группы?",
                    IsSystemAction = false,
                    NeedSign = false,
                    ActionCode = AccessRightEnum.TrainingCounselors.ToEducationFinished,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Bout.Confirmed,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    ToStateId = StateMachineStateEnum.Bout.Confirmed,
                    ActionName = "Подтвердить заезд",
                    ActionCode = AccessRightEnum.Bout.Confirmed,
                    Description = "Подтвердить заезд?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Bout.Closed,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    ToStateId = StateMachineStateEnum.Bout.Closed,
                    ActionName = "Закрыть заезд",
                    ActionCode = AccessRightEnum.Bout.Closed,
                    Description = "Закрыть заезд?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Bout.FromConfirmedToFormed,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    ToStateId = StateMachineStateEnum.Bout.Formed,
                    ActionName = "В сформировано",
                    ActionCode = AccessRightEnum.Bout.FromConfirmedToFormed,
                    Description = "Откатить заезд в сформировано?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Bout.FromClosedToConfirmed,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    ToStateId = StateMachineStateEnum.Bout.Confirmed,
                    ActionName = "В подтвержден",
                    ActionCode = AccessRightEnum.Bout.FromClosedToConfirmed,
                    Description = "Откатить заезд в подтвержден?",
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PedParty.Formed,
                    StateMachineId = (long) StateMachineEnum.PedParty,
                    ToStateId = StateMachineStateEnum.PedParty.Formed,
                    ActionName = "Сформировать",
                    ActionCode = AccessRightEnum.PedParty.PedPartyToFormed,
                    Description = "Вы действительно хотите сформировать?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PedParty.Editing,
                    StateMachineId = (long) StateMachineEnum.PedParty,
                    ToStateId = StateMachineStateEnum.PedParty.Editing,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.PedParty.PedPartyToEdit,
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.LimitRequest.Editing,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    ToStateId = StateMachineStateEnum.LimitRequest.Editing,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Limit.Request.Edit,
                    Description = "Вы действительно хотите отправить на редактирование?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.LimitRequest.OnAprove,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    ToStateId = StateMachineStateEnum.LimitRequest.OnAprove,
                    ActionName = "На утверждение",
                    ActionCode = AccessRightEnum.Limit.Request.ToApprove,
                    Description = "Вы действительно хотите отправить на утверждение?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.LimitRequest.Approved,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    ToStateId = StateMachineStateEnum.LimitRequest.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Limit.Request.Approve,
                    Description = "Вы действительно хотите утвердить?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.LimitRequest.Declined,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    ToStateId = StateMachineStateEnum.LimitRequest.Declined,
                    ActionName = "Отказать",
                    ActionCode = AccessRightEnum.Limit.Request.Decline,
                    Description = "Вы действительно хотите отказать?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Calculation.Cancelled,
                    StateMachineId = (long) StateMachineEnum.CalculationState,
                    ToStateId = StateMachineStateEnum.Calculation.Cancelled,
                    ActionName = "Аннулировать",
                    ActionCode = AccessRightEnum.Calculation.ToCancelled,
                    Description = "Вы действительно хотите аннулировать начисление?",
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Calculation.Paid,
                    StateMachineId = (long) StateMachineEnum.CalculationState,
                    ToStateId = StateMachineStateEnum.Calculation.Paid,
                    ActionName = "Оплачено",
                    ActionCode = AccessRightEnum.Calculation.ToPaid,
                    IsSystemAction = true,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.OnWorking,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.OnWorking,
                    ActionName = "В работу",
                    ActionCode = AccessRightEnum.Request.ToOnWorkig,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.ReadyToPay,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.ReadyToPay,
                    ActionName = "В оплату",
                    ActionCode = AccessRightEnum.Request.ToReadyToPay,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.OnApprove,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.OnApprove,
                    ActionName = "Запрошено",
                    ActionCode = AccessRightEnum.Request.ToOnApprove,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.ReadyToPayFull,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.ReadyToPayFull,
                    ActionName = "Готово к доплате",
                    ActionCode = AccessRightEnum.Request.ToReadyToPayFull,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.CertificateIssued,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.CertificateIssued,
                    ActionName = "Оплачено",
                    ActionCode = AccessRightEnum.Request.ToCertificateIssued,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.Reject,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.Reject,
                    ActionName = "Аннулировать",
                    ActionCode = AccessRightEnum.Request.ToReject,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.Draft,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.Draft,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Request.ToDraft,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.Denial,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.Denial,
                    ActionName = "Отказать",
                    ActionCode = AccessRightEnum.Request.ToDenial,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Request.Payed,
                    StateMachineId = (long) StateMachineEnum.Request,
                    ToStateId = StateMachineStateEnum.Request.Payed,
                    ActionName = "В оплачен аванс",
                    ActionCode = AccessRightEnum.Request.ToPayed,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TradeUnion.Approved,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    ToStateId = StateMachineStateEnum.TradeUnion.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.TradeUnionList.ToApproved,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TradeUnion.Declined,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    ToStateId = StateMachineStateEnum.TradeUnion.Declined,
                    ActionName = "Отклонить",
                    ActionCode = AccessRightEnum.TradeUnionList.ToDeclined,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TradeUnion.Edit,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    ToStateId = StateMachineStateEnum.TradeUnion.Edit,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.TradeUnionList.ToEdit,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TradeUnion.Finish,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    ToStateId = StateMachineStateEnum.TradeUnion.Finish,
                    ActionName = "Завершить внос сведений о заехавших",
                    ActionCode = AccessRightEnum.TradeUnionList.ToFinish,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TradeUnion.OnAproving,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    ToStateId = StateMachineStateEnum.TradeUnion.OnAproving,
                    ActionName = "На утверждение",
                    ActionCode = AccessRightEnum.TradeUnionList.ToOnAproving,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.TradeUnion.EditForAll,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    ToStateId = StateMachineStateEnum.TradeUnion.Edit,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.TradeUnionList.ToEditFromAll,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                //Группы (потребности) приютов
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.Formed,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.Formed,
                    ActionName = "Сформировать",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupForm,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.OnAgreement,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.OnAgreement,
                    ActionName = "Отправить на согласование",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupOnAgreement,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.Agreed,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.Agreed,
                    ActionName = "Согласовать",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupAgree,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.Approved,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupApprove,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.EditForAll,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.Formation,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupEdit,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.EditForMGT,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.Formation,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupEditFromApprove,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroup.Deleted,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    ToStateId = StateMachineStateEnum.PupilGroup.Deleted,
                    ActionName = "Удалить",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupDelete,
                    IsSystemAction = true,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                //Списки (группы отправки) приюта
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroupList.Formed,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.PupilGroupList.Formed,
                    ActionName = "Сформировать",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupListForm,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroupList.Approved,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.PupilGroupList.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupListApprove,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroupList.EditForAll,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.PupilGroupList.Formation,
                    ActionName = "Редактировать",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupListEdit,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.PupilGroupList.Deleted,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    ToStateId = StateMachineStateEnum.PupilGroupList.Deleted,
                    ActionName = "Удалить",
                    ActionCode = AccessRightEnum.Orphans.PupilGroupListDelete,
                    IsSystemAction = true,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                // Мониторинг. Сведения о численности детей
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                    StateMachineId = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                    ActionName = "Отправить на согласование",
                    ActionCode = AccessRightEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed,
                    StateMachineId = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed,
                    ActionName = "Согласовать",
                    ActionCode = AccessRightEnum.Monitoring.ChildrenNumberInformation.Agree,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.EditForAll,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation,
                    ActionName = "Отправить на доработку",
                    ActionCode = AccessRightEnum.Monitoring.ChildrenNumberInformation.ToEdit,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Approved,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Monitoring.ChildrenNumberInformation.Approve,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                // Мониторинг. Сведения о финансировании оздоровительной кампании
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.OnAgreement,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.FinanceInformation.OnAgreement,
                    ActionName = "Отправить на согласование",
                    ActionCode = AccessRightEnum.Monitoring.FinanceInformation.OnAgreement,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.Agreed,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Agreed,
                    ActionName = "Согласовать",
                    ActionCode = AccessRightEnum.Monitoring.FinanceInformation.Agree,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.EditForAll,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Formation,
                    ActionName = "Отправить на доработку",
                    ActionCode = AccessRightEnum.Monitoring.FinanceInformation.ToEdit,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.Approved,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    ToStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Monitoring.FinanceInformation.Approve,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                // Мониторинг. Сведения о малых формах занятости детей
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.OnApproving,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    ToStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.OnApproving,
                    ActionName = "Отправить на утверждение",
                    ActionCode = AccessRightEnum.Monitoring.SmallLeisureInfoData.OnApproving,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.EditForAll,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    ToStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation,
                    ActionName = "Отправить на доработку",
                    ActionCode = AccessRightEnum.Monitoring.SmallLeisureInfoData.ToEdit,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                },
                new StateMachineAction
                {
                    Id = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    ToStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved,
                    ActionName = "Утвердить",
                    ActionCode = AccessRightEnum.Monitoring.SmallLeisureInfoData.Approve,
                    IsSystemAction = false,
                    NeedSign = false,
                    LastUpdateTick = ++index
                });

            SetEidAndLastUpdateTicks(context.StateMachineAction.ToList());
            context.SaveChanges();
        }
    }
}
