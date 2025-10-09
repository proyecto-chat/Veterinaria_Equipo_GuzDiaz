using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Data.Models
{
    public class Animal
    {
        public string TipoAnimal { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public double Estatura { get; set; }

        public void UpdateEdad(int edad)
        {
            this.Edad = edad;
        }

        public void UpdatePeso(double peso)
        {
            this.Peso = peso;
        }

        public void UpdateEstatura(double estatura)
        {
            this.Estatura = estatura;
        }


    }
}