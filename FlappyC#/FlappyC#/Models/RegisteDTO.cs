﻿using System.ComponentModel.DataAnnotations;

namespace FlappyC_.Models
{
    public class RegisteDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string PasswordConfirm { get; set; } = null!;
    }
}
