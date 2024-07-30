using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace LibraRestaurant.CmskitService.EntityFrameworkCore
{
    [ConnectionStringName(CmskitServiceDbProperties.ConnectionStringName)]
    public interface ICmskitServiceDbContext : IEfCoreDbContext
    {
    }
}
