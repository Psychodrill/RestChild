using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.ConsoleProjects
{
	public class FreeBookings
	{
		private static long[] exchangeUtsIds = new long[]
		{
			1270300,
			1321970,
			1298674,
			1326793,
			1332906,
			1269833,
			1270096,
			1322275,
			1322273,
			1326703,
			1332160,
			1326785,
			1331962,
			1258659,
			1321985,
			1329707,
			1326591,
			1330012,
			1318579,
			1281386,
			1332098,
			1251892,
			1280896,
			1332158,
			1309988,
			1322066,
			1324792,
			1321997,
			1322833,
			1331955,
			1332237,
			1268468,
			1321887,
			1332307,
			1326334,
			1320964,
			1332309,
			1321983,
			1314226,
			1251276,
			1280788,
			1251476,
			1326204,
			1320627,
			1270098,
			1320736,
			1326736,
			1326593,
			1251857,
			1272012,
			1278706,
			1326248,
			1331496,
			1332315,
			1332564,
			1319814,
			1309558,
			1332199,
			1326728,
			1331566,
			1270355,
			1332584,
			1326137,
			1332534,
			1326799,
			1321972,
			1332281,
			1326795,
			1321993,
			1308698,
			1326299,
			1332060,
			1322027,
			1318901,
			1321425,
			1326326,
			1333147,
			1314909,
			1332304,
			1321551,
			1307738,
			1290327,
			1326331,
			1326357,
			1318312,
			1325066,
			1321944,
			1326791,
			1332560,
			1332891,
			1321977,
			1332318,
			1332156,
			1332154,
			1321895,
			1326783,
			1326732,
			1326734,
			1321286,
			1326801,
			1242363,
			1301913,
			1321420,
			1326140,
			1321494,
			1307229,
			1299541,
			1301915,
			1301908,
			1332103,
			1326691,
			1326152,
			1332589,
			1326738,
			1325915,
			1326689,
			1321995,
			1264494,
			1326797,
			1320310,
			1326371,
			1332062,
			1321991,
			1307767,
			1332311,
			1326807,
			1332730,
			1312434,
			1332197,
			1318878,
			1332562,
			1326142,
			1314811,
			1332106,
			1326103,
			1326831,
			1309048,
			1311203,
			1326805,
			1251098,
			1329691,
			1302780,
			1326047,
			1332313,
			1309352,
			1326578,
			1326572,
			1278547,
			1326604,
			1313813,
			1326803,
			1321387,
			1324970,
			1333155,
			1332128,
			1238170,
			1238024,
			1253742,
			1253733,
			1253728,
			1332252,
			1332294,
			1332043,
			1333109,
			1331957,
			1331038,
			1332592,
			1331995,
			1332292,
			1332271,
			1333153,
			1333100,
			1332594,
			1333157,
			1332247,
			1332108,
			1333133,
			1332899,
			1331871,
			1332208,
			1333131,
			1333098,
			1333129,
			1331985,
			1333118
		};

		public static void ProcessFreeBooking()
		{
			using (var uw = new UnitOfWork())
			{
				var client = new BookingForCall.BookingServiceClient();

				foreach (var id in exchangeUtsIds)
				{
					var exchangeUts = uw.GetById<ExchangeUTS>(id);
					if (exchangeUts == null)
					{
						continue;
					}

					if (exchangeUts.Request.StatusId != 1090)
					{
						continue;
					}

					try
					{
						var releaseRequest = new Comon.Dto.Booking.BookingRequest
						{
							TypeOfRestId = exchangeUts.TypeOfRestId ?? 0,
							BookingGuid = exchangeUts.BookingGuid
						};

						var res = client.ReleaseBooking(releaseRequest);
						if (res.IsError)
						{
							exchangeUts.IsErrorOnReleaseBooking = true;
							Console.WriteLine(
								$"Не произошло снятие бронирования. BookingGuid={exchangeUts.BookingGuid}, MessageId={exchangeUts.MessageId}, Error={res.ErrorMessage}");
							uw.SaveChanges();
						}
					}
					catch (Exception ex)
					{
						exchangeUts.IsErrorOnReleaseBooking = true;
						Console.WriteLine($"Не произошло снятие бронирования. BookingGuid={exchangeUts.BookingGuid}, MessageId={exchangeUts.MessageId}");
						Console.WriteLine(ex.Message);
					}
				}
			}
		}

		/*
			select 'new Tuple<long, long?, Guid?>(' + cast(b.Id as varchar) + ', ' +
			CASE WHEN b.TypeOfRestId IS NULL THEN '0' ELSE cast(b.TypeOfRestId as varchar) END + ', ' +
			CASE WHEN b.Code IS NULL THEN 'null' ELSE 'Guid.Parse("' + cast(b.Code as varchar(36)) END + '")),'
			from Booking b where b.RequestId is null and b.Canceled = 0 order by Id;
		*/
		private static List<Tuple<long, long?, Guid?>> bookingToFree = new List<Tuple<long, long?, Guid?>>
		{
			new Tuple<long, long?, Guid?>(206141, 11, Guid.Parse("EE8D022F-3644-422F-92F1-D71EA102E116")),
			new Tuple<long, long?, Guid?>(206143, 11, Guid.Parse("7FDCC5D9-7751-4AE3-A0F2-245939B4E6D4")),
			new Tuple<long, long?, Guid?>(206144, 11, Guid.Parse("0F4FB7FD-E983-4F50-B838-8C3721FA52CE")),
			new Tuple<long, long?, Guid?>(206145, 11, Guid.Parse("9330EDC1-32B3-4B1B-ACDE-B68FC10E747A")),
			new Tuple<long, long?, Guid?>(206146, 11, Guid.Parse("882E8DA0-0C25-42FA-91F8-B7545DB0AE4B")),
			new Tuple<long, long?, Guid?>(206147, 11, Guid.Parse("7DC6F9C0-1D90-412B-8C8A-FBFD3BC6F74D")),
			new Tuple<long, long?, Guid?>(206148, 11, Guid.Parse("37F9DAC7-0E3F-4CC1-841C-15A6FC291727")),
			new Tuple<long, long?, Guid?>(206149, 11, Guid.Parse("2C4716CB-3D70-4F52-84AD-C8A5750FCD48")),
			new Tuple<long, long?, Guid?>(206150, 11, Guid.Parse("8AA49288-5A62-49E2-9A70-626DB1BF1693")),
			new Tuple<long, long?, Guid?>(206151, 11, Guid.Parse("FADDF833-E4C2-4AE0-B3D4-51FE40D1682C")),
			new Tuple<long, long?, Guid?>(206152, 11, Guid.Parse("4341DA02-2F29-45E9-9021-E275EDE9859E")),
			new Tuple<long, long?, Guid?>(206153, 11, Guid.Parse("9A806830-074E-46FF-8757-3EBC9662FA22")),
			new Tuple<long, long?, Guid?>(206154, 11, Guid.Parse("30C1CAAA-DF84-4C29-85BB-3088EF7841DE")),
			new Tuple<long, long?, Guid?>(206155, 11, Guid.Parse("835D49B7-BFF4-41EB-B592-2F14BD2A3AD1")),
			new Tuple<long, long?, Guid?>(206156, 11, Guid.Parse("50B30B4F-51CB-40D0-87EF-5AB0B97C063D")),
			new Tuple<long, long?, Guid?>(206157, 11, Guid.Parse("271EDC90-2CC7-4C68-8C0B-7412DC4B0EB2")),
			new Tuple<long, long?, Guid?>(206158, 11, Guid.Parse("84BCEE16-F665-4F09-979B-394136715BE2")),
			new Tuple<long, long?, Guid?>(206159, 11, Guid.Parse("70BF16C3-FC5F-4BA4-BA6D-7B5B7E942604")),
			new Tuple<long, long?, Guid?>(206160, 11, Guid.Parse("42DF2444-A2C6-466D-960F-EA825CCBD002")),
			new Tuple<long, long?, Guid?>(206161, 11, Guid.Parse("95483C7B-B3DB-41A7-A18F-86D345D2483A")),
			new Tuple<long, long?, Guid?>(206162, 11, Guid.Parse("5DEAA12C-F359-4740-A489-36DC3D2C7E69")),
			new Tuple<long, long?, Guid?>(206163, 11, Guid.Parse("AAFB049B-44D1-495A-95A6-B5F2D4DF53B3")),
			new Tuple<long, long?, Guid?>(206164, 11, Guid.Parse("CE8016B8-FE63-425D-9A9C-D75EC4528814")),
			new Tuple<long, long?, Guid?>(206165, 11, Guid.Parse("65695B85-0F05-4641-8007-78E56C8A73B9")),
			new Tuple<long, long?, Guid?>(206166, 11, Guid.Parse("10500FC0-073C-498F-AC83-916CA168953B")),
			new Tuple<long, long?, Guid?>(206167, 11, Guid.Parse("90356561-94E7-413E-B7A7-B9F700D42A74")),
			new Tuple<long, long?, Guid?>(206168, 11, Guid.Parse("6AFFFE04-DC66-4B56-B408-AA5B1C88136D")),
			new Tuple<long, long?, Guid?>(206169, 11, Guid.Parse("E043E5AF-F9D6-47E7-B91E-ACFDD38D89A9")),
			new Tuple<long, long?, Guid?>(206170, 11, Guid.Parse("72155C0C-FF87-4044-9FAF-AB81B5F50ADD")),
			new Tuple<long, long?, Guid?>(206171, 11, Guid.Parse("D62629E1-86F0-4D9F-9F92-563B6E2CE7D3")),
			new Tuple<long, long?, Guid?>(206172, 11, Guid.Parse("32CF1BEB-6B83-45B8-8E0D-AEF503C0DE3E")),
			new Tuple<long, long?, Guid?>(206173, 11, Guid.Parse("15293312-9818-48D2-9DF7-82BB194FA1C3")),
			new Tuple<long, long?, Guid?>(206174, 11, Guid.Parse("DC166146-4179-4B0C-94AC-109C9A836C54")),
			new Tuple<long, long?, Guid?>(206175, 11, Guid.Parse("666B1317-A98E-4856-AA30-6B4D4954DF57")),
			new Tuple<long, long?, Guid?>(206176, 11, Guid.Parse("FE490EBF-4612-40CC-8776-9065DE9E0E4E")),
			new Tuple<long, long?, Guid?>(206177, 11, Guid.Parse("A5FF0639-9702-4FEB-9BA2-00ADCA7A087B")),
			new Tuple<long, long?, Guid?>(206178, 11, Guid.Parse("61AE6B35-670C-4456-9D8B-1D6C2E92A4D9")),
			new Tuple<long, long?, Guid?>(206179, 11, Guid.Parse("FB98D3CF-397B-4817-8E61-D90303890B61")),
			new Tuple<long, long?, Guid?>(206180, 11, Guid.Parse("B6935C87-D9C4-4D7F-95E3-D6D0D43BF22E")),
			new Tuple<long, long?, Guid?>(206181, 11, Guid.Parse("8C16E625-BF03-40E6-B3D7-A415DF449D1F")),
			new Tuple<long, long?, Guid?>(206182, 11, Guid.Parse("16ACB9A3-ECA4-47CE-89E5-1A4754B4AD8C")),
			new Tuple<long, long?, Guid?>(206183, 11, Guid.Parse("415606A4-B32B-49B8-AD50-6821B8C10477")),
			new Tuple<long, long?, Guid?>(206184, 11, Guid.Parse("CDB3D613-77DE-40F1-B9E6-23D37893602D")),
			new Tuple<long, long?, Guid?>(206185, 11, Guid.Parse("A7EF812B-52E5-44FF-8503-85664C9FFA71")),
			new Tuple<long, long?, Guid?>(206186, 11, Guid.Parse("85F6FD02-B6AA-45A5-9783-6B98413B0194")),
			new Tuple<long, long?, Guid?>(206187, 11, Guid.Parse("CF6B0737-250F-4E41-8753-F1D78D2EA46A")),
			new Tuple<long, long?, Guid?>(206188, 11, Guid.Parse("562AAE29-F70A-49D4-B155-F51250BCAC69")),
			new Tuple<long, long?, Guid?>(206189, 11, Guid.Parse("FC832827-7BB7-40F8-A898-395EE713C233")),
			new Tuple<long, long?, Guid?>(206190, 11, Guid.Parse("E630EEA3-6C3A-40C3-9632-CE011B4423B7")),
			new Tuple<long, long?, Guid?>(206191, 11, Guid.Parse("B49DC871-7E2F-487D-B025-7D109F487CB3")),
			new Tuple<long, long?, Guid?>(206192, 11, Guid.Parse("55913341-E131-447D-B1E5-787AA7D2FADC")),
			new Tuple<long, long?, Guid?>(206193, 11, Guid.Parse("422C07A9-69AC-43F0-A0BA-6BA7A8812683")),
			new Tuple<long, long?, Guid?>(206194, 11, Guid.Parse("45D2D111-077C-4466-9B22-1AC17EA658CD")),
			new Tuple<long, long?, Guid?>(206195, 11, Guid.Parse("A087F28A-92F4-45F2-AC71-03B3BB931B9C")),
			new Tuple<long, long?, Guid?>(206196, 11, Guid.Parse("90EF1439-841E-4F8D-B880-94ECA19254FA")),
			new Tuple<long, long?, Guid?>(206197, 11, Guid.Parse("A83AB051-331F-4935-A6C2-08FF440D5D96")),
			new Tuple<long, long?, Guid?>(206198, 11, Guid.Parse("0BF76FE4-97B0-4E5C-8039-2215919C695C")),
			new Tuple<long, long?, Guid?>(206199, 11, Guid.Parse("9967F2DE-E3CF-4D61-8EB9-AFAE98D7A49F")),
			new Tuple<long, long?, Guid?>(206200, 11, Guid.Parse("7EEEB85B-0737-4B60-A81B-14070502325D")),
			new Tuple<long, long?, Guid?>(206201, 11, Guid.Parse("5A82976D-BE53-4695-9312-B3C8D445944B")),
			new Tuple<long, long?, Guid?>(206202, 11, Guid.Parse("624728BD-7A97-4DC4-A564-0361E048A8D0")),
			new Tuple<long, long?, Guid?>(206203, 11, Guid.Parse("CC9FE306-DFCF-4673-896A-4DF54C86B86E")),
			new Tuple<long, long?, Guid?>(206204, 11, Guid.Parse("C58EE0B0-E409-47F8-A4A7-24D04967DEE6")),
			new Tuple<long, long?, Guid?>(206205, 11, Guid.Parse("6D6EF037-1AA9-4150-961A-9FDE6B7DFB26")),
			new Tuple<long, long?, Guid?>(206206, 11, Guid.Parse("CA277EA1-62BA-4285-A8D6-290AA33B4D9A")),
			new Tuple<long, long?, Guid?>(206207, 11, Guid.Parse("E43E16DA-F723-4963-A4B6-4E2614E587F7")),
			new Tuple<long, long?, Guid?>(206208, 11, Guid.Parse("2A6A7CDB-4B75-415B-893F-2240425849B9")),
			new Tuple<long, long?, Guid?>(206209, 11, Guid.Parse("3BB4BA60-8271-41CC-A762-69612484E847")),
			new Tuple<long, long?, Guid?>(206210, 11, Guid.Parse("378105B9-BD23-4C68-81B9-E7A0549BF2F4")),
			new Tuple<long, long?, Guid?>(206211, 11, Guid.Parse("5B954773-6ACB-4CAD-A2F6-FA0427EA88E8")),
			new Tuple<long, long?, Guid?>(206212, 11, Guid.Parse("2D74C9D1-C3DF-4C4E-8F69-F437C835F2B6")),
			new Tuple<long, long?, Guid?>(206213, 11, Guid.Parse("DF2C02AA-1781-4CE5-91FE-D43B42E6B99F")),
			new Tuple<long, long?, Guid?>(206214, 11, Guid.Parse("33753C5D-C6FB-4014-8262-749B67C73593")),
			new Tuple<long, long?, Guid?>(206215, 11, Guid.Parse("F04C8C2F-5186-4956-A43D-63792C9DC41F")),
			new Tuple<long, long?, Guid?>(206216, 11, Guid.Parse("85490F42-D52D-4789-A8B7-719CDC6D2C39")),
			new Tuple<long, long?, Guid?>(206217, 11, Guid.Parse("127A1799-BCA6-41C2-BD9B-A03852E50123")),
			new Tuple<long, long?, Guid?>(206218, 11, Guid.Parse("83EE7FE4-D686-4F3B-89E3-F560647E327C")),
			new Tuple<long, long?, Guid?>(206219, 11, Guid.Parse("D5CD6D66-62CD-444E-9F7C-FBA651584830")),
			new Tuple<long, long?, Guid?>(206220, 11, Guid.Parse("E3AD83F4-6BC4-43E6-B61D-5191771DFCA9")),
			new Tuple<long, long?, Guid?>(206221, 11, Guid.Parse("BCCD1302-F58F-4638-A74A-8626A387292D")),
			new Tuple<long, long?, Guid?>(206222, 11, Guid.Parse("DA3296CD-B698-4E56-A3BE-D64448F6A4FF")),
			new Tuple<long, long?, Guid?>(206223, 11, Guid.Parse("D677F535-88EB-4CE5-82E5-B0D719093512")),
			new Tuple<long, long?, Guid?>(206224, 11, Guid.Parse("DEF3E2C0-1865-4713-8C72-33A7743CDE20")),
			new Tuple<long, long?, Guid?>(206225, 11, Guid.Parse("7FA00EC2-D15B-400F-BA40-0ECBAFE40EFF")),
			new Tuple<long, long?, Guid?>(206226, 11, Guid.Parse("D6D6B4D2-F2C8-4C70-BB32-F72A2DB2E6DC")),
			new Tuple<long, long?, Guid?>(206227, 11, Guid.Parse("C493C4CC-7D87-4796-A2D3-8ADB82977392")),
			new Tuple<long, long?, Guid?>(206228, 11, Guid.Parse("B9748B45-A00C-43CB-8DAC-60A89CB0DABF")),
			new Tuple<long, long?, Guid?>(206229, 11, Guid.Parse("6C0980BF-82B4-4B24-B946-0CE233352F93")),
			new Tuple<long, long?, Guid?>(206230, 11, Guid.Parse("100157D7-5AFE-445A-8E3B-EEBA41E57531")),
			new Tuple<long, long?, Guid?>(206231, 11, Guid.Parse("4BE596BE-8BF8-4F77-9A26-079E78864C7A")),
			new Tuple<long, long?, Guid?>(206232, 11, Guid.Parse("B8435954-517D-408F-84AB-517C55C72EF5")),
			new Tuple<long, long?, Guid?>(206233, 11, Guid.Parse("994260AE-E703-46D3-858E-54BB1E814082")),
			new Tuple<long, long?, Guid?>(206234, 11, Guid.Parse("E335C447-6237-423F-9289-F63E415F0FF9")),
			new Tuple<long, long?, Guid?>(206235, 11, Guid.Parse("C4E323FE-3FDF-4210-8704-6EC4C492DE18")),
			new Tuple<long, long?, Guid?>(206236, 11, Guid.Parse("AED1CFB2-5BFE-42F5-9CFB-4A8E26660343")),
			new Tuple<long, long?, Guid?>(206237, 11, Guid.Parse("5403D2A3-5F10-44F2-8DCF-47C3B55D3342")),
			new Tuple<long, long?, Guid?>(206238, 11, Guid.Parse("B9696733-1B24-4C38-BFBD-90EDC8DF256B")),
			new Tuple<long, long?, Guid?>(206239, 11, Guid.Parse("B471BDB5-1F93-43F5-8864-07583A9673EF")),
			new Tuple<long, long?, Guid?>(206240, 11, Guid.Parse("3E8FF3B4-FBE3-442D-A07E-504CE50427F1")),
			new Tuple<long, long?, Guid?>(206241, 11, Guid.Parse("3E82541D-74C6-4E48-A4DB-AF798F102796")),
			new Tuple<long, long?, Guid?>(206242, 11, Guid.Parse("59474AB3-0E8C-4C0C-9A63-B3A1E58F4F25")),
			new Tuple<long, long?, Guid?>(206243, 11, Guid.Parse("7B59E6F0-DA41-4434-8CB3-CBAC8EDBE226")),
			new Tuple<long, long?, Guid?>(206244, 11, Guid.Parse("99C1A6C9-77DA-4D4F-A554-7A088C6111C9")),
			new Tuple<long, long?, Guid?>(206245, 11, Guid.Parse("F327C38F-21CF-48E1-9447-3CF70BD32448")),
			new Tuple<long, long?, Guid?>(206246, 11, Guid.Parse("E965340A-BC63-4E55-A129-9F863628732A")),
			new Tuple<long, long?, Guid?>(206247, 11, Guid.Parse("097BCA4E-73C7-420F-98D4-ADA7589E062F")),
			new Tuple<long, long?, Guid?>(206248, 11, Guid.Parse("16A1E54F-EE53-4F18-8C28-9EA16527DBB7")),
			new Tuple<long, long?, Guid?>(206249, 11, Guid.Parse("A6B4460A-C9B8-4F01-B668-D8EDE02D4FCE")),
			new Tuple<long, long?, Guid?>(206250, 11, Guid.Parse("18B4070D-E0C9-4139-B4A3-F530A4250BFF")),
		};

		public static void ProcessFreeBooking2()
		{
			using (var client = new BookingForCall.BookingServiceClient())
			{
				foreach (var booking in bookingToFree)
				{
					try
					{
						var releaseRequest = new Comon.Dto.Booking.BookingRequest
						{
							TypeOfRestId = booking.Item2,
							BookingGuid = booking.Item3
						};

						var res = client.ReleaseBooking(releaseRequest);
						if (res.IsError)
						{
							Console.WriteLine($"Не произошло снятие бронирования. BookingId={booking.Item1}, BookingGuid={booking.Item3}, Error={res.ErrorMessage}");
						}
					}
					catch
					{
						Console.WriteLine($"Не произошло снятие бронирования. BookingId={booking.Item1}, BookingGuid={booking.Item3}");
					}
				}
			}
		}
	}
}
