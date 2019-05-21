using System;
using System.Collections.Generic;
using System.Text;
using CleverCore.Utilities.Dtos;
using Xunit;

namespace CleverCore.Utilities.Test.Dtos
{
    public class PagedResultTest
    {
        [Fact]
        public void Constructor_CreateObject_NotNullObject()
        {
            var pagedResult = new PagedResult<Array>();
            Assert.NotNull(pagedResult);
        }

        [Fact]
        public void Constructor_CreateObject_WithResultNotNull()
        {
            var pagedResult = new PagedResult<Array>();
            Assert.NotNull(pagedResult.Results);
        }
    }
}
