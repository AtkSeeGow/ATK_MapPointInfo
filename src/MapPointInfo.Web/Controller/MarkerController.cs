using MapPointInfo.Domain;
using MapPointInfo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MapPointInfo.Web
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "PermissionHandler")]

    public class MarkerController : Controller
    {
        private readonly MarkerRepository markerRepository;
        private readonly MarkerInfoRepository markerInfoRepository;

        public MarkerController(
            MarkerRepository markerRepository,
            MarkerInfoRepository markerInfoRepository)
        {
            this.markerRepository = markerRepository;
            this.markerInfoRepository = markerInfoRepository;
        }

        [HttpPost]
        public IEnumerable<Marker> FetchBy(Condition condition)
        {
            var markerInfos = markerInfoRepository.FetchBy(condition);
            var titles = markerInfos.Select(item => item.Title).ToArray();
            var markers = markerRepository.FindBy(item => titles.Contains(item.Title)).Result;
            return markers;
        }
    }
}