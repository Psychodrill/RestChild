using Common.Logging;
using RestChild.Booking.Logic.Logic;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Services;

namespace RestChild.Booking.Logic.Services
{
	public class RestChildrenService : IRestChildrenService
	{
		private readonly RestChildrenIndex _context;
		private readonly IndexRestChecker _restChecker;
		private readonly ILog _logger;

		public RestChildrenService()
		{
			_logger = LogManager.GetLogger(typeof (RestChildrenService));
            _context = new RestChildrenIndex(_logger);
			_restChecker = new IndexRestChecker(_logger);
		}

		public SearchIndexResult GetChildren(RestChildFilterDto restChildFilterDto)
		{
			return _context.SearchChildren(restChildFilterDto);
		}

		public void RebuildIndex()
		{
			_context.RebuildIndex();
		}

		public void CheckFlagsToRefreshIndex()
		{
			_restChecker.Check();
		}
	}
}