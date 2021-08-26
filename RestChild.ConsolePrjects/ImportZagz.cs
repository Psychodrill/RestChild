using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.ConsoleProjects
{

	/// <summary>
	/// импорт загс
	/// </summary>
	public static class ImportZagz
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

		public static List<List<string>> GetDataTableFromExcel(string path, bool hasHeader = true)
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
				for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
				{
					var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
					var row = new List<string>();
					foreach (var cell in wsRow)
					{
						row.Add(cell.Text);
					}

					res.Add(row);
				}

				return res;
			}
		}

		/// <summary>
		/// импорт
		/// </summary>
		public static void ImportZagzFromExcel()
		{
			using (var uv = new UnitOfWork())
			{
				var table = GetDataTableFromExcel("D:\\Work\\Детский отдых\\загс\\zagz.xlsx");
				foreach (var row in table)
				{
					var id = row[0].LongParse();
					var exch = uv.GetById<ExchangeBaseRegistry>(id);
					if (exch != null)
					{
						if ((exch.IsProcessed && exch.Success) || exch.ExchangeBaseRegistryTypeId != (long)ExchangeBaseRegistryTypeEnum.Relationship || exch.NotActual)
						{
							continue;
						}

						var responseGuid = Guid.NewGuid();
						if (row[10] == "NULL")
						{
							exch.ResponseText = string.Format(responseErrorTemplate, responseGuid, exch.ServiceNumber,
								exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(), exch.ServiceNumber);
						}
						else
						{
							exch.ResponseText = string.Format(responseSuccessTemplate, responseGuid, exch.ServiceNumber,
								exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(),
								exch.Child.DateOfBirth.DateTimeToXml(),
								exch.Child.PlaceOfBirth, exch.Child.FirstName, exch.Child.LastName, exch.Child.MiddleName,
								row[4].TryParseDateDdMmYyyy().DateTimeToXml(), row[2].NullEmpty(),
								row[1].NullEmpty(), row[3].NullEmpty(),
								row[8].TryParseDateDdMmYyyy().DateTimeToXml(), row[6].NullEmpty(),
								row[5].NullEmpty(), row[7].NullEmpty(), row[11].NullEmpty(), row[12].TryParseDateDdMmYyyy().DateTimeToXml(),
								row[13].NullEmpty(), row[9].NullEmpty(), row[10].NullEmpty(), string.Empty);
						}

						exch.IsProcessed = false;
						exch.ResponseDate = DateTime.Now;
						exch.ResponseGuid = responseGuid.ToString();

						uv.SaveChanges();
					}
				}
			}
		}
	}
}
