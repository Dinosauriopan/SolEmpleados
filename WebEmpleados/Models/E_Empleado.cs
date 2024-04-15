using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEmpleados.Models
{
    public class E_Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string NumeroEmpleado { get; set; }
        public decimal Sueldo { get; set;}
        public DateTime FechaNacimientos { get; set;}
        public bool TiempoCompleto { get; set; }

        public string TiempoCompletoDescripcion 
        {
            get
            {
                if (TiempoCompleto == true)
                    return "Si";
                else
                    return "No";
            }
        }

        public int Edad
        {
            get
            {
                //obtenemos la fecha actual
                DateTime fechaActual = DateTime.Now;
                return fechaActual.Year - FechaNacimientos.Year;
            }
        }
    }
}