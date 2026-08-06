using ProniaMVCPA302.Models;

namespace ProniaMVCPA302.ViewModels
{
    public class DetailVM
    {
        public Product Product { get; set; }
        public List<Product> RelatedProduct { get; set; }
    }
}
