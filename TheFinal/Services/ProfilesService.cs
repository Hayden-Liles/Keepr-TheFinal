namespace TheFinal.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal List<Keep> GetKeepsByProfile(string id)
        {
            List<Keep> keeps = _repo.GetKeepsByProfile(id);
            if(keeps == null) throw new Exception("Invalid Id");
            return keeps;
        }

        internal Profile GetProfile(string id)
        {
            Profile profile = _repo.GetProfile(id);
            if(profile == null) throw new Exception("Invalid Id");
            return profile;
        }

        internal List<Vault> GetVaultsByProfile(string id, Profile userInfo)
        {
            List<Vault> vaults = _repo.GetVaultsByProfile(id);
            if(vaults == null) throw new Exception("Invalid Id");
            List<Vault> nVaults = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userInfo?.Id);
            return nVaults;
        }
    }
}