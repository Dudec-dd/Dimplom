using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MonitoringAndAnalysis
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public DateTime BirthdayDate { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
    public class ContractsForCompany
    {
        public int Id { get; set; }
        public int ContractsCount {  get; set; }
        public int Companyid {  get; set; }
        public Company Company { get; set; }
    }
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string OfficialName { get; set; } = "";
        [Required]
        public int ORGN { get; set; }
        [Required]
        public int INN { get; set; }
        [Required]
        public int KPP { get; set; }
        [Required]
        public int NumberEmployers{ get; set; }
        public string Address { get; set; } = "";
        public string ContactInfo {  get; set; } = "";
    }
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Address { get; set; } = "";
        public bool IsEmployed { get; set; }
        public Company Company { get; set; }
        public DateTime BirthdayDate { get; set; }
    }

    public class LogIn
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }

    }

    public class Role
    {
        public int Id { get; set;}
        [Required]
        public string Name { get; set; }
    }
    public class Contract 
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int Companyid { get; set; }
        public Company Company { get; set; }
        public int EmployersAmount { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime ContractDateStart { get; set; }
        public DateTime ContractDateEnd { get; set; }
    }

    public class AppDbContext : DbContext
    {   
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LogIn> LogIns { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        
        public void InitDB()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        public AppDbContext() { }
        public DbSet<ContractsForCompany> ContractsForCompanies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u=>u.Login).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(u => u.INN).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(u => u.KPP).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(u => u.ORGN).IsUnique();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Укажите здесь вашу строку подключения к базе данных
            optionsBuilder.UseSqlServer("Server=DESKTOP-TFKOU7A;Initial Catalog=MonitoringAndAnalysis;User Id=sa;Password=sa;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
