using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MySQLFactory
    {
        public static void getUOW(Action<UnitOfWork> action)
        {
            using (var uow = new UnitOfWork(new MyDBContext()))
            {
                uow.BeginTransaction();
                action(uow);
                
            }
        }
    }
}
