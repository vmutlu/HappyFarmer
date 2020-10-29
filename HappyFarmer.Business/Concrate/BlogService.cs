using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICommentRepository _commentRepository;
        public BlogService(IBlogRepository blogRepository, ICommentRepository commentRepository)
        {
            _blogRepository = blogRepository;
            _commentRepository = commentRepository;
        }

        public void Create(FarmerBlog entity)
        {
            _blogRepository.Create(entity);
        }

        public void Delete(FarmerBlog entity)
        {
            _blogRepository.Delete(entity);
        }

        public List<FarmerBlog> GetAll(int? page = 0, int? pageSize = 0)
        {
            using (var context = new FarmerContext())
            {
                if (page == 0 && pageSize == 0)
                {
                    var response = context.Blogs
                      .OrderBy(i => i.QueueNumber).AsNoTracking().ToList();

                    return response;
                }

                else
                {
                    var blogs = context.Blogs.AsQueryable();

                    blogs = blogs
                        .OrderByDescending(i => i.CreatedDate)
                        .AsNoTracking();                        

                    return blogs.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
            }

        }

        public FarmerBlog GetById(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = (from i in context.Blogs
                                where i.Id == id
                                select new FarmerBlog
                                {
                                    Id = i.Id,
                                    CreatedDate = i.CreatedDate,
                                    Description = i.Description,
                                    ImagePath = i.ImagePath,
                                    QueueNumber = i.QueueNumber,
                                    Title = i.Title,
                                    UserId = i.UserId,
                                    Comments = (from c in i.Comments
                                                where i.Id == c.BlogId
                                                select new FarmerComment
                                                {
                                                    BlogId = c.BlogId,
                                                    Email = c.Email,
                                                    CommentDate = c.CommentDate,
                                                    Id = c.Id,
                                                    Comment = c.Comment,
                                                    Name = c.Name,
                                                    Surname = c.Surname,
                                                    UserId = c.UserId,
                                                    WebSite = c.WebSite
                                                }).ToList()
                                }).FirstOrDefault();

                return response;
            }
        }

        public void Update(FarmerBlog entity)
        {
            _blogRepository.Update(entity);
        }

        public void CreateComment(FarmerComment entity)
        {
            _commentRepository.Create(entity);
        }

    }
}
