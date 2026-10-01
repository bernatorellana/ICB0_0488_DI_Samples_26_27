using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DiccionarisApp.model
{
    public class Persona : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private String nif;
        private String nom;
        private String cognoms;

        public Persona(string nIF, string nom, string cognoms)
        {
            NIF = nIF;
            Nom = nom;
            Cognoms = cognoms;
        }

        public string NIF { get => nif; set { 
                nif = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NIF"));
            } 
        }
        public string Nom { get => nom; set => nom = value; }
        public string Cognoms { get => cognoms; set => cognoms = value; }

        public override string ToString()
        {
            return $"Persona: {NIF},{Nom} {Cognoms} ";
        }
    }
}
