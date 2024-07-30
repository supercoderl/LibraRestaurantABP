using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;

namespace Volo.CmsKit.Public.Ratings
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IRatingPublicAppService), typeof(RatingPublicClientProxy))]
    public partial class RatingPublicClientProxy : ClientProxyBase<IRatingPublicAppService>, IRatingPublicAppService
    {
        public virtual async Task<RatingDto> CreateAsync(string entityType, string entityId, CreateUpdateRatingInput input)
        {
            return await RequestAsync<RatingDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId },
            { typeof(CreateUpdateRatingInput), input }
        });
        }

        public virtual async Task DeleteAsync(string entityType, string entityId)
        {
            await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId }
        });
        }

        public virtual async Task<List<RatingWithStarCountDto>> GetGroupedStarCountsAsync(string entityType, string entityId)
        {
            return await RequestAsync<List<RatingWithStarCountDto>>(nameof(GetGroupedStarCountsAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId }
        });
        }
    }
}
