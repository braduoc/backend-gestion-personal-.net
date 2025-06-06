﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto
{
    public class CreateCustomerDto
    {

        [Required(ErrorMessage = "nombre requerido")]
        public string? First_Name { get; set; }
        [Required(ErrorMessage = "apellido requerido")]
        public string? Last_Name { get; set; }
        [Required(ErrorMessage = "email requerido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "phone requerido")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "direccion requerido")]
        public string? Address { get; set; }

    }
}
