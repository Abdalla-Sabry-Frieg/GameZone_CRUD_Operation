using GameZone.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
	public class CreateGameFromViewModel : GameFromViewModel
	{
		
		//validate extention and size 

		[AllowedExtensionsAttribute(Settings.ImageLocation.AllowedExtensions),MaxFileSize(Settings.ImageLocation.MaxFileSizeInByte)] // validation extension and size of image
		public IFormFile Cover { get; set; } = default!; //IFormFile => beause it will save a image 

	}
}
