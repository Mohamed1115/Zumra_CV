using Zumra.Data;

namespace Zumra.Repositories;

public class UserImageRepository : Repository<UserImage> , IUserImageRepository
{
    public UserImageRepository(ApplicationDbContext context) : base(context)
    {
        // public async Task<UserImage> GetImageById(str){}
    }
}