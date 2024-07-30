﻿using LibraRestaurant.IdentityService.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.IdentityService.BackgroundJobs.Users
{
    public class KeycloakUserDeletionJob : AsyncBackgroundJob<IdentityUserDeletionArgs>, ITransientDependency
    {
        private readonly IKeycloakService _keycloakService;
        private readonly ILogger _logger;

        public KeycloakUserDeletionJob(IKeycloakService keycloakService,
            ILogger<KeycloakUserCreationJob> logger)
        {
            _keycloakService = keycloakService;
            _logger = logger;
        }

        public override async Task ExecuteAsync(IdentityUserDeletionArgs args)
        {
            try
            {
                var keycloakUser = (await _keycloakService.GetUsersAsync())
                    .FirstOrDefault(q => q.UserName == args.UserName);
                if (keycloakUser == null)
                {
                    _logger.LogError($"Keycloak user could not be found to delete! Username:{args.UserName}");
                    throw new UserFriendlyException($"Keycloak user with the username:{args.UserName} could not be found!");
                }

                var result = await _keycloakService.DeleteUserAsync(keycloakUser.Id);
                if (result)
                {
                    _logger.LogInformation($"Keycloak user with the username:{args.UserName} has been deleted.");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Keycloak user deletion failed! Username:{args.UserName}");
            }
        }
    }

    public class IdentityUserDeletionArgs
    {
        public string UserName { get; init; }

        public IdentityUserDeletionArgs(string userName)
        {
            UserName = userName;
        }

        public IdentityUserDeletionArgs() { }
    }
}
