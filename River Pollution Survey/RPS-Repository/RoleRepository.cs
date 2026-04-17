using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;

namespace River_Pollution_Survey.RPS_Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RiverDBContext dBContext) : base(dBContext)
        {

        }

    }
}
