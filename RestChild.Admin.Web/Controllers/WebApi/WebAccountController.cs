using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Admin.Web.Models;
using RestChild.Security;

namespace RestChild.Admin.Web.Controllers.WebApi
{
    /// <summary>
    /// APi аккаунтов
    /// </summary>
    [Authorize]
    public class WebAccountController : BaseController
    {
        /// <summary>
        /// Загрузить аккаунт
        /// </summary>
        /// <param name="id">Идентификатор аккаунта</param>
        /// <returns></returns>
        public virtual Account GetAccount(long id)
        {
            return UnitOfWork.GetById<Account>(id);
        }

        /// <summary>
        /// Поиск аккаунта
        /// </summary>
        /// <param name="q">поисковое слово</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AccountPublicDto> SearchAccount(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                q = q.ToLower();
            }

            return UnitOfWork
               .GetSet<Account>()
               .Where(a => a.IsActive)
               .Where(a => a.Name.ToLower().Contains(q) || a.Login.ToLower().Contains(q))
               .Take(10)
               .OrderBy(a => a.Name)
               .ToList()
               .Select(a => new AccountPublicDto { FullName = $"{a.Name} ({a.Login})", Id = a.Id }).ToList();
        }

        /// <summary>
        /// Список аккаунтов
        /// </summary>
        public CommonPagedList<Account> AccountList(string search, PagerState pager)
        {
            var query = UnitOfWork.GetSet<Account>().AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                var items = search.ToLower().Split(' ');
                query = items.Aggregate(query,
                   (current, item) =>
                      current.Where(a => a.Login.ToLower().Contains(item) || a.Name.ToLower().Contains(item)));
            }

            return new CommonPagedList<Account>(query.GetPage(pager), pager.CurrentPage,
               pager.PerPage, query.Count());
        }

        /// <summary>
        /// Сохранить аккаунт
        /// </summary>
        internal Account SaveAccount(Account user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            if (!Security.HasRight(AccessRightEnum.AccountManage) && Security.GetCurrentAccountId() != user.Id)
            {
                throw new AccessViolationException("Нет прав для редактирования пользоватлей.");
            }

            Account userDomain;
            using (var tran = UnitOfWork.GetTransactionScope())
            {
                if (user.Id == 0)
                {
                    if (user.Rights != null)
                    {
                        foreach (var right in user.Rights)
                        {
                            if (right.AccessRight != null)
                            {
                                right.AccessRight = UnitOfWork.GetOrAttachEntity(right.AccessRight);
                            }
                        }
                    }

                    if (user.Roles != null)
                    {
                        foreach (var role in user.Roles)
                        {
                            if (role.Role != null)
                            {
                                role.Role = UnitOfWork.GetOrAttachEntity(role.Role);
                            }
                        }
                    }

                    userDomain = UnitOfWork.AddEntity(user);
                    userDomain.DateUpdate = DateTime.Now;
                    userDomain.CreateUserId = Security.GetCurrentAccountId();
                    userDomain.DateCreate = DateTime.Now;
                    var salt = PasswordUtility.GenerateSalt();
                    userDomain.Password = Convert.ToBase64String(PasswordUtility.GetPasswordHash(user.Password, salt));
                    userDomain.Salt = Convert.ToBase64String(salt);
                    UnitOfWork.SaveChanges();

                    #region IB

                    //Записываем в лог ИБ о создании пользователя
                    RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.UserDataChange, "Создан новый пользователь", string.Format("Создан новый пользователь {0} ({1})", userDomain.Name, userDomain.Id), Security.GetCurrentAccountId().Value);
                    //Создаём оповещение ИБ о создании пользователя
                    RestChild.Security.Logger.SecurityLogger.AddEmailEvent(UnitOfWork, SecurityJournalEventType.UserDataChange, "Создан новый пользователь", string.Format("Создан новый пользователь {0} ({1})", userDomain.Name, userDomain.Id), Security.GetCurrentAccountId().Value);

                    //записываем в лог ИБ о присвоенных пользователю правах
                    foreach (var right in userDomain.Rights.Where(r => r.AccessRightId != null).ToList())
                    {
                        RestChild.Security.Logger.SecurityLogger.AddToLogUserRightChange(UnitOfWork, right.AccessRightId.Value, true, userDomain.Id, Security.GetCurrentAccountId().Value);
                    }

                    //записываем в лог ИБ о присвоенных пользователю ролях
                    foreach (var role in userDomain.Roles.Where(r => r.RoleId != null).ToList())
                    {
                        RestChild.Security.Logger.SecurityLogger.AddToLogUserRoleChange(UnitOfWork, role.RoleId.Value, true, userDomain.Id, Security.GetCurrentAccountId().Value);
                    }

                    #endregion

                    UnitOfWork.SaveChanges();
                }
                else
                {
                    userDomain = UnitOfWork.GetById<Account>(user.Id);
                    if (userDomain == null)
                    {
                        throw new KeyNotFoundException("Не найден пользователь");
                    }

                    var _delete = user.IsDeleted || user.DateDelete.HasValue;

                    List<string> Fields = new List<string>();

                    userDomain.DateUpdate = DateTime.Now;
                    userDomain.CreateUserId = Security.GetCurrentAccountId();

                    if (userDomain.Login != user.Login)
                        Fields.Add("Имя пользователя");
                    userDomain.Login = user.Login;

                    if (userDomain.Name != user.Name)
                        Fields.Add("ФИО пользователя");
                    userDomain.Name = user.Name;

                    if (userDomain.Position != user.Position)
                        Fields.Add("Должность");
                    userDomain.Position = user.Position;

                    if (userDomain.Email != user.Email)
                        Fields.Add("Электронная почта");
                    userDomain.Email = user.Email;

                    if (userDomain.Phone != user.Phone)
                        Fields.Add("Телефон");
                    userDomain.Phone = user.Phone;

                    if (_delete)
                    {
                        RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.UserDataChange, "Пользователь удален", string.Format("Пользователь {0} ({1}) удален", userDomain.Name, userDomain.Id), Security.GetCurrentAccountId().Value);
                    }
                    else
                    {
                        //Записываем в лог ИБ об изменении пользователя
                        RestChild.Security.Logger.SecurityLogger.AddToLogUserDataChange(UnitOfWork, userDomain.Id, Security.GetCurrentAccountId().Value, Fields.ToArray());
                    }

                    if (!_delete && Security.HasRight(AccessRightEnum.AccountManage))
                    {
                        if (userDomain.IsActive != user.IsActive)
                        {
                            RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.UserDataChange, "Измененён статус пользователя", $"Пользователь {userDomain.Name} ({userDomain.Id}) {(user.IsActive ? "Активирован" : "Заблокирован")}", Security.GetCurrentAccountId().Value);
                        }
                        userDomain.IsActive = user.IsActive;

                        //разблокировка пользователя
                        if(user.IsActive)
                        {
                            UnitOfWork.AddEntity(new AccountHistoryLogin {
                                AccountId = userDomain.Id,
                                DateEnter = DateTime.Now,
                                DateLastActivity = DateTime.Now,
                                StopSession = false,
                                IsAuthorized = true
                            });
                        }

                        foreach (var role in user.Roles.Where(r => !userDomain.Roles.Select(ur => ur.Id).Contains(r.Id)).ToList())
                        {
                            role.Account = userDomain;
                            role.AccountId = userDomain.Id;
                            UnitOfWork.AddEntity(role);

                            RestChild.Security.Logger.SecurityLogger.AddToLogUserRoleChange(UnitOfWork, role.RoleId.Value, true, userDomain.Id, Security.GetCurrentAccountId().Value);
                        }

                        foreach (var role in userDomain.Roles.Where(r => !user.Roles.Select(ur => ur.Id).Contains(r.Id)).ToList())
                        {
                            if (role.Role.AccessRights.All(a => string.IsNullOrWhiteSpace(a.GroupCode) || Security.HasRight(a.GroupCode.ToString())))
                            {
                                RestChild.Security.Logger.SecurityLogger.AddToLogUserRoleChange(UnitOfWork, role.RoleId.Value, false, userDomain.Id, Security.GetCurrentAccountId().Value);

                                UnitOfWork.Delete(role);
                            }
                        }

                        foreach (var right in user.Rights.Where(r => !userDomain.Rights.Select(ur => ur.Id).Contains(r.Id)).ToList())
                        {
                            right.Account = userDomain;
                            right.AccountId = userDomain.Id;
                            UnitOfWork.AddEntity(right);

                            RestChild.Security.Logger.SecurityLogger.AddToLogUserRightChange(UnitOfWork, right.AccessRightId.Value, true, userDomain.Id, Security.GetCurrentAccountId().Value);
                        }

                        foreach (var right in userDomain.Rights.Where(r => !user.Rights.Select(ur => ur.Id).Contains(r.Id)).ToList())
                        {
                            if (string.IsNullOrWhiteSpace(right.AccessRight.GroupCode) || Security.HasRight(right.AccessRight.GroupCode))
                            {
                                RestChild.Security.Logger.SecurityLogger.AddToLogUserRightChange(UnitOfWork, right.AccessRightId.Value, false, userDomain.Id, Security.GetCurrentAccountId().Value);

                                UnitOfWork.Delete(right);
                            }
                        }

                        foreach (var right in userDomain.Rights.Where(r => user.Rights.Select(ur => ur.Id).Contains(r.Id)).ToList())
                        {
                            var rig = user.Rights.FirstOrDefault(r => r.Id == right.Id);
                            right.OrganizationId = rig.OrganizationId;
                        }
                    }
                }

                UnitOfWork.SaveChanges();

                tran.Complete();
            }

            return userDomain;
        }
    }
}
