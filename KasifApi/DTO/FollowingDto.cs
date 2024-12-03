using KasifApi.Models;

namespace KasifApi.DTO;

public class FollowingDto
{
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public DateTime Created { get; set; }
    public Customer FromUser { get; set; }
    public Customer ToUser { get; set; }
}