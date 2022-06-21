using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APBDRetake1.Dto;
using APBDRetake1.Services;
using System.Threading.Tasks;

namespace APBDRetake1.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {

        private readonly IDatabaseService _dbService;

        public AlbumsController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{IdAlbum}")]
        public async Task<IActionResult> GetInfoAlbum([FromRoute]int IdAlbum)
        {
            if(!await _dbService.DoesAlbumExistAsync(IdAlbum))
            {
                return NotFound($"The Album with id {IdAlbum} does not exist");
            }
            var result = _dbService.GetInfoAlbumAsync(IdAlbum);
            return Ok(result);
        }
    }
}
