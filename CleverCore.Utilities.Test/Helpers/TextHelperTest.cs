using System;
using System.Collections.Generic;
using System.Text;
using CleverCore.Utilities.Helpers;
using Xunit;

namespace CleverCore.Utilities.Test.Helpers
{
    public class TextHelperTest
    {
        [Theory]
        [InlineData("TEDU - Học lập trình trực tuyến - TEDU.COM.VN")]
        [InlineData("TEDU -- Học lập trình trực tuyến - TEDU.COM.VN")]
        [InlineData("TEDU - Học lập trình  trực tuyến - TEDU.COM.VN?")]
        public void ToUnsignString_UpperCaseInput_LowerCaseOutput(string input)
        {
            var result = TextHelper.ToUnsignString(input);
            Assert.Equal("tedu-hoc-lap-trinh-truc-tuyen-tedu-com-vn",result);
        }

        [Fact]
        public void ToString_NumberInput_CharactersNumber()
        {
            var result = TextHelper.ToString(120);
            Assert.Equal("một trăm hai mươi đồng chẵn", result);
        }
    }
}
