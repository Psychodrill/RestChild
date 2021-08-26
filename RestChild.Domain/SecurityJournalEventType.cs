using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
   public enum SecurityJournalEventType : long
   {
      SecurityIntsenders = 1,
      Processes = 2,
      RightsAndRoles = 3,
      UserDataChange = 4,
      OutSystemsInteractions = 5
   }
}
