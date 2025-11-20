
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberService.BO.Common;
using MemberService.BO.Enums;

namespace MemberService.BO.Entites
{
    [Table("payment")]
    public class Payment : BaseEntity
    {
        [Key]
        [Column("payment_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("order_id")]
        [Required(ErrorMessage = "The OrderId field is required.")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [Column("payment_date", TypeName = "datetime")]
        [Required(ErrorMessage = "The PaymentDate field is required.")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Column("amount", TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }

        [Column("currency", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "The Currency field is required.")]
        [EnumDataType(typeof(Currency), ErrorMessage = "The Currency field must be a valid Currency value.")]
        public Currency Currency { get; set; } = Currency.VNĐ;

        [Column("payment_method", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "The PaymentMethod field is required.")]
        [EnumDataType(typeof(PaymentMethod), ErrorMessage = "The PaymentMethod field must be a valid PaymentMethod value.")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column("payment_status", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "The PaymentStatus field is required.")]
        [EnumDataType(typeof(PaymentStatus), ErrorMessage = "The PaymentStatus field must be a valid PaymentStatus value.")]
        public PaymentStatus PaymentStatus { get; set; }
    }
}