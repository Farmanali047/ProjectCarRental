using System.ComponentModel.DataAnnotations;

namespace ProjectCarRental.Models
{
    public class NotAllowedWordsAttribute : ValidationAttribute
    {
        private readonly string[] _notAllowedWords;

        public NotAllowedWordsAttribute(string[] notAllowedWords)
        {
            _notAllowedWords = notAllowedWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = value as string;

            if (!string.IsNullOrEmpty(input))
            {
                foreach (var word in _notAllowedWords)
                {
                    if (input.Contains(word))
                    {
                        return new ValidationResult(ErrorMessage ?? "The field contains not allowed words.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
