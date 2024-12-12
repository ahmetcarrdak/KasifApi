namespace KasifApi.Models;

public class School
{
    public int SchoolId { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string WebSite { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Faks { get; set; }
    public string Address { get; set; }
    public string Logo { get; set; } // url olarak tutulacak
    public string Password { get; set; }
}

