using RestChild.Comon;
using RestChild.DAL;
using RestChild.ERL;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Интеграция с ЕГАС (загрузка школ)
    /// </summary>
    [Task]
    public class EGASIntegrationTask : BaseTask
    {
        protected override void Execute()
        {
            try
            {
                Logger.Info("EGASIntegrationTask start...");

                using (var unitOfWork = new UnitOfWork())
                {
                    RestChild.EGASIntegration.IntegrationRepository.UpdateSchools(unitOfWork);
                }

                Logger.Info("EGASIntegrationTask finish...");
            }
            catch (Exception mqe)
            {
                Logger.Error($"EGASIntegrationTask error: {mqe.Message}", mqe);
            }
        }
    }
}
