using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_BE.Controllers;
using Repository.Models;
using Repository.Repository;

namespace PE_Pratice_SE160242.Controllers
{
    public class FootballPlayerController : ApiControllerBase
    {
        public readonly FootballPlayerService _playerService;
        public readonly FootballClubService _clubService;
        public FootballPlayerController()
        {
            _playerService = new FootballPlayerService();
            _clubService = new FootballClubService();
        }
        [Authorize(Roles = "1, 2")]
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_playerService.GetAll().AsNoTracking());
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Create([FromBody] FootballPlayer footballPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y.Count > 0)
                           .ToList());
            }
            if (_playerService.GetAll().Where(x => x.FootballPlayerId == footballPlayer.FootballPlayerId).AsNoTracking().FirstOrDefault() != null) return BadRequest("PlayerId already exist!!!");
            if (_clubService.GetAll().Where(x => x.FootballClubId == footballPlayer.FootballClubId).AsNoTracking().FirstOrDefault() == null) return BadRequest("ClubId not exist!!!");
            _playerService.Create(footballPlayer);
            return Ok();
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        public IActionResult Update(FootballPlayer footballPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y.Count > 0)
                           .ToList());
            }
            if (_playerService.GetAll().Where(x => x.FootballPlayerId == footballPlayer.FootballPlayerId).AsNoTracking().FirstOrDefault() == null) return BadRequest();
            if (_clubService.GetAll().Where(x => x.FootballClubId == footballPlayer.FootballClubId).AsNoTracking().FirstOrDefault() == null) return BadRequest("ClubId not exist!!!");
            _playerService.Update(footballPlayer);
            return Ok();
        }
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y.Count > 0)
                           .ToList());
            }
            var silverExist = _playerService.GetAll().AsNoTracking().Where(x => x.FootballPlayerId == id).FirstOrDefault();
            if (silverExist == null) return BadRequest();
            _playerService.Delete(silverExist);
            return Ok();
        }
    }
}
