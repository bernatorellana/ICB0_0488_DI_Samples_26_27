using System;
using System.Collections.Generic;
using System.Text;

namespace DemoClassesCollections.model
{
    internal class Vehicle
    {
        private string matricula;
        private string model;
        private string marca;

        public Vehicle(string matricula, string model, string marca)
        {
            this.Matricula = matricula;
            this.Model = model;
            this.Marca = marca;
        }

        public string FullDesc { get => $"{matricula} : {model} / {marca}"; }

        public string Matricula { get => matricula; set => matricula = value; }
        public string Model { get => model; set => model = value; }
        public string Marca
        {
            get => marca; 
            set
            {
                if (value == null || value.Trim().Length < 2) 
                    throw new Exception("Marca no correcta");
                marca = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Vehicle vehicle &&
                   matricula == vehicle.matricula;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(matricula);
        }


    }
}
