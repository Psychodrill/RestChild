namespace RestChild.Booking.Logic.Indexing
{
	using System;

	using Lucene.Net.Documents;

	public static class LuceneExtensions
	{
		public static IFieldable AddDouble(this Document doc, string name, double value, bool store = false)
		{
			var field = new NumericField(name, store ? Field.Store.YES : Field.Store.NO, true);
			field.SetDoubleValue(value);
			doc.Add(field);

			return field;
		}

		public static IFieldable AddLong(this Document doc, string name, long value, bool store = false)
		{
			var field = new NumericField(name, store ? Field.Store.YES : Field.Store.NO, true);
			field.SetLongValue(value);
			doc.Add(field);

			return field;
		}

		public static IFieldable AddDate(this Document doc, string name, DateTime value, bool store = false)
		{
			return doc.AddLong(name, value.Ticks, store);
		}

		public static IFieldable AddString(this Document doc, string name, string value, bool store = false, bool analyzed = false)
		{
			var field = new Field(name,
				value,
				store ? Field.Store.YES : Field.Store.NO,
				analyzed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED);
			doc.Add(field);

			return field;
		}
	}
}
