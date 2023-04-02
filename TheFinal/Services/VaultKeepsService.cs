namespace TheFinal.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService = null)
        {
            _repo = repo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep createVaultKeep(VaultKeep vaultKeepData, Profile userInfo)
        {
            Vault vault = _vaultsService.GetVault(vaultKeepData.VaultId, userInfo);
            if(vault.CreatorId != userInfo.Id) throw new Exception("You are not the creator of this vault");
            return _repo.createVaultKeep(vaultKeepData);
        }

        internal void deleteVaultKeep(int vkId, string userId)
        {
            VaultKeep vaultKeep = _repo.getVaultKeepById(vkId);
            if(vaultKeep.CreatorId != userId){
                throw new Exception("You are not the creator of this vault keep");
            }
            _repo.deleteVaultKeep(vaultKeep);
        }
    }
}