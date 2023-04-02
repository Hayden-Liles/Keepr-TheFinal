namespace TheFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth;

        public KeepsController(KeepsService keepsService, Auth0Provider auth)
        {
            _keepsService = keepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData){
            try 
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                keepData.CreatorId = userInfo.Id;
                keepData.Creator = userInfo;
                Keep keep = _keepsService.CreateKeep(keepData);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Keep>> GetKeeps(){
            try 
            {
                List<Keep> keeps = _keepsService.GetKeeps();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> getKeep(int id){
            try 
            {
                Keep keep = _keepsService.GetKeep(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> updateKeep([FromBody] Keep keepData, int id){
            try 
            {
                Profile userinfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                keepData.CreatorId = userinfo.Id;
                Keep keep = _keepsService.updateKeep(keepData, id);
                return keep;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<string>> deleteKeep(int id){
            try 
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                string message = _keepsService.deleteKeep(userInfo.Id, id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}