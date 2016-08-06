using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace PictureAnalysis.Mobile.Services
{
    public class PictureRestClient : BaseRestClient
    {

        public PictureRestClient() : base("Analyze")
        {
        }

        public async Task<AnalysisResult> Post(byte[] image)
        {

            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new ByteArrayContent( image ), "image", "image.jpg");

            HttpResponseMessage response = await _client.PostAsync(_url, form);

            _client.Dispose();
            var caption = JsonConvert.DeserializeObject< AnalysisResult>(await response.Content.ReadAsStringAsync());

            return caption;
        }
    }
}
