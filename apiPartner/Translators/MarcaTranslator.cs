using apiPartner.Model;
using apiPartner.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apiPartner.Translators
{
    public static class MarcaTranslator
    {
        public static MarcasModel TranslateAsMarca(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }

            var item = new MarcasModel();
            if (reader.IsColumnExists("MarcaId"))
                item.Id = SqlHelper.GetNullableInt32(reader, "MarcaId");

            if (reader.IsColumnExists("Nome"))
                item.Nome = SqlHelper.GetNullableString(reader, "Nome");
                        
            return item;
        }

        public static List<MarcasModel> TranslateAsListaDeMarcas(this SqlDataReader reader)
        {
            var list = new List<MarcasModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsMarca(reader, true));
            }
            return list;
        }

    }
}
