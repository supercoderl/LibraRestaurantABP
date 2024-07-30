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
    public class KeycloakRoleCreationJob : AsyncBackgroundJob<IdentityRoleCreationArgs>, ITransientDependency
    {
        private readonly IKeycloakService _keycloakService;
        private readonly ILogger<KeycloakRoleCreationJob> _logger;

        public KeycloakRoleCreationJob(IKeycloakService keycloakService, ILogger<KeycloakRoleCreationJob> logger)
        {
            _keycloakService = keycloakService;
            _logger = logger;
        }

        public override async Task ExecuteAsync(IdentityRoleCreationArgs args)
        {
            try
            {
                var existingRole = (await _keycloakService.GetRolesAsync()).FirstOrDefault(q => q.Name == args.Name);
                if (existingRole != null)
                {
                    return;
                }

                await _keycloakService.CreateRoleAsync(args.Name);
            }
            catch (Exception)
            {
                _logger.LogWarning($"Keycloak role creation with the name:{args.Name} failed!");
                throw;
            }
        }
    }

    public record IdentityRoleCreationArgs(string Name);
}
