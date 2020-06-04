using Microsoft.AspNetCore.Mvc;
using Moq;
using Mutants.Core;
using Mutants.Core.Infrastructure;
using Xunit;

namespace Mutants.Core.Tests
{
    public class LinkRewriter_RewriteShould
    {
        [Fact]
        public void ReturnNewLinkWithHref_WhenListIsValid()
        {
            const string routeName = "SomeRoute";
            var expectedHref = $"http://local/{routeName}";
            var linkToRewrite = Link.To(routeName);
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(x => x.Link(routeName, It.IsAny<object>())).Returns(expectedHref);

            var linkRewriter = new LinkRewriter(urlHelperMock.Object);
            var newLink = linkRewriter.Rewrite(linkToRewrite);
            
            Assert.NotNull(newLink);
            Assert.Equal(expectedHref, newLink.Href);
        }
        
        [Fact]
        public void ReturnNull_WhenListIsNull()
        {
            var urlHelperMock = new Mock<IUrlHelper>();

            var linkRewriter = new LinkRewriter(urlHelperMock.Object);
            var newLink = linkRewriter.Rewrite(null);

            Assert.Null(newLink);
        }
    }
}