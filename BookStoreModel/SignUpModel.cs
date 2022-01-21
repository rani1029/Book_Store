using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore_App.BookStoreModel
{
    public class SignUpModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        // [RegularExpression("@^[A-Z]{1}[A-Za-z]{2,}$", ErrorMessage = "Firstname is not valid. Please Enter valid FirstName")]
        public string CustomerName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "E-mail is not valid. Please Enter valid email")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression("@^(?=.*[a - z])(?=.*[A - Z])(?=.*[0 - 9])(?=.*[~`!@#$%^&*_+=,./?]).{8,}$", ErrorMessage = "Password is not valid. Please Enter valid Password")]
        public string Password { get; set; }
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public long PhoneNumber { get; set; }


    }
}
