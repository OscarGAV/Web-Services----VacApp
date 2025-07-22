using Microsoft.EntityFrameworkCore;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.ValueObjects;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using VacApp_Bovinova_Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace VacApp_Bovinova_Platform.CampaignManagement.Infrastructure.Repositories;

public class CampaignRepository(AppDbContext context) : BaseRepository<Campaign>(context), ICampaignRepository
{
    public async Task<Campaign?> FindByNameAsync(string name)
    {
        return await Context.Set<Campaign>().FirstOrDefaultAsync(c => c.Name == name);
    }
    
    public async Task<IEnumerable<Goal>> FindByCampaignId(int campaignId)
    {
        return await Context.Set<Goal>().Where(g=>g.CampaignId == campaignId).ToListAsync();
    }

    public async Task<IEnumerable<Channel>> FindChannelsByCampaignId(int campaignId)
    {
        return await Context.Set<Channel>().Where(c=>c.CampaignId == campaignId).ToListAsync();
    }
    
    public async Task<IEnumerable<Campaign>> FindByUserIdAsync(CampaignUserId userId)
    {
        return await Context.Set<Campaign>().Where(f => f.CampaignUserId == userId).ToListAsync();
    }
}