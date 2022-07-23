using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Vrnz2.TestUtils
{
    public abstract class AbstractControllerTest
        : AbstractTest
    {
        #region Attributes

        protected HttpRequest HttpRequest { get; private set; }
        protected HttpContext HttpContext { get; private set; }
        protected ControllerContext ControllerContext { get; private set; }

        #endregion

        #region Methods

        //Request Creation
        protected virtual HttpRequest CreateHttpRequest()
            => CreateHttpRequest("http", "http://localhost:8080", "/api", new HeaderDictionary());

        protected virtual HttpRequest CreateHttpRequest(HeaderDictionary headers)
            => CreateHttpRequest("http", "http://localhost:8080", "/api", headers);

        protected virtual HttpRequest CreateHttpRequest(string scheme, string host, string pathBase, HeaderDictionary headers) 
        {
            var request = new Mock<HttpRequest>();

            request.Setup(x => x.Scheme).Returns(scheme);
            request.Setup(x => x.Host).Returns(HostString.FromUriComponent(host));
            request.Setup(x => x.PathBase).Returns(PathString.FromUriComponent(pathBase));
            request.Setup(x => x.Headers).Returns(headers);

            return HttpRequest = request.Object;
        }

        //HTTP Context Creation
        protected virtual HttpContext CreateHttpContext()
        {
            HttpContext = Mock.Of<HttpContext>(_ => _.Request == HttpRequest );

            return HttpContext;
        }

        //Controller Context Creation
        protected virtual ControllerContext CreateControllerContext()
            => ControllerContext = new ControllerContext { HttpContext = HttpContext };

        protected void Init()
        {
            CreateHttpRequest();
            CreateHttpContext();
            CreateControllerContext();
        }

        protected void Init(HeaderDictionary headers)
        {
            CreateHttpRequest(headers);
            CreateHttpContext();
            CreateControllerContext();
        }

        #endregion
    }
}
