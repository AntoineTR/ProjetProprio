﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProprioInfiniConn : DbContext
    {
        public ProprioInfiniConn()
            : base("name=ProprioInfiniConn")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Batiment> Batiments { get; set; }
        public DbSet<Proprietaire> Proprietaires { get; set; }
        public DbSet<Rue> Rues { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}
