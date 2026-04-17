
using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;

namespace River_Pollution_Survey.RPS_Repository
{
    public class SiteRepository : RepositoryBase<Site>, ISiteRepository
    {
        public SiteRepository(RiverDBContext dBContext) : base(dBContext)
        {

        }

    }
}
