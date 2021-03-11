using Xunit;
using inetmon;

namespace test_inetmon
{
    public class TestOptionsList
    {
        [Fact]
        public void TestUrlDefaultsToGoogle()
        {
            var options = new InputOptions(null);
            Assert.Equal("www.google.com", options.Get("url"));
            Assert.True(options.ValidOptions);
        }

        [Fact]
        public void EmptyArgsListReturnsTest0()
        {
            var options = new InputOptions(null);
            Assert.Equal(0, options.GetInt("test"));
            Assert.True(options.ValidOptions);
        }

        [Fact]
        public void FullTestOptionReturnsPing1()
        {
            string[] array = new string[] { "--test" };
            var options = new InputOptions(array);
            Assert.Equal(1, options.GetInt("test"));
            Assert.True(options.ValidOptions);
        }

        [Fact]
        public void ShortTestOptionReturnsPing1()
        {
            string[] array = new string[] { "-t" };
            var options = new InputOptions(array);
            Assert.Equal(1, options.GetInt("test"));
            Assert.True(options.ValidOptions);
        }

        [Fact]
        public void InvalidOptionReturnsInvalidOption()
        {
            string[] array = new string[] { "--xxxyyyzzz asdfasdfasdf" };
            var options = new InputOptions(array);
            Assert.False(options.ValidOptions);
        }

        [Fact]
        public void CheckOutOfBoundsArgumentsReturnInvalidOption()
        {
            string[] array = new string[] { "-p" };
            var options = new InputOptions(array);
            Assert.False(options.ValidOptions);
            Assert.Equal(60, options.GetInt("ping"));
        }

    }
}
