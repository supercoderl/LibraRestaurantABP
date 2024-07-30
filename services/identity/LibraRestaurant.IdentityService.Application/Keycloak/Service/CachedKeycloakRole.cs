﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace LibraRestaurant.IdentityService.Service
{
    [CacheName("KeycloakRole")]
    public class CachedKeycloakRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Composite { get; set; }
        public CachedRoleComposite Composites { get; set; }
        public bool? ClientRole { get; set; }
        public string ContainerId { get; set; }
        public IDictionary<string, object> Attributes { get; set; }
    }

    public class CachedRoleComposite
    {
        public IDictionary<string, string> Client { get; set; }
        public IEnumerable<string> Realm { get; set; }
    }
}
