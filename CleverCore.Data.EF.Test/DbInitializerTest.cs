using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleverCore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CleverCore.Data.EF.Test
{
    public class DbInitializerTest
    {
        private readonly AppDbContext _context;
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<RoleManager<AppRole>> _mockRoleManager;

        public DbInitializerTest()
        {
            _context = ContextFactory.Create();
            _context.Database.EnsureCreated();

            _mockUserManager = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<AppUser>>().Object,
                new IUserValidator<AppUser>[0],
                new IPasswordValidator<AppUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<AppUser>>>().Object);

            _mockRoleManager = new Mock<RoleManager<AppRole>>(
                new Mock<IRoleStore<AppRole>>().Object,
                new IRoleValidator<AppRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<AppRole>>>().Object);
        }

        [Fact]
        public async Task Should_Success_When_Seed_Data()
        {
            var dbInitizer = new DbInitializer(_context, _mockUserManager.Object, _mockRoleManager.Object);
            await dbInitizer.Seed();

            Assert.True(await _context.Functions.AnyAsync());
        }


    }
}
