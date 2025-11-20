
using System.ComponentModel.DataAnnotations;
using MemberService.BO.Enums;

namespace MemberService.BO.Requests
{
    public class PackageTypeRequest
    {

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(200, ErrorMessage = "The Description field cannot exceed 200 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Level field is required.")]
        [EnumDataType(typeof(TypeLevel), ErrorMessage = "The Level field must be a valid PackageType value.")]
        public TypeLevel Level { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [EnumDataType(typeof(Status), ErrorMessage = "The Status field must be a valid Status value.")]
        public Status Status { get; set; }
    }
}