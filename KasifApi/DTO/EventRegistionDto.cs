namespace KasifApi.DTO;

// Listeleme ve detay görüntüleme için DTO
public class EventRegistionDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int CustomerId { get; set; }
    public DateTime Created { get; set; }
}