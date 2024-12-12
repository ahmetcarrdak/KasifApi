namespace KasifApi.Models;

public class Post
{
    public int PostId { get; set; }
    public int CustomerId { get; set; }
    public int GalleryId { get; set; }
    public int AddressId { get; set; }
    public int SchoolId { get; set; }
    public string Description { get; set; }
    public int? PartnerShareId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsLike { get; set; }
    public bool IsLikeCount { get; set; }
    public bool IsCommentWrite { get; set; }
    public bool IsComment { get; set; }
    public bool IsActived { get; set; }

    public Customer Customer { get; set; }
    public Address Address { get; set; }
    public School School { get; set; }
    public Gallery Gallery { get; set; }
}