using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace apiPartner.Model
{
    [DataContract]
    public class Retorno<T>
    {
        public Retorno()
        {
        }

        public Retorno(bool v1, string v2)
        {
            this.IsSuccess = v1;
            this.ReturnMessage = v2;
        }

        [DataMember(Name = "Sucesso")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "MsgRetorno")]
        public string ReturnMessage { get; set; }

    }
}
