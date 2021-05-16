using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sendo.WebApi.Data.Access;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Endpoints.Security
{
    public class CampaignAuthorizationService : IEntityAuthorizationService<Campaign>
    {
        private readonly IRepository<Campaign> _campaignRepository;

        public CampaignAuthorizationService(IRepository<Campaign> campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public bool Authorize(User user, Campaign entity)
        {
            var campaign = _campaignRepository.Query()
                                              .FirstOrDefault(c =>
                                                  c.Id == entity.Id && c.UserId == user.Id
                                              );

            return campaign != null;
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
