using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CleverCore.Data.EF.Test
{
    public class AppDbContextTest
    {
        [Fact]
        public void Constructor_CreateInMemoryDb_Success()
        {
            var context = ContextFactory.Create();
            Assert.True(context.Database.EnsureCreated());
        }


    }
}
