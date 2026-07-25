// Models/Formateur.cs
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace GestionFormation.Models
{
    public class Formateur
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Nom { get; set; }

        [Required, StringLength(50)]
        public string Prenom { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }

        public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    }
}