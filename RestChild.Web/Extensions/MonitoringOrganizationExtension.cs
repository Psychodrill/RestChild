using RestChild.Comon;
using RestChild.Web.Controllers;
using System.Linq;

namespace RestChild.Web.Extensions
{
    /// <summary>
    ///     Расширение для работы с организациями участниками мониторинга
    /// </summary>
    public class MonitoringOrganizationExtension
    {
        /// <summary>
        ///     Наличие права и соответствующей организации
        /// </summary>
        public static bool HasRightOfMonitoringOrganization(string code)
        {
            var security = Security.SecurityBasis.GetSecurity();
            var hasRight = security.Any(s => s.StartsWith(code));

            if (!hasRight)
                return false;

            var organizationIds = code.GetSecurityOrganiztion();

            bool validOrganization;
            if (organizationIds == null || !organizationIds.Any())
            {
                var uw = WindsorHolder.Resolve<IUnitOfWork>();
                try
                {
                    validOrganization =  uw.GetSet<Domain.Organization>().Any(o => !o.IsDeleted && o.IsInMonitoring);
                }
                finally
                {
                    WindsorHolder.Release(uw);
                }
            }
            else
            {
                var uw = WindsorHolder.Resolve<IUnitOfWork>();
                try
                {
                    validOrganization = uw.GetSet<Domain.Organization>().Any(o => !o.IsDeleted && o.IsInMonitoring && organizationIds.Contains(o.Id));
                }
                finally
                {
                    WindsorHolder.Release(uw);
                }
            }

            return validOrganization;
        }
    }
}
