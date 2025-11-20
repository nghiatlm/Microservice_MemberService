
using System.ComponentModel.DataAnnotations;
using MemberService.BO.Enums;

namespace MemberService.BO.Requests
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "The TotalAmount field is required.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "The AccountId field is required.")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "The PackageId field is required.")]
        public int PackageId { get; set; }
        [MaxLength(500, ErrorMessage = "The Notes field cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "The PaymentMethod field is required.")]
        [EnumDataType(typeof(PaymentMethod), ErrorMessage = "The PaymentMethod field must be a valid PaymentMethod value.")]
        public PaymentMethod PaymentMethod { get; set; }
    }
}