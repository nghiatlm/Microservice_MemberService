
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberService.BO.Common;
using MemberService.BO.Enums;

namespace MemberService.BO.Entites
{
    [Table("member_ship")]
    public class Membership : BaseEntity
    {
        [Key]
        [Column("menber_ship_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("account_id")]
        [Required(ErrorMessage = "The MemberId field is required.")]
        public int AccountId { get; set; }

        [Column("package_id")]
        [Required(ErrorMessage = "The PackageId field is required.")]
        public int PackageId { get; set; }

        [ForeignKey("PackageId")]
        public Package? Package { get; set; }

        [Column("price_at_purchase", TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The PriceAtPurchase field is required.")]
        public decimal PriceAtPurchase { get; set; }

        [Column("purchase_date", TypeName = "datetime")]
        [Required(ErrorMessage = "The PurchaseDate field is required.")]
        public DateTime PurchaseDate { get; set; }

        [Column("level_at_purchase", TypeName = "varchar(100)")]
        [Required(ErrorMessage = "The LevelAtPurchase field is required.")]
        [EnumDataType(typeof(TypeLevel), ErrorMessage = "The LevelAtPurchase field must be a valid TypeLevel value.")]
        public TypeLevel LevelAtPurchase { get; set; }

        [Column("start_date", TypeName = "datetime")]
        [Required(ErrorMessage = "The StartDate field is required.")]
        public DateTime StartDate { get; set; }

        [Column("end_date", TypeName = "datetime")]
        [Required(ErrorMessage = "The EndDate field is required.")]
        public DateTime EndDate { get; set; }

        [Column("status", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "The Status field is required.")]
        [EnumDataType(typeof(MembershipStatus), ErrorMessage = "The Status field must be a valid MembershipStatus value.")]
        public MembershipStatus Status { get; set; }
    }
}