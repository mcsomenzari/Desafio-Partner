using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiPartner.Model;
using apiPartner.Repository;
using apiPartner.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace apiPartner.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PatrimoniosController : Controller
    {
        private readonly string connection;

        public PatrimoniosController(IConfiguration myconfig)
        {
            connection = myconfig.GetConnectionString("DbConn");
        }

        [HttpPut]
        [HttpPost]
        public IActionResult GravaPatrimonio([FromBody]PatrimonioModel mymodel)
        {
            if (mymodel == null)
            {
                return Ok(new Retorno<PatrimonioModel>(false, "Parâmetros inválidos."));
            }
            else if (mymodel.Nome == null || mymodel.Nome.ToString() == string.Empty)
            {
                return Ok(new Retorno<PatrimonioModel>(false, "Campo Nome é obrigatório."));
            }
            else if (mymodel.MarcaId == 0)
            {
                return Ok(new Retorno<PatrimonioModel>(false, "Campo MarcaId é obrigatório."));
            }

            Retorno<PatrimonioModel> msg = new Retorno<PatrimonioModel>();
            string data = DbClientFactory<PatrimonioDbClient>.Instance.UpdPatrimonio(mymodel, connection);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (mymodel.IdTombo == 0)
                    msg.ReturnMessage = "Patrimônio gravado com sucesso.";
                else
                    msg.ReturnMessage = "Patrimônio alterado com sucesso.";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id do patrimônio não encontrado.";
            }
            else if (data == "C204")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id da marca não encontrado.";
            }
            return Ok(msg);
        }

        [HttpGet]
        public IActionResult GetPatrimonios()
        {
            List<PatrimonioModel> data = DbClientFactory<PatrimonioDbClient>.Instance.GetPatrimonios(0, connection);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatrimonio(int id)
        {
            Retorno<PatrimonioModel> msg = new Retorno<PatrimonioModel>();
            string data = DbClientFactory<PatrimonioDbClient>.Instance.DelPatrimonio(id, connection);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Patrimônio excluído.";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Número do tombo não encontrado.";
            }
            return Ok(msg);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatrimonio(int id)
        {
            List<PatrimonioModel> data = DbClientFactory<PatrimonioDbClient>.Instance.GetPatrimonios(id, connection);

            
            if (data.Count == 0)
            {
                Retorno<PatrimonioModel> msg = new Retorno<PatrimonioModel>();
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id do patrimônio não encontrado.";
                return Ok(msg);
            }

            return Ok(data);
        }
    }
}