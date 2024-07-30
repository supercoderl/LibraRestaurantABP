using AutoMapper;
using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;
using LibraRestaurant.IdentityService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.IdentityService.Application
{
    public class LibraRestaurantIdentityServiceAutoMapperProfile : Profile
    {
        public LibraRestaurantIdentityServiceAutoMapperProfile()
        {
            CreateMap<User, CachedKeycloakUser>().ReverseMap();
            CreateMap<UserAccess, CachedUserAccess>().ReverseMap();
            CreateMap<UserConsent, CachedUserConsent>().ReverseMap();
            CreateMap<Credentials, CachedCredentials>().ReverseMap();
            CreateMap<FederatedIdentity, CachedFederatedIdentity>();

            CreateMap<Role, CachedKeycloakRole>().ReverseMap();
            CreateMap<RoleComposite, CachedRoleComposite>().ReverseMap();
        }
    }
}
