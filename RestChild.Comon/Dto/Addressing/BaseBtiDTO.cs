using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Addressing
{
    [DataContract]
    [Serializable]
    public class BaseBtiDTO
    {
        [DataMember] public virtual long Id { get; set; }
    }
}
