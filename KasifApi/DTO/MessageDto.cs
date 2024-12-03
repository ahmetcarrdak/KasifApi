namespace KasifApi.DTO;

public class MessageDto
{
    public int From { get; set; }
    public int To { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }
}