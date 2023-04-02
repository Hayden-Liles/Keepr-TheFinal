namespace TheFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth;

        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> createVaultKeep([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                vaultKeepData.CreatorId = userInfo.Id;
                VaultKeep vaultKeep = _vaultKeepsService.createVaultKeep(vaultKeepData, userInfo);
                return vaultKeep;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<string>> deleteVaultKeep(int id)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                _vaultKeepsService.deleteVaultKeep(id, userInfo.Id);
                return Ok("Successfully Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}