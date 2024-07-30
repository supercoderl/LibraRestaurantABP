using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;

namespace Volo.CmsKit.Public.Comments
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ICommentPublicAppService), typeof(CommentPublicClientProxy))]
    public partial class CommentPublicClientProxy : ClientProxyBase<ICommentPublicAppService>, ICommentPublicAppService
    {
        public virtual async Task<ListResultDto<CommentWithDetailsDto>> GetListAsync(string entityType, string entityId)
        {
            return await RequestAsync<ListResultDto<CommentWithDetailsDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId }
        });
        }

        public virtual async Task<CommentDto> CreateAsync(string entityType, string entityId, CreateCommentInput input)
        {
            return await RequestAsync<CommentDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), entityType },
            { typeof(string), entityId },
            { typeof(CreateCommentInput), input }
        });
        }

        public virtual async Task<CommentDto> UpdateAsync(Guid id, UpdateCommentInput input)
        {
            return await RequestAsync<CommentDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(UpdateCommentInput), input }
        });
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
        }
    }
}
