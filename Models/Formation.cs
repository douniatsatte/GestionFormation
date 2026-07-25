// Models/Formation.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace GestionFormation.Models
{
    public class Formation
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Titre { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Prix { get; set; }

        public int CategorieId { get; set; }
        public Categorie? Categorie { get; set; }

        public int? FormateurId { get; set; }
        public Formateur? Formateur { get; set; }

        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
    }
}