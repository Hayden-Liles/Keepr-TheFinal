namespace TheFinal.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault GetVault(int id, Account userInfo)
        {
            Vault vault = _repo.getVault(id);
            if(vault.IsPrivate == true){
                if(userInfo == null) throw new Exception($"You are unauthorized to view this vault");
                if(vault.CreatorId == userInfo?.Id){
                    return vault;
                }
                throw new Exception($"You are unauthorized to view this vault");
            }
            return vault;
        }

        internal Vault updateVault(Vault vaultData, Account userInfo)
        {
            Vault vaultToCheck = this.GetVault(vaultData.Id, userInfo);

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

        internal string deleteVault(int vaultId, Account userInfo)
        {
            this.GetVault(vaultId, userInfo);
            _repo.deleteVault(vaultId);
            return $"Vault at Id:{vaultId} has been successfuly deleted!";
        }

        internal List<Keep> getKeepsInVault(int id)
        {
            throw new NotImplementedException();
        }
    }
}