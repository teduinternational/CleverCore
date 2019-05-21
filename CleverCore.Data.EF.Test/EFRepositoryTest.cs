using CleverCore.Data.Entities;
using CleverCore.Data.Enums;
using CleverCore.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CleverCore.Data.EF.Test
{
    public class EFRepositoryTest
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public EFRepositoryTest()
        {
            _context = ContextFactory.Create();
            _context.Database.EnsureCreated();
            _unitOfWork = new EFUnitOfWork(_context);
        }

        [Fact]
        public void Constructor_Should_Success_When_Create_EfRepository()
        {
            EfRepository<Function, string> repository = new EfRepository<Function, string>(_context);
            Assert.NotNull(repository);
        }

        [Fact]
        public void Add_Should_Have_Record_When_Insert()
        {
            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);
            efRepository.Add(new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            });
            _unitOfWork.Commit();

            Function function = efRepository.FindById("USER");
            Assert.NotNull(function);

        }


        [Fact]
        public void FindAll_Should_Return_All_Record_In_Table()
        {
            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);

            efRepository.Add(new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            });
            efRepository.Add(new Function()
            {
                Id = "ROLE",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 2
            });
            _unitOfWork.Commit();

            List<Function> functions = efRepository.FindAll().ToList();
            Assert.Equal(2, functions.Count);

        }

        [Fact]
        public void FindByIdAsync_Should_Return_True_Record_In_Table()
        {
            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);

            efRepository.Add(new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            });
            _unitOfWork.Commit();

            Function function = efRepository.FindById("USER");
            Assert.Equal("Test", function.Name);

        }


        [Fact]
        public void Update_Should_Have_Change_Record()
        {

            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);
            efRepository.Add(new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            });
            _unitOfWork.Commit();
            Function function1 = efRepository.FindById("USER");
            function1.Name = "Test2";
            efRepository.Update(function1);
            _unitOfWork.Commit();

            Function function = efRepository.FindById("USER");
            Assert.Equal("Test2", function.Name);
        }

        [Fact]
        public void Remove_Should_Success_When_Pass_Valid_Id()
        {

            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);
            Function function = new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            };
            efRepository.Add(function);
            _unitOfWork.Commit();

            efRepository.Remove(function);
            _unitOfWork.Commit();

            Function dbFunction = efRepository.FindById("USER");
            Assert.Null(dbFunction);
        }



        [Fact]
        public void FindSingle_Should_Return_One_Record_If_Condition_Is_Match()
        {
            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);

            Function function = new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            };
            efRepository.Add(function);
            _unitOfWork.Commit();

            Function result = efRepository.FindSingle(x => x.Name == "Test");
            Assert.NotNull(result);
        }


    }
}
