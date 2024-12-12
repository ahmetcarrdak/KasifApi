namespace KasifApi.Models;

public class PostSaved
{
    public int PostSavedId { get; set; }
    public int CustomerId { get; set; }
    public int PostId { get; set; }
    
    public Customer Customer { get; set; }
    public Post Post { get; set; }
}