using ProniaMVCPA302.Models;

namespace ProniaMVCPA302.Areas.ViewModel
{
    public class ProductCreateVM
    {
        public Product Product { get; set; } = new Product();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
