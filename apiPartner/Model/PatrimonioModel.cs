using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace apiPartner.Model
{
    [DataContract]
    public class PatrimonioModel
    {
        [DataMember(Name = "IdTombo")]
        public int IdTombo { get; set; }

        [DataMember(Name = "Nome")]
        public string Nome { get; set; }

        [DataMember(Name = "Descricao")]
        public string Descricao { get; set; }

        [DataMember(Name = "MarcaId")]
        public int MarcaId { get; set; }
    }
    
}
