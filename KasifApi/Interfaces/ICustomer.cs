using KasifApi.DTO;
using KasifApi.Models;

namespace KasifApi.Interfaces;

public interface ICustomer
{
    // Auth
    Task<Customer?> LoginAsync(CustomerLoginDto customerLoginDto);
    Task<Customer?> RegisterAsync(CustomerRegisterDto customerRegisterDto);

    // Id
    Task<Customer?> GetByIdAsync(int customerId);

    // Username
    Task<bool> IsUsernameExistsAsync(string username);

    // Update
    Task<string> UpdateAsync(CustomerUpdateDto customerUpdateDto);

    // Toggle Delete
    Task<string> ToggleDeleteAsync(int customerId);

    // Toggle Active
    Task<string> ToggleActivateAsync(int customerId);
}