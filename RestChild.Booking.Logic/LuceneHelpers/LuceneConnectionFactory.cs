using System;
using System.Configuration;
using System.IO;
using System.Threading;
using Lucene.Net.Linq;
using Lucene.Net.Linq.Mapping;
using Lucene.Net.Store;

namespace RestChild.Booking.Logic.LuceneHelpers
{
	public static class LuceneConnection
	{
		public static readonly global::Lucene.Net.Util.Version LuceneVersion = Lucene.Net.Util.Version.LUCENE_30;
		private static readonly string LuceneIndexDirectory = Settings.Default.LuceneIndexPath;

		public static FSDirectory GetIndexDirectory(string indexName)
		{
			var indexPath = Path.Combine(LuceneIndexDirectory,indexName);

			if (!System.IO.Directory.Exists(indexPath))
			{
				System.IO.Directory.CreateDirectory(indexPath);
			}

			var dir = FSDirectory.Open(new DirectoryInfo(indexPath));
			return dir;
		}
	}

	public static class LuceneConnectionFactory
	{
		public static ReaderWriterLock RestChildWriteLock = new ReaderWriterLock();

		public static ISession<T> GetLuceneConnection<T>(string indexDirectory, IDocumentMapper<T> documentMapper,bool enableMultipleEntities = false)
			where T:new ()
		{
			var directory = LuceneConnection.GetIndexDirectory(indexDirectory);
			var provider = new LuceneDataProvider(directory, LuceneConnection.LuceneVersion);
			var session = provider.OpenSession(documentMapper);
			provider.Settings.EnableMultipleEntities = enableMultipleEntities;
			return new LuceneSession<T>(directory, provider, session);
		}
	}
}
