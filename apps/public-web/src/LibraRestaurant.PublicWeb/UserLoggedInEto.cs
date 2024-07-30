using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace LibraRestaurant.PublicWeb
{
    [EventName("LibraRestaurant.Identity.UserLoggedIn")]
    [Serializable]
    public class UserLoggedInEto : EtoBase
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? UserName { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
