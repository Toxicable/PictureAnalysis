using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Mobile.Services
{
    public class BaseRestClient : IDisposable
    {
        public readonly string _baseUrl = "http://pictureanalysisapi.azurewebsites.net/";
        public readonly HttpClient _client;
        public readonly string _endPoint;
        public string _url { get { return _baseUrl + _endPoint; } }
                
        public BaseRestClient(string endPoint)
        {
            _endPoint = endPoint;
            _client = new HttpClient();
        }

        public Uri NewUri(string url)
        {
            return new Uri(url);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
