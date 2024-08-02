using System.ComponentModel.DataAnnotations;

namespace PhamQuocHuy_BE_DependencyInjection.Model
{
    public class Users
    {
        public int Id { get;set; }  
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng.")]
        [StringLength(30, ErrorMessage = "Tên tài khoản không được quá dài")]
        [MinLength(6, ErrorMessage = "Tên tài khoản vui lòng hơn 6 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Mật khẩu phải dài hơn 6 ký tự, có số, ký tự đặc biệt, và chữ in hoa.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^(0[139785])\d{8}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        [EnumDataType(typeof(GenderType), ErrorMessage = "Giới tính không hợp lệ")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tỉnh/thành phố.")]
        public string Province { get; set; }
    }
    public enum GenderType
    {
        Nam,
        Nữ,
        Khác
    }
}
