using Microsoft.AspNetCore.Mvc.Rendering;

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
        public int ?page_id { get; set; }
        public int ?Page_id
        {
            get { return page_id; }
            set { page_id = value; }
        }
        public MediaFiles() { }
        public MediaFiles(string name, byte[] file, int page_id)
        {
            this.name = name;
            this.file = file;
            this.page_id = page_id;
        }
    }
    public class MediaFileViewModel
    {
        public string Name { get; set; }
        public int page_id { get; set; }
        public IFormFile File { get; set; }
        public IEnumerable<SelectListItem> Pages { get; set; }
    }
    public class EditMediaFileViewModel
    {
        public MediaFiles MediaFile { get; set; }
        public IFormFile File { get; set; }
        public IEnumerable<SelectListItem> Pages { get; set; }
    }
}
