
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberService.BO.Common;
using MemberService.BO.Enums;
namespace MemberService.BO.Entites
{
    [Table("package")]
    public class Package : BaseEntity
    {
        [Key]
        [Column("package_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("package_code", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "The Code field is required.")]
        [MaxLength(50, ErrorMessage = "The Code field cannot exceed 50 characters.")]
        public string Code { get; set; } = null!;

        [Column("package_name", TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(200, ErrorMessage = "The Name field cannot exceed 200 characters.")]
        public string Name { get; set; } = null!;

        [Column("package_description", TypeName = "nvarchar(500)")]
        [MaxLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Column("package_price", TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }

        [Column("duration_in_days")]
        [Required(ErrorMessage = "The DurationInDays field is required.")]
        public int DurationInDays { get; set; }

        [Column("package_type")]
        [Required(ErrorMessage = "The PackageType field is required.")]
        public int PackageTypeId { get; set; }

        [ForeignKey("PackageTypeId")]
        public PackageType? PackageType { get; set; }

        [Column("is_active", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "The IsActive field is required.")]
        [EnumDataType(typeof(Status), ErrorMessage = "The IsActive field must be a valid Status value.")]
        public Status IsActive { get; set; }
    }
}