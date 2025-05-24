using System.ComponentModel.DataAnnotations;
using WebApplication1.Dto;

public class CustomerEntity
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(50)]
        public string? First_Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(50)]
        public string? Last_Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Número de teléfono inválido.")]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto
            {
                Id = Id,
                First_Name = First_Name,
                Last_Name = Last_Name,
                Email = Email,
                Phone = Phone,
                Address = Address
            };
        }
    }