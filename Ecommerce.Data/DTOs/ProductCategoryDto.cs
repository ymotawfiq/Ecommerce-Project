

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ProductCategoryDto
    {
        public string? Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; } = string.Empty;
        public string? ParentCategoryId { get; set; }
    }
}
