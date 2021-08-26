using RestChild.Comon;

namespace RestChild.Domain.Interfaces
{
	/// <summary>
	/// интерфейс сущности у которой есть статус
	/// </summary>
	public interface IStateEntity : IEntityBase
	{
		/// <summary>
		///     статус
		/// </summary>
		StateMachineState State { get; set; }

		long? StateId { get; set; }
	}
}
