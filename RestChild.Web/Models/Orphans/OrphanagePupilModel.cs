using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель воспитанника детского дома
    /// </summary>
    public class OrphanagePupilModel : ViewModelBase<Pupil>
    {
        public OrphanagePupilModel() : base(new Pupil())
        {
            Files = new Dictionary<string, FileOrLink>();
            Doses = new Dictionary<string, PupilDose>();
            NoMiddleName = false;
        }

        public OrphanagePupilModel(Pupil p) : base(p)
        {
            Files = p.LinkToFiles?.Files?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ?? new Dictionary<string, FileOrLink>();
            Doses = p.Drugs?.Where(ss => !ss.IsDeleted).ToDictionary(s => s.Id.ToString(), s => s) ?? new Dictionary<string, PupilDose>();
            OrphanageName = p.OrphanageAddress?.Organisation?.Name;
            OrphanageId = p.OrphanageAddress?.OrganisationId;
            NoMiddleName = !p.Child?.HaveMiddleName ?? false;
        }

        /// <summary>
        ///     Активная вкладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     Название детского дома
        /// </summary>
        public string OrphanageName { get; set; }

        /// <summary>
        ///     Идентификатор детского дома
        /// </summary>
        public long? OrphanageId { get; set; }

        /// <summary>
        ///     Файлы воспитанника
        /// </summary>
        public Dictionary<string, FileOrLink> Files { get; set; }

        /// <summary>
        ///     Транки воспитанника
        /// </summary>
        public Dictionary<string, PupilDose> Doses { get; set; }

        /// <summary>
        ///     Нет отчества
        /// </summary>
        public bool NoMiddleName { get; set; }

        public override Pupil BuildData()
        {
            var res = base.BuildData();
            res.Drugs = Doses?.Values.ToList() ?? new List<PupilDose>(0);
            res.LinkToFiles = res.LinkToFilesId.HasValue ? new LinkToFile { Id = res.LinkToFilesId.Value } : new LinkToFile();
            res.LinkToFiles.Files = Files?.Where(ss => !string.IsNullOrWhiteSpace(ss.Key)).Select(ss => ss.Value).ToList() ?? new List<FileOrLink>();
            return res;
        }
    }
}
