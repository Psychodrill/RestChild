using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	///     список типов комнат
	/// </summary>
	public class TypeOfRoomViewModel : ViewModelBase<TypeOfRooms>
	{
		public TypeOfRoomViewModel()
			: base(new TypeOfRooms())
		{
			RoomRates = new List<RoomRates>();
		}

		public TypeOfRoomViewModel(TypeOfRooms trs, List<RoomRates> roomRates)
			: base(trs)
		{
			RoomRates = roomRates;
		}

		public List<RoomRates> RoomRates { get; set; }
	}
}