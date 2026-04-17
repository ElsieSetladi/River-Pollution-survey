namespace River_Pollution_Survey.RPS_Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        ISiteRepository SiteRepository { get; }
        IWasteRepository WasteRepository { get; }
        IRoleRepository RoleRepository { get; }
    }
}
