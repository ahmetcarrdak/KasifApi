namespace KasifApi.DTO;

public class PostDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int GalleryId { get; set; }
    public int AddressesId { get; set; }
    public string Description { get; set; }
    public bool IsActived { get; set; }
    public bool IsDeleted { get; set; }
}