using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    internal class Utils
    {
        public static void CrearParametre(
            DbCommand consulta,
            object value,
            string name,
            System.Data.DbType tipus
        )
            {
                DbParameter p = consulta.CreateParameter();
                p.Value = value;
                p.ParameterName = name;
                p.DbType = tipus;
                consulta.Parameters.Add(p);
            }
    }
}
