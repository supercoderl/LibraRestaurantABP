using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraRestaurant.IdentityService.Service
{
    public interface IKeycloakService : ITransientDependency
    {
        Task<List<CachedKeycloakUser>> GetUsersAsync(string? search = null, string? username = null, string? email = null, CancellationToken cancellationToken = default);

        Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);

        Task<bool> UpdateUserAsync(string userId, User user, CancellationToken cancellationToken = default);

        Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);

        Task<List<CachedKeycloakRole>> GetRolesAsync(CancellationToken cancellationToken = default);

        Task<bool> AddRealmRolesToUserAsync(string userId, IEnumerable<Role> roles, CancellationToken cancellationToken = default);

        Task<bool> RemoveRealmRolesFromUserAsync(string userId, IEnumerable<Role> roles, CancellationToken cancellationToken = default);

        Task<bool> CreateRoleAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> DeleteRoleByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<bool> UpdateRoleAsync(string id, Role role, CancellationToken cancellationToken = default);
    }
}
