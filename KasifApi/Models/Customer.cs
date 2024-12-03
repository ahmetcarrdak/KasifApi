namespace KasifApi.Models;

public class Customer
{
    public int Id { get; set; }
    public int ImageId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int AddressesId { get; set; }
    public string PhoneNumber { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string SchoolId { get; set; }
    public string Bio { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    
    public School School { get; set; }
    public Gallery Gallery { get; set; }
}