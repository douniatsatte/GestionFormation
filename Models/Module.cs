// Models/Module.cs
namespace GestionFormation.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int Ordre { get; set; }
        public string? Contenu { get; set; }

        public int FormationId { get; set; }
        public Formation? Formation { get; set; }

        public ICollection<Modalite> Modalites { get; set; } = new List<Modalite>();
    }
}