using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
	public partial class Party
	{
		[NotMapped]
		public ICollection<Child> Childs { get; set; } 
	}
}
