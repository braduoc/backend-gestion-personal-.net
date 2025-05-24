using WebApplication1.Context;
using ZstdSharp;
using static WebApplication1.Context.CustomerDB;

namespace WebApplication1.Dto
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        public readonly CustomerDB customerOptions;
        public UpdateCustomerUseCase(CustomerDB _customerOptions)
        {
            customerOptions = _customerOptions;
        }
        public async Task<CustomerDto> UpdateCustomer(CustomerDto customer)
{
    if (customer.Id == null)
    {
        throw new ArgumentException("El Id del cliente no puede ser nulo.");
    }

    var entity = await customerOptions.GetByIdCustomer(customer.Id);

    if (entity == null)
    {
        return null;
    }

    // Validaciones adicionales
    if (string.IsNullOrWhiteSpace(customer.First_Name) ||
        string.IsNullOrWhiteSpace(customer.Last_Name) ||
        string.IsNullOrWhiteSpace(customer.Email))
    {
        throw new ArgumentException("Nombre, Apellido y Email son obligatorios.");
    }

    if (!customer.Email.Contains("@"))
    {
        throw new ArgumentException("El correo electrónico no es válido.");
    }

    // Actualizar las propiedades
    entity.First_Name = customer.First_Name;
    entity.Last_Name = customer.Last_Name;
    entity.Email = customer.Email;
    entity.Phone = customer.Phone;
    entity.Address = customer.Address;

    try
    {
        await customerOptions.ActualizarCustomer(entity);
    }
    catch (Exception ex)
    {
        throw new ApplicationException("Hubo un error al actualizar el cliente: " + ex.Message);
    }

    return entity.ToDto();
}

    }

    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDto> UpdateCustomer(CustomerDto customer);
    }
}