using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Состояния машины статусов
        /// </summary>
        private static void StateMachineState(Context context)
        {
            context.StateMachineState.AddOrUpdate(t => t.Id,
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Deleted,
                    Name = "Удалено"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Oiv.Formation,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    Name = "Формирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Oiv.Brought,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    Name = "Доведены до ведомства/ Формирование квот по учреждениям"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Oiv.BroughtToOrganization,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    Name = "Доведены до учреждений"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Oiv.GatheringRequirements,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    Name = "Сбор потребностей"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Organization.Formation,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "Формирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Organization.Brought,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "Доведены до организации/ Формирование списков"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Organization.ToApprove,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "Отправлена на утверждение в ОИВ"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Organization.Approved,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "Утверждено"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Organization.Confirmed,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "Подтверждено ДКгМ"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.Organization.OnCompletion,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    Name = "На доработку"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.List.Formation,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    Name = "Формирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.List.Formed,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    Name = "Сформирован"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.List.IncludedInTour,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    Name = "Ввод сведений об оплате"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Limit.List.IncludedPayment,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    Name = "Ввод сведений об оплате завершен"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Tour.Formation,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    Name = "Формирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Tour.ToFormed,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    Name = "Сформирован"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Tour.Formed,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    Name = "Утвержден"
                },
                //new StateMachineState
                //{
                //	Id = StateMachineStateEnum.Tour.Paid,
                //	StateMachineId = (long)StateMachineEnum.TourState,
                //	Name = "Оплачен"
                //},
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Hotel.Editing,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    Name = "Редактирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Hotel.ForApprove,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    Name = "Отправлен на утверждение"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Hotel.Approved,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    Name = "Утвержден"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Hotel.ForRework,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    Name = "На доработку"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Hotel.OnReworking,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    Name = "На доработке"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.Editing,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    Name = "Редактирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.ForApprove,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    Name = "Отправлен на утверждение"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.Approved,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    Name = "Утвержден"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.ForRework,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    Name = "На доработку"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.OnReworking,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    Name = "На доработке"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Contract.New,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    Name = "Новый"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Contract.Active,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    Name = "Действующий"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Contract.Archive,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    Name = "Архивный"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Bout.Editing,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    Name = "Редактирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Bout.Formed,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    Name = "Сформирован"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Party.Forming,
                    StateMachineId = (long) StateMachineEnum.PartyState,
                    Name = "Формирование"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Party.Formed,
                    StateMachineId = (long) StateMachineEnum.PartyState,
                    Name = "Сформирован"
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.DirectoryFlights.Forming,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.DirectoryFlightsState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.DirectoryFlights.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.DirectoryFlightsState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonService.Forming,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.AddonServiceState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonService.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.AddonServiceState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonService.Archive,
                    Name = "Архивная",
                    StateMachineId = (long) StateMachineEnum.AddonServiceState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTask.Approved,
                    Name = "На согласовании",
                    StateMachineId = (long) StateMachineEnum.CounselorTask
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTask.Delivered,
                    Name = "Задача поставлена",
                    StateMachineId = (long) StateMachineEnum.CounselorTask
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTask.Solved,
                    Name = "Задача решена",
                    StateMachineId = (long) StateMachineEnum.CounselorTask
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTask.Completion,
                    Name = "На доработке",
                    StateMachineId = (long) StateMachineEnum.CounselorTask
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTask.Sended,
                    Name = "Не прочитано",
                    StateMachineId = (long) StateMachineEnum.CounselorTask
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTask.Readed,
                    Name = "Прочитано",
                    StateMachineId = (long) StateMachineEnum.CounselorTask
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.Request,
                    Name = "Заявление на вожатого",
                    StateMachineId = (long) StateMachineEnum.CounselorState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Counselor.RequestDecline,
                    Name = "Заявление на вожатого (отказано)",
                    StateMachineId = (long) StateMachineEnum.CounselorState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AdministratorTour.Editing,
                    Name = "Редактирование",
                    StateMachineId = (long) StateMachineEnum.AdministratorTour
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AdministratorTour.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.AdministratorTour
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTest.Editing,
                    Name = "Редактирование",
                    StateMachineId = (long) StateMachineEnum.CounselorTest
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.CounselorTest.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.CounselorTest
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonServiceLink.Draft,
                    Name = "Черновик",
                    StateMachineId = (long) StateMachineEnum.AddonServiceLinkState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonServiceLink.Offered,
                    Name = "Выставлено",
                    StateMachineId = (long) StateMachineEnum.AddonServiceLinkState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonServiceLink.Formed,
                    Name = "Сформировано",
                    StateMachineId = (long) StateMachineEnum.AddonServiceLinkState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.AddonServiceLink.Canceled,
                    Name = "Отменено",
                    StateMachineId = (long) StateMachineEnum.AddonServiceLinkState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Bout.Confirmed,
                    Name = "Подтвержден",
                    StateMachineId = (long) StateMachineEnum.BoutState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Bout.Closed,
                    Name = "Завершен",
                    StateMachineId = (long) StateMachineEnum.BoutState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Calculation.Unpaid,
                    Name = "Не оплачено",
                    StateMachineId = (long) StateMachineEnum.CalculationState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Calculation.Paid,
                    Name = "Оплачено",
                    StateMachineId = (long) StateMachineEnum.CalculationState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Calculation.Cancelled,
                    Name = "Аннулировано",
                    StateMachineId = (long) StateMachineEnum.CalculationState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Transport.Forming,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.TransportState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Transport.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.TransportState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.TrainingCounselors.Editing,
                    Name = "Редактирование",
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.TrainingCounselors.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.TrainingCounselors.EducationFinished,
                    Name = "Обучение завершено",
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PedParty.Editing,
                    Name = "Редактирование",
                    StateMachineId = (long) StateMachineEnum.PedParty
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PedParty.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.PedParty
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.LimitRequest.Editing,
                    Name = "Редактирование",
                    StateMachineId = (long) StateMachineEnum.LimitRequest
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.LimitRequest.OnAprove,
                    Name = "На утверждении",
                    StateMachineId = (long) StateMachineEnum.LimitRequest
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.LimitRequest.Approved,
                    Name = "Утверждено",
                    StateMachineId = (long) StateMachineEnum.LimitRequest
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.LimitRequest.Declined,
                    Name = "Отклонено",
                    StateMachineId = (long) StateMachineEnum.LimitRequest
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Payment.Unlinked,
                    Name = "Не связано с начислением",
                    StateMachineId = (long) StateMachineEnum.Payments
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Payment.Linked,
                    Name = "Связано с начислением",
                    StateMachineId = (long) StateMachineEnum.Payments
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Payment.Anuled,
                    Name = "Аннулировано",
                    StateMachineId = (long) StateMachineEnum.Payments
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.CertificateIssued,
                    Name = "Оплачено",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.ReadyToPayFull,
                    Name = "Готова к доплате",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.ReadyToPay,
                    Name = "Готово к оплате",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.OnWorking,
                    Name = "В работе",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.Reject,
                    Name = "Аннулировано",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.OnApprove,
                    Name = "Запрошено",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.Payed,
                    Name = "Оплачен аванс",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = (long) StatusEnum.Denial,
                    Name = "Отказ",
                    StateMachineId = (long) StateMachineEnum.Request
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.TradeUnion.Approved,
                    Name = "Утверждено",
                    StateMachineId = (long) StateMachineEnum.TradeUnionList
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.TradeUnion.Declined,
                    Name = "Отклонено",
                    StateMachineId = (long) StateMachineEnum.TradeUnionList
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.TradeUnion.Edit,
                    Name = "Редактирование",
                    StateMachineId = (long) StateMachineEnum.TradeUnionList
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.TradeUnion.Finish,
                    Name = "Сведения о заехавших внесены",
                    StateMachineId = (long) StateMachineEnum.TradeUnionList
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.TradeUnion.OnAproving,
                    Name = "На утверждении",
                    StateMachineId = (long) StateMachineEnum.TradeUnionList
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroup.Formation,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.PupilGroup
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroup.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.PupilGroup
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroup.OnAgreement,
                    Name = "Согласование ДТСЗН",
                    StateMachineId = (long) StateMachineEnum.PupilGroup
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroup.Agreed,
                    Name = "Согласована",
                    StateMachineId = (long) StateMachineEnum.PupilGroup
                }, new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroup.Approved,
                    Name = "Утверждена",
                    StateMachineId = (long) StateMachineEnum.PupilGroup
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroup.Deleted,
                    Name = "Удалена",
                    StateMachineId = (long) StateMachineEnum.PupilGroup
                },
                //-----------
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroupList.Formation,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.LimitListState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroupList.Formed,
                    Name = "Сформирован",
                    StateMachineId = (long) StateMachineEnum.LimitListState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroupList.Approved,
                    Name = "Утверждён",
                    StateMachineId = (long) StateMachineEnum.LimitListState
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.PupilGroupList.Deleted,
                    Name = "Удалён",
                    StateMachineId = (long) StateMachineEnum.LimitListState
                },
                //-----------
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Certificate.PaidOff,
                    Name = "Использован",
                    StateMachineId = (long) StateMachineEnum.Certificate,
                    Eid = 34000
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Certificate.Paid,
                    Name = "Оплачен",
                    StateMachineId = (long) StateMachineEnum.Certificate,
                    Eid = 34001
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Certificate.ReportingSubmitted,
                    Name = "Отчетность сдана",
                    StateMachineId = (long) StateMachineEnum.Certificate,
                    Eid = 34002
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Certificate.ReportingAccepted,
                    Name = "Отчетность принята",
                    StateMachineId = (long) StateMachineEnum.Certificate,
                    Eid = 34003
                },
                //-----------
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                    Name = "Согласование",
                    StateMachineId = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed,
                    Name = "Согласована",
                    StateMachineId = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Approved,
                    Name = "Утверждена",
                    StateMachineId = (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                },
                //-----------
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.Formation,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.MonitoringFinanceInformation,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.OnAgreement,
                    Name = "Согласование",
                    StateMachineId = (long) StateMachineEnum.MonitoringFinanceInformation,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.Agreed,
                    Name = "Согласована",
                    StateMachineId = (long) StateMachineEnum.MonitoringFinanceInformation,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.FinanceInformation.Approved,
                    Name = "Утверждена",
                    StateMachineId = (long) StateMachineEnum.MonitoringFinanceInformation,
                },
                //-----------
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation,
                    Name = "Формирование",
                    StateMachineId = (long) StateMachineEnum.MonitoringSmallLeisureInfoData,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.OnApproving,
                    Name = "Утверждение",
                    StateMachineId = (long) StateMachineEnum.MonitoringSmallLeisureInfoData,
                },
                new StateMachineState
                {
                    Id = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved,
                    Name = "Утверждена",
                    StateMachineId = (long) StateMachineEnum.MonitoringSmallLeisureInfoData,
                }
                //-----------
            );

            SetEidAndLastUpdateTicks(context.StateMachineState.Where(ss => ss.Eid == null).ToList());
            context.SaveChanges();
        }
    }
}
