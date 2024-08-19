using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace metallist.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<MediaFiles> MediaFiles { get; set; }
        public DbSet<Info> Info { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        
    }
    public class Admins
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        [DataType(DataType.Password)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public Admins() { }
        public Admins(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
