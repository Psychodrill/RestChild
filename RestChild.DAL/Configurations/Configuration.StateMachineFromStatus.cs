using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Источники действий машины статусов
        /// </summary>
        private static void StateMachineFromStatus(Context context)
        {
            var id = 1;

            context.StateMachineFromStatus.AddOrUpdate(t => t.Id,
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.Limit.List.Formed,
                    FromStateId = StateMachineStateEnum.Limit.List.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.Limit.List.Formation,
                    FromStateId = StateMachineStateEnum.Limit.List.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.Limit.List.IncludedPayment,
                    FromStateId = StateMachineStateEnum.Limit.List.IncludedInTour
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.Limit.List.IncludedPayment,
                    FromStateId = StateMachineStateEnum.Limit.List.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.Brought,
                    FromStateId = StateMachineStateEnum.Limit.Organization.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.Formation,
                    FromStateId = StateMachineStateEnum.Limit.Organization.Brought
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.Formation,
                    FromStateId = StateMachineStateEnum.Limit.Organization.ToApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.Formation,
                    FromStateId = StateMachineStateEnum.Limit.Organization.OnCompletion
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.ToApprove,
                    FromStateId = StateMachineStateEnum.Limit.Organization.Brought
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.BroughtToCompetiotion,
                    FromStateId = StateMachineStateEnum.Limit.Organization.OnCompletion
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.Approved,
                    FromStateId = StateMachineStateEnum.Limit.Organization.ToApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.OnCompletion,
                    FromStateId = StateMachineStateEnum.Limit.Organization.ToApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.Confirmed,
                    FromStateId = StateMachineStateEnum.Limit.Organization.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.ApprovedToCompetiotion,
                    FromStateId = StateMachineStateEnum.Limit.Organization.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.Brought,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.Formation,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.Brought
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.BroughtToOrganization,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.Brought
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.Brought,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.GatheringRequirements
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.Formation,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.BroughtToOrganization
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.OnCompletion,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.BroughtToOrganization
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOivState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Oiv.GatheringRequirements,
                    FromStateId = StateMachineStateEnum.Limit.Oiv.Brought
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    StateMachineActionId = StateMachineStateEnum.Tour.ToFormed,
                    FromStateId = StateMachineStateEnum.Tour.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    StateMachineActionId = StateMachineStateEnum.Tour.Formed,
                    FromStateId = StateMachineStateEnum.Tour.ToFormed
                },
                //new StateMachineFromStatus
                //{
                //	Id = ++id,
                //	StateMachineId = (long)StateMachineEnum.TourState,
                //	StateMachineActionId = StateMachineStateEnum.Tour.Paid,
                //	FromStateId = StateMachineStateEnum.Tour.Formed
                //},
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    StateMachineActionId = StateMachineStateEnum.Tour.Formation,
                    FromStateId = StateMachineStateEnum.Tour.ToFormed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    StateMachineActionId = StateMachineStateEnum.Hotel.ForApprove,
                    FromStateId = StateMachineStateEnum.Hotel.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    StateMachineActionId = StateMachineStateEnum.Hotel.ForApprove,
                    FromStateId = StateMachineStateEnum.Hotel.OnReworking
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    StateMachineActionId = StateMachineStateEnum.Hotel.Approved,
                    FromStateId = StateMachineStateEnum.Hotel.ForApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    StateMachineActionId = StateMachineStateEnum.Hotel.OnReworking,
                    FromStateId = StateMachineStateEnum.Hotel.ForRework
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    StateMachineActionId = StateMachineStateEnum.Hotel.ForRework,
                    FromStateId = StateMachineStateEnum.Hotel.ForApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.HotelState,
                    StateMachineActionId = StateMachineStateEnum.Hotel.ForRework,
                    FromStateId = StateMachineStateEnum.Hotel.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.ForApprove,
                    FromStateId = StateMachineStateEnum.Counselor.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.ForApprove,
                    FromStateId = StateMachineStateEnum.Counselor.OnReworking
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.Approved,
                    FromStateId = StateMachineStateEnum.Counselor.ForApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.OnReworking,
                    FromStateId = StateMachineStateEnum.Counselor.ForRework
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.ForRework,
                    FromStateId = StateMachineStateEnum.Counselor.ForApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    StateMachineActionId = StateMachineStateEnum.Contract.Active,
                    FromStateId = StateMachineStateEnum.Contract.New
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    StateMachineActionId = StateMachineStateEnum.Contract.New,
                    FromStateId = StateMachineStateEnum.Contract.Archive
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    StateMachineActionId = StateMachineStateEnum.Contract.New,
                    FromStateId = StateMachineStateEnum.Contract.Active
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.ContractState,
                    StateMachineActionId = StateMachineStateEnum.Contract.Archive,
                    FromStateId = StateMachineStateEnum.Contract.Active
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    StateMachineActionId = StateMachineStateEnum.Bout.Formed,
                    FromStateId = StateMachineStateEnum.Bout.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    StateMachineActionId = StateMachineStateEnum.Party.Formed,
                    FromStateId = StateMachineStateEnum.Party.Forming
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.DirectoryFlightsState,
                    StateMachineActionId = StateMachineStateEnum.DirectoryFlights.Formed,
                    FromStateId = StateMachineStateEnum.DirectoryFlights.Forming
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.DirectoryFlightsState,
                    StateMachineActionId = StateMachineStateEnum.DirectoryFlights.Forming,
                    FromStateId = StateMachineStateEnum.DirectoryFlights.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TransportState,
                    StateMachineActionId = StateMachineStateEnum.Transport.Formed,
                    FromStateId = StateMachineStateEnum.Transport.Forming
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TransportState,
                    StateMachineActionId = StateMachineStateEnum.Transport.Forming,
                    FromStateId = StateMachineStateEnum.Transport.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PartyState,
                    StateMachineActionId = StateMachineStateEnum.Party.Forming,
                    FromStateId = StateMachineStateEnum.Party.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    StateMachineActionId = StateMachineStateEnum.Bout.Editing,
                    FromStateId = StateMachineStateEnum.Bout.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitOrganizationState,
                    StateMachineActionId = StateMachineStateEnum.Limit.Organization.ConfirmedToApproved,
                    FromStateId = StateMachineStateEnum.Limit.Organization.Confirmed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    StateMachineActionId = StateMachineStateEnum.AddonService.Formed,
                    FromStateId = StateMachineStateEnum.AddonService.Forming
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    StateMachineActionId = StateMachineStateEnum.AddonService.Forming,
                    FromStateId = StateMachineStateEnum.AddonService.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TourState,
                    StateMachineActionId = StateMachineStateEnum.Tour.ToFormationFromFormed,
                    FromStateId = StateMachineStateEnum.Tour.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    StateMachineActionId = StateMachineStateEnum.Bout.Confirmed,
                    FromStateId = StateMachineStateEnum.Bout.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    StateMachineActionId = StateMachineStateEnum.Bout.Closed,
                    FromStateId = StateMachineStateEnum.Bout.Confirmed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    StateMachineActionId = StateMachineStateEnum.Bout.FromConfirmedToFormed,
                    FromStateId = StateMachineStateEnum.Bout.Confirmed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.BoutState,
                    StateMachineActionId = StateMachineStateEnum.Bout.FromClosedToConfirmed,
                    FromStateId = StateMachineStateEnum.Bout.Closed
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Delivered,
                    FromStateId = StateMachineStateEnum.CounselorTask.Completion
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Approved,
                    FromStateId = StateMachineStateEnum.CounselorTask.Delivered
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Solved,
                    FromStateId = StateMachineStateEnum.CounselorTask.Approved
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Completion,
                    FromStateId = StateMachineStateEnum.CounselorTask.Approved
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Completion,
                    FromStateId = StateMachineStateEnum.CounselorTask.Solved
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.RequestDecline,
                    FromStateId = StateMachineStateEnum.Counselor.Request
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.Request,
                    FromStateId = StateMachineStateEnum.Counselor.Editing
                }, new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.Approved,
                    FromStateId = StateMachineStateEnum.Counselor.Request
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.AdministratorTour,
                    StateMachineActionId = StateMachineStateEnum.AdministratorTour.Editing,
                    FromStateId = StateMachineStateEnum.AdministratorTour.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.AdministratorTour,
                    StateMachineActionId = StateMachineStateEnum.AdministratorTour.Formed,
                    FromStateId = StateMachineStateEnum.AdministratorTour.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    StateMachineActionId = StateMachineStateEnum.AddonService.Archive,
                    FromStateId = StateMachineStateEnum.AddonService.Forming
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.AddonServiceState,
                    StateMachineActionId = StateMachineStateEnum.AddonService.Archive,
                    FromStateId = StateMachineStateEnum.AddonService.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorState,
                    StateMachineActionId = StateMachineStateEnum.Counselor.ForRework,
                    FromStateId = StateMachineStateEnum.Counselor.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Readed,
                    FromStateId = StateMachineStateEnum.CounselorTask.Sended
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTask,
                    StateMachineActionId = StateMachineStateEnum.CounselorTask.Sended,
                    FromStateId = StateMachineStateEnum.CounselorTask.Readed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTest,
                    StateMachineActionId = StateMachineStateEnum.CounselorTest.Editing,
                    FromStateId = StateMachineStateEnum.CounselorTest.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CounselorTest,
                    StateMachineActionId = StateMachineStateEnum.CounselorTest.Formed,
                    FromStateId = StateMachineStateEnum.CounselorTest.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors,
                    StateMachineActionId = StateMachineStateEnum.TrainingCounselors.EducationFinished,
                    FromStateId = StateMachineStateEnum.TrainingCounselors.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors,
                    StateMachineActionId = StateMachineStateEnum.TrainingCounselors.Editing,
                    FromStateId = StateMachineStateEnum.TrainingCounselors.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TrainingCounselors,
                    StateMachineActionId = StateMachineStateEnum.TrainingCounselors.Formed,
                    FromStateId = StateMachineStateEnum.TrainingCounselors.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PedParty,
                    StateMachineActionId = StateMachineStateEnum.PedParty.Formed,
                    FromStateId = StateMachineStateEnum.PedParty.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PedParty,
                    StateMachineActionId = StateMachineStateEnum.PedParty.Editing,
                    FromStateId = StateMachineStateEnum.PedParty.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    StateMachineActionId = StateMachineStateEnum.LimitRequest.Editing,
                    FromStateId = StateMachineStateEnum.LimitRequest.Declined
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    StateMachineActionId = StateMachineStateEnum.LimitRequest.OnAprove,
                    FromStateId = StateMachineStateEnum.LimitRequest.Editing
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    StateMachineActionId = StateMachineStateEnum.LimitRequest.Approved,
                    FromStateId = StateMachineStateEnum.LimitRequest.OnAprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitRequest,
                    StateMachineActionId = StateMachineStateEnum.LimitRequest.Declined,
                    FromStateId = StateMachineStateEnum.LimitRequest.OnAprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CalculationState,
                    StateMachineActionId = StateMachineStateEnum.Calculation.Cancelled,
                    FromStateId = StateMachineStateEnum.Calculation.Unpaid
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.CalculationState,
                    StateMachineActionId = StateMachineStateEnum.Calculation.Paid,
                    FromStateId = StateMachineStateEnum.Calculation.Unpaid
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.Draft
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.CertificateIssued
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.OnWorking
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.ReadyToPay
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.OnApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.ReadyToPayFull
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.Payed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Reject,
                    FromStateId = StateMachineStateEnum.Request.Denial
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.OnWorking,
                    FromStateId = StateMachineStateEnum.Request.Draft
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.CertificateIssued
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.OnWorking
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.ReadyToPay
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.ReadyToPayFull
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.OnApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.Payed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.Denial
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.ReadyToPay,
                    FromStateId = StateMachineStateEnum.Request.OnWorking
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Payed,
                    FromStateId = StateMachineStateEnum.Request.ReadyToPay
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.OnApprove,
                    FromStateId = StateMachineStateEnum.Request.Payed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Denial,
                    FromStateId = StateMachineStateEnum.Request.OnApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.ReadyToPayFull,
                    FromStateId = StateMachineStateEnum.Request.OnApprove
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.CertificateIssued,
                    FromStateId = StateMachineStateEnum.Request.ReadyToPayFull
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.Request,
                    StateMachineActionId = StateMachineStateEnum.Request.Draft,
                    FromStateId = StateMachineStateEnum.Request.Reject
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.Edit,
                    FromStateId = StateMachineStateEnum.TradeUnion.Declined
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.Declined,
                    FromStateId = StateMachineStateEnum.TradeUnion.OnAproving
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.Approved,
                    FromStateId = StateMachineStateEnum.TradeUnion.OnAproving
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.OnAproving,
                    FromStateId = StateMachineStateEnum.TradeUnion.Edit
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.Finish,
                    FromStateId = StateMachineStateEnum.TradeUnion.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.EditForAll,
                    FromStateId = StateMachineStateEnum.TradeUnion.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.EditForAll,
                    FromStateId = StateMachineStateEnum.TradeUnion.OnAproving
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.TradeUnionList,
                    StateMachineActionId = StateMachineStateEnum.TradeUnion.EditForAll,
                    FromStateId = StateMachineStateEnum.TradeUnion.Finish
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.Formed,
                    FromStateId = StateMachineStateEnum.PupilGroup.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.OnAgreement,
                    FromStateId = StateMachineStateEnum.PupilGroup.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.Agreed,
                    FromStateId = StateMachineStateEnum.PupilGroup.OnAgreement
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.Approved,
                    FromStateId = StateMachineStateEnum.PupilGroup.Agreed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.EditForAll,
                    FromStateId = StateMachineStateEnum.PupilGroup.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.EditForAll,
                    FromStateId = StateMachineStateEnum.PupilGroup.OnAgreement
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.EditForAll,
                    FromStateId = StateMachineStateEnum.PupilGroup.Agreed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.PupilGroupList.Formed,
                    FromStateId = StateMachineStateEnum.PupilGroupList.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.PupilGroupList.Approved,
                    FromStateId = StateMachineStateEnum.PupilGroupList.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.PupilGroupList.EditForAll,
                    FromStateId = StateMachineStateEnum.PupilGroupList.Formed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.LimitListState,
                    StateMachineActionId = StateMachineStateEnum.PupilGroupList.EditForAll,
                    FromStateId = StateMachineStateEnum.PupilGroupList.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                    FromStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed,
                    FromStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.OnAgreement
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Approved,
                    FromStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.OnAgreement
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringMonitoringChildrenNumberInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.FinanceInformation.OnAgreement,
                    FromStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.FinanceInformation.Agreed,
                    FromStateId = StateMachineStateEnum.Monitoring.FinanceInformation.OnAgreement
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.FinanceInformation.Approved,
                    FromStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Agreed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.FinanceInformation.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.FinanceInformation.OnAgreement
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.FinanceInformation.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Agreed
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringFinanceInformation,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.FinanceInformation.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.FinanceInformation.Approved
                },
                //-----
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.OnApproving,
                    FromStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved,
                    FromStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.OnApproving
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.OnApproving
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long)StateMachineEnum.MonitoringSmallLeisureInfoData,
                    StateMachineActionId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.EditForAll,
                    FromStateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved
                },
                new StateMachineFromStatus
                {
                    Id = ++id,
                    StateMachineId = (long) StateMachineEnum.PupilGroup,
                    StateMachineActionId = StateMachineStateEnum.PupilGroup.EditForMGT,
                    FromStateId = StateMachineStateEnum.PupilGroup.Approved
                }
            );

            SetEidAndLastUpdateTicks(context.StateMachineFromStatus.ToList());
            context.SaveChanges();
        }
    }
}
