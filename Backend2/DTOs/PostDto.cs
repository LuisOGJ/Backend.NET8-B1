namespace Backend2.DTOs
{
    public class PostDto
    {
        public int id {  get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? body { get; set; }
    }
}
