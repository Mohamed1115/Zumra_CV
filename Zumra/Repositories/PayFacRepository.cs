using Microsoft.EntityFrameworkCore;
using Zumra.Data;
using Zumra.IRepositories;

namespace Zumra.Repositories;

public class PayFacRepository : Repository<PayFac>, IPayFacRepository
{
    public PayFacRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<PayFac?> FindBySessionIdAsync(string sessionId)
    {
        return await _context.PayFacs
            .FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
    }
}
