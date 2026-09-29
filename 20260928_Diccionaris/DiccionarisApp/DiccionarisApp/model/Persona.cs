using System;
using System.Collections.Generic;
using System.Text;

namespace DiccionarisApp.model
{
    public class Persona
    {
        private String nif;
        private String nom;
        private String cognoms;

        public Persona(string nIF, string nom, string cognoms)
        {
            NIF = nIF;
            Nom = nom;
            Cognoms = cognoms;
        }

        public string NIF { get => nif; set => nif = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Cognoms { get => cognoms; set => cognoms = value; }

        public override string ToString()
        {
            return $"Persona: {NIF},{Nom} {Cognoms} ";
        }
    }
}
