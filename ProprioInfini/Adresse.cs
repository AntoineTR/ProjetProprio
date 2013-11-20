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
    
    public partial class Adresse
    {
        public Adresse()
        {
            this.Batiments = new HashSet<Batiment>();
            this.Proprietaires = new HashSet<Proprietaire>();
        }
    
        public int Id { get; set; }
        public Nullable<int> VilleId { get; set; }
        public string CodePostal { get; set; }
        public Nullable<int> RueId { get; set; }
        public System.Data.Spatial.DbGeography GeoLocation { get; set; }
        public Nullable<int> NumeroCivic { get; set; }
    
        public virtual Rue Rue { get; set; }
        public virtual Ville Ville { get; set; }
        public virtual ICollection<Batiment> Batiments { get; set; }
        public virtual ICollection<Proprietaire> Proprietaires { get; set; }
    }
}
