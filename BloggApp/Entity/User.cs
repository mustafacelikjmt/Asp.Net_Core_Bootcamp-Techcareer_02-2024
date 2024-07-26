using System.ComponentModel.DataAnnotations;

namespace BloggApp.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public List<Post> Posts { get; set; } = new(); //new List<Post>(); yazıncada aynı anlama geliyo
        public List<Comment> Comments { get; set; } = new();
    }
}
