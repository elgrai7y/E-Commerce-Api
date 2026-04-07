namespace E_Commerce.DAL
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUserWithOrdersAsync();
    }
}