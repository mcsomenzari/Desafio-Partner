using apiPartner.Model;
using apiPartner.Repository;
using apiPartner.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiPartner.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MarcasController : Controller
    {
        private readonly string connection;

        public MarcasController(IConfiguration myconfig)
        {
            connection = myconfig.GetConnectionString("DbConn");
        }

        [HttpGet]
        public IActionResult GetAllMarcas()
        {
            List<MarcasModel> data = DbClientFactory<MarcaDbClient>.Instance.GetMarcas(0, connection);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetMarca(int id)
        {
            List<MarcasModel> data = DbClientFactory<MarcaDbClient>.Instance.GetMarcas(id, connection);
            
            if (data.Count == 0)
            {
                Retorno<MarcasModel> msg = new Retorno<MarcasModel>();
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id da marca não encontrado.";
                return Ok(msg);
            }

            return Ok(data);
        }

        [HttpPut]
        [HttpPost]
        public IActionResult GravaMarca([FromBody]MarcasModel mymodel)
        {
            if (mymodel == null)
            {
                return Ok(new Retorno<MarcasModel>(false, "Parâmetros inválidos."));
            }
            else if (mymodel.Nome == null || mymodel.Nome.ToString() == string.Empty)
            {
                return Ok(new Retorno<MarcasModel>(false, "Campo Nome é obrigatório."));
            }


            Retorno<MarcasModel> msg = new Retorno<MarcasModel>();
            string data = DbClientFactory<MarcaDbClient>.Instance.UpdMarca(mymodel, connection);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (mymodel.Id == 0)
                    msg.ReturnMessage = "Marca gravada com sucesso.";
                else
                    msg.ReturnMessage = "Marca alterada com sucesso.";
            }
            else if (data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Marca já cadastrada.";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id da marca não encontrado.";
            }
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaMarca(int id)
        {
            Retorno<MarcasModel> msg = new Retorno<MarcasModel>();
            string data = DbClientFactory<MarcaDbClient>.Instance.DelMarca(id, connection);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Marca apagada.";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Id da marca não encontrado.";
            }
            return Ok(msg);
        }

        [HttpGet("Patrimonios/{id}")]
        public IActionResult GetPatrimoniosDeUmaMarca(int id)
        {
            List<PatrimonioModel> data = DbClientFactory<PatrimonioDbClient>.Instance.GetPatrimonioPorMarca(id, connection);
            
            if (data.Count == 0)
            {
                Retorno<PatrimonioModel> msg = new Retorno<PatrimonioModel>
                {
                    IsSuccess = false,
                    ReturnMessage = "Não existe patrimônio da marca informada: " + id.ToString()
                };
                return Ok(msg);
            }

            return Ok(data);
        }


    }
}
