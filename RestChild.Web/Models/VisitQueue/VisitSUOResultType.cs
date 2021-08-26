using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.VisitQueue
{
    /// <summary>
    ///     Типы вариантов ответов для СУО
    /// </summary>
    public enum VisitSUOResultType : int
    {
        /// <summary>
        ///     Пинкод верный, можно выдавать талон
        /// </summary>
        Valid = 1,
        /// <summary>
        ///     Пинкод не верный (или пользователь опоздал)
        /// </summary>
        Unvalid = 2,
        /// <summary>
        ///     Пинкод верный, но посетитель явился слишком рано
        /// </summary>
        ValidButEarly = 3
    }
}
