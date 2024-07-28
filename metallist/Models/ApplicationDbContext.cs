using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace metallist.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        
    }
    public class Categories
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Categories() { }
        public Categories(string name)
        {
            this.name = name;
        }
    }
    public class Products
    {
        public int id { get; set; }
        public string name, desc, vendor, size;
        public int cost, category_id;
        public byte[] img;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string? Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        public string Vendor
        {
            get { return vendor; }
            set { vendor = value; }
        }
        public string? Size
        {
            get { return size; }
            set { size = value; }
        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public byte[] Img
        {
            get { return img; }
            set { img = value; }
        }
        public int Category_id
        {
            get { return category_id; }
            set { category_id = value; }
        }
        public Products() { }
        public Products(string name, string vendor, string desc, string size, int cost, byte[] img, int category_id)
        {
            this.name = name;
            this.vendor = vendor;
            this.desc = desc;
            this.size = size;
            this.cost = cost;
            this.img = img;
            this.category_id = category_id;
        }

    }

}
