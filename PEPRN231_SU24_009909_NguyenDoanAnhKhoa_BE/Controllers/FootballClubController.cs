using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_BE.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
namespace PE_Pratice_SE160242.Controllers
{
    public class FootballClubController : ApiControllerBase
    {
        public readonly FootballClubService _clubService;
        public FootballClubController()
        {
            _clubService = new FootballClubService();
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clubService.GetAll());
        }
    }
}
