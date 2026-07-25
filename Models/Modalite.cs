// Models/Modalite.cs
namespace GestionFormation.Models
{
    public enum TypeModalite
    {
        Cours,
        Exercice,
        Examen
    }

    public class Modalite
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public TypeModalite Type { get; set; }
        public string? Contenu { get; set; }   // texte du cours, énoncé d'exercice, questions d'examen...

        public int ModuleId { get; set; }
        public Module? Module { get; set; }
    }
}