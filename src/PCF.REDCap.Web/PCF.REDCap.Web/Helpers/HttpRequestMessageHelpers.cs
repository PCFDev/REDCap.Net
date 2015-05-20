using System;
using System.Net;
using System.Net.Http;

namespace PCF.REDCap.Web.Helpers
{
    public static class HttpRequestMessageHelpers
    {
        public static HttpResponseMessage CreateRedirectResponse(this HttpRequestMessage request, HttpStatusCode statusCode, Uri url)
        {
            var response = request.CreateResponse(statusCode);
            response.Headers.Location = url;
            return response;
        }

        public static HttpResponseMessage CreateRedirectResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, Uri url, T value)
        {
            var response = request.CreateResponse(statusCode, value);
            response.Headers.Location = url;
            return response;
        }
    }
}