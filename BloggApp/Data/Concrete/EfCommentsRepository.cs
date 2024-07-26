using BloggApp.Data.Abstract;
using BloggApp.Data.Concrete.EfCore;
using BloggApp.Entity;

namespace BloggApp.Data.Concrete
{
    public class EfCommentsRepository : ICommentRepository
    {
        private BlogContext _context;
        public EfCommentsRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComments(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
