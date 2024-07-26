using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels
{
    public class UpdateMemberModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Tên truy cập là bắt buộc.")]
        public string? MemberName { get; set; }
        [Required(ErrorMessage = "Năm sinh là bắt buộc.")]
        public int YearOfBirth { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc.")]
        //public string Email { get; set; }
        //[Required(ErrorMessage = "Phone là bắt buộc.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public Gender Gender { get; set; }
    }
}
