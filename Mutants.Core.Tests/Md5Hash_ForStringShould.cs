using Mutants.Core.Cryptography;
using Xunit;

namespace Mutants.Core.Tests
{
    public class Md5Hash_ForStringShould
    {
        [Fact]
        public void ReturnSameHashForSameInput()
        {
            const string input = "This i$ th3 1nput for h45hing!";
            var hash = Md5Hash.ForString(input);
            var secondHash = Md5Hash.ForString(input);
            Assert.Equal(hash, secondHash);
        }
    }
}