using apiPartner.Model;
using apiPartner.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apiPartner.Repository
{
    public static class PatrimonioTranslator
    {
        public static PatrimonioModel TranslateAsPatrimonio(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();
            }

            var item = new PatrimonioModel();
            if (reader.IsColumnExists("IdTombo"))
                item.IdTombo = SqlHelper.GetNullableInt32(reader, "IdTombo");

            if (reader.IsColumnExists("Nome"))
                item.Nome = SqlHelper.GetNullableString(reader, "Nome");

            if (reader.IsColumnExists("Descricao"))
                item.Descricao = SqlHelper.GetNullableString(reader, "Descricao");

            if (reader.IsColumnExists("MarcaId"))
                item.MarcaId = SqlHelper.GetNullableInt32(reader, "MarcaId");

            return item;
        }

        public static List<PatrimonioModel> TranslateAsListaDePatrimonios(this SqlDataReader reader)
        {
            var list = new List<PatrimonioModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsPatrimonio(reader, true));
            }
            return list;
        }

    }
}
