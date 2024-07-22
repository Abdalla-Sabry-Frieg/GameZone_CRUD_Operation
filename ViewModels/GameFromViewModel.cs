namespace GameZone.ViewModels
{
    public class GameFromViewModel 
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Category")] // to change name of CategoryId to Category in site to be more frindly to user
        public int CategoryId { get; set; } // user can select one item

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>(); // to select list from database to view 

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = new List<int>(); // user can select more than one item 

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>(); // to select list from database to view 

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
    }
}
