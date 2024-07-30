using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraRestaurant.IdentityService.Service
{
    public static class KeycloakServiceExtensions
    {
        public static string GenerateCacheKeyBasedOnValues(this IEnumerable<Role> roles)
        {
            return GenerateUniqueCacheKeyBasedOnList(roles);
        }

        public static string GenerateCacheKeyBasedOnValues(this IEnumerable<User> users)
        {
            return GenerateUniqueCacheKeyBasedOnList(users);
        }

        private static string GenerateUniqueCacheKeyBasedOnList<T>(IEnumerable<T> list)
        {
            string serializedList = JsonSerializer.Serialize(list);
            byte[] bytes = Encoding.UTF8.GetBytes(serializedList);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            string hashString = BitConverter.ToString(hash).Replace("-", "");
            return hashString;
        }
    }
}
