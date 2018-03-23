using Resources.Model;
using Resources.Models;
using System;
using System.Collections.Generic;

namespace Resources.Repositories
{
    public interface ISqlRepository
    {
        IEnumerable<UserPrice> GetAllUserPrice();
        void UpdateUserPrice(IEnumerable<UserPrice> users);
        List<WorkspaceInfo> GetWorkspacesInfo(DateTime start, DateTime end);
    }
}
