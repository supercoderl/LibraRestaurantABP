using LibraRestaurant.IdentityService.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.IdentityService.BackgroundJobs.Roles
{
    public class KeycloakRoleDeletionJob : AsyncBackgroundJob<IdentityRoleDeletionArgs>, ITransientDependency
    {
        private readonly IKeycloakService _keycloakService;
        private readonly ILogger<KeycloakRoleDeletionJob> _logger;

        public KeycloakRoleDeletionJob(IKeycloakService keycloakService, ILogger<KeycloakRoleDeletionJob> logger)
        {
            _keycloakService = keycloakService;
            _logger = logger;
        }

        public override async Task ExecuteAsync(IdentityRoleDeletionArgs args)
        {
            try
            {
                var existingRole = (await _keycloakService.GetRolesAsync()).FirstOrDefault(q => q.Name == args.Name);
                if (existingRole == null)
                {
                    return;
                }

                await _keycloakService.DeleteRoleByIdAsync(existingRole.Id);
            }
            catch (Exception)
            {
                _logger.LogWarning($"Could not delete the role with the name:{args.Name} from Keycloak server!");
                throw;
            }
        }
    }

    public record IdentityRoleDeletionArgs(string Name);
}
