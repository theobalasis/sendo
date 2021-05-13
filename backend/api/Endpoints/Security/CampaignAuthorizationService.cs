using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public class CampaignAuthorizationService : IEntityAuthorizationService<Campaign>
    {
        private readonly IRepository<Campaign> _campaignRepository;

        public CampaignAuthorizationService(IRepository<Campaign> campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<bool> AuthorizeAsync(User user, Campaign entity)
        {
            var campaign = await _campaignRepository.Query()
                                                    .FirstOrDefaultAsync<Campaign?>(c =>
                                                        c.Id == entity.Id && c.UserId == user.Id
                                                    );

            return campaign != null;
        }
    }
}
