using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumnosWebApp.Models
{
    public class TareaAlumno
    {
        [Key, Column(Order =1), ForeignKey("Tarea")]
        public int IdTarea { get; set; }

        [Key, Column(Order = 2), ForeignKey("Alumno")]
        public int IdAlumno { get; set; }

        public string Mensaje { get; set; }
        public string ArchivoURL { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public bool Evaluado { get; set; }

        public virtual Tarea Tarea { get; set; }
        public virtual Alumno Alumno { get; set; }
    }
}