using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
	public class AllowedExtensionsAttribute : ValidationAttribute // Add extension validation in server side
	{
		private readonly string _allowedExtensions;

		// make constractor to receive the data as parametars
		public AllowedExtensionsAttribute(string allowedExtensions)
		{
			_allowedExtensions = allowedExtensions;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file is not null) 
			{
				var extension = Path.GetExtension(file.FileName); // method to name of image user select in side 
				var isAllowed = _allowedExtensions.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase);

				if (!isAllowed) 
				{
					return new ValidationResult($"{_allowedExtensions} only are allowed ! .");
				}
			}
			return ValidationResult.Success;
		}
	}
}
