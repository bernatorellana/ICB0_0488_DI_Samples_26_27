using System;
using System.Collections.Generic;
using System.Text;

namespace DiccionarisApp.model
{
    class Equip
    {
        #region atributs
        private int id;
        private string nom;
        #endregion


        public Equip(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }
        #region Properties
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        #endregion

        public override string ToString()
        {
            return $"{Id}:{Nom}";
        }


        //==========================================
        #region Singleton

        private static List<Equip> _equips = null;

        public static List<Equip> GetEquips()
        {
            if (_equips == null)
            {
                _equips = new List<Equip>();
                _equips.Add(new Equip(1, "Equip A"));
                _equips.Add(new Equip(2, "Equip B"));
                _equips.Add(new Equip(3, "Equip C"));
            }
            return _equips;
        }

        #endregion

    }
}
