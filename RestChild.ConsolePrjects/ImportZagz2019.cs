using System;
using System.Data.Entity;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.ConsoleProjects
{
   public static class ImportZagz2019
   {
      public static void LoadData()
      {
         using (var uw = new UnitOfWork())
         {
            foreach (var id in ImportZagz2019Data.Ids)
            {
               var exch = uw.GetById<ExchangeBaseRegistry>(id);

               if (exch.ExchangeBaseRegistryTypeId != 22)
               {
                  Console.WriteLine("Не верные данные для обновления");
                  return;
               }

               if (exch.Success)
               {
                  continue;
               }

               exch.Success = true;

               exch.ResponseText = $@"<?xml version=""1.0"" encoding=""utf-16""?>
<CoordinateStatusMessage xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
	<ServiceHeader>
		<FromOrgCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1111</FromOrgCode>
		<ToOrgCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">2064</ToOrgCode>
		<MessageId xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{exch.RequestGuid}</MessageId>
		<ServiceNumber xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{exch.ServiceNumber}</ServiceNumber>
		<RequestDateTime xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{exch.SendDate.DateTimeToXml()}</RequestDateTime>
	</ServiceHeader>
	<StatusMessage>
		<RequestId xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{exch.RequestGuid}</RequestId>
		<ResponseDate xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">{DateTime.Now.DateTimeToXml()}</ResponseDate>
		<PlanDate xsi:nil=""true"" xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/""/>
		<StatusCode xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">1004</StatusCode>
		<Documents xmlns=""http://asguf.mos.ru/rkis_gu/coordinate/v5/"">
			<ServiceDocument>
				<DocCode>22</DocCode>
				<DocDate>2018-11-08T10:07:22.444Z</DocDate>
				<ValidityPeriod>2018-11-14T10:07:22.444Z</ValidityPeriod>
				<ListCount>1</ListCount>
				<CopyCount>1</CopyCount>
				<CustomAttributes>
					<CertOfBirthResponse:CertOfBirthResponse id=""obj"" xmlns:CertOfBirthResponse=""http://nakhodka.ru/documentsservice/types"" xmlns=""http://nakhodka.ru/documentsservice/types"">
						<CertOfBirthResponse:Child>
							<CertOfBirthResponse:BirthDate>{exch.Child.DateOfBirth.DateTimeToXml()}</CertOfBirthResponse:BirthDate>
							<CertOfBirthResponse:BirthPlace>{exch.Child.PlaceOfBirth}</CertOfBirthResponse:BirthPlace>
							<CertOfBirthResponse:FirstName>{exch.Child.FirstName}</CertOfBirthResponse:FirstName>
							<CertOfBirthResponse:LastName>{exch.Child.LastName}</CertOfBirthResponse:LastName>
							<CertOfBirthResponse:Patronymic>{exch.Child.MiddleName}</CertOfBirthResponse:Patronymic>
						</CertOfBirthResponse:Child>
						<CertOfBirthResponse:Mother>
							<CertOfBirthResponse:BirthDate></CertOfBirthResponse:BirthDate>
							<CertOfBirthResponse:FirstName></CertOfBirthResponse:FirstName>
							<CertOfBirthResponse:LastName></CertOfBirthResponse:LastName>
							<CertOfBirthResponse:Patronymic></CertOfBirthResponse:Patronymic>
							<CertOfBirthResponse:Citizenship></CertOfBirthResponse:Citizenship>
						</CertOfBirthResponse:Mother>
						<CertOfBirthResponse:Father>
							<CertOfBirthResponse:BirthDate></CertOfBirthResponse:BirthDate>
							<CertOfBirthResponse:FirstName></CertOfBirthResponse:FirstName>
							<CertOfBirthResponse:LastName></CertOfBirthResponse:LastName>
							<CertOfBirthResponse:Patronymic></CertOfBirthResponse:Patronymic>
							<CertOfBirthResponse:Citizenship></CertOfBirthResponse:Citizenship>
						</CertOfBirthResponse:Father>
						<CertOfBirthResponse:ActRequisites>
							<CertOfBirthResponse:ActNumber>-</CertOfBirthResponse:ActNumber>
							<CertOfBirthResponse:ActDate>-</CertOfBirthResponse:ActDate>
							<CertOfBirthResponse:NameOfRegistrar>{exch.Child.DocumentSubjectIssue}</CertOfBirthResponse:NameOfRegistrar>
						</CertOfBirthResponse:ActRequisites>
						<CertOfBirthResponse:CertRequisites>
							<CertOfBirthResponse:CertSeries>{exch.Child.DocumentSeria}</CertOfBirthResponse:CertSeries>
							<CertOfBirthResponse:CertNumber>{exch.Child.DocumentNumber}</CertOfBirthResponse:CertNumber>
							<CertOfBirthResponse:CertDate>{exch.Child.DocumentDateOfIssue.DateTimeToXml()}</CertOfBirthResponse:CertDate>
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
               exch.ServiceNumber = "Загрузка данных";
               uw.SaveChanges();
               uw.Context.Entry(exch.Child).State = EntityState.Detached;
               uw.Context.Entry(exch).State = EntityState.Detached;
            }
         }
      }
   }
}
