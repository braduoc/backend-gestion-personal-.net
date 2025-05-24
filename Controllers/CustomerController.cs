using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Dto;
using static WebApplication1.Context.CustomerDB;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/customers")]  
    public class CustomerController : ControllerBase 
    {
        private readonly CustomerDB customerOptions;
        private readonly IUpdateCustomerUseCase updateCustomer;

        public CustomerController(CustomerDB _customerOptions, IUpdateCustomerUseCase _updateCustomer)
        {
            customerOptions = _customerOptions;
            updateCustomer = _updateCustomer;
        }

        // Obtener todos los clientes
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerDto()
        {
            var customers = await customerOptions.GetAllCustomers();
            if (customers == null || !customers.Any()) 
                return NotFound("No se encontraron clientes.");

            return Ok(customers.Select(c => c.ToDto())); 
        }

        // Obtener un cliente por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDtoById(int id)
        {
            var result = await customerOptions.GetByIdCustomer(id);
            if (result == null) 
                return NotFound($"Cliente con ID {id} no encontrado.");

            return Ok(result.ToDto());
        }

        // Crear un nuevo cliente
        [HttpPost]
        public async Task<IActionResult> CreateCustomerDtoById(CreateCustomerDto createCustomer)
        {
            if (createCustomer == null) 
                return BadRequest("Datos inválidos para crear un cliente.");

            var creado = await customerOptions.AddCustomer(createCustomer);
            if (creado == null)
                return StatusCode(500, "Hubo un problema al crear el cliente.");

            return CreatedAtAction(nameof(GetCustomerDtoById), new { id = creado.Id }, creado.ToDto());
        }

        // Actualizar cliente
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            if (customer == null) 
                return BadRequest("Los datos del cliente son inválidos.");

            var result = await updateCustomer.UpdateCustomer(customer);
            if (result == null)
                return NotFound($"Cliente con ID {customer.Id} no encontrado.");

            return Ok(result); 
        }

        // Eliminar cliente por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDtoById(int id)
        {
            var result = await customerOptions.DeleteByIdCustomer(id);
            if (!result)
                return NotFound($"No se encontró el cliente con ID {id}.");

            return NoContent(); 
        }
    }
}
