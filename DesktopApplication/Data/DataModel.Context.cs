﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopApplication.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class repairingShopEntities : DbContext
    {
        public repairingShopEntities()
            : base("name=repairingShopEntities")
        {
        }
        private static repairingShopEntities context;

        public static repairingShopEntities GetContext()
        {
            if (context == null)
            {
                context = new repairingShopEntities();
            }
            return context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<clients> clients { get; set; }
        public virtual DbSet<comments> comments { get; set; }
        public virtual DbSet<masters> masters { get; set; }
        public virtual DbSet<requests> requests { get; set; }
        public virtual DbSet<userRequests> userRequests { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}