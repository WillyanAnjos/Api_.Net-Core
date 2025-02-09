using System;
using ApiTeste.Domain.Models.CompanyAggregate;
using ApiTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Infra;

public class ConnectionDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Company> Company { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Server=localhost;"+
            "Port=5432;"+
            "Database=db_csharp;"+
            "User Id=postgres;"+
            "Password=postgres");
    }
}
