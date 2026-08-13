using ProniaMVCPA302.Models;

namespace ProniaMVCPA302.ViewModels
{
    public class ProductItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MainImage { get; set; }
        public string SecondaryImage { get; set; }
    }
}
