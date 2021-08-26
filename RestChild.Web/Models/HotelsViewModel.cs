using System.Collections.Generic;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class HotelsViewModel : ViewModelBase<Hotels>
	{
		/// <summary>
		/// Статус
		/// </summary>
		public ViewModelState State { get; set; }

		public string StateMachineActionString { get; set; }

		public string ActiveTab { get; set; }

		public bool CanEdit { get; set; }

		public bool CanEditName { get; set; }

		public string Link3DTour { get; set; }

		public long? Link3DTourId { get; set; }

		public HotelsViewModel()
			: base(new Hotels())
		{
		}

		public HotelsViewModel(Hotels hotel)
			: base(hotel)
		{
			var file3DLink = hotel?.Files?.FirstOrDefault(f => f.FileTypeId == (long) FileTypeEnum.Link3DTour);
			Link3DTour = file3DLink?.FileUrl;
			Link3DTourId = file3DLink?.Id;
		}

		public override Hotels BuildData()
		{
			var entity = base.BuildData();
			var files = entity?.Files?.ToList() ?? new List<FileHotel>();

			if (!string.IsNullOrWhiteSpace(Link3DTour))
			{
				files.Add(new FileHotel
				{
					Id = Link3DTourId ?? 0,
					FileUrl = Link3DTour,
					FileName = Link3DTour,
					FileTypeId = (long) FileTypeEnum.Link3DTour
				});
			}

			if (entity != null)
			{
				entity.Files = files;
			}

			return entity;
		}

		#region Справочники

		public ICollection<PlaceOfRest> Regions { get; set; }

		public List<FileType> FileTypes { get; set; }

		public IEnumerable<HotelType> HotelTypes { get; set; }

		public IList<StateMachineState> States { get; set; }

		public List<City> Cities { get; set; }

		public List<FunctioningType> FunctioningType { get; set; }

		public List<HotelPlacement> HotelPlacements { get; set; }

		#endregion
	}
}
