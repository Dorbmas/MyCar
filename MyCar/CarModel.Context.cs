﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCar
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarEntities3 : DbContext
    {
        private static CarEntities3 _context;
        public CarEntities3()
            : base("name=CarEntities3")
        {
        }
    
        public static CarEntities3 GetContext()
        {
            if (_context == null)
                _context = new CarEntities3();
            
            return _context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<Models> Models { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TransmissionTypes> TransmissionTypes { get; set; }
    }
}
