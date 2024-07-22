using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
	public class MaxFileSizeAttribute : ValidationAttribute
	{
		private readonly int _MaxFileSize;

		// make constractor to receve the data as parametars
		public MaxFileSizeAttribute(int MaxFileSize)
		{
			_MaxFileSize = MaxFileSize;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file is not null)
			{
				
				if (file.Length > _MaxFileSize)
				{
					return new ValidationResult($"The max file size is : {_MaxFileSize} ");
				}
			}
			return ValidationResult.Success;
		}
	}
}
