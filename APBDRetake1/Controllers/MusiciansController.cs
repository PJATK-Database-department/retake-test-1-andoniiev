using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APBDRetake1.Dto;
using APBDRetake1.Services;
using System.Threading.Tasks;

namespace APBDRetake1.Controllers
{
    [Route("api/musicians")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly IDatabaseService _dbService;

        public MusiciansController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpDelete("{IdMusician}")]
        public async Task<ActionResult> DeleteMusician(int IdMusician)
        {
            if (!await _dbService.DoesMusicianExistAsync(IdMusician))
            {
                return NotFound($"The Musician with id {IdMusician} does not exist");
            }
            var result = _dbService.DeleteMusicianAsync(IdMusician);
            return Ok(result);
        }
    }
}
