﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmployeeDetailDBEntities : DbContext
    {
        public EmployeeDetailDBEntities()
            : base("name=EmployeeDetailDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<EmpDocument> EmpDocuments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<Zone_copy> Zone_copy { get; set; }
    }
}
