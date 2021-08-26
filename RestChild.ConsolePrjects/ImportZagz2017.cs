using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.ConsoleProjects
{
	/// <summary>
	/// загрузка данных из ЗАГС
	/// </summary>
	public static class ImportZagz2017
	{
		private static string responseErrorTemplate = @"<?xml version=""1.0"" encoding=""utf-16""?>
<CoordinateStatusMessage xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
	<ServiceHeader>
		<FromOrgCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1111</FromOrgCode>
		<ToOrgCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">2064</ToOrgCode>
		<MessageId xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{0}</MessageId>
		<ServiceNumber xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{1}</ServiceNumber>
		<RequestDateTime xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{2}</RequestDateTime>
	</ServiceHeader>
	<StatusMessage>
		<RequestId xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{3}</RequestId>
		<ResponseDate xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{4}</ResponseDate>
		<PlanDate xsi:nil=""true"" xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/""/>
		<StatusCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1007</StatusCode>
		<Note xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">Сведения не найдены</Note>
		<ServiceNumber xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{5}</ServiceNumber>
	</StatusMessage>
</CoordinateStatusMessage>";

		static string responseSuccessTemplate = @"<?xml version=""1.0"" encoding=""utf-16""?>
<CoordinateStatusMessage xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
	<ServiceHeader>
		<FromOrgCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1111</FromOrgCode>
		<ToOrgCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">2064</ToOrgCode>
		<MessageId xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{0}</MessageId>
		<ServiceNumber xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{1}</ServiceNumber>
		<RequestDateTime xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{2}</RequestDateTime>
	</ServiceHeader>
	<StatusMessage>
		<RequestId xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{3}</RequestId>
		<ResponseDate xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{4}</ResponseDate>
		<PlanDate xsi:nil=""true"" xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/""/>
		<StatusCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1004</StatusCode>
		<Documents xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">
			<ServiceDocument>
				<DocCode>22</DocCode>
				<DocDate>2016-03-14T10:07:22.444Z</DocDate>
				<ValidityPeriod>2016-03-14T10:07:22.444Z</ValidityPeriod>
				<ListCount>1</ListCount>
				<CopyCount>1</CopyCount>
				<CustomAttributes>
					<CertOfBirthResponse:CertOfBirthResponse id=""obj"" xmlns:CertOfBirthResponse=""http://nakhodka.ru/documentsservice/types"" xmlns=""http://nakhodka.ru/documentsservice/types"">
						<CertOfBirthResponse:Child>
							<CertOfBirthResponse:BirthDate>{5}</CertOfBirthResponse:BirthDate>
							<CertOfBirthResponse:BirthPlace>{6}</CertOfBirthResponse:BirthPlace>
							<CertOfBirthResponse:FirstName>{7}</CertOfBirthResponse:FirstName>
							<CertOfBirthResponse:LastName>{8}</CertOfBirthResponse:LastName>
							<CertOfBirthResponse:Patronymic>{9}</CertOfBirthResponse:Patronymic>
						</CertOfBirthResponse:Child>
						<CertOfBirthResponse:Mother>
							<CertOfBirthResponse:BirthDate>{10}</CertOfBirthResponse:BirthDate>
							<CertOfBirthResponse:FirstName>{11}</CertOfBirthResponse:FirstName>
							<CertOfBirthResponse:LastName>{12}</CertOfBirthResponse:LastName>
							<CertOfBirthResponse:Patronymic>{13}</CertOfBirthResponse:Patronymic>
							<CertOfBirthResponse:Citizenship>России</CertOfBirthResponse:Citizenship>
						</CertOfBirthResponse:Mother>
						<CertOfBirthResponse:Father>
							<CertOfBirthResponse:BirthDate>{14}</CertOfBirthResponse:BirthDate>
							<CertOfBirthResponse:FirstName>{15}</CertOfBirthResponse:FirstName>
							<CertOfBirthResponse:LastName>{16}</CertOfBirthResponse:LastName>
							<CertOfBirthResponse:Patronymic>{17}</CertOfBirthResponse:Patronymic>
							<CertOfBirthResponse:Citizenship>России</CertOfBirthResponse:Citizenship>
						</CertOfBirthResponse:Father>
						<CertOfBirthResponse:ActRequisites>
							<CertOfBirthResponse:ActNumber>{18}</CertOfBirthResponse:ActNumber>
							<CertOfBirthResponse:ActDate>{19}</CertOfBirthResponse:ActDate>
							<CertOfBirthResponse:NameOfRegistrar>{20}</CertOfBirthResponse:NameOfRegistrar>
						</CertOfBirthResponse:ActRequisites>
						<CertOfBirthResponse:CertRequisites>
							<CertOfBirthResponse:CertSeries>{21}</CertOfBirthResponse:CertSeries>
							<CertOfBirthResponse:CertNumber>{22}</CertOfBirthResponse:CertNumber>
							<CertOfBirthResponse:CertDate>{23}</CertOfBirthResponse:CertDate>
						</CertOfBirthResponse:CertRequisites>
					</CertOfBirthResponse:CertOfBirthResponse>
				</CustomAttributes>
			</ServiceDocument>
		</Documents>
		<Note xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">Получен ответ из ФОИВ/ОИВ по запросу документа</Note>
		<Result xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">
			<ResultCode>1</ResultCode>
		</Result>
		<ServiceNumber xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{1}</ServiceNumber>
		<ReasonCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1</ReasonCode>
	</StatusMessage>
</CoordinateStatusMessage>";

		private static List<List<string>> GetDataTableFromExcel(string path, bool hasHeader = true)
		{
			using (var pck = new OfficeOpenXml.ExcelPackage())
			{
				using (var stream = File.OpenRead(path))
				{
					pck.Load(stream);
				}
				var ws = pck.Workbook.Worksheets.First();
				var res = new List<List<string>>();
				var startRow = hasHeader ? 2 : 1;

				var columnsCount = ws.Dimension.End.Column;
				for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
				{
					var row = new List<string>();
					for(var i = 1; i<=columnsCount; i++)
					{
						using (var cell = ws.Cells[rowNum, i])
						{
							row.Add(cell.Text);
						}
					}

					res.Add(row);
				}

				return res;
			}
		}

		/// <summary>
		/// импорт
		/// </summary>
		public static void ImportZagzFromExcel(string path)
		{
			using (var uv = new UnitOfWork())
			{
				Console.WriteLine("Opening Excel data file...");
				var table = GetDataTableFromExcel(path);
				Console.WriteLine("Excel data file opened. Load " + table.Count + " rows.");

				var index = 0;
				var procent = 0;
				var message = "Import " + procent + "% complited.";
				Console.Write(message + "    ");
				var countPoints = 1;
				var indexCountPoints = 0;
				foreach (var row in table)
				{
					if (row.Count < 3)
					{
						continue;
					}

					var requestId = row[0].LongParse();
					var applicantId = row[1].LongParse();
					var childId = row[2].LongParse();

					var exch = new ExchangeBaseRegistry
					{
						ApplicantId = applicantId,
						ChildId = childId,
						Child = uv.GetById<Child>(childId),
						IsIncoming = false,
						SendDate = DateTime.Now,
						ResponseDate = DateTime.Now,
						ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Relationship,
						ServiceNumber = RelationshipLoadServiceNumber.Name,
						OperationType = RelationshipLoadServiceNumber.Name,
						IsProcessed = true,
						RequestGuid = RelationshipLoadServiceNumber.Name,
					};

					if (!exch.ChildId.HasValue || !exch.ApplicantId.HasValue || requestId != exch.Child?.RequestId)
					{
						continue;
					}

					// сброс старых записей
					var clear = uv.GetSet<ExchangeBaseRegistry>()
						.Where(e => !e.NotActual && e.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Relationship &&
						            e.ChildId == exch.ChildId && e.ApplicantId == exch.ApplicantId);
					foreach (var c in clear)
					{
						c.NotActual = true;
					}

					var responseGuid = Guid.NewGuid();
					if (row.Count<26 || row[22] == "NULL" || string.IsNullOrWhiteSpace(row[22]))
					{
						exch.ResponseText = string.Format(responseErrorTemplate, responseGuid, exch.ServiceNumber,
							exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(), exch.ServiceNumber);
						exch.Success = false;
					}
					else
					{
						exch.ResponseText = string.Format(responseSuccessTemplate, responseGuid, exch.ServiceNumber,
							exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(),
							row[11].TryParseDateDdMmYyyy().DateTimeToXml(),
							exch.Child?.PlaceOfBirth, row[9], row[8], row[10],
							row[16].TryParseDateDdMmYyyy().DateTimeToXml(), row[14].NullEmpty(),
							row[13].NullEmpty(), row[15].NullEmpty(),
							row[20].TryParseDateDdMmYyyy().DateTimeToXml(), row[18].NullEmpty(),
							row[17].NullEmpty(), row[19].NullEmpty(), row[23].NullEmpty(), row[24].TryParseDateDdMmYyyy().DateTimeToXml(),
							row[25].NullEmpty(), row[21].NullEmpty(), row[22].NullEmpty(), string.Empty);

						//exch.Success = (exch.Child?.DocumentTypeId != (long) DocumentTypeEnum.CertOfBirth && exch.Child?.DocumentTypeId != 23) ||
						//               (exch.Child.DocumentNumber == row[22] && exch.Child.DocumentSeria == row[21]);
						exch.Success = true;
					}

					uv.AddEntity(exch, false);
					uv.SaveChanges();

					foreach (var entry in uv.Context.ChangeTracker.Entries())
					{
						entry.State = EntityState.Detached;
					}
					var needRefresh = false;
					var currentProcent = index * 100 / table.Count;
					if (currentProcent != procent)
					{
						procent = currentProcent;
						message = "Import " + procent + "% complited.";
						needRefresh = true;
					}


					if (indexCountPoints > 100)
					{
						countPoints++;
						indexCountPoints = 0;
						needRefresh = true;
					}

					if (countPoints > 3)
					{
						countPoints = 0;
						needRefresh = true;
					}

					if (needRefresh)
					{
						Console.SetCursorPosition(0, Console.CursorTop);
						Console.Write(message + string.Join("", Enumerable.Repeat(".", countPoints)) + "    ");
					}

					index++;
					indexCountPoints++;
				}

				Console.SetCursorPosition(0, Console.CursorTop);
				Console.WriteLine("Import 100% complited.  ");
			}
		}
	}
}
