using Resources.Model;
using System.Collections.Generic;

namespace Resources.Repositories
{
    public interface ISqlRepository
    {
        IEnumerable<UserPrice> GetAllUserPrice();
        void UpdateUserPrice(IEnumerable<UserPrice> users);
    }
}
