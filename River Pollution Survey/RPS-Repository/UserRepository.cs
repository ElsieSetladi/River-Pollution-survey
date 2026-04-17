using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;

namespace River_Pollution_Survey.RPS_Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {


        public UserRepository(RiverDBContext dbContext) : base(dbContext)
        {

        }

        
    }
}
