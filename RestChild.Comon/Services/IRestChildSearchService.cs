using System.ServiceModel;
using RestChild.Comon.Dto.SearchRestChild;

namespace RestChild.Comon.Services
{
    /// <summary>
    ///     работа с индексом детей
    /// </summary>
    [ServiceContract]
    public interface IRestChildrenService
    {
        [OperationContract]
        SearchIndexResult GetChildren(RestChildFilterDto restChildFilterDto);

        [OperationContract]
        void RebuildIndex();

        [OperationContract]
        void CheckFlagsToRefreshIndex();
    }
}
