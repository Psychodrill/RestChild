using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
	public partial class TourVolume
	{
		[NotMapped]
		public ICollection<Booking> Bookings { get; set; } 
	}
}
