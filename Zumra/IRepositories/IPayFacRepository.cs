using System.Linq.Expressions;

namespace Zumra.IRepositories;

public interface IPayFacRepository : IRepository<PayFac>
{
    Task<PayFac?> FindBySessionIdAsync(string sessionId);
}
