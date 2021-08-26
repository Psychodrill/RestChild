using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Dto.Addressing;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebBtiStreetsController : BaseController
	{
		[Route("api/WebBtiStreets")]
		public IEnumerable<BtiStreet> Get(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				return new List<BtiStreet>();
			}

			var words = query.ToLower().Split();
			var dbquery = UnitOfWork.GetSet<BtiStreet>().AsQueryable();
			foreach (var word in words)
			{
				var wordCopy = word;
				dbquery = dbquery.Where(x => x.Name.ToLower().Contains(wordCopy));
			}
			return dbquery.OrderBy(x => x.Name.Length)
					.Take(Settings.Default.WebBtiStreetsResponseCount)
					.ToList();
		}

		[Route("api/WebBtiStreets/{id}/houses")]
		public IEnumerable<HouseViewModel> GetHouses(int id)
		{
			return UnitOfWork.GetSet<BtiAddress>()
				.Where(x => x.BtiStreetId == id && x.Status == (long)BtiAddressEnum.Main)
				.OrderBy(x => x.ShortAddress)
				.Select(x => new HouseViewModel
				{
					AddressId = x.Id,
					ShortAddress = x.ShortAddress,
					District = x.BtiDistrict != null ? x.BtiDistrict.Name : string.Empty,
					Region = x.BtiRegion != null ? x.BtiRegion.Name : string.Empty
				});
		}

		internal void UpdateStreets(
			ICollection<StreetBtiDTO> added,
			ICollection<StreetBtiDTO> modified,
			ICollection<StreetBtiDTO> deleted)
		{
			InsertOrModifyEntities<BtiStreet, StreetBtiDTO>(added, modified,
				(src, dst) =>
					{
						if (dst.Id == 0)
						{
							dst.Id = src.Id;
						}
						dst.Name = src.Name;
					});

			if (deleted != null && deleted.Any())
			{
				foreach (var streetForDelete in deleted)
				{
					UnitOfWork.AddEntity(new SendEmailAndSms()
											{
												IsSmsSended = true,
												DateCreate = DateTime.Now,
												EmailTitle = "Удаление улицы из реестра БТИ",
												EmailMessage = $"Запрос на удаление улицы из реестра БТИ (Id: {streetForDelete.Id})",
												Email = Settings.Default.EmailForReplicationReceiverMessages
											});
					log4net.LogManager.GetLogger("Service").InfoFormat("Запрос на удаление улицы из реестра БТИ (Id: {0})", streetForDelete.Id);
				}

			}

			UnitOfWork.SaveChanges();
		}

		protected void InsertOrModifyAddresses(ICollection<AddressBtiDTO> added, ICollection<AddressBtiDTO> modified, Action<AddressBtiDTO, BtiAddress> updateFunc)

		{
			var forAddOrModify = (added ?? new List<AddressBtiDTO>());
			forAddOrModify.AddRange(modified ?? new List<AddressBtiDTO>());
			foreach (var entity in forAddOrModify)
			{
				var found = UnitOfWork.GetSet<BtiAddress>().FirstOrDefault(s => s.Unom == (entity.UNOM??0) && s.Unod == (entity.UNAD??0)) ??
						UnitOfWork.GetSet<BtiAddress>().FirstOrDefault(s=>s.Id == entity.Id);
				if (found != null)
				{
					updateFunc(entity, found);
				}
				else
				{
					var obj = new BtiAddress();
					updateFunc(entity, obj);
					UnitOfWork.AddEntity(obj);
				}
			}
		}

		internal void UpdateAddresses(
			ICollection<AddressBtiDTO> added,
			ICollection<AddressBtiDTO> modified,
			ICollection<AddressBtiDTO> deleted)
		{
			InsertOrModifyAddresses(added, modified,
				(src, dst) =>
				{
					if (dst.Id == 0)
					{
						dst.Id = src.Id;
					}
					dst.ShortAddress = src.DmtKrtLit;
					if (src.Ul != null)
					{
						dst.FullAddress = dst.NullSafe(d => d.BtiStreet.Name) + " " + src.DmtKrtLit;
					}

					var distict = src.District?.Id;
					if (distict.HasValue)
					{
						var givz = distict;
						distict = UnitOfWork.GetSet<BtiDistrict>().FirstOrDefault(d => d.Givz*100 == givz)?.Id;
					}

					var region = src.Region?.Id;
					if (region.HasValue)
					{
						var givz = region;
						region = UnitOfWork.GetSet<BtiRegion>().FirstOrDefault(d => d.Givz == givz)?.Id;
					}

					dst.BtiDistrictId = distict;
					dst.BtiRegionId = region;
					dst.BtiStreetId = src.Ul?.Id;

					dst.Status = src.Status ?? 0;
					dst.Unod = src.UNAD ?? 0;
					dst.Unom = src.UNOM ?? 0;
				});


			if (deleted != null && deleted.Any() && !string.IsNullOrWhiteSpace(Settings.Default.EmailForReplicationReceiverMessages))
			{
				foreach (var addressForDelete in deleted)
				{
					UnitOfWork.AddEntity(new SendEmailAndSms()
											{
												IsSmsSended = true,
												DateCreate = DateTime.Now,
												EmailTitle = "Удаление адреса из реестра БТИ",
												EmailMessage = $"Запрос на удаление адреса из реестра БТИ (Id: {addressForDelete.Id})",
												Email = Settings.Default.EmailForReplicationReceiverMessages
											});

					log4net.LogManager.GetLogger("Service").InfoFormat("Запрос на удаление адреса из реестра БТИ (Id: {0})", addressForDelete.Id);
				}

			}
			UnitOfWork.SaveChanges();
		}


	}
}
