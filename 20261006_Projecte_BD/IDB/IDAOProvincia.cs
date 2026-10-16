using _20251001_Controls_Senders.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
    public interface IDAOProvincia
    {

        public int GetNumeroProvincies(string nom);

        public ObservableCollection<Provincia> GetProvincies(string id, string nom, int offset = 0, int rows_per_page = -1);

        public Provincia GetProvincia(int id);

        public bool UpdateProvincia(Provincia c);

        public bool InsertProvincia(Provincia c);

        public bool DeleteProvincia(int id);

    }
}
