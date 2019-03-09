using apiPartner.Model;
using apiPartner.Translators;
using apiPartner.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apiPartner.Repository
{
    public class MarcaDbClient
    {
        public List<MarcasModel> GetMarcas(int id, string connString)
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

            return SqlHelper.ExecuteProcedureReturnData(connString, "getMarcas", r => r.TranslateAsListaDeMarcas(), param);
        }

        public string UpdMarca(MarcasModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@Id", SqlDbType.Int),
                new SqlParameter("@nome", SqlDbType.VarChar, 150),
                outParam
            };

            param[0].Value = model.Id;
            param[1].Value = model.Nome;

            SqlHelper.ExecuteProcedureReturnString(connString, "updMarca", param);
            return (string)outParam.Value;
        }
                
        public string DelMarca(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@Id", id),
                outParam
            };

            SqlHelper.ExecuteProcedureReturnString(connString, "delMarca", param);
            return (string)outParam.Value;
        }

    }
}
