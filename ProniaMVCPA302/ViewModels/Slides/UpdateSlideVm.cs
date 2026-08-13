namespace ProniaMVCPA302.ViewModels
{
    public class UpdateSlideVm
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string? Image { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
