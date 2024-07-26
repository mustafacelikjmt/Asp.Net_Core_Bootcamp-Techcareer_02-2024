using BloggApp.Data.Abstract;
using BloggApp.Data.Concrete.EfCore;
using BloggApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BloggApp.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var entity = _context.Posts.FirstOrDefault(x => x.PostId == id);
            if (entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void EditPost(Post post)
        {
            var entity = _context.Posts.FirstOrDefault(x => x.PostId == post.PostId);
            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;
                _context.SaveChanges();
            }
        }

        public void EditPost(Post post, int[] tagIds)
        {
            var entity = _context.Posts.Include(x => x.Tags).FirstOrDefault(x => x.PostId == post.PostId);
            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsActive = post.IsActive;
                entity.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();
                _context.SaveChanges();
            }
        }
    }
}
