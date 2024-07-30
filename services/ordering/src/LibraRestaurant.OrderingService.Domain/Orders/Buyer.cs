using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace LibraRestaurant.OrderingService.Orders
{
    public class Buyer : ValueObject
    {
        public Guid? Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        private Buyer()
        {
        }

        public Buyer(string email, [CanBeNull] string name, Guid? id)
        {
            Email = Check.NotNullOrEmpty(email, nameof(email));
            Name = name;
            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;
            yield return Email;
        }
    }
}
