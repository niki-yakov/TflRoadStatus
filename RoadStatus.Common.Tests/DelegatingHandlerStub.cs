using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RoadStatus.Common.Tests
{
    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStub()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StringContent("content");

            _handlerFunc = (request, cancellationToken) => 
            {
                if (request.RequestUri.PathAndQuery.IndexOf("HttpStatus200") >= 0)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("[" +
                                      "{" +
                                      "  \"$type\": \"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\"," +
                                      "  \"id\": \"a2\"," +
                                      "  \"displayName\": \"A2\"," +
                                      "  \"statusSeverity\": \"Good\"," +
                                      "  \"statusSeverityDescription\": \"No Exceptional Delays\"," +
                                      "  \"bounds\": \"[[-0.0857,51.44091],[0.17118,51.49438]]\"," +
                                      "  \"envelope\": \"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\"," +
                                      "  \"url\": \"/Road/a2\" " +
                                    "}]");
                }
                else if (request.RequestUri.PathAndQuery.IndexOf("HttpStatus400") >= 0)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                else if (request.RequestUri.PathAndQuery.IndexOf("HttpStatus404") >= 0)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Content = new StringContent(
                                    "{ \"$type\": \"Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities\", " +
                                    " \"timestampUtc\": \"2018-08-26T06:16:38.3973386Z\", " +
                                    " \"exceptionType\": \"EntityNotFoundException\", " +
                                    " \"httpStatusCode\": 404, " +
                                    " \"httpStatus\": \"NotFound\", " +
                                    " \"relativeUri\": \"/Road/A223\", " +
                                    " \"message\": \"The following road id is not recognised: A223\"} ");

                }

                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
}