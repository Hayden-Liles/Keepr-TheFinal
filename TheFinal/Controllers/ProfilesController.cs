namespace TheFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly Auth0Provider _auth;

        public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
        {
            _profilesService = profilesService;
            _auth = auth;
        }


        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfile(string id)
        {
            try
            {
                Profile profile = _profilesService.GetProfile(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfile(string id)
        {
            try
            {
                List<Keep> keeps = _profilesService.GetKeepsByProfile(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetVaultsByProfile(string id)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                List<Vault> vaults = _profilesService.GetVaultsByProfile(id, userInfo);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}