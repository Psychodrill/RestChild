using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Проверка законного представительства внутри АИС ДО
    /// </summary>
    [Task]
    public class AisoLegalRepresentationCheckTask : BaseTask
    {
        /// <summary>
        ///     Сбросить значения для проверки
        /// </summary>
        private void ResetCheckChildInBaseRegistry(UnitOfWork uw, long childId, ExchangeBaseRegistryTypeEnum type)
        {
            var exchangeBaseRegistries =
                uw.GetSet<RestChild.Domain.ExchangeBaseRegistry>()
                    .Where(e => !e.NotActual && e.ChildId == childId &&
                                (e.ExchangeBaseRegistryTypeId == (long) type ||
                                 !e.ExchangeBaseRegistryTypeId.HasValue))
                    .ToList();
            foreach (var exchangeBaseRegistry in exchangeBaseRegistries)
            {
                exchangeBaseRegistry.NotActual = true;
            }

            uw.SaveChanges();
        }

        /// <summary>
        ///     Проверка законного представительства внутри АИС ДО
        /// </summary>
        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("AisoLegalRepresentationCheckTask started");

                var exec = unitOfWork.GetSet<ExchangeBaseRegistryType>().Any(ss =>
                    ss.Id == (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck && ss.SendMessage);


                if (!exec)
                {
                    Logger.Info("AisoLegalRepresentationCheckTask disabled");
                    Logger.Info("AisoLegalRepresentationCheckTask finished");
                    return;
                }

                var requests = unitOfWork.GetSet<Request>().Where(r =>
                        !r.NeedSendForCPMPK && !r.IsDeleted && !r.NeedSendForBenefit && !r.NeedSendToRelative &&
                        !r.NeedSendForSnils && !r.NeedSendForPassport && !r.NeedSendForRegistrationByPassport &&
                        r.NeedSendForAisoLegalRepresentation).Include(r => r.Child)
                    .Take(1000).ToList();

                var benefitTypes = new long[] {8, 9, 7, 41, 4, 5, 6, 47, 30, 36, 37, 3, 49, 50, 44};

                foreach (var request in requests)
                {
                    var checkAttendants = new List<Applicant> {request.Applicant};

                    checkAttendants.AddRange(request.Attendant.Where(a =>
                        a.ApplicantTypeId != (long) ApplicantTypeEnum.Confidant && a.ApplicantTypeId.HasValue &&
                        a.IsAccomp));

                    if (request.Child.Any(ss =>
                        benefitTypes.Any(sx => sx == ss.BenefitType?.Id) ||
                        benefitTypes.Any(sx => sx == ss.BenefitType?.SameBenefitId)))
                    {
                        Logger.Info($"AisoLegalRepresentationCheckTask request.id={request.Id} started");
                        var yid = new List<long>();
                        var y = request.YearOfRest;
                        for (var i = 1; i <= 3; i++)
                        {
                            yid.Add(unitOfWork.GetSet<YearOfRest>().First(ss => ss.Year == y.Year - i).Id);
                        }

                        Logger.Info(
                            $"AisoLegalRepresentationCheckTask request.id={request.Id} years={string.Join(",", yid)} info");


                        foreach (var child in request.Child.Where(ss =>
                            benefitTypes.Any(sx => sx == ss.BenefitType?.Id) ||
                            benefitTypes.Any(sx => sx == ss.BenefitType?.SameBenefitId)).ToList())
                        {
                            ResetCheckChildInBaseRegistry(unitOfWork, child.Id,
                                ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck);
                            foreach (var applicant in checkAttendants)
                            {
                                Logger.Info(
                                    $"AisoLegalRepresentationCheckTask request.id={request.Id} child.Id={child.Id} applicant.Id={applicant.Id} started...");

                                var r = unitOfWork.GetSet<Request>().FirstOrDefault(ss =>
                                    ss.Id != request.Id &&
                                    yid.Any(sx => sx == ss.YearOfRestId) &&
                                    ss.StatusId == (long) StatusEnum.CertificateIssued &&
                                    ss.Applicant.Snils == applicant.Snils &&
                                    ss.Child.Any(sx => sx.Snils == child.Snils)
                                );

                                Applicant attendant = null;

                                if (r == null)
                                {
                                    attendant = unitOfWork.GetSet<Applicant>().FirstOrDefault(ss =>
                                        ss.RequestId != request.Id &&
                                        yid.Any(sx => sx == ss.Request.YearOfRestId) &&
                                        ss.Request.StatusId == (long) StatusEnum.CertificateIssued &&
                                        ss.Snils == applicant.Snils &&
                                        ss.Request.Child.Any(sx => sx.Snils == child.Snils)
                                    );

                                    r = attendant?.Request;
                                }
                                else
                                {
                                    attendant = r.Applicant;
                                }

                                if (r != null && attendant != null)
                                {
                                    unitOfWork.AddEntity(new RestChild.Domain.ExchangeBaseRegistry
                                    {
                                        RequestGuid = Guid.NewGuid().ToString(),
                                        ChildId = child.Id,
                                        ApplicantId = applicant.Id,
                                        RequestText = string.Empty,
                                        ResponseText =
                                            $"Заявление: {r.RequestNumber}</br>Законный представитель: {applicant.LastName} {applicant.FirstName} {applicant.MiddleName} - {applicant.DateOfBirth:dd.MM.yyyy} для ребёнка {child.LastName} {child.FirstName} {child.MiddleName} - {child.DateOfBirth:dd.MM.yyyy}",
                                        SendDate = DateTime.Now,
                                        ResponseDate = DateTime.Now,
                                        IsProcessed = false,
                                        IsIncoming = false,
                                        OperationType = "AisoLegalRepresentationCheckRequest",
                                        Success = true,
                                        ExchangeBaseRegistryTypeId =
                                            (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck,
                                        ServiceNumber = "б/н",
                                        ResponseGuid = "б/н"
                                    });
                                }
                                else
                                {
                                    unitOfWork.AddEntity(new RestChild.Domain.ExchangeBaseRegistry
                                    {
                                        RequestGuid = Guid.NewGuid().ToString(),
                                        ChildId = child.Id,
                                        ApplicantId = applicant.Id,
                                        RequestText = string.Empty,
                                        ResponseText =
                                            $"Запрашиваемые сведения для </br>законного представителя: {applicant.LastName} {applicant.FirstName} {applicant.MiddleName} - {applicant.DateOfBirth:dd.MM.yyyy} и ребёнка {child.LastName} {child.FirstName} {child.MiddleName} - {child.DateOfBirth:dd.MM.yyyy} не подтверждены",
                                        SendDate = DateTime.Now,
                                        ResponseDate = DateTime.Now,
                                        IsProcessed = false,
                                        IsIncoming = false,
                                        OperationType = "AisoLegalRepresentationCheckRequest",
                                        Success = false,
                                        ExchangeBaseRegistryTypeId =
                                            (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck,
                                        ServiceNumber = "б/н",
                                        ResponseGuid = "б/н"
                                    });
                                }

                                Logger.Info(
                                    $"AisoLegalRepresentationCheckTask request.id={request.Id} child.Id={child.Id} applicant.Id={applicant.Id} finish.");
                            }
                        }
                    }

                    request.NeedSendForAisoLegalRepresentation = false;
                    unitOfWork.SaveChanges();
                }
            }

            Logger.Info("AisoLegalRepresentationCheckTask finished");
        }
    }
}
