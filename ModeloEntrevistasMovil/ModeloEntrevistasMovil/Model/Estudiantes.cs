using System;

namespace ModeloEntrevistasMovil.Model
{
    public class Estudiantes
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Curso { get; set; }
        public int Estado { get; set; }
    }
}
