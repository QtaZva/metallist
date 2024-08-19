namespace metallist.Models
{
    public class Info
    {
        public int id { get; set; }
        public string page { get; set; }
        public string information { get; set; }
        public string Page
        {
            get { return page; }
            set { page = value; }
        }
        public string Information
        {
            get { return information; }
            set { information = value; }
        }
        public Info() { }
        public Info(string page, string information)
        {
            this.page = page;
            this.information = information;
        }
    }
}
