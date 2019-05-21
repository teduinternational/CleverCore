using CleverCore.Data.Entities;
using CleverCore.Data.Enums;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CleverCore.Data.EF.Test
{
    public class EFUnitOfWorkTest
    {
        public EFUnitOfWorkTest()
        {
            _context = ContextFactory.Create();
        }

        private readonly AppDbContext _context;

        [Fact]
        public void Commit_Should_Success_When_Save_Data()
        {
            EfRepository<Function, string> efRepository = new EfRepository<Function, string>(_context);
            EFUnitOfWork unitOfWork = new EFUnitOfWork(_context);
            efRepository.Add(new Function()
            {
                Id = "USER",
                Name = "Test",
                Status = Status.Active,
                SortOrder = 1
            });
            unitOfWork.Commit();

            List<Function> functions = efRepository.FindAll().ToList();
            Assert.Single(functions);
        }
    }
}
