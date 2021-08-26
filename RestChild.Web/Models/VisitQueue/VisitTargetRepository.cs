using RestChild.Comon;
using RestChild.Web.Logic;
using System.Collections.Generic;
using System.Linq;

namespace RestChild.Web.Models.VisitQueue
{
    /// <summary>
    ///     Репозиторий целей визита в МГТ
    /// </summary>
    public class VisitTargetRepository : ILogic
    {
        public VisitTargetRepository(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     Выбрать все цели визита для МПГУ
        /// </summary>
        public IEnumerable<IVisitTarget> GetAllValidVisitTargets()
        {
            return UnitOfWork.GetSet<Domain.MGTVisitTarget>().Where(ss => ss.IsActive && ss.IsForMPGU).Select(ss => new VisitTarget() { Id = ss.Id, Name = ss.Name, Description = ss.Description }).ToList();
        }
    }
}
