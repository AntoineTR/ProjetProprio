//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProprioInfini
{
    using System;
    using System.Collections.Generic;
    
    public partial class Batiment
    {
        public Batiment()
        {
            this.Annonces = new HashSet<Annonce>();
        }
    
        public int Id { get; set; }
        public Nullable<decimal> NombrePiece { get; set; }
        public Nullable<bool> EstChauffer { get; set; }
        public Nullable<bool> Electricite { get; set; }
        public Nullable<bool> Internet { get; set; }
        public Nullable<bool> Stationnement { get; set; }
        public Nullable<int> AdresseId { get; set; }
        public string Nom { get; set; }
    
        public virtual Adresse Adresse { get; set; }
        public virtual ICollection<Annonce> Annonces { get; set; }
    }
}
