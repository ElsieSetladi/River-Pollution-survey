using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;

namespace River_Pollution_Survey.RPS_Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RiverDBContext _dbContext;

        private IUserRepository _userRepository;
        private ISiteRepository _siteRepository;
        private IWasteRepository _wasteRepository;
        private IRoleRepository _roleRepository;
        public RepositoryWrapper(RiverDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        public ISiteRepository SiteRepository
        {
            get
            {
                if (_siteRepository == null)
                {
                    _siteRepository = new SiteRepository(_dbContext);
                }
                return _siteRepository;
            }
        }
        public IWasteRepository WasteRepository
        {
            get
            {
                if (_wasteRepository == null)
                {
                    _wasteRepository = new WasteRepository(_dbContext);
                }
                return _wasteRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_dbContext);
                }
                return _roleRepository;
            }
        }
    }
}
