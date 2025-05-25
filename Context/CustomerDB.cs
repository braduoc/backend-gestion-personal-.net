using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;

namespace WebApplication1.Context;

public class CustomerDB : DbContext
{
    public CustomerDB(DbContextOptions<CustomerDB> options) : base(options)
    {
    }

    public DbSet<CustomerEntity> Customer { get; set; }

    public async Task<List<CustomerEntity>> GetAllCustomers()
    {
        return await Customer.ToListAsync();
    }

    public async Task<CustomerEntity?> GetByIdCustomer(int? id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");

        return await Customer.FirstOrDefaultAsync(x => x.Id == id);
    }

   public async Task<CustomerEntity> AddCustomer(CreateCustomerDto createCustomer)
{
    // Validaciones manuales mínimas
    if (string.IsNullOrWhiteSpace(createCustomer.First_Name) ||
        string.IsNullOrWhiteSpace(createCustomer.Last_Name) ||
        string.IsNullOrWhiteSpace(createCustomer.Email))
    {
        throw new ArgumentException("Nombre, Apellido y Email son obligatorios.");
    }

    if (!createCustomer.Email.Contains("@"))
    {
        throw new ArgumentException("El correo electrónico no es válido.");
    }

    if (!string.IsNullOrWhiteSpace(createCustomer.Phone) && !Regex.IsMatch(createCustomer.Phone, @"^\+?[1-9]\d{1,14}$"))
    {
        throw new ArgumentException("El número de teléfono no tiene un formato válido.");
    }

    var newCustomer = new CustomerEntity
    {
        First_Name = createCustomer.First_Name,
        Last_Name = createCustomer.Last_Name,
        Email = createCustomer.Email,
        Phone = createCustomer.Phone,
        Address = createCustomer.Address
    };

    try
    {
        var response = await Customer.AddAsync(newCustomer);
        await SaveChangesAsync();

        var createdCustomer = await GetByIdCustomer(response.Entity.Id ?? throw new ArgumentNullException(nameof(response.Entity.Id), "No se pudo obtener el ID del cliente creado."));
        
        return createdCustomer;
    }
    catch (Exception ex)
    {
        // Aquí puedes hacer log del error o tratarlo de otra forma más específica
        throw new ApplicationException("Hubo un problema al crear el cliente: " + ex.Message);
    }
}

    public async Task<bool> ActualizarCustomer(CustomerEntity customer)
    {
        if (customer.Id == null)
            throw new ArgumentException("El ID del cliente es obligatorio.");

        Customer.Update(customer);
        await SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteByIdCustomer(int id)
    {
        var entidad = await GetByIdCustomer(id);
        if (entidad == null)
            throw new Exception("El cliente no existe.");

        Customer.Remove(entidad);
        await SaveChangesAsync();
        return true;
    }
    
}
