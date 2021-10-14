using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Цепочки статусов для МПГУ
        /// </summary>
        private static void RequestStatusChainForMpgu(Context context)
        {
            var id = 0;
            var chainId = 0;
            var eventIds = 1;

            var year2020 = context.YearOfRest.Where(ss => ss.Year == 2020).Select(ss => (long?) ss.Id).FirstOrDefault();

            #region 1050 зарегистрировано

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Send,
                StatusId = (long) StatusEnum.Send,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Send,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false,
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Send,
                Status = 1050,
                Name = "заявление зарегистрировано",
                Commentary =
                    "Ваше заявление зарегистрировано.\n\rУведомление о результате рассмотрения заявления будет направлено в ваш Личный кабинет на Портале в период с 17 января по 22 февраля 2022 г.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Send,
                Status = 10090,
                Name = "Доступен отзыв заявления",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 1035.1 отказ в регистрации (ЛОК 2022)

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.RegistrationDecline,
                StatusId = (long) StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202107
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.RegistrationDecline,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.RegistrationDecline,
                Status = 1035,
                ReasonCode = "1",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r На указанных в заявлении детей уже подано заявление о предоставлении услуг отдыха и оздоровления.",
                    //"Заявление является повторным. \n\r На следующих лиц уже подано заявление о предоставлении услуг отдыха и оздоровления:",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion

            #region 1035 отказ в регистрации сопровождение

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.RegistrationDecline + 1000000,
                StatusActionId = (long) StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202101
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.RegistrationDecline + 1000000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.RegistrationDecline + 1000000,
                Status = 1035,
                ReasonCode = null,
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r Вы уже подали заявление о предоставлении услуг отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion            

            #region 1169

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "Отказ в отзыве заявления",
                EventCode = RequestEventEnum.DeclineCancel
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 1169,
                Name = "Возобновление. Отказ в отзыве заявления",
                Commentary =
                    "Отзыв заявления невозможен в связи с окончанием стадии приёма заявлений ГАУК «МОСГОРТУР».",
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region 1052

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "На исполнении",
                EventCode = RequestEventEnum.ResultMaking
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 1052,
                Name = "На исполнении",
                Commentary = null,
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region 7704.1

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "7704.1 Получение документов из Базового Регистра (проверка льготы и получения ежемесячного пособия на ребёнка в ДТСЗН).",
                EventCode = RequestEventEnum.SendRequestInBenefit
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "1",
                Name = "Сбор сведений",
                Commentary =
                    "Получение документов из Базового Регистра (проверка льготы и получения ежемесячного пособия на ребёнка в ДТСЗН).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.1

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.1 Документы из Базового Регистра получены (проверка льготы в ДТСЗН).",
                EventCode = RequestEventEnum.GetResponseInBenefit
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "1",
                Name = "Сведения получены",
                Commentary = "Документы из Базового Регистра получены (проверка льготы в ДТСЗН).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7704.2

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7704.2 Получение документов из Базового Регистра (проверка родства в УЗАГС, ФЗАГС).",
                EventCode = RequestEventEnum.SendRequestForRelatives
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "2",
                Name = "Сбор сведений",
                Commentary = "Получение документов из Базового Регистра (проверка родства в УЗАГС, ФЗАГС).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.2

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.2 Документы из Базового Регистра получены (проверка родства в ЗАГС).",
                EventCode = RequestEventEnum.GetResponseForRelatives
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "2",
                Name = "Сведения получены",
                Commentary = "Документы из Базового Регистра получены (проверка родства в УЗАГС, ФЗАГС).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7704.3

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7704.3 Получение документов из Базового Регистра Пенсионного фонда РФ (проверка СНИЛС).",
                EventCode = RequestEventEnum.SendRequestForSnils
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "3",
                Name = "Сбор сведений",
                Commentary = "Получение документов из Базового Регистра Пенсионного фонда РФ (проверка СНИЛС).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.3

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.3 Документы из Базового Регистра Пенсионного фонда РФ получены (проверка СНИЛС).",
                EventCode = RequestEventEnum.GetResponseForRelatives
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "3",
                Name = "Сведения получены",
                Commentary = "Документы из Базового Регистра Пенсионного фонда РФ получены (проверка СНИЛС).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7704.4

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7704.4 Получение документов из АИС «ЦПМПК» (проверка льготы в ЦПМПК)",
                EventCode = RequestEventEnum.SendRequestInCPMPK
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "4",
                Name = "Сбор сведений",
                Commentary = "Получение документов из АИС «ЦПМПК» (проверка льготы в ЦПМПК).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.4

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.4 Документы из Базового Регистра получены в АИС «ЦПМПК» (проверка льготы в ЦПМПК).",
                EventCode = RequestEventEnum.GetResponseForCPMPK
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "4",
                Name = "Сведения получены",
                Commentary = "Документы из Базового Регистра получены в АИС «ЦПМПК» (проверка льготы в ЦПМПК).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7704.5

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "7704.5 Получение документов из Базового Регистра (проверка законного представительства ребёнка льготной категории дети - сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе приемной или патронатной семье в ДТСЗН).",
                EventCode = RequestEventEnum.SendRequestInBRDTSZN
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "5",
                Name = "Сбор сведений",
                Commentary =
                    "Получение документов из Базового Регистра (проверка законного представительства ребёнка льготной категории дети - сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе приемной или патронатной семье в ДТСЗН).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.5

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.5 Документы из Базового Регистра получены (проверка законного представительства в ДТСЗН).",
                EventCode = RequestEventEnum.GetResponseForBRDTSZN
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "5",
                Name = "Сведения получены",
                Commentary = "Документы из Базового Регистра получены (проверка законного представительства в ДТСЗН).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7704.6

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7704.6 Получение документов из Базового Регистра АС УР (проверка паспорта в МВД).",
                EventCode = RequestEventEnum.SendRequestInBRASUR
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "6",
                Name = "Сбор сведений",
                Commentary = "Получение документов из Базового Регистра АС УР (проверка паспорта в МВД).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.6

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.6 Документы из Базового Регистра АС УР получены (проверка паспорта в ДИТ).",
                EventCode = RequestEventEnum.GetResponseForBRASUR
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "6",
                Name = "Сведения получены",
                Commentary = "Документы из Базового Регистра АС УР получены (проверка паспорта в МВД).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7704.7 Сбор сведений

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "7704.7 Сбор сведений",
                EventCode = RequestEventEnum.SendRequestForRegistrationByPassport
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = "7",
                Name = "Сбор сведений",
                Commentary =
                    "Получение документов из Базового Регистра АС УР (проверка адреса регистрации в МВД детей всех льготных категорий и лиц из числа детей-сирот и детей, оставшихся без попечения родителей).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 7705.7 Сведения получены

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705.7 Сведения получены",
                EventCode = RequestEventEnum.GetResponseFoRegistrationByPassport
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = "7",
                Name = "Сбор сведений",
                Commentary =
                    "Документы из Базового Регистра АС УР получены (проверка адреса регистрации в МВД детей всех льготных категорий и лиц из числа детей-сирот и детей, оставшихся без попечения родителей).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region 1055 ранжирование

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Ranging,
                StatusId = (long) StatusEnum.Ranging,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Ranging,
                    Status = 10190,
                    Name = "Отзыв заявления недоступен",
                    Commentary = string.Empty,
                    OrderField = id,
                    SendEmail = false
                }, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Ranging,
                    Status = 1055,
                    Name = "Проведение Комиссии",
                    Commentary = "Рассмотрение всех заявлений и принятие решения.",
                    OrderField = id,
                    SendEmail = false
                });

            #endregion

            #region 1054 Включено в список (1075)

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.IncludedInList,
                StatusId = (long) StatusEnum.IncludedInList,
                RequestOnMoney = false,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.IncludedInList,
                Status = 1056,
                ReasonCode = "2",
                Name = "результат рассмотрения заявления",
                Commentary = "Ваше заявление рассмотрено. Вы допущены к участию во втором этапе заявочной кампании.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationOfNeedToChoose
                });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.IncludedInList + 10000,
                RequestOnMoney = true,
                StatusActionId = (long) StatusEnum.IncludedInList + 10000,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.IncludedInList + 10000,
                Status = 1056,
                ReasonCode = "1",
                Name = "результат рассмотрения заявления",
                Commentary = "Ваше заявление рассмотрено.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.IncludedInList + 10000,
                Status = 1052,
                Name = "Формирование результата",
                Commentary = null,
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.IncludedInList + 10000,
                Status = 1075,
                ReasonCode = "2",
                Name = "Услуга оказана. Решение положительное",
                Commentary =
                    "Направляем сертификат на отдых и оздоровление.\n\rПриложенный документ носит информационный характер.",
                OrderField = id,
                SendEmail = true,
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            #endregion

            #region Начало 2 этапа

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.DecisionMaking,
                StatusId = (long) StatusEnum.DecisionMaking,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionMaking,
                Status = 1048,
                Name = "Выбор конкретной организации отдыха и оздоровления",
                Commentary =
                    "Направляем предложения по выбору конкретной организации отдыха и оздоровления по заявленным направлениям.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionMaking,
                Status = 8021,
                Name = "Необходимо предоставить недостающие сведения",
                Commentary = "Доступен выбор организации отдыха и оздоровления.",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region Начало дополнительной кампании 2020

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.DecisionMakingCovid,
                StatusId = (long) StatusEnum.DecisionMakingCovid,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionMakingCovid,
                Status = 1048,
                ReasonCode = "",
                Name = "Выбор конкретной организации отдыха и оздоровления",
                Commentary =
                    "Оказание услуги возобновлено. Направляем предложения по выбору конкретной организации отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionMakingCovid,
                Status = 8021,
                ReasonCode = "",
                Name = "Необходимо предоставить недостающие сведения",
                Commentary = "Доступен выбор организации отдыха и оздоровления.",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region Решение принято

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.DecisionIsMade,
                StatusActionId = (long) StatusEnum.DecisionIsMade,
                RequestOnMoney = true,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionIsMade,
                Status = 1148,
                ReasonCode = "1",
                Name = "Сведения получены ведомством",
                Commentary = "Вы выбрали:",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionIsMade,
                Status = 1052,
                Name = "Формирование результата.",
                Commentary = null,
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.DecisionIsMade,
                Status = 1075,
                ReasonCode = "2",
                Name = "Услуга оказана. Решение положительное",
                Commentary =
                    "Направляем сертификат на отдых и оздоровление. \n\rПриложенный документ носит информационный характер.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.DecisionIsMade + 10000,
                StatusActionId = (long) StatusEnum.DecisionIsMade,
                RequestOnMoney = false,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.DecisionIsMade + 10000,
                    Status = 8031,
                    ReasonCode = "2",
                    Name = "Сведения предоставлены Заявителем при личном визите",
                    Commentary = "Выбор организации отдыха и оздоровления недоступен.",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.DecisionIsMade + 10000,
                    Status = 1148,
                    ReasonCode = "1",
                    Name = "Сведения получены ведомством",
                    Commentary = "Вы выбрали:",
                    OrderField = id,
                    SendEmail = true
                }
                , new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.DecisionIsMade + 10000,
                    Status = 1052,
                    Name = "Формирование результата.",
                    Commentary = null,
                    OrderField = id,
                    SendEmail = true
                }, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.DecisionIsMade + 10000,
                    Status = 1075,
                    ReasonCode = "1",
                    Name = "Услуга оказана. Решение положительное",
                    Commentary =
                        "Направляем путевку для отдыха и оздоровления. Приложенный документ носит информационный характер (путевка). Вы можете самостоятельно распечатать приложенный документ.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            #endregion

            #region Услуга оказана 1

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued,
                StatusActionId = (long) StatusEnum.CertificateIssued,
                StatusId = (long) StatusEnum.CertificateIssued,
                RequestOnMoney = false,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued,
                    Status = 1052,
                    Name = "На исполнении",
                    Commentary = null,
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued,
                    Status = 1075,
                    ReasonCode = "1",
                    Name = "Услуга оказана",
                    Commentary =
                        "Направляем путевку для отдыха и оздоровления. Приложенный документ носит информационный характер (путевка). Вы можете самостоятельно распечатать приложенный документ.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            #endregion

            #region Услуга оказана 2

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued + 1000000,
                StatusId = (long) StatusEnum.CertificateIssued,
                StatusActionId = (long) StatusEnum.CertificateIssued,
                RequestOnMoney = true,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 1000000,
                    Status = 1052,
                    Name = "На исполнении",
                    Commentary = null,
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 1000000,
                    Status = 1075,
                    ReasonCode = "2",
                    Name = "Услуга оказана",
                    Commentary =
                        "Направляем сертификат на отдых и оздоровление. \n\rПриложенный документ носит информационный характер.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            #endregion

            #region 7700

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued + 3000000,
                StatusActionId = (long) StatusEnum.CertificateIssued + 200000,
                StatusId = (long) StatusEnum.CertificateIssued,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.CertificateIssued + 3000000,
                Status = 7700,
                Name = "Результат выдан заявителю",
                Commentary = "",
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region Вызов для подтверждения родства

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.WaitApplicant,
                StatusId = (long) StatusEnum.WaitApplicant,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.WaitApplicant,
                Status = 1060,
                Name = "Ожидание прихода заявителя",
                Commentary =
                    @"Для подтверждения сведений, указанных в заявлении, вам необходимо явиться в течение 10 рабочих дней с момента направления этого уведомления по адресу: г. Москва, Малый Харитоньевский переулок д. 6 стр. 3, с 8:00 до 20:00 ежедневно (кроме государственных праздников). Предварительно необходимо <a href='https://www.mos.ru/pgu/ru/services/link/3109/?onsite_from=67532'>записаться на приём</a>.
С перечнем документов, которые необходимо иметь при себе, можно ознакомиться во вложении.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationWaitApplicant
                });

            #endregion

            #region Формирование результата оказания услуги

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.ApplicantCome,
                StatusId = (long) StatusEnum.ApplicantCome,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.ApplicantCome,
                Status = 1160,
                Name = "Возобновлено",
                Commentary = "Документы предоставлены в ГАУК «МОСГОРТУР».",
                OrderField = id,
                SendEmail = true,
                ReasonCode = "1"
            });

            #endregion

            #region Отозвано 7739 (уважительная причина)

            var res77091U = (long) StatusEnum.CancelByApplicant;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = res77091U,
                StatusActionId = 3,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77091U,
                    Status = 7709,
                    Name = "Подан запрос на отказ от результата услуги",
                    Commentary =
                        @"Отказ от осуществления отдыха, путем подачи письменного заявления в офисе ГАУК «МОСГОРТУР».",
                    OrderField = id,
                    SendEmail = true,
                    ReasonCode = "1"
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77091U,
                    Status = 7739,
                    Name = "Отказ от результата признан уважительным",
                    Commentary = "Отказ от осуществления отдыха одобрен. Причина отказа является уважительной.",
                    OrderField = id,
                    ReasonCode = "1",
                    SendEmail = true
                });

            //ветка с сертификатами
            var res77092U = (long) StatusEnum.CancelByApplicant + 2020040000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = res77092U,
                StatusActionId = 3,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77092U,
                    Status = 7709,
                    Name = "Подан запрос на отказ от результата услуги",
                    Commentary =
                        @"Отказ от использования сертификата на отдых и оздоровление, путем подачи письменного заявления в офисе ГАУК «МОСГОРТУР».",
                    OrderField = id,
                    SendEmail = true,
                    ReasonCode = "2"
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77092U,
                    Status = 7739,
                    Name = "Отказ от результата признан уважительным",
                    Commentary =
                        "Отказ от сертификата на отдых и оздоровление одобрен. Причина отказа является уважительной.",
                    OrderField = id,
                    ReasonCode = "2",
                    SendEmail = true
                });

            #endregion

            #region Отозвано (не уважительная причина)

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "Отказ в отзыве заявления",
                EventCode = RequestEventEnum.RequestDeclineNotApproved
            });

            var res77091Nu = (long) StatusEnum.CancelByApplicant + 20000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = res77091Nu,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77091Nu,
                    Status = 7709,
                    Name = "Подан запрос на отказ от результата услуги",
                    Commentary =
                        @"Отказ от осуществления отдыха, путем подачи письменного заявления в офисе ГАУК «МОСГОРТУР».",
                    OrderField = id,
                    ReasonCode = "1",
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77091Nu,
                    Status = 7749,
                    Name = "Отказ от результата признан неуважительным",
                    Commentary = "Отказ от осуществления отдыха не одобрен. Причина отказа не является уважительной.",
                    OrderField = id,
                    ReasonCode = "1",
                    SendEmail = true
                });

            //ветка с сертификатами
            var res77092Nu = (long) StatusEnum.CancelByApplicant + 2020050000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = res77092Nu,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77092Nu,
                    Status = 7709,
                    Name = "Подан запрос на отказ от результата услуги",
                    Commentary =
                        "Отказ от использования сертификата на отдых и оздоровление, путем подачи письменного заявления в офисе ГАУК «МОСГОРТУР».",
                    OrderField = id,
                    ReasonCode = "2",
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = res77092Nu,
                    Status = 7749,
                    Name = "Отказ от результата признан неуважительным",
                    Commentary =
                        "Отказ от сертификата на отдых и оздоровление не одобрен. Причина отказа не является уважительной.",
                    OrderField = id,
                    ReasonCode = "2",
                    SendEmail = true
                });

            #endregion

            #region Отозвано

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CancelByApplicant + 10000,
                StatusActionId = (long) StatusEnum.CancelByApplicant,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CancelByApplicant + 10000,
                    Status = 1090,
                    Name = "Отозвано по инициативе заявителя",
                    Commentary =
                        "Заявление отозвано по вашему обращению. Если вы не делали этого, обратитесь в Службу поддержки Портала: +7 (495) 539-55-55.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Вызов для подтверждения данных для платежа

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.WaitApplicantMoney,
                IsFirstCompany = true,
                StatusId = null //(long) StatusEnum.WaitApplicantMoney
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.WaitApplicantMoney,
                Status = 1046,
                Name = "Ожидание прихода заявителя",
                Commentary =
                    @"Для подтверждения сведений, указанных в заявлении, Вам необходимо явиться в течение 10 рабочих дней с момента получения этого уведомления по адресу: г. Москва, Малый Харитоньевский переулок д. 6 стр. 3 с 8:00 до 20:00 ежедневно (кроме государственных праздников). Предварительно необходимо  <a href=""https://www.mos.ru/pgu/ru/services/link/2242/"">записаться на приём</a>.
При себе необходимо иметь:
- Документ, удостоверяющий личность ребёнка;
- Документ, удостоверяющий личность заявителя;
- Банковские реквизиты;
- Документ, подтверждающий полномочия на подачу заявления (для заявителя по доверенности).",
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region Реквизиты счета подтверждены

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued + 2000000,
                IsFirstCompany = true,
                StatusId = null, //(long) StatusEnum.CertificateIssued,
                StatusActionId = null, //(long) StatusEnum.CertificateIssued + 100000,
                RequestOnMoney = null //true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.CertificateIssued + 2000000,
                Status = 1146,
                Name = "Заявитель явился в ОИВ",
                Commentary =
                    "Все необходимые сведения получены.",
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region Реквизиты счета не подтверждены (не пришёл "Уведомление заявителю по уточнению выплаты")

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued + 4000000,
                IsFirstCompany = true,
                StatusId = null, //(long) StatusEnum.CertificateIssued,
                StatusActionId = null, //(long) StatusEnum.CertificateIssued + 300000,
                RequestOnMoney = null //true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.CertificateIssued + 4000000,
                Status = 1246,
                Name = "Заявитель не явился в ОИВ",
                Commentary =
                    "Вы не явились в указанный срок, вместе с тем право на получение выплаты сохраняется до окончания календарного года.",
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region Отказ 1080.1

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 10000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 202001
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 10000,
                    Status = 1160,
                    Name = "Возобновлено",
                    Commentary = "Документы не предоставлены в ГАУК «МОСГОРТУР» в установленный срок.",
                    OrderField = id,
                    SendEmail = true,
                    ReasonCode = "2"
                }, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 10000,
                    Status = 10190,
                    Name = "Отзыв заявления недоступен",
                    Commentary = string.Empty,
                    OrderField = id,
                    SendEmail = false
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 10000,

                    Status = 1080,
                    ReasonCode = "1",
                    Name = "Отказ в предоставление услуги",
                    Commentary =
                        "Непредставление в течение 10 рабочих дней со дня направления соответствующего уведомления оригиналов документов, подтверждающих сведения, указанные в заявлении.",
                    OrderField = id,
                    SendEmail = true
                }
            );

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.2

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 20000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201904
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 20000,
                    Status = 10190,
                    Name = "Отзыв заявления недоступен",
                    Commentary = string.Empty,
                    OrderField = id,
                    SendEmail = false
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 20000,
                    Status = 1080,
                    ReasonCode = "2",
                    Name = "Отказ в предоставление услуги",
                    Commentary =
                        "Представление документов, несоответствующих требованиям, установленным правовыми актами Российской Федерации, правовыми актами города Москвы, противоречивых или недостоверных сведений, либо утрата силы предоставленных документов в случае, если в документах указан срок их действия или срок их действия установлен законодательством.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.3 (старый, более не используется)

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 30000,
                IsFirstCompany = false,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 8
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 30000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 30000,

                Status = 1080,
                ReasonCode = "3",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "Отсутствие места жительства ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве.",
                OrderField = id,
                SendEmail = true
            });

            #endregion

            #region Отказ 1080.13

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 40000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201911
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 40000,
                    Status = 8031,
                    ReasonCode = "1",
                    Name = "Предоставление недостающих сведений недоступно. Истек срок предоставления.",
                    Commentary = "Выбор организации отдыха и оздоровления недоступен.",
                    OrderField = id,
                    SendEmail = false
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 40000,
                    Status = 1148,
                    ReasonCode = "3",
                    Name = "Сведения не получены ведомством",
                    Commentary = "Вы не приняли участие во втором этапе.",
                    //"Вы не приняли участие во втором этапе.",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 40000,
                    Status = 1080,
                    ReasonCode = "13",
                    Name = "Отказ в предоставление услуги",
                    Commentary = "Вы не приняли участие во втором этапе заявочной кампании.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.5

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 50000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201705,
                YearOfRestId = null
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 50000,
                Status = 1056,
                ReasonCode = "1",
                Name = "Результат рассмотрения заявления",
                Commentary = "Ваше заявление рассмотрено.",
                OrderField = id,
                SendEmail = true,
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 50000,

                Status = 1080,
                ReasonCode = "5",
                Name = "Отказ в предоставление услуги",
                Commentary = "Отсутствие квоты на отдых и оздоровление.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.6

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 60000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 202003
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 60000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 60000,

                Status = 1080,
                ReasonCode = "6",
                Name = "Отказ в предоставление услуги",
                Commentary = "Отсутствие подтверждения льготной категории.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.7

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 70000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 202004
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 70000,
                    Status = 10190,
                    Name = "Отзыв заявления недоступен",
                    Commentary = string.Empty,
                    OrderField = id,
                    SendEmail = false
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.Reject + 70000,

                    Status = 1080,
                    ReasonCode = "7",
                    Name = "Отказ в предоставление услуги",
                    Commentary = "Отсутствие подтверждения получения ежемесячного пособия на ребёнка.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.9

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 80000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 202006
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 80000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 80000,

                Status = 1080,
                ReasonCode = "9",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "Наличие сведений о нарушениях правил отдыха и оздоровления в текущем календарном году ребёнком, сопровождающим лицом (в случае организации совместного выездного отдыха).",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.10

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 90000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201908
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 90000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 90000,

                Status = 1080,
                ReasonCode = "10",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "Наличие сведений о нарушениях сопровождающим лицом в текущем календарном году обязательств, предусмотренных соглашением об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления, заключённым с ГАУК \"МОСГОРТУР\".",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.4

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 100000,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 202002
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 100000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 100000,

                Status = 1080,
                ReasonCode = "4",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "Возраст детей не соответствует возрасту, предусмотренному для соответствующего вида отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.11

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 110000,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201909,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 110000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 110000,

                Status = 1080,
                ReasonCode = "11",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "Наличие сведений о неосуществлении отдыха и оздоровления без уважительных причин, на основании предоставленной в текущем календарном году путевки для отдыха и оздоровления с оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.3

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 120000,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201912,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 120000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 120000,
                Status = 1080,
                ReasonCode = "3",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "Отсутствие места жительства ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.8

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 130000,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 202005,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 130000,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 130000,
                Status = 1080,
                ReasonCode = "8",
                Name = "Отказ в предоставлении услуги",
                Commentary = "Наличие сведений о нарушениях условий реализации сертификатов на отдых и оздоровление.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.12

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 140000,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = 201902,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 140000,
                Status = 8031,
                ReasonCode = "2",
                Name =
                    "Предоставление недостающих сведений недоступно. Сведения предоставлены Заявителем при личном визите.",
                Commentary = "Выбор организации отдыха и оздоровления недоступен.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 140000,
                Status = 1148,
                ReasonCode = "2",
                Name = "Сведения получены ведомством",
                Commentary = "Вы отказались от представленных вариантов организаций отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 140000,
                Status = 1080,
                ReasonCode = "12",
                Name = "Отказ в предоставлении услуги",
                Commentary = "Отказ от предложенных вариантов организаций отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Отказ 1080.12 (ПГУ)

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.Reject + 150000,
                StatusId = (long) StatusEnum.ErrorRequest
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 150000,
                Status = 8011,
                ReasonCode = "2",
                Name = "Сведения от заявителя переданы в ведомство",
                Commentary = "Вы отказались от представленных вариантов организаций отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 150000,
                Status = 1148,
                ReasonCode = "2",
                Name = "Сведения получены ведомством",
                Commentary = "Вы отказались от представленных вариантов организации отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.Reject + 150000,
                Status = 1080,
                ReasonCode = "12",
                Name = "Отказ в предоставлении услуги",
                Commentary = "Отказ от предложенных вариантов организации отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion           

            #region Отказ 1075.1 (Путёвка выбрана ПГУ)

            var chainId10751 = (long) StatusEnum.CertificateIssued + 202010000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = chainId10751,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.ErrorRequest
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10751,
                    Status = 8011,
                    ReasonCode = "1",
                    Name = "Сведения не получены ведомством",
                    Commentary = "Вы выбрали:",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10751,
                    Status = 1148,
                    ReasonCode = "1",
                    Name = "Сведения получены ведомством",
                    Commentary = "Вы выбрали:",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10751,
                    Status = 1052,
                    Name = "Формирование результата",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10751,
                    Status = 1075,
                    ReasonCode = "1",
                    Name = "Услуга оказана. Решение положительное",
                    Commentary =
                        "Направляем путевку для отдыха и оздоровления. Приложенный документ носит информационный характер (путевка). Вы можете самостоятельно распечатать приложенный документ.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            #endregion

            #region Отказ 1075.3 (Принят отказ от путевки)

            var chainId10753 = (long) StatusEnum.CertificateIssued + 202020000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = chainId10753,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.CertificateIssued,
                StatusActionId = (long) StatusEnum.CertificateIssued + 202020000
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10753,
                    Status = 8031,
                    ReasonCode = "1",
                    Name = "Предоставление недостающих сведений недоступно. Истек срок предоставления.",
                    Commentary = "Выбор организации отдыха и оздоровления недоступен.",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10753,
                    Status = 1148,
                    ReasonCode = "3",
                    Name = "Сведения не получены ведомством",
                    Commentary = "Вы не приняли участие во втором этапе.",
                    //"Вы не приняли участие во втором этапе.",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10753,
                    Status = 1052,
                    Name = "Формирование результата",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = chainId10753,
                    Status = 1075,
                    ReasonCode = "3",
                    Name = "Услуга оказана. Неучастие заявителя во втором этапе.",
                    Commentary = "Вы не приняли участие во втором этапе заявочной кампании.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Минилок 2020 Услуга оказана по сертификату

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "Отправка что сертификат выдан",
                EventCode = RequestEventEnum.SendCertificateIssued
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued + 5000000,
                RequestEventId = eventIds,
                RequestOnMoney = true,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 5000000,
                    Status = 1040,
                    Name = "заявление доставлено в ведомство",
                    Commentary = string.Empty,
                    OrderField = id,
                    SendEmail = false,
                }, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 5000000,
                    Status = 1050,
                    Name = "заявление зарегистрировано",
                    Commentary =
                        "Ваше заявление зарегистрировано.",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 5000000,
                    Status = 1052,
                    Name = "На исполнении",
                    Commentary = null,
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 5000000,
                    Status = 1075,
                    ReasonCode = "2",
                    Name = "Услуга оказана",
                    Commentary =
                        "Направляем сертификат на отдых и оздоровление. \n\rПриложенный документ носит информационный характер.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.SaveCertificateToRequest
                },
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                });

            #endregion

            #region Минилок 2020 Услуга оказана по заявлению

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name =
                    "Отправка что услуга оказана",
                EventCode = RequestEventEnum.SendCertificateIssuedByParent
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.CertificateIssued + 6000000,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 6000000,
                    Status = 8031,
                    ReasonCode = "2",
                    Name =
                        "Сведения предоставлены Заявителем при личном визите предоставлены Заявителем при личном визите",
                    Commentary = "Выбор организации отдыха и оздоровления недоступен.",
                    OrderField = id,
                    SendEmail = false
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long) StatusEnum.CertificateIssued + 6000000,
                    Status = 1075,
                    ReasonCode = "4",
                    Name = "Услуга оказана",
                    Commentary =
                        "Вы приняли участие в выборе варианта организации отдыха и оздоровления детей при личном визите.",
                    OrderField = id,
                    SendEmail = true
                });

            #endregion

            #region Минилок 2020 Отказ 1080.13 (второй вариант)

            var miniLOK2020108013IdAlter = (long) StatusEnum.Reject + 202030000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = miniLOK2020108013IdAlter,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = (long) DeclineReasonEnum.NonParticipationOfApplicant
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = miniLOK2020108013IdAlter,
                    Status = 8031,
                    ReasonCode = "1",
                    Name = "Предоставление недостающих сведений недоступно. Истек срок предоставления.",
                    Commentary = "Выбор организации отдыха и оздоровления недоступен.",
                    OrderField = id,
                    SendEmail = false
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = miniLOK2020108013IdAlter,
                    Status = 1148,
                    ReasonCode = "3",
                    Name = "Сведения не получены ведомством",
                    Commentary = "Вы не приняли участие в выборе вариантов организации отдыха и оздоровления детей.",
                    OrderField = id,
                    SendEmail = true
                },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = miniLOK2020108013IdAlter,
                    Status = 1080,
                    ReasonCode = "13",
                    Name = "Отказ в предоставление услуги",
                    Commentary = "Вы не приняли участие во втором этапе заявочной кампании.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Минилок 2020 Отказ 1080 (отказ тем кто не участвовал)

            var refuseAllVariantsCovid2020 = (long) StatusEnum.Reject + 202040000;

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = refuseAllVariantsCovid2020,
                IsFirstCompany = true,
                StatusId = (long) StatusEnum.Reject,
                DeclineReasonId = (long) DeclineReasonEnum.RefuseAllVariantsCovid2020
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = refuseAllVariantsCovid2020,
                Status = 1080,
                ReasonCode = "",
                Name = "Отказ в предоставление услуги",
                Commentary =
                    "пункт 5.8 Порядка: \"Заявитель вправе не участвовать в выборе конкретной организации отдыха и оздоровления из числа предлагаемых ГАУК \"МОСГОРТУР\" в соответствии с пунктом 5.5 Порядка. При этом неучастие заявителя в выборе конкретной организации отдыха и оздоровления не может являться основанием для отказа в предоставлении услуг отдыха и оздоровления при организации отдыха и оздоровления в 2021-2023 годах в порядке, утвержденном постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\".",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region Лок 2022 1035.2 отказ в регистрации

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long) StatusEnum.RegistrationDecline + 202100000,
                StatusId = (long) StatusEnum.RegistrationDecline,
                StatusActionId = (long) StatusEnum.RegistrationDecline + 202200000,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.RegistrationDecline + 202100000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long) StatusEnum.RegistrationDecline + 202100000,
                Status = 1035,
                ReasonCode = "2",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r На указанного ребёнка уже подано заявление о предоставлении услуг отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion

            #region Лок 2022 7704 Сбор сведений общий

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7704 Сбор сведений",
                EventCode = RequestEventEnum.SendRequestBase
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7704,
                ReasonCode = null,
                Name = "Сбор сведений",
                Commentary = "1. Получение документов из Базового Регистра (проверка льготной категории и получения ежемесячного пособия на ребёнка в ДТСЗН).\n2. Получение документов из Базового Регистра (проверка родства в ФЗАГС).\n3. Получение документов из Базового Регистра Пенсионного фонда РФ (проверка СНИЛС).\n4. Получение документов из АИС «ЦПМПК» (проверка льготной категории дети с ограниченными возможностями здоровья в ЦПМПК).\n5. Получение документов из Федеральной государственной информационной системы «Федеральный реестр инвалидов» (проверка льготной категории дети-инвалиды в ПФР).\n6. Получение документов из Базового Регистра АС УР (проверка паспорта в МВД).\n7. Получение документов из Базового Регистра АС УР (проверка адреса регистрации в МВД детей всех льготных категорий и лиц из числа детей-сирот и детей, оставшихся без попечения родителей).\n8. Получение документов из Базового Регистра ДТСЗН (проверка полномочий законного представителя ребёнка для льготной категории дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе приемной или патронатной семье в ДТСЗН).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region Лок 2022 7705 Сведения получены общий

            context.RequestEvent.AddOrUpdate(e => e.Id, new RequestEvent
            {
                Id = ++eventIds,
                Name = "7705 Сведения получены",
                EventCode = RequestEventEnum.GetResponseBase
            });

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = --chainId,
                RequestEventId = eventIds,
                IsFirstCompany = true
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = chainId,
                Status = 7705,
                ReasonCode = null,
                Name = "Сбор сведений",
                Commentary = "1. Документы из Базового Регистра получены (проверка льготной категории и получения ежемесячного пособия на ребёнка в ДТСЗН).\n2. Документы из Базового Регистра получены (проверка родства в ФЗАГС).\n3. Документы из Базового Регистра Пенсионного фонда РФ получены (проверка СНИЛС).\n4. Документы из Базового Регистра получены в АИС «ЦПМПК» (проверка льготной категории дети с ограниченными возможностями здоровья в ЦПМПК).\n5. Документы из Федеральной государственной информационной системы «Федеральный реестр инвалидов» получены (проверка льготной категории дети-инвалиды в ПФР).\n6. Документы из Базового Регистра АС УР получены (проверка паспорта в МВД).\n7. Документы из Базового Регистра АС УР получены (проверка адреса регистрации в МВД детей всех льготных категорий и лиц из числа детей-сирот и детей, оставшихся без попечения родителей).\n8. Документы из Базового Регистра получены (проверка законного представительства в ДТСЗН).",
                OrderField = id,
                SendEmail = false
            });

            #endregion

            #region Отказ 1080.14

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long)StatusEnum.Reject + 160000,
                StatusId = (long)StatusEnum.Reject,
                DeclineReasonId = 202101,
                IsFirstCompany = true,
                RequestOnMoney = false,
            });
            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id,  new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.Ranging,
                Status = 10190,
                Name = "Отзыв заявления недоступен",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            },
                new RequestStatusForMpgu
                {
                    Id = ++id,
                    ChainId = (long)StatusEnum.Reject + 160000,
                    Status = 1080,
                    ReasonCode = "14",
                    Name = "Отказ в предоставление услуги",
                    Commentary =
                        "Нарушение правил подачи заявления о предоставлении услуг отдыха и оздоровления.",
                    OrderField = id,
                    SendEmail = true
                });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRefuse
                });

            #endregion

            #region 1035.2 отказ в регистрации путёвка сопровождение

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long)StatusEnum.RegistrationDecline + 1100000,
                StatusId = (long)StatusEnum.RegistrationDecline,
                StatusActionId = (long)StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202102

            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1100000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1100000,
                Status = 1035,
                ReasonCode = "2",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r На указанное(ые) в заявлении сопровождающее(ие) лицо(а) уже подано заявление о предоставлении бесплатной путёвки для отдыха и оздоровления.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion

            #region 1035.3 отказ в регистрации сертификат сопровождение

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long)StatusEnum.RegistrationDecline + 1200000,
                StatusId = (long)StatusEnum.RegistrationDecline,
                StatusActionId = (long)StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202103
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1200000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1200000,
                Status = 1035,
                ReasonCode = "3",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r На указанное(ые) в заявлении сопровождающее(ие) лицо(а) уже подано заявление о предоставлении сертификата на отдых и оздоровление.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion

            #region 1035.5 отказ в регистрации сертификат сопровождение

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long)StatusEnum.RegistrationDecline + 1300000,
                StatusId = (long)StatusEnum.RegistrationDecline,
                StatusActionId = (long)StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202105
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1300000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1300000,
                Status = 1035,
                ReasonCode = "5",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r Заявление о предоставлении бесплатной путёвки для отдыха и оздоровления от имени родителя (законного представителя) уже подано.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion

            #region 1035.6 отказ в регистрации сертификат сопровождение

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long)StatusEnum.RegistrationDecline + 1400000,
                StatusId = (long)StatusEnum.RegistrationDecline,
                StatusActionId = (long)StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202106
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1400000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1400000,
                Status = 1035,
                ReasonCode = "6",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r Заявление о предоставлении сертификата на отдых и оздоровление от имени родителя (законного представителя) уже подано.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion

            #region 1035.4 отказ в регистрации сертификат сопровождение

            context.RequestStatusChainForMpgu.AddOrUpdate(a => a.Id, new RequestStatusChainForMpgu
            {
                Id = (long)StatusEnum.RegistrationDecline + 1500000,
                StatusId = (long)StatusEnum.RegistrationDecline,
                StatusActionId = (long)StatusEnum.RegistrationDecline,
                IsFirstCompany = true,
                DeclineReasonId = 202104
            });

            context.RequestStatusForMpgu.AddOrUpdate(a => a.Id, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1500000,
                Status = 1040,
                Name = "заявление доставлено в ведомство",
                Commentary = string.Empty,
                OrderField = id,
                SendEmail = false
            }, new RequestStatusForMpgu
            {
                Id = ++id,
                ChainId = (long)StatusEnum.RegistrationDecline + 1500000,
                Status = 1035,
                ReasonCode = "4",
                Name = "отказ в регистрации заявления",
                Commentary =
                    "Заявление является повторным. \n\r Заявление на указанное в заявлении лицо из числа детей-сирот и детей, оставшихся без попечения родителей уже было подано.",
                OrderField = id,
                SendEmail = true
            });

            context.RequestStatusCshedSendAndSignDocument.AddOrUpdate(a => a.Id,
                new RequestStatusCshedSendAndSignDocument
                {
                    MpguStatusId = id,
                    SignNeed = true,
                    DocumentPath = DocumentGenerationEnum.NotificationRegistration
                });

            #endregion
        }
    }
}
