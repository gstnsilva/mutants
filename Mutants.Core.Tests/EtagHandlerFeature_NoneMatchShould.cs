using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Mutants.Core.Caching;
using Xunit;

namespace Mutants.Core.Tests
{
    public class EtagHandlerFeature_NoneMatchShould
    {
        [Fact]
        public void ReturnTrue_WhenHeadersAreEmpty()
        {
            StringValues etags;
            var taggeableMock = new Mock<IEtaggable>();
            var feature = SetupEtagHandlerFeature(false, etags);
            var noneMatched = feature.NoneMatch(taggeableMock.Object);
            Assert.True(noneMatched);
        }

        private EtagHandlerFeature SetupEtagHandlerFeature(bool isHeaderPresent, StringValues headerTags)
        {
            var headersMock = new Mock<IHeaderDictionary>();
            headersMock.Setup(x => x.TryGetValue("If-None-Match", out headerTags)).Returns(isHeaderPresent);

            return new EtagHandlerFeature(headersMock.Object);
        }

        [Fact]
        public void ReturnFalse_WhenHeadersContainTag()
        {
            const string tag = "\"TagToCompare\"";
            StringValues etags = new StringValues(tag);

            var taggeableMock = new Mock<IEtaggable>();
            taggeableMock.Setup(x => x.GetEtag()).Returns(tag);

            var feature = SetupEtagHandlerFeature(true, etags);
            var noneMatched = feature.NoneMatch(taggeableMock.Object);
            
            Assert.False(noneMatched);
        }

        [Fact]
        public void ReturnTrue_WhenHeadersDontContainTag()
        {
            const string tag = "\"TagToCompare\"";
            StringValues etags = new StringValues(tag);

            var taggeableMock = new Mock<IEtaggable>();
            taggeableMock.Setup(x => x.GetEtag()).Returns("AnotherTag");

            var feature = SetupEtagHandlerFeature(true, etags);
            var noneMatched = feature.NoneMatch(taggeableMock.Object);
            
            Assert.True(noneMatched);
        }
    }
}