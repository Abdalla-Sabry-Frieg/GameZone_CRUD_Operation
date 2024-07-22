using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel : GameFromViewModel
    {
            public int id {  get; set; } 

            public string? currentCover { get; set; }
      
            //validate extention and size 

            [AllowedExtensionsAttribute(Settings.ImageLocation.AllowedExtensions), MaxFileSize(Settings.ImageLocation.MaxFileSizeInByte)] // validation extension and size of image
            public IFormFile? Cover { get; set; } = default!; //IFormFile => beause it will save a image 

        
    }
}
