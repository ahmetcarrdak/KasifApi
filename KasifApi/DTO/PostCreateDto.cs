namespace KasifApi.DTO;

public class PostCreateDto
{
    public int CustomerId { get; set; }
    public int GalleryId { get; set; }
    public int AddressesId { get; set; }
    public string Description { get; set; }
}