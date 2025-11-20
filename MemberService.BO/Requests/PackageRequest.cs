

using System.ComponentModel.DataAnnotations;
using MemberService.BO.Enums;

namespace MemberService.BO.Requests
{
    public class PackageRequest
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(200, ErrorMessage = "The Name field cannot exceed 200 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The DurationInDays field is required.")]
        public int DurationInDays { get; set; }

        [Required(ErrorMessage = "The PackageType field is required.")]
        public int PackageTypeId { get; set; }

        [Required(ErrorMessage = "The IsActive field is required.")]
        [EnumDataType(typeof(Status), ErrorMessage = "The IsActive field must be a valid Status value.")]
        public Status IsActive { get; set; }
    }
}