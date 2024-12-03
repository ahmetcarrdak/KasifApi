namespace KasifApi.DTO;

public class MessageResponseDto
{
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }
    public bool IsRead { get; set; }
    public DateTime Created { get; set; }
}