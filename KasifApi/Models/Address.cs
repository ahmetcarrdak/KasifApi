namespace KasifApi.Models;

public class Address
{
    public int AddressId { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string road { get; set; }
    public string posteCode { get; set; }
}