
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemberService.BO.Common;
using MemberService.BO.Enums;

namespace MemberService.BO.Entites
{
    [Table("package_type")]
    public class PackageType : BaseEntity
    {
        [Key]
        [Column("package_type_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name", TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Column("description", TypeName = "nvarchar(200)")]
        [MaxLength(200, ErrorMessage = "The Description field cannot exceed 200 characters.")]
        public string? Description { get; set; }

        [Column("level", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "The Level field is required.")]
        [EnumDataType(typeof(TypeLevel), ErrorMessage = "The Level field must be a valid PackageType value.")]
        public TypeLevel Level { get; set; }

        [Column("status", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "The Status field is required.")]
        [EnumDataType(typeof(Status), ErrorMessage = "The Status field must be a valid Status value.")]
        public Status Status { get; set; }
    }
}