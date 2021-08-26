using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DocumentGeneration
{
    /// <summary>
    ///     маршрутизация генерации документа
    /// </summary>
    public static class DocumentSwitch
    {
        /// <summary>
        ///     генерация документа
        /// </summary>
        public static ICshedDocument Switch(IUnitOfWork unitOfWork, Account account, long requestId,
            string documentPath, bool requestOnMoney = false, long? sendStatusId = null)
        {
            ICshedDocument doc;
            if (documentPath.Equals(DocumentGenerationEnum.SaveCertificateToRequest))
            {
                doc = DocumentProcessor.SaveCertificateToRequest(requestId, unitOfWork, sendStatusId);

                if (doc != null)
                {
                    if (requestOnMoney)
                    {
                        doc.RequestFileTypeId = (long) RequestFileTypeEnum.CertificateOnPayment;
                        doc.FileName = "Сертификат.pdf";
                    }
                    else
                    {
                        doc.RequestFileTypeId = (long) RequestFileTypeEnum.CertificateOnRest;
                        doc.FileName = "Путёвка.pdf";
                    }
                }
            }
            else if (documentPath.Equals(DocumentGenerationEnum.NotificationRegistration))
            {
                doc = DocumentProcessor.NotificationRegistration(requestId, account, unitOfWork);
            }
            else if (documentPath.Equals(DocumentGenerationEnum.NotificationRefuse))
            {
                doc = DocumentProcessor.NotificationRefuse(requestId, account, unitOfWork);
            }
            else if (documentPath.Equals(DocumentGenerationEnum.NotificationWaitApplicant))
            {
                doc = DocumentProcessor.NotificationWaitApplicant(requestId, account, unitOfWork);
            }
            else if (documentPath.Equals(DocumentGenerationEnum.NotificationAboutDecision))
            {
                doc = DocumentProcessor.NotificationAboutDecision(requestId, account, unitOfWork);
            }
            else if (documentPath.Equals(DocumentGenerationEnum.NotificationOfNeedToChoose))
            {
                // Уведомление о необходимости выбора организации отдыха и оздоровления
                doc = DocumentProcessor.NotificationOrgChoose(requestId, account, unitOfWork);
            }
            else
            {
                return null;
            }

            //загадочное поведение генерации файла
            if (doc?.FileBody == null)
            {
                return new CshedDocumentResult();
            }

            if (!doc.RequestFileTypeId.HasValue || doc.RequestFileTypeId.Value < 1)
            {
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.Notifications;
            }

            return doc;
        }
    }
}
