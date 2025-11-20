
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberService.BO.Common;
using MemberService.BO.Enums;

namespace MemberService.BO.Entites
{
    [Table("order")]
    public class Order : BaseEntity
    {
        [Key]
        [Column("order_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("order_date", TypeName = "datetime")]
        [Required(ErrorMessage = "The OrderDate field is required.")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column("total_amount", TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The TotalAmount field is required.")]
        public decimal TotalAmount { get; set; }

        [Column("account_id")]
        [Required(ErrorMessage = "The AccountId field is required.")]
        public int AccountId { get; set; }

        [Column("package_id")]
        [Required(ErrorMessage = "The PackageId field is required.")]
        public int PackageId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package? Package { get; set; }

        [Column("order_staus", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "The OrderStatus field is required.")]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "The OrderStatus field must be a valid OrderStatus value.")]
        public OrderStatus OrderStatus { get; set; }

        [Column("notes", TypeName = "nvarchar(500)")]
        [MaxLength(500, ErrorMessage = "The Notes field cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}