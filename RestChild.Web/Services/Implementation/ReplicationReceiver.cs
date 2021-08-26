using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using RestChild.Comon;
using RestChild.Comon.Dto.Addressing;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Services.Contract;

namespace RestChild.Web.Services.Implementation
{
	[ReplicationMessageInspector]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ReplicationReceiver : IReplicationReceiver
	{
		private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
		private const string AddressRegistryConst = "БТИ. Адресный реестр";
		private const string StreetRegistryConst = "БТИ. Классификатор улиц";
		//private const string DistrictRegistryConst = "БТИ. Классификатор административных округов";
		//private const string RegionRegistryConst = "БТИ. Классификатор районов";
		private readonly WebBtiDistrictsController _districtsController = WindsorHolder.Resolve<WebBtiDistrictsController>();

		//private readonly AddressController _addressController = WindsorContainerHolder.Resolve<AddressController>();
		private readonly WebBtiStreetsController _streetsController = WindsorHolder.Resolve<WebBtiStreetsController>();

		public XDocument PackageDocument { get; set; }

		public string receive_change(string xml)
		{
			int? packageId = null;
			try
			{
				SaveFileIfNeed(xml);
				PackageDocument = XDocument.Parse(xml);
				var schemaError = GetSchemaError();
				if (!string.IsNullOrEmpty(schemaError))
				{
					return $"FAIL::{schemaError}";
				}

				packageId = Convert.ToInt32(PackageDocument?.Root?.Attribute("id")?.Value ?? "0");

				//districtsController.UpdateDistricts(
				//	SelectByAction(DistrictRegistryConst, "ADDED").Where(s => s != null).Select(XmlToDistrict).ToList(),
				//	SelectByAction(DistrictRegistryConst, "MODIFIED").Where(s => s != null).Select(XmlToDistrict).ToList(),
				//	SelectByAction(DistrictRegistryConst, "DELETED").Where(s => s != null).Select(XmlToDistrict).ToList());

				//districtsController.UpdateRegions(
				//	SelectByAction(RegionRegistryConst, "ADDED").Where(s => s != null).Select(XmlToRegion).ToList(),
				//	SelectByAction(RegionRegistryConst, "MODIFIED").Where(s => s != null).Select(XmlToRegion).ToList(),
				//	SelectByAction(RegionRegistryConst, "DELETED").Where(s => s != null).Select(XmlToRegion).ToList());

				_streetsController.UpdateStreets(
					SelectByAction(StreetRegistryConst, "ADDED").Where(s => s != null).Select(XmlToStreet).ToList(),
					SelectByAction(StreetRegistryConst, "MODIFIED").Where(s => s != null).Select(XmlToStreet).ToList(),
					SelectByAction(StreetRegistryConst, "DELETED").Where(s => s != null).Select(XmlToStreet).ToList());

				_streetsController.UpdateAddresses(
					SelectByAction(AddressRegistryConst, "ADDED").Where(s => s != null).Select(XmlToAddress).ToList(),
					SelectByAction(AddressRegistryConst, "MODIFIED").Where(s => s != null).Select(XmlToAddress).ToList(),
					SelectByAction(AddressRegistryConst, "DELETED").Where(s => s != null).Select(XmlToAddress).ToList());

				return $"OK:{packageId}";
			}
			catch (Exception ex)
			{
				return $"FAIL:{(packageId != null ? packageId.ToString() : string.Empty)}:{ex.Message}";
			}
			finally
			{
				WindsorHolder.Release(_streetsController);
				WindsorHolder.Release(_districtsController);
			}
		}

		private XElement[] SelectByAction(string catalogName, string action)
		{
			return (from doc in PackageDocument.Descendants("data")
				// ReSharper disable once PossibleNullReferenceException
				where doc.Attribute("action").Value.Equals(action) && doc.Parent.Attribute("name").Value.Equals(catalogName)
				select doc).ToArray();
		}

		private DateTime? ParseDateTime(string s)
		{
			DateTime d;
			if (DateTime.TryParseExact(s, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
			{
				return d;
			}
			return null;
		}

		private string ExtractValue(XElement doc, string atribute)
		{
			try
			{
				return (from x in doc.Elements("attribute").Where(e => e.Attribute("name").Value.Equals(atribute))
					select x.Element("values")?.Element("value")?.Value).FirstOrDefault();
			}
			catch
			{
				return null;
			}
		}

		private long? ExtractValueLong(XElement doc, string atribute)
		{
			var v = ExtractValue(doc, atribute);
			if (!string.IsNullOrEmpty(v))
			{
				long res;
				if (long.TryParse(v, out res))
				{
					return res;
				}
			}

			return null;
		}


		private int? ExtractValueInt(XElement doc, string atribute)
		{
			var v = ExtractValue(doc, atribute);
			if (!string.IsNullOrEmpty(v))
			{
				int res;
				if (int.TryParse(v, out res))
				{
					return res;
				}
			}

			return null;
		}

		private DateTime? ExtractValueDateTime(XElement doc, string atribute)
		{
			return ParseDateTime(ExtractValue(doc, atribute));
		}

		private StreetBtiDTO XmlToStreet(XElement doc)
		{
			if (!doc.HasElements)
			{
				return null;
			}
			return new StreetBtiDTO
			{
				Id = ExtractValue(doc, "KOD_GIVZ").IntParse() ?? 0,
				Name = ExtractValue(doc, "NMDOC2")
			};
		}

		//private DistrictBtiDTO XmlToDistrict(XElement doc)
		//{
		//	if (!doc.HasElements)
		//	{
		//		return null;
		//	}

		//	return new DistrictBtiDTO
		//	{
		//		Id = ExtractValue(doc, "KOD").IntParse() ?? 0,
		//		Name = ExtractValue(doc, "NM"),
		//		Givz = ExtractValueInt(doc, "GIVC")
		//	};
		//}

		//private RegionBtiDTO XmlToRegion(XElement doc)
		//{
		//	if (!doc.HasElements)
		//	{
		//		return null;
		//	}

		//	return new RegionBtiDTO
		//	{
		//		Id = ExtractValue(doc, "KOD").IntParse() ?? 0,
		//		Name = ExtractValue(doc, "NM"),
		//		Givz = ExtractValueInt(doc, "GIVZ")
		//	};
		//}

		//private FeaturesBuildBtiDTO XmlToFeatures(XElement doc)
		//{
		//	if (!doc.HasElements)
		//	{
		//		return null;
		//	}

		//	return new FeaturesBuildBtiDTO
		//	{
		//		Id = ExtractValue(doc, "KOD").IntParse() ?? 0,
		//		UNOM = ExtractValueLong(doc, "UNOM"),
		//		Kl = ExtractValueLong(doc, "KL"),
		//		Naz = ExtractValueLong(doc, "NAZ"),
		//		Sost = ExtractValueLong(doc, "SOST"),
		//		Dtsost = ExtractValueDateTime(doc, "DTSOST"),
		//		Tehpasp = ExtractValueLong(doc, "TEHPASP"),
		//		Samovol = ExtractValueLong(doc, "SAMOVOL"),
		//		Avarzd = ExtractValueLong(doc, "AVARZD"),
		//		Dtavarzd = ExtractValueDateTime(doc, "DTAVARZD"),
		//		Otskorp = ExtractValueLong(doc, "OTSKORP"),
		//		Bti = ExtractValueLong(doc, "BTI"),
		//		Kat = ExtractValueLong(doc, "KAT"),
		//		Mst = ExtractValueLong(doc, "MST"),
		//		Kap = ExtractValueLong(doc, "KAP"),
		//		Et = ExtractValueLong(doc, "ET"),
		//		Gdpostr = ExtractValueLong(doc, "GDPOSTR"),
		//		Gddo1917 = ExtractValueLong(doc, "GDDO1917"),
		//		Pdzq = ExtractValueLong(doc, "PDZQ"),
		//		OplG = ExtractValue(doc, "OPL_G").DecimalParse(),
		//		OplN = ExtractValue(doc, "OPL_N").DecimalParse(),
		//		Ser = ExtractValueLong(doc, "SER"),
		//		Prkor = ExtractValueLong(doc, "PRKOR"),
		//		LiftPass = ExtractValueLong(doc, "LFPQ"),
		//		LiftPassGrz = ExtractValueLong(doc, "LFGPQ"),
		//		LiftGrz = ExtractValueLong(doc, "LFGQ")
		//	};
		//}

		private AddressBtiDTO XmlToAddress(XElement doc)
		{
			if (!doc.HasElements)
			{
				return null;
			}
			var ul = ExtractValueInt(doc, "UL");
			var dto = new AddressBtiDTO
			{
				Id = long.Parse(ExtractValue(doc, "ID")),
				UNOM = ExtractValueLong(doc, "UNOM"),
				UNAD = ExtractValueInt(doc, "UNAD"),
				Ul = ul.HasValue ? new StreetBtiDTO {Id = ul.Value} : null,
				Dmt = ExtractValue(doc, "DMT"),
				Vld = ExtractValueInt(doc, "VLD"),
				Krt = ExtractValue(doc, "KRT"),
				Strt = ExtractValue(doc, "STRT"),
				Lit = ExtractValueInt(doc, "LIT"),
				Soor = ExtractValueInt(doc, "SOOR"),
				Status = ExtractValueInt(doc, "STATUS"),
				Tdoc = ExtractValueInt(doc, "TDOC"),
				Ndoc = ExtractValue(doc, "NDOC"),
				Ddoc = ExtractValueDateTime(doc, "DDOC"),
				Sdoc = ExtractValueInt(doc, "SDOC"),
				Nreg = ExtractValueInt(doc, "NREG"),
				Dreg = ExtractValueDateTime(doc, "DREG"),
				DopAddr = ExtractValueInt(doc, "DOP_ADR"),
				District = new DistrictBtiDTO {Id = ExtractValueInt(doc, "AOK") ?? 0},
				Region = new RegionBtiDTO {Id = ExtractValueInt(doc, "MR") ?? 0}
			};

			if (dto.District.Id == 0)
			{
				dto.District = null;
			}

			if (dto.Region.Id == 0)
			{
				dto.Region = null;
			}

			dto.DmtKrtLit = string.Empty;
			if (!string.IsNullOrEmpty(dto.Dmt))
			{
				dto.DmtKrtLit += (dto.Vld == 1
					? "владение "
					: dto.Vld == 3 ? "домовладение " : "дом ") + dto.Dmt;
			}

			if (!string.IsNullOrEmpty(dto.Krt))
			{
				if (!string.IsNullOrEmpty(dto.DmtKrtLit))
				{
					dto.DmtKrtLit += ", ";
				}

				dto.DmtKrtLit += "корпус " + dto.Krt;
			}

			if (!string.IsNullOrEmpty(dto.Strt))
			{
				if (!string.IsNullOrEmpty(dto.DmtKrtLit))
				{
					dto.DmtKrtLit += ", ";
				}

				if (dto.Soor == 1)
				{
					dto.DmtKrtLit += "сооружение ";
				}
				else if (dto.Soor == 2)
				{
					dto.DmtKrtLit += "строение ";
				}

				dto.DmtKrtLit += dto.Strt;
			}

			dto.Name = string.Empty;

			if (dto.Region != null && dto.Ul == null)
			{
				//var region = _addressController.GetRegionBtiById(dto.Region.Id);
				//dto.Name += "район " + region.Name;
			}

			if (dto.Ul != null)
			{
				if (!string.IsNullOrEmpty(dto.Name))
				{
					dto.Name += ", ";
				}

				dto.Name += dto.Ul.Name;
			}

			if (!string.IsNullOrEmpty(dto.Name) && !string.IsNullOrEmpty(dto.DmtKrtLit))
			{
				dto.Name += ", ";
			}

			dto.Name += dto.DmtKrtLit;

			return dto;
		}

		public string GetSchemaError()
		{
			string error = null;
			var schemas = new XmlSchemaSet();
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
				"RestChild.Web.Services.ReplicationMessage.xsd"))
			{
				// ReSharper disable once AssignNullToNotNullAttribute
				schemas.Add(string.Empty, XmlReader.Create(new StreamReader(stream)));
			}

			PackageDocument.Validate(schemas, (o, e) => { error = e.Message; });
			return error;
		}

		private void SaveFileIfNeed(string xml)
		{
			try
			{
				var path = ConfigurationManager.AppSettings["SaveReplXmlFilesPath"];
				if (string.IsNullOrEmpty(path)) return;
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);
				File.WriteAllText(Path.Combine(path, $@"soap{DateTime.Now:ddMMyyHHmmss}_{Guid.NewGuid()}.xml"),
					xml, Encoding.UTF8);
			}
			catch
			{
				// ignored
			}
		}
	}
}
