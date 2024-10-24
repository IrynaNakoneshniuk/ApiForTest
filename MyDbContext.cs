using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json;
using System.Xml;

namespace ApiForTest;

public partial class TestContext : DbContext
{

    public DbSet<Author> Author { get; set; }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "server=localhost;port=3306;uid=root;pwd=1111;database=test;";
        optionsBuilder
            .UseMySql(
                 connectionString,
                   new MySqlServerVersion(new Version(8, 0, 27)),
                   option => option.UseMicrosoftJson()) 
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Author>().
            Property(p => p.Contact)
            .HasColumnType("json");


        base.OnModelCreating(modelBuilder);
    }
}


public class ContactInfo
{
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class Author
{
   
    public int Id { get; private set; }
    public string Name { get; set; }
    public List<ContactInfo> Contact { get; set; }
}
