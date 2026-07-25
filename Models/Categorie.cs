// Models/Categorie.cs
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nom { get; set; }

        public string? Description { get; set; }

        public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    }
}