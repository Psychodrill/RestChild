using System.Linq;
using RestChild.DAL;
using RestChild.Mobile.Domain;
using Account = RestChild.Domain.Account;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     миграция пользователей в БД мобильного приложения
    /// </summary>
    [Task]
    public class MigrateUserForMobileTask : BaseTask
    {
        protected override void Execute()
        {
            Logger.Info("MigrateUserForMobileTask started");

            using (var uwAiso = new UnitOfWork())
            {
                using (var mobile = new RestChild.Mobile.DAL.MobileUnitOfWork())
                {
                    var lastLut = mobile.GetSet<AccountExternal>().Select(e => e.LastUpdateTick).DefaultIfEmpty().Max();
                    var accounts = uwAiso.GetSet<Account>().Where(a => a.LastUpdateTick > lastLut)
                        .OrderBy(b => b.LastUpdateTick).Take(500).ToList();
                    mobile.NotUpdateLut = true;
                    var ids = accounts.Select(a => a.Id).ToArray();
                    var presentAccount = mobile.GetSet<AccountExternal>().Where(b => ids.Contains(b.Id)).ToList();

                    foreach (var account in accounts)
                    {
                        var present = presentAccount.FirstOrDefault(p => p.Id == account.Id);

                        if (present == null)
                        {
                            mobile.AddEntity(new AccountExternal
                            {
                                Id = account.Id,
                                Email = account.Email,
                                Name = account.Name,
                                Phone = account.Phone,
                                IsBlocked = !account.IsActive || account.IsDeleted,
                                LastUpdateTick = account.LastUpdateTick
                            });
                        }
                        else
                        {
                            present.Name = account.Name;
                            present.Email = account.Email;
                            present.Phone = account.Phone;
                            present.IsBlocked = !account.IsActive || account.IsDeleted;
                            present.LastUpdateTick = account.LastUpdateTick;
                        }
                    }

                    mobile.SaveChanges();
                }
            }

            Logger.Info("MigrateUserForMobileTask finish");
        }
    }
}
