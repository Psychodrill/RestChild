using RestChild.Domain;

namespace RestChild.Extensions.Models
{
    public class SpecializedCampsCampersReport
    {
        public int VolumeAdded { get; set; }

        public int RestPlacesBrought { get; set; }

        public int VolumeBrought { get; set; }

        public int ChildsInserted { get; set; }

        public int ForAprove { get; set; }

        public int Approved { get; set; }

        public int Paid { get; set; }

        public TimeOfRest TimeOfRest { get; set; }

        public Organization Organisation { get; set; }

        public bool IsSum { get; set; }
    }
}
