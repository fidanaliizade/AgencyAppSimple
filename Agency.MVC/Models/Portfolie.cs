using Agency.MVC.Models.BaseModel;

namespace Agency.MVC.Models
{
	public class Portfolie:BaseEntity
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
