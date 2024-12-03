namespace KasifApi.DTO;

public class FollowingCreateDto
{
    public int From { get; set; }  // Takip eden kullanıcı ID
    public int To { get; set; }    // Takip edilen kullanıcı ID
}