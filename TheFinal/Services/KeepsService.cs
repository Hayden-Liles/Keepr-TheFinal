namespace TheFinal.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            return _repo.CreateKeep(keepData);
        }

        internal List<Keep> GetKeeps()
        {
            return _repo.getKeeps();
        }

        internal Keep GetKeep(int id)
        {
            return _repo.getKeep(id);
        }

        internal Keep updateKeep(Keep keepData, int id)
        {
            Keep keepToCheck = _repo.getKeep(id);
            if(keepToCheck.CreatorId != keepData.CreatorId) {
                throw new Exception($"You are not Authorized to be able to edit keep at id: {id}");
            }
            if(keepData.Name == null){
                keepData.Name = keepToCheck.Name;
            } 
            if(keepData.Description == null){
                keepData.Description = keepToCheck.Description;
            } 
            if(keepData.Img == null){
                keepData.Img = keepToCheck.Img;
            }
            keepData.Id = keepToCheck.Id;
            _repo.updateKeep(keepData);
            return keepData;
        }

        internal string deleteKeep(string userId, int keepId)
        {
            Keep keep = _repo.getKeep(keepId);
            if(keep.CreatorId != userId){
                throw new Exception($"You are unauthorized to delete keep at Id: {keepId}");
            }
            return _repo.deleteKeep(keepId);
        }
    }
}