using _20251001_Controls_Senders.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
    public interface IDAOClient
    {

        public int GetNumeroClients(string id_filtre, string rao_social_filtre);

        public ObservableCollection<Client> GetClients(string id, string rao_social, int offset = 0, int rows_per_page = -1);

        public Client GetClient(int id);

        public bool UpdateClient(Client c);

        public bool InsertClient(Client c);

        public bool DeleteClient(int id);

    }
}
