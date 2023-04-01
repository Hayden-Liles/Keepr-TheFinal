namespace TheFinal.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep createVaultKeep(VaultKeep vaultKeepData)
        {
            return _repo.createVaultKeep(vaultKeepData);
        }
    }
}