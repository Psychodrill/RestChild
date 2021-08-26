using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration.PDFDocuments;
using RestChild.Domain;

namespace RestChild.DocumentGeneration
{
    /// <summary>
    ///     генерация документов для подписания
    /// </summary>
    public static class DocumentProcessor
    {
        /// <summary>
        ///     Сертификат (на отдых и пр)
        /// </summary>
        public static ICshedDocument SaveCertificateToRequest(long id, IUnitOfWork unitOfWork,
            long? sendStatusId = null)
        {
            var request = unitOfWork.GetById<Request>(id);

            if (request == null || request.IsDeleted || request.StatusId != (long) StatusEnum.CertificateIssued &&
                sendStatusId != (long) StatusEnum.CertificateIssued)
            {
                return null;
            }

            var cert = PdfProcessor.CertificateForRequestTemporaryFile(unitOfWork, id, sendStatusId);

            return new CshedDocumentResult(cert);
        }

        /// <summary>
        ///     Уведомление о регистрации заявления
        /// </summary>
        public static ICshedDocument NotificationRegistration(long requestId, Account account, IUnitOfWork unitOfWork)
        {
            var doc = WordProcessor.NotificationRegistration(unitOfWork, account, requestId);

            return new CshedDocumentResult(doc);
        }

        /// <summary>
        ///     Уведомление (базовое)
        /// </summary>
        public static ICshedDocument NotificationRefuse(long requestId, Account account, IUnitOfWork unitOfWork)
        {
            var doc = WordProcessor.NotificationDataSwitch(unitOfWork, account, requestId);

            return new CshedDocumentResult(doc);
        }

        /// <summary>
        ///     Уведомление о приостановлении рассмотрения заявления
        /// </summary>
        public static ICshedDocument NotificationWaitApplicant(long requestId, Account account, IUnitOfWork unitOfWork)
        {
            var doc = WordProcessor.NotificationWaitApplicant(unitOfWork, account, requestId);

            return new CshedDocumentResult(doc);
        }

        /// <summary>
        ///     Уведомление о необходимости выбора организации
        /// </summary>
        public static ICshedDocument NotificationOrgChoose(long requestId, Account account, IUnitOfWork unitOfWork)
        {
            var request = unitOfWork.GetById<Request>(requestId);

            var doc = WordProcessor.NotificationOrgChoose(request, account);

            return new CshedDocumentResult(doc);
        }

        /// <summary>
        ///     Уведомление о принятом решении
        /// </summary>
        public static ICshedDocument NotificationAboutDecision(long requestId, Account account, IUnitOfWork unitOfWork)
        {
            var doc = WordProcessor.NotificationAboutDecision(unitOfWork, account, requestId);

            return new CshedDocumentResult(doc);
        }
    }
}
