    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    namespace WebApplication1.Dto
    {
        public class CustomerDto
        {
            
            public int? Id {  get; set; }
            public string? First_Name { get; set; }
            public string? Last_Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }           
        }
    }
