// Models/Inscription.cs
namespace GestionFormation.Models
{
    public enum StatutInscription
    {
        EnCours,
        Terminee,
        Abandonnee
    }

    public class Inscription
    {
        public int Id { get; set; }
        public DateTime DateInscription { get; set; } = DateTime.Now;
        public StatutInscription Statut { get; set; } = StatutInscription.EnCours;

        public int ApprenantId { get; set; }
        public Apprenant? Apprenant { get; set; }

        public int FormationId { get; set; }
        public Formation? Formation { get; set; }
    }
}