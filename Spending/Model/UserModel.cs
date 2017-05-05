using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EntityModels
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name field is required")]
        [StringLength(50, ErrorMessage="Max of 50 characters")]
        [RegularExpression(@"^(?=.*[a-zA-Z])([a-zA-Z ]*)$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [StringLength(50, ErrorMessage="Max of 50 characters")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "This is not a valid email.")]
        public string  Email { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        //[StringLength(8 , MinimumLength = 8, ErrorMessage="Requires 8 characters")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]{8})$", ErrorMessage = "8 characters letters/numbers.")]
        public string  Password { get; set; }

        public bool IsInvalid { get; set; }

        public int WalletId { get; set; }
    }
}