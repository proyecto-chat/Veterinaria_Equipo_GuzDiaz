using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Interfaces
{
    public interface IAnimal
    {
        public void UpdatePeso();
        public void UpdateEdad();
        public void UpdateEstatura();
    }
}