using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using CleverCore.Application.AutoMapper;
using CleverCore.Application.Implementation;
using CleverCore.Application.ViewModels.Blog;
using CleverCore.Data.Entities;
using CleverCore.Data.Enums;
using CleverCore.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace CleverCore.Application.Test.Implementation
{
    public class BlogServiceTest
    {
        private readonly Mock<IRepository<Blog, int>> _mockBlogRepository;
        private readonly Mock<IRepository<Tag, string>> _mockTagRepository;
        private readonly Mock<IRepository<BlogTag, int>> _mockBlogTagRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        public BlogServiceTest()
        {
            _mockBlogRepository = new Mock<IRepository<Blog, int>>();

            _mockBlogTagRepository = new Mock<IRepository<BlogTag, int>>();

            _mockTagRepository = new Mock<IRepository<Tag, string>>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();

        }

        [Fact]
        public void Add_ValidInput_SucessResult()
        {
            var mockMapper = new MapperConfiguration(cfg=>
                cfg.AddProfile(new DomainToViewModelMappingProfile()));
            var mapper = mockMapper.CreateMapper();

            var listTags = new List<Tag>
            {
                new Tag {Id = "tag1", Name = "Tag 1"},
                new Tag {Id = "tag2", Name = "Tag 2"},
                new Tag {Id = "tag3", Name = "Tag 3"}
            };

            _mockTagRepository.Setup(x => x.FindAll()).Returns(listTags.AsQueryable());

            _mockTagRepository.Setup(x => x.Add(It.IsAny<Tag>()));

            _mockBlogRepository.Setup(x => x.Add(It.IsAny<Blog>()));

            var blogService = new BlogService(_mockBlogRepository.Object, _mockBlogTagRepository.Object,
                _mockTagRepository.Object, _mockUnitOfWork.Object, mapper);

            var result = blogService.Add(new BlogViewModel()
            {
                Id = 0,
                Name ="test",
                Status = Status.Active,
                Tags = "test"
            });
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_ValidQuery_ResultSuccess()
        {
            var mockMapper = new MapperConfiguration(cfg => 
                cfg.AddProfile(new DomainToViewModelMappingProfile()));
            var mapper = mockMapper.CreateMapper();

            var blogs = new List<Blog>
            {
                new Blog {Id = 1, Name = "test 1", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 2, Name = "test 2", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 3, Name = "test 2", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 4, Name = "test 3", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 5, Name = "test 4", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}}
            };
            _mockBlogRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<Blog,object>>>()))
                .Returns(blogs.AsQueryable());

            var blogService = new BlogService(_mockBlogRepository.Object, _mockBlogTagRepository.Object,
                _mockTagRepository.Object, _mockUnitOfWork.Object, mapper);
            var result = blogService.GetAll();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GetAllPaging_ValidQuery_ResultSuccess()
        {
            var mockMapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new DomainToViewModelMappingProfile()));
            var mapper = mockMapper.CreateMapper();

            var blogs = new List<Blog>
            {
                new Blog {Id = 1, Name = "test 1", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 2, Name = "test 2", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 3, Name = "test 2", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 4, Name = "test 3", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 5, Name = "test 4", Status = Status.Active,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}}
            };
            _mockBlogRepository.Setup(x => x.FindAll())
                .Returns(blogs.AsQueryable());

            var blogService = new BlogService(_mockBlogRepository.Object, _mockBlogTagRepository.Object,
                _mockTagRepository.Object, _mockUnitOfWork.Object, mapper);
            var result = blogService.GetAllPaging(string.Empty,2,1);

            Assert.Equal(3, result.PageCount);
        }

        [Fact]
        public void GetById_ValidQuery_ResultSuccess()
        {
            var mockMapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new DomainToViewModelMappingProfile()));
            var mapper = mockMapper.CreateMapper();

            _mockBlogRepository.Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(new Blog { Id = 1, Name = "test 1", Status = Status.Active });

            var blogService = new BlogService(_mockBlogRepository.Object, _mockBlogTagRepository.Object,
                _mockTagRepository.Object, _mockUnitOfWork.Object, mapper);
            var result = blogService.GetById(1);

            Assert.Equal(1, result.Id);
        }


        [Fact]
        public void GetLastest_ValidQuery_ResultSuccess()
        {
            var mockMapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new DomainToViewModelMappingProfile()));
            var mapper = mockMapper.CreateMapper();

            var blogs = new List<Blog>
            {
                new Blog {Id = 1, Name = "test 1", Status = Status.Active,DateCreated = DateTime.Now,BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 2, Name = "test 2", Status = Status.Active,DateCreated = DateTime.Now.AddDays(-1),BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 3, Name = "test 2", Status = Status.Active,DateCreated = DateTime.Now.AddDays(-2),BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 4, Name = "test 3", Status = Status.Active,DateCreated = DateTime.Now.AddDays(-3),BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}},
                new Blog {Id = 5, Name = "test 4", Status = Status.Active,DateCreated = DateTime.Now.AddDays(-4),BlogTags = new List<BlogTag>(){new BlogTag(){Id=1,TagId = "test"}}}
            };
            _mockBlogRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<Blog, bool>>>()))
                .Returns(blogs.AsQueryable());

            var blogService = new BlogService(_mockBlogRepository.Object, _mockBlogTagRepository.Object,
                _mockTagRepository.Object, _mockUnitOfWork.Object, mapper);
            var result = blogService.GetLastest(2);

            Assert.Equal(2, result.Count);
            Assert.Equal(1,result[0].Id);
        }

    }
}
