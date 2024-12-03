namespace KasifApi.DTO;

public class PostUpdateDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsActived { get; set; }
    public bool IsDeleted { get; set; }
}