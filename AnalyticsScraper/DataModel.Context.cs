﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnalyticsScraper
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StrategicStrikeEntities : DbContext
    {
        public StrategicStrikeEntities()
            : base("name=StrategicStrikeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<plCountry> plCountries { get; set; }
        public virtual DbSet<plState> plStates { get; set; }
        public virtual DbSet<plCompanyCategory> plCompanyCategories { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<plJobKeyword> plJobKeywords { get; set; }
    }
}
