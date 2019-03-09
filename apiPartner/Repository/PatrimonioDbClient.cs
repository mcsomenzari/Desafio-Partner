using apiPartner.Model;
using apiPartner.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apiPartner.Repository
{
    public class PatrimonioDbClient
    {
        public string UpdPatrimonio(PatrimonioModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@idTombo", SqlDbType.Int),
                new SqlParameter("@Nome", SqlDbType.VarChar, 150),
                new SqlParameter("@Descricao", SqlDbType.VarChar, 150),
                new SqlParameter("@MarcaId", SqlDbType.Int),
                outParam
            };

            param[0].Value = model.IdTombo;
            param[1].Value = model.Nome;
            param[2].Value = model.Descricao;
            param[3].Value = model.MarcaId;

            SqlHelper.ExecuteProcedureReturnString(connString, "updPatrimonio", param);
            return (string)outParam.Value;
        }

        public List<PatrimonioModel> GetPatrimonios(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@Id", SqlDbType.Int),
                outParam
            };

            param[0].Value = id;

            return SqlHelper.ExecuteProcedureReturnData(connString, "getPatrimonios", r => r.TranslateAsListaDePatrimonios(), param);
        }

        public string DelPatrimonio(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@Id", id),
                outParam
            };

            SqlHelper.ExecuteProcedureReturnString(connString, "delPatrimonio", param);
            return (string)outParam.Value;
        }

        public List<PatrimonioModel> GetPatrimonioPorMarca(int idmarca, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@IdMarca", SqlDbType.Int),
                outParam
            };

            param[0].Value = idmarca;

            return SqlHelper.ExecuteProcedureReturnData(connString, "getPatrimoniosPorMarca", r => r.TranslateAsListaDePatrimonios(), param);

        }
    }
}
