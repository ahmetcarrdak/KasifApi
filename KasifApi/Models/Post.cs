namespace KasifApi.Models;

public class Post
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string GalleryId { get; set; }
    public int AddressesId { get; set; }
    public string Description { get; set; }
    public bool IsActived { get; set; }
    public bool IsDeleted { get; set; }
    
    public Customer Customer { get; set; }
}