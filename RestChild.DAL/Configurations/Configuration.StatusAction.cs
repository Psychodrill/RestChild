using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Действия в статусах
        /// </summary>
        private static void StatusAction(Context context)
        {
            foreach (var s in context.RequestStatusCshedSendAndSignDocument.ToList())
            {
                context.RequestStatusCshedSendAndSignDocument.Remove(s);
            }

            foreach (var s in context.RequestStatusForMpgu.ToList())
            {
                context.RequestStatusForMpgu.Remove(s);
            }

            foreach (var c in context.RequestStatusChainForMpgu.ToList())
            {
                context.RequestStatusChainForMpgu.Remove(c);
            }

            var actions = context.StatusAction.ToList();
            foreach (var statusAction in actions)
            {
                context.StatusAction.Remove(statusAction);
            }

            context.SaveChanges();

            var statuses = context.Status.ToList();
            var statusCertificateIssued = context.Status.GetById((long) StatusEnum.CertificateIssued);

            var id = 1;

            context.StatusAction.AddOrUpdate(r => r.Code,

                #region Общие переходы

                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToSend,
                    Name = "Зарегистрировать заявление",
                    ToStatusId = (long) StatusEnum.Send,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Draft}.Contains(s.Id)).ToList()
                },
                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToRegistrationDecline,
                    Name = "Отказать в регистрации заявления",
                    ToStatusId = (long) StatusEnum.RegistrationDecline,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Draft}.Contains(s.Id)).ToList()
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.RegistrationDecline,
                    Code = AccessRightEnum.Status.ToRegistrationDeclineAttendant,
                    Name = "Отказать в регистрации заявления",
                    ToStatusId = (long) StatusEnum.RegistrationDecline,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Draft}.Contains(s.Id)).ToList()
                },
                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToCancelByApplicant,
                    Name = "Отозвать заявление",
                    ToStatusId = (long) StatusEnum.CancelByApplicant,
                    FromStatus =
                        statuses.Where(
                            s =>
                                !new[]
                                {
                                    (long) StatusEnum.RegistrationDecline, (long) StatusEnum.Denial,
                                    (long) StatusEnum.CancelByApplicant, (long) StatusEnum.Reject,
                                    (long) StatusEnum.Draft
                                }.Contains(s.Id)).ToList()
                },

                #endregion

                #region Стандартная форма

                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToOperatorCheck,
                    Name = "На проверку оператором",
                    ToStatusId = (long) StatusEnum.OperatorCheck,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Send}.Contains(s.Id)).ToList(),
                    IsFirstCompany = false
                },
                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToWaitApplicant,
                    Name = "Запросить визит заявителя",
                    ToStatusId = (long) StatusEnum.WaitApplicant,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.OperatorCheck}.Contains(s.Id)).ToList(),
                    IsFirstCompany = false
                },
                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToReject,
                    Name = "Отказать",
                    ToStatusId = (long) StatusEnum.Reject,
                    FromStatus =
                        statuses.Where(
                                s =>
                                    new[]
                                    {
                                        (long) StatusEnum.ApplicantCome, (long) StatusEnum.OperatorCheck,
                                        (long) StatusEnum.DecisionIsMade,
                                        (long) StatusEnum.DecisionMaking, (long) StatusEnum.DecisionMakingCovid,
                                        (long) StatusEnum.IncludedInList,
                                        (long) StatusEnum.Ranging
                                    }.Contains(s.Id))
                            .ToList(),
                    IsFirstCompany = false
                }, new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToRejectNormal,
                    Name = "Отказать",
                    ToStatusId = (long) StatusEnum.Reject,
                    FromStatus =
                        statuses.Where(s =>
                                new[] {(long) StatusEnum.WaitApplicant, (long) StatusEnum.WaitApplicantMoney}.Contains(
                                    s.Id))
                            .ToList(),
                    IsFirstCompany = false
                }, new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.CertificateIssued,
                    Name = "Включить в Реестр",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus =
                        statuses.Where(s =>
                                new[] {(long) StatusEnum.ApplicantCome, (long) StatusEnum.OperatorCheck}.Contains(s.Id))
                            .ToList(),
                    IsFirstCompany = false
                }, new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.EditInWaitApplicant,
                    Name = "Подтвердить прием документов",
                    ToStatusId = (long) StatusEnum.ApplicantCome,
                    FromStatus =
                        statuses.Where(s =>
                                new[] {(long) StatusEnum.WaitApplicant, (long) StatusEnum.OperatorCheck}.Contains(s.Id))
                            .ToList(),
                    IsFirstCompany = false
                },
                new StatusAction
                {
                    Id = id++,
                    Code = AccessRightEnum.Status.ToRejectFromCertificateIssued,
                    Name = "Отказать",
                    ToStatusId = (long) StatusEnum.Reject,
                    FromStatus = new List<Status> {statusCertificateIssued},
                    IsFirstCompany = null
                },
                new StatusAction
                {
                    Id = id,
                    Code = AccessRightEnum.RetryRequestInBaseRegistry,
                    Name = "Отправить запрос в Базовый регистр",
                    ToStatusId = (long) StatusEnum.Send,
                    FromStatus =
                        statuses.Where(
                            s =>
                                new[]
                                    {
                                        (long) StatusEnum.OperatorCheck, (long) StatusEnum.ApplicantCome,
                                        (long) StatusEnum.WaitApplicant
                                    }
                                    .Contains(s.Id)).ToList(),
                    IsFirstCompany = false
                },

                #endregion

                #region Многоэтапная заявочная кампания

                new StatusAction
                {
                    Id = (long) StatusEnum.Ranging,
                    Code = AccessRightEnum.Status.FcToRanging,
                    Name = "В ранжирование",
                    ToStatusId = (long) StatusEnum.Ranging,
                    FromStatus = statuses
                        .Where(s => new[] {(long) StatusEnum.Send, (long) StatusEnum.ApplicantCome}.Contains(s.Id))
                        .ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.IncludedInList,
                    Code = AccessRightEnum.Status.FcToIncludedInList,
                    Name = "Включить в список",
                    ToStatusId = (long) StatusEnum.IncludedInList,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Ranging}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true,
                    RequestOnMoney = false
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.IncludedInList + 10000,
                    Code = AccessRightEnum.Status.FcToIncludedInList,
                    Name = "Включить в список",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Ranging}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true,
                    RequestOnMoney = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.DecisionMaking,
                    Code = AccessRightEnum.Status.FcToDecisionMaking,
                    Name = "Открыть варианты для выбора",
                    ToStatusId = (long) StatusEnum.DecisionMaking,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.IncludedInList}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.DecisionMakingCovid,
                    Code = AccessRightEnum.Status.FcToDecisionMakingCovid,
                    Name = "Открыть варианты для выбора (Дополнительная кампания)",
                    ToStatusId = (long) StatusEnum.DecisionMakingCovid,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.CertificateIssued}.Contains(s.Id))
                        .ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.DecisionIsMade,
                    Code = AccessRightEnum.Status.FcToDecisionIsMade,
                    Name = "Выбрать вариант",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus = statuses.Where(s =>
                            new[] {(long) StatusEnum.DecisionMaking, (long) StatusEnum.DecisionMakingCovid}
                                .Contains(s.Id))
                        .ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.WaitApplicant,
                    Code = AccessRightEnum.Status.FcToWaitApplicant,
                    Name = "Запросить визит заявителя",
                    ToStatusId = (long) StatusEnum.WaitApplicant,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Send}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.ApplicantCome,
                    Code = AccessRightEnum.Status.ApplicantCome,
                    Name = "Подтвердить прием документов",
                    ToStatusId = (long) StatusEnum.ApplicantCome,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.WaitApplicant}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.CertificateIssued,
                    Code = AccessRightEnum.Status.FcToCertificateIssued,
                    Name = "Услуга оказана",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus =
                        statuses.Where(s =>
                                new[] {(long) StatusEnum.DecisionIsMade}.Contains(s.Id))
                            .ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.CertificateIssued + 100000,
                    Code = AccessRightEnum.Status.FcToMoneyAccountAccepted,
                    Name = "Реквизиты подтверждены",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus =
                        statuses.Where(s => new[] {(long) StatusEnum.WaitApplicantMoney}.Contains(s.Id))
                            .ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.WaitApplicantMoney,
                    Code = AccessRightEnum.Status.FcToWaitApplicantMoney,
                    Name = "Запросить визит заявителя",
                    ToStatusId = (long) StatusEnum.WaitApplicantMoney,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.CertificateIssued}.Contains(s.Id))
                        .ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.Denial,
                    Code = AccessRightEnum.Status.FcToReject,
                    Name = "Отказать",
                    ToStatusId = (long) StatusEnum.Reject,
                    FromStatus =
                        statuses.Where(
                            s =>
                                !new[]
                                {
                                    (long) StatusEnum.Draft, (long) StatusEnum.Reject,
                                    (long) StatusEnum.RegistrationDecline,
                                    (long) StatusEnum.CancelByApplicant, (long) StatusEnum.CertificateIssued
                                }.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.CancelByApplicant,
                    Code = AccessRightEnum.Status.FcToCancelByRequest,
                    Name = "Отозвать",
                    ToStatusId = (long) StatusEnum.CancelByApplicant,
                    FromStatus =
                        statuses.Where(
                            s =>
                                !new[]
                                {
                                    (long) StatusEnum.Draft, (long) StatusEnum.Reject,
                                    (long) StatusEnum.RegistrationDecline,
                                    (long) StatusEnum.CancelByApplicant, (long) StatusEnum.CertificateIssued
                                }.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.CertificateIssued + 200000,
                    Code = AccessRightEnum.Status.FcFinishWorkWithRequest + "123",
                    Name = "Завершить обработку заявления",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus =
                        statuses.Where(
                            s =>
                                new[] {(long) StatusEnum.CertificateIssued}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.CertificateIssued + 300000,
                    Code = AccessRightEnum.Status.FcNotComeOnMoney,
                    Name = "Уведомление заявителю по уточнению выплаты",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus =
                        statuses.Where(
                            s =>
                                new[] {(long) StatusEnum.WaitApplicantMoney}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },
                new StatusAction
                {
                    Id = (long) StatusEnum.DecisionMaking + 200000,
                    Code = AccessRightEnum.Status.FcRepareRequest,
                    Name = "Восстановить заявление по решению комиссии",
                    ToStatusId = (long) StatusEnum.DecisionMaking,
                    FromStatus =
                        statuses.Where(
                            s =>
                                new[] {(long) StatusEnum.Reject, (long) StatusEnum.CertificateIssued}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true
                },

                #endregion

                #region Странное

                new StatusAction
                {
                    Id = (long) StatusEnum.CertificateIssued + 202020000,
                    Code = AccessRightEnum.RequestTo10753,
                    Name = "Отказать в связи с неучастием",
                    ToStatusId = (long) StatusEnum.CertificateIssued,
                    FromStatus = statuses.Where(s => new[] {(long) StatusEnum.DecisionMaking}.Contains(s.Id)).ToList(),
                    IsFirstCompany = true,
                    RequestOnMoney = false
                },

                #endregion

                #region ЛОК 2022

                new StatusAction
                    {
                        Id = (long) StatusEnum.RegistrationDecline + 202200000,
                        Code = AccessRightEnum.Status.ToRegistrationDeclineChildDiffSSOId,
                        Name = "Отказать в регистрации заявления",
                        ToStatusId = (long) StatusEnum.RegistrationDecline,
                        FromStatus = statuses.Where(s => new[] {(long) StatusEnum.Draft}.Contains(s.Id)).ToList(),
                        IsFirstCompany = true,
                        RequestOnMoney = false
                    }

                    #endregion

            );

            SetEidAndLastUpdateTicks(context.StatusAction.ToList());
            context.SaveChanges();
        }
    }
}
