namespace metallist.Models
{
    public class MediaFiles
    {
        public int id {  get; set; }
        public string name { get; set; }
        public byte[] file { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public byte[] File
        {
            get { return file; }
            set { file = value; }
        }
        public MediaFiles() { }
        public MediaFiles(string name, byte[] file)
        {
            this.name = name;
            this.file = file;
        }
    }
    public class MediaFileViewModel
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
