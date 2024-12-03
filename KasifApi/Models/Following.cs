namespace KasifApi.Models;

public class Following
{
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    
    public Customer FromUser { get; set; }
    public Customer ToUser { get; set; }
}