namespace TheFinal.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault createVault(Vault vaultData)
        {
            return _repo.createVault(vaultData);
        }

        internal Vault GetVault(int id, Profile userInfo)
        {
            Vault vault = _repo.getVault(id);
            if(vault.IsPrivate == true || vault == null){
                if(userInfo == null || vault == null) throw new Exception($"You are unauthorized to view this vault");
                if(vault.CreatorId != userInfo?.Id) throw new Exception($"You are unauthorized to view this vault");
            }
            return vault;
        }

        internal Vault updateVault(Vault vaultData, Profile userInfo)
        {
            Vault vaultToCheck = this.GetVault(vaultData.Id, userInfo);
            if(vaultToCheck.CreatorId != userInfo.Id) throw new Exception("You are unauthorized to edit this vault");

            if(vaultData.Name == null){
                vaultData.Name = vaultToCheck.Name;
            }

            if(vaultData.Description == null){
                vaultData.Description = vaultToCheck.Description;
            }

            if(vaultData.Img == null){
                vaultData.Img = vaultToCheck.Img;
            }
            vaultData.Creator = vaultToCheck.Creator;
            vaultData.CreatorId = vaultToCheck.CreatorId;
            Vault vault = _repo.updateVault(vaultData);
            return vault;
        }

        internal string deleteVault(int vaultId, Profile userInfo)
        {
            Vault vault = this.GetVault(vaultId, userInfo);
            if(vault.CreatorId != userInfo.Id) throw new Exception("You are unauthorized to delete this vault");
            _repo.deleteVault(vaultId);
            return $"Vault at Id:{vaultId} has been successfuly deleted!";
        }

        internal List<VaultedKeep> getKeepsInVault(int vaultId, Profile userInfo)
        {
            this.GetVault(vaultId, userInfo);
            return _repo.getKeepsInVault(vaultId);
        }

        internal List<Vault> GetVaultsByProfile(Profile userInfo)
        {
            List<Vault> vaults = _repo.GetVaultsByProfile(userInfo?.Id);
            if(vaults == null) throw new Exception("Invalid Id");
            List<Vault> nVaults = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userInfo?.Id);
            return nVaults;
        }
    }
}