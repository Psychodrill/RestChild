using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель сотрудника детского дома
    /// </summary>
    public class OrganisatorCollaboratorModel : ViewModelBase<OrganisatorCollaborator>
    {
        public OrganisatorCollaboratorModel() : base(new OrganisatorCollaborator())
        {
            NoMiddleName = false;
        }

        public OrganisatorCollaboratorModel(OrganisatorCollaborator c) : base(c)
        {
            NoMiddleName = c.Applicant?.Id > 0 && (!c.Applicant?.HaveMiddleName ?? false);
        }

        /// <summary>
        ///     Нет отчества
        /// </summary>
        public bool NoMiddleName { get; set; }
    }
}
