using System.ServiceModel;

namespace RestChild.Web.Services.Contract
{
	[ServiceContract(Namespace = ConfigNameSpaces.NsiAsur)]
	public interface IReplicationReceiver
	{
		[OperationContract(Action = ConfigNameSpaces.NsiAsurReceiveChange)]
		string receive_change(string xml);
	}
}