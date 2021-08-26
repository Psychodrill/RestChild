using System.Linq;
using Lucene.Net.Linq;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace RestChild.Booking.Logic.LuceneHelpers
{
	public class LuceneSession<T> : ISession<T>
	{
		private readonly FSDirectory _directory;
		private readonly LuceneDataProvider _luceneDataProvider;
		private readonly ISession<T> _session;

		public LuceneSession(FSDirectory directory,LuceneDataProvider luceneDataProvider, ISession<T> session)
		{
			_directory = directory;
			_luceneDataProvider = luceneDataProvider;
			_session = session;
		}

		public void Dispose()
		{
			_session.Dispose();
			_luceneDataProvider.Dispose();
            _directory.Dispose();
		}

		public IQueryable<T> Query()
		{
			return _session.Query();
		}

		public void Add(params T[] items)
		{
			_session.Add(items);
		}

		public void Add(KeyConstraint constraint, params T[] items)
		{
			_session.Add(constraint,items);
		}

		public void Delete(params T[] items)
		{
			_session.Delete(items);
		}

		public void Delete(params Query[] items)
		{
			_session.Delete(items);
		}

		public void DeleteAll()
		{
			_session.DeleteAll();
		}

		public void Commit()
		{
			_session.Commit();
		}

		public void Rollback()
		{
			_session.Rollback();
		}
	}
}