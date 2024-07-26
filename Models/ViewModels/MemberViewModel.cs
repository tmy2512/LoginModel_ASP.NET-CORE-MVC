using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels
{
    public class MemberViewModel
    {
        [Required(ErrorMessage = "Tên truy cập là bắt buộc hihi.")]
        public string? MemberName { get; set; }
        public int YearOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(8, ErrorMessage = "Độ dài tối thiểu là 8 ký tự.")]
        public string? MemberPassword { get; set; }
    }
}
