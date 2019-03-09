using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace apiPartner.Model
{
    [DataContract]
    public class MarcasModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Nome")]
        public string Nome { get; set; }
    }
}
