using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Машины статусов
        /// </summary>
        private static void StateMachine(Context context)
        {
            context.StateMachine.AddOrUpdate(m => m.Id,
                new StateMachine
                {
                    Id = (long) StateMachineEnum.LimitListState,
                    Name = "статусы для списка организации"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "квота по организации"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.LimitOivState,
                    Name = "квота по ОИВ"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.LimitGeneralState,
                    Name = "Общегородской список"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.TourState,
                    Name = "Блок мест"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.HotelState,
                    Name = "Место отдыха"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.CounselorState,
                    Name = "Вожатый"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.ContractState,
                    Name = "Контракт"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.BoutState,
                    Name = "Заезд"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.PartyState,
                    Name = "Отряд"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.DirectoryFlightsState,
                    Name = "Рейс"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.AddonServiceState,
                    Name = "Доп услуги."
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.CounselorTask,
                    Name = "Задачи вожатым."
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.AdministratorTour,
                    Name = "Администратор заезда"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.CounselorTest,
                    Name = "Тестирование вожатых"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.TransportState,
                    Name = "Транспорт"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.CalculationState,
                    Name = "Начисление"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.AddonServiceLinkState,
                    Name = "Связь с услугой"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.TrainingCounselors,
                    Name = "Вожатые. Группа обучения."
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.PedParty,
                    Name = "Педотряд"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.LimitRequest,
                    Name = "Профильники. Заявка на квоту"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.Payments,
                    Name = "Платежи"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.Request,
                    Name = "Заявки"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.TradeUnionList,
                    Name = "Профсоюзные списки"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.PupilGroup,
                    Name = "Группы (потребности) приютов"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.Certificate,
                    Name = "Сертификаты"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    Name = "Мониторинг. Сведения о численности детей"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.MonitoringFinanceInformation,
                    Name = "Мониторинг. Сведения о финансировании оздоровительной кампании"
                },
                new StateMachine
                {
                    Id = (long) StateMachineEnum.MonitoringSmallLeisureInfoData,
                    Name = "Мониторинг. Сведения о малых формах занятости детей"
                });

            SetEidAndLastUpdateTicks(context.StateMachine.ToList());
            context.SaveChanges();
        }
    }
}
