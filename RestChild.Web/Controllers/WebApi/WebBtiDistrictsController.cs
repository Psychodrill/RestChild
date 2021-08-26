using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DocumentFormat.OpenXml.Wordprocessing;

using RestChild.Comon;
using RestChild.Comon.Dto.Addressing;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebBtiDistrictsController : BaseController
	{
		[Route("api/WebBtiDistricts")]
		public IEnumerable<BtiDistrict> Get()
		{
			return UnitOfWork.GetSet<BtiDistrict>()
				.OrderBy(x => x.Name)
				.ToList();
		}

		[Route("api/WebBtiDistricts/{id}/regions")]
		public IEnumerable<BtiRegion> GetRegions(long id)
		{
			return UnitOfWork.GetSet<BtiRegion>()
				.Where(x => x.BtiDistrictId == id)
				.OrderBy(x => x.Name)
				.ToList();
		}

		[Route("api/WebBtiDistricts/regions")]
		public IEnumerable<BtiRegion> GetAllRegions()
		{
			return UnitOfWork.GetSet<BtiRegion>()
				.OrderBy(x => x.Name)
				.ToList();
		}

		internal void UpdateDistricts(
			ICollection<DistrictBtiDTO> added,
			ICollection<DistrictBtiDTO> modified,
			ICollection<DistrictBtiDTO> deleted)
		{
			InsertOrModifyEntities<BtiDistrict, DistrictBtiDTO>(added, modified,
				(src, dst) =>
				{
					if (dst.Id == 0)
					{
						dst.Id = src.Id;
					}
					dst.Name = src.Name;
					dst.Givz = src.Givz ?? 0;
				});

			if (deleted != null && deleted.Any())
			{
				foreach (var districtForDelete in deleted)
				{
					UnitOfWork.AddEntity(new SendEmailAndSms()
											{
												IsSmsSended = true,
												DateCreate = DateTime.Now,
												EmailTitle = "Удаление округа из реестра БТИ",
												EmailMessage = string.Format("Запрос на удаление округа из реестра БТИ (Id: {0})", districtForDelete.Id),
												Email = Properties.Settings.Default.EmailForReplicationReceiverMessages
											});
					log4net.LogManager.GetLogger("Service").InfoFormat("Запрос на удаление округа из реестра БТИ (Id: {0})", districtForDelete.Id);
				}

			}
			UnitOfWork.SaveChanges();
		}

		internal void UpdateRegions(
			ICollection<RegionBtiDTO> added,
			ICollection<RegionBtiDTO> modified,
			ICollection<RegionBtiDTO> deleted)
		{
			InsertOrModifyEntities<BtiRegion, RegionBtiDTO>(added, modified,
				(src, dst) =>
				{
					if (dst.Id == 0)
					{
						dst.Id = src.Id;
					}
					dst.Name = src.Name;
					dst.Givz = src.Givz ?? 0;
				});

			if (deleted != null && deleted.Any())
			{
				foreach (var regionForDelete in deleted)
				{
					UnitOfWork.AddEntity(new SendEmailAndSms()
											{
												IsSmsSended = true,
												DateCreate = DateTime.Now,
												EmailTitle = "Удаление района из реестра БТИ",
												EmailMessage = string.Format("Запрос на удаление района из реестра БТИ (Id: {0})", regionForDelete.Id),
												Email = Properties.Settings.Default.EmailForReplicationReceiverMessages
											});
					log4net.LogManager.GetLogger("Service").InfoFormat("Запрос на удаление района из реестра БТИ (Id: {0})", regionForDelete.Id);
				}

			}
			UnitOfWork.SaveChanges();
		}
	}
}
