namespace TheFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth, KeepsService keepsService)
        {
            _vaultsService = vaultsService;
            _auth = auth;
            _keepsService = keepsService;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Vault>> createVault([FromBody] Vault vaultData)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                vaultData.Creator = userInfo;
                Vault vault = _vaultsService.createVault(vaultData);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> getVault(int id)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                Vault vault = _vaultsService.GetVault(id, userInfo);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> updateVault([FromBody] Vault vaultData, int id)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                vaultData.Id = id;
                Vault vault = _vaultsService.updateVault(vaultData, userInfo);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> deleteVault(int id)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                string message = _vaultsService.deleteVault(id, userInfo);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}/keeps")]
        public async Task<ActionResult<List<VaultedKeep>>> getKeepsInVault(int vaultId)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                List<VaultedKeep> keeps = _vaultsService.getKeepsInVault(vaultId, userInfo);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}