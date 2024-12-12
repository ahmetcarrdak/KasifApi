namespace KasifApi.Models;

public class EventRegistion
{
    public int EventRegistionId { get; set; }
    public int PostId { get; set; }
    public int CustomerId { get; set; }
    public DateTime Created { get; set; }
    
    public virtual Customer Customer { get; set; }
    public virtual Post Post { get; set; }
}