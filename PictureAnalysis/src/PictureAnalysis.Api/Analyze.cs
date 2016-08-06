using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PictureAnalysis.Api
{
    [Route("Analyze")]
    public class AnalyzeController : Controller
    {

        #region Contructors and Private fields
        private readonly VisionServiceClient _computerVisionClient;
        private readonly VisualFeature[] _visualFeatures;

        public AnalyzeController(
            VisionServiceClient computerVision
            )
        {
            _computerVisionClient = computerVision;
            _visualFeatures = new VisualFeature[] {
                VisualFeature.Adult,
                VisualFeature.Categories,
                VisualFeature.Color,
                VisualFeature.Description,
                VisualFeature.Faces,
                VisualFeature.ImageType,
                VisualFeature.Tags
            };
        }
        #endregion

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile image)
        {
            AnalysisResult analysisResult = await _computerVisionClient.AnalyzeImageAsync(
                image.OpenReadStream(),
                _visualFeatures
                );
            
            return new ObjectResult(analysisResult);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
    
}
