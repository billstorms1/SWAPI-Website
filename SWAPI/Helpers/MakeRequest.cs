using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using static SWAPI.Helpers.RequestHandler;

namespace SWAPI.Helpers
{
    public interface IMakeRequest
    {
        List<JObject> GetGeneralData(string topic);
        JObject GetSpecificData(string url);
    }
    public class MakeRequest : IMakeRequest
    {
        private readonly ICache _cache;
        private readonly IRequestHandler _requestHandler;
        private List<JObject> _resultBlobs;
        private JObject _resultBlob;

        public MakeRequest(ICache cache, IRequestHandler requestHandler)
        {
            _cache = cache;
            _requestHandler = requestHandler;
        }

        public List<JObject> GetGeneralData(string topic)
        {
            var cacheResults = _cache.CheckCache(topic);
            if (cacheResults == null)
            {
                _resultBlobs = _requestHandler.MakeGeneralRequest($"https://swapi.co/api/{topic}");
                _cache.SetCache(_resultBlobs, topic);
                return _resultBlobs;
            }            
            return cacheResults;
        }

        public JObject GetSpecificData(string requestUrl)
        {
            var cacheResults = _cache.CheckCache(requestUrl);
            if (cacheResults == null){
                _resultBlobs = _requestHandler.MakeGeneralRequest(requestUrl);
                _resultBlob = _resultBlobs.FirstOrDefault();
                _cache.SetCache(_resultBlobs, requestUrl);
                return _resultBlob;
            }
            return cacheResults[0];
        }
    }
}