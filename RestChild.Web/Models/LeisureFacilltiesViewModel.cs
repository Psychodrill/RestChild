using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
    public class LeisureFacilltiesViewModel : ViewModelBase<LeisureFacilities>
    {
        public LeisureFacilltiesViewModel() : base(new LeisureFacilities())
        {
            Factures = new List<LeisureFacilities>();
            
        }

        public LeisureFacilltiesViewModel(LeisureFacilities data) : base(data)
        {
            Parent = data;
            
        }

        public string StateMachineActionString { get; set; }

        public ViewModelState State { get; set; }

        public bool IsEditable { get; set; }

        public LeisureFacilities Parent { get; set; }
        /// <summary>
		/// банковские реквизиты
		/// </summary>
		public List<LeisureFacilities> Factures { get; set; }

        /// <summary>
        ///     регионы
        /// </summary>
        public List<StateDistrict> StateDistricts { get; set; }
    }
}
