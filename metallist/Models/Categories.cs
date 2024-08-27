namespace metallist.Models
{
    public class Categories
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Categories() 
        { 

        }
        public Categories(string name)
        {
            this.name = name;
        }
    }
}
