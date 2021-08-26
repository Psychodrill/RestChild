using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.Comon.Dto.Addressing;
using RestChild.Web.Logic;

namespace RestChild.Web.Controllers.WebApi
{
    public abstract class BaseController : ApiController, ILogic
    {
        public ILogger Logger { get; set; }

        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     заполнить во всех дочерних классах
        /// </summary>
        public void SetUnitOfWorkInRefClass()
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
        }

        /// <summary>
        ///     заполнить во всех дочерних классах
        /// </summary>
        public virtual void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Удаление сущностей из БД
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <typeparam name="TDto">DTO для сущности</typeparam>
        /// <param name="deleted">Коллекция сущностей для всех</param>
        protected void DeleteEntities<T, TDto>(ICollection<TDto> deleted)
            where T : class, IEntityBase, new()
            where TDto : BaseBtiDTO
        {
            var idsForDelete = (deleted ?? new List<TDto>()).Select(s => s.Id);
            var forDelete = UnitOfWork.GetSet<T>().Where(s => idsForDelete.Contains(s.Id));
            foreach (var street in forDelete)
            {
                UnitOfWork.Delete(street);
            }
        }

        /// <summary>
        ///     Добавление или изменение сущностей в БД
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <typeparam name="TDto">DTO для сущности</typeparam>
        /// <param name="added">Сущности для добавления</param>
        /// <param name="modified">Сущности для изменения</param>
        /// <param name="updateFunc">Функция обновления сущностей</param>
        protected void InsertOrModifyEntities<T, TDto>(ICollection<TDto> added, ICollection<TDto> modified,
            Action<TDto, T> updateFunc)
            where T : class, IEntityBase, new()
            where TDto : BaseBtiDTO
        {
            var forAddOrModify = added ?? new List<TDto>();
            forAddOrModify.AddRange(modified ?? new List<TDto>());
            var idsForAddOrModify = forAddOrModify.Select(a => a.Id);
            var existed = UnitOfWork.GetSet<T>().Where(s => idsForAddOrModify.Contains(s.Id)).ToList();
            foreach (var street in forAddOrModify)
            {
                var found = existed.FirstOrDefault(s => s.Id == street.Id);
                if (found != null)
                {
                    updateFunc(street, found);
                }
                else
                {
                    var obj = new T();
                    updateFunc(street, obj);
                    UnitOfWork.AddEntity(obj);
                }
            }
        }
    }
}
