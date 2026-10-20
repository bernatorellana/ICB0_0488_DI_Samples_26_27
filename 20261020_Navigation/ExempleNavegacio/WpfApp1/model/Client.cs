using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.model
{
    internal class Client
    {
        public Client(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }

        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
