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

        public MarkerController(MarkerRepository markerRepository)
        {
            this.markerRepository = markerRepository;
        }

        [HttpPost]
        public IEnumerable<Marker> FetchBy(Condition condition)
        {
            var markers = new List<Marker>();

            this.markerRepository.FindBy(item => )

            return markers;
        }
    }
}