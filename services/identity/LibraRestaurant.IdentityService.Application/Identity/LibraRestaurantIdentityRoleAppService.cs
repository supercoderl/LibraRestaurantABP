using LibraRestaurant.IdentityService.BackgroundJobs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace LibraRestaurant.IdentityService.Identity
{
    [ExposeServices(typeof(IdentityRoleAppService), typeof(IIdentityRoleAppService))]
    public class LibraRestaurantIdentityRoleAppService : IdentityRoleAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IdentityRoleManager _identityRoleManager;

        public LibraRestaurantIdentityRoleAppService(IdentityRoleManager roleManager, IIdentityRoleRepository roleRepository,
            IBackgroundJobManager backgroundJobManager, IdentityRoleManager identityRoleManager) : base(
            roleManager, roleRepository)
        {
            _backgroundJobManager = backgroundJobManager;
            _identityRoleManager = identityRoleManager;
        }

        public override async Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            var result = await base.CreateAsync(input);
            await _backgroundJobManager.EnqueueAsync(new IdentityRoleCreationArgs(result.Name));

            return result;
        }

        public override async Task<IdentityRoleDto> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
        {
            var role = await _identityRoleManager.GetByIdAsync(id);
            var existingRoleName = role.Name;
            var result = await base.UpdateAsync(id, input);

            await _backgroundJobManager.EnqueueAsync(new IdentityRoleUpdatingArgs(existingRoleName, input.Name));

            return result;
        }

        public override async Task DeleteAsync(Guid id)
        {
            var existingRole = await _identityRoleManager.FindByIdAsync(id.ToString());
            await base.DeleteAsync(id);
            if (existingRole == null)
            {
                return;
            }

            await _backgroundJobManager.EnqueueAsync(new IdentityRoleDeletionArgs(existingRole.Name));
        }
    }
}
