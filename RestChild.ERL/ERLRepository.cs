using System;
using System.Linq;
using RestChild.Comon;

using RestChild.Domain;

namespace RestChild.ERL
{
    /// <summary>
    ///     работа с БД в ЕРЛ
    /// </summary>
    public class ErlRepository
    {
        /// <summary>
        ///     Установить ЕРЛ идентификатор для персоны 
        /// </summary>
        public static void ChildCitizenPkSet(IUnitOfWork unitOfWork, string childId, Guid citizenPk, Guid messageId)
        {
            if (long.TryParse(childId, out long cid))
            {
                var c = unitOfWork.GetSet<ERLPersonStatus>().FirstOrDefault(child => child.Child.ChildUniqeId == cid && child.PersonUid == null);

                if (c != null)
                {
                    c.PersonUid = citizenPk;
                    unitOfWork.SaveChanges();
                }
            }
            else if (long.TryParse(childId.TrimStart('c'), out cid))
            {
                var c = unitOfWork.GetSet<ERLPersonStatus>().FirstOrDefault(child => child.ChildId == cid && child.PersonUid == null);
                if (c != null)
                {
                    c.PersonUid = citizenPk;
                    unitOfWork.SaveChanges();
                }
            }
        }

        /// <summary>
        ///     Установить признак получения сообщения в ЕРЛ
        /// </summary>
        public static void ERLMessageIsCommited(IUnitOfWork unitOfWork, Guid messageId)
        {
            foreach(var benefit in unitOfWork.GetSet<ERLBenefitStatus>().Where(ss => ss.ERLMessageId == messageId).ToList())
            {
                benefit.ERLCommited = true;
                unitOfWork.SaveChanges();
            }
            foreach (var person in unitOfWork.GetSet<ERLPersonStatus>().Where(ss => ss.ERLMessageId == messageId).ToList())
            {
                person.ERLCommited = true;
                unitOfWork.SaveChanges();
            }

        }
    }
}
