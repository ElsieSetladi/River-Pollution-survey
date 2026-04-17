using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;

namespace River_Pollution_Survey.RPS_Repository
{
    public class WasteRepository : RepositoryBase<Waste>, IWasteRepository
    {
        public  WasteRepository(RiverDBContext dBContext) : base(dBContext)
        {

        }

    }
}
