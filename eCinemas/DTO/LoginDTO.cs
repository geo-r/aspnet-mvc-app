﻿using System.ComponentModel.DataAnnotations;

namespace eCinemas.DTO
{
    public class LoginDTO
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}