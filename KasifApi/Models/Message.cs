namespace KasifApi.Models;

public class Message
{
    public int MessageId { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }
    public bool IsRead { get; set; }
    public DateTime Created { get; set; }
    
    public Customer FromUser { get; set; }
    public Customer ToUser { get; set; }
    public Post Post { get; set; }

    public Customer ReadBy { get; set; }
}