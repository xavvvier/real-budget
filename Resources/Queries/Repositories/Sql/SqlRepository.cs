using Relativity.API;
using Resources.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Resources.Models;
using System;
using Resources.Constants;

namespace Resources.Repositories.Sql
{
    public class SqlRepository : ISqlRepository
    {
        public SqlRepository(Func<int, IDBContext> context)
        {
            this.Context = context;
        }

        public Func<int, IDBContext> Context { get; private set; }

        public IEnumerable<UserPrice> GetAllUserPrice()
        {
            var table = this.Context(-1).ExecuteSqlStatementAsDataTable(Queries.Sql.GetAll);
            return table.AsEnumerable().Select(row => new UserPrice
            {
                ArtifactID = (int)row["ArtifactID"],
                UserName = row["UserName"].ToString(),
                PricePerHour = (decimal)row["PricePerHour"]
            });
        }

        public List<WorkspaceInfo> GetWorkspacesInfo(DateTime start, DateTime end)
        {
            var infoList = new List<WorkspaceInfo>();
            var workspacesWithApp = WorkspacesWithAppInstalled();
            var budgetProgress = GetTotalInstance();
            foreach (var workspace in workspacesWithApp)
            {
                WorkspaceInfo workspaceInfo = ReadWorkspaceInfo(workspace, start, end);
                workspaceInfo.budgetProgress = budgetProgress;
                infoList.Add(workspaceInfo);
            }
            return infoList;
        }

        public decimal GetTotalInstance()
        {
            var users = GetAllUserPrice();
            var workspacesWithApp = WorkspacesWithAppInstalled();
            decimal total = 0;
            foreach (var workspace in workspacesWithApp)
            {
                var table = Context(workspace.ArtifactID).ExecuteSqlStatementAsDataTable(Queries.Sql.TotalTimeByUSer);
                var userTimes = table.AsEnumerable().Select(row => new UserTime
                {
                    ArtifactID = (int)row["User"],
                    TotalSeconds = (int)row["TotalSeconds"]
                }).ToList();
                foreach (var userTime in userTimes)
                {
                    var userPrice = users.FirstOrDefault(x => x.ArtifactID == userTime.ArtifactID);
                    if(userPrice != null)
                    {
                        total += userPrice.PricePerHour * (userTime.TotalSeconds / 3600);
                    }
                }
            }
            return total;
        }

        public WorkspaceInfo ReadWorkspaceInfo(Artifact workspace, DateTime start, DateTime end)
        {
            var users = GetAllUserPrice();
            var table = Context(workspace.ArtifactID).ExecuteSqlStatementAsDataTable(Queries.Sql.MetricsByWorkspace,
                new SqlParameter[]
                {
                    new SqlParameter("Date1", start),
                    new SqlParameter("Date2", end)
                });
            var workspaceInfo = new WorkspaceInfo
            {
                WorkspaceArtifactId = workspace.ArtifactID,
                WorkspaceName = workspace.Name,
            };
            workspaceInfo.User = table.AsEnumerable().Select(row => {
                int seconds = (int)row["Seconds"];
                var user = new User
                {
                    EditsHour = (int)row["Edits"],
                    EditsHourBadge = (int)row["DistinctEdits"],
                    ViewsHour = (int)row["Views"],
                    ViewsHourBadge = (int)row["DistinctViews"],
                    UserName = row["UserName"].ToString(),
                    UserArtifactId = (int)row["UserID"],
                    Seconds = seconds,
                    AverageTime = (int)row["Average"]
                };
                var userPrice = users.FirstOrDefault(u => u.ArtifactID == user.UserArtifactId)?.PricePerHour ?? 0;
                if (seconds > 0)
                {
                    double hours = seconds / 3600.0;
                    user.EditsHour = (int)(user.EditsHour *1.0 / hours);
                    user.EditsHourBadge = (int)(user.EditsHour / hours);
                    user.ViewsHour = (int)(user.ViewsHour / hours);
                    user.ViewsHourBadge = (int)(user.ViewsHourBadge / hours);
                    user.CostDay = (int)(userPrice * (decimal)hours);
                }
                return user;
            }).ToList();
            workspaceInfo.Seconds = workspaceInfo.User.Sum(u => u.Seconds);
            workspaceInfo.EditsHour = workspaceInfo.User.Sum(u => u.EditsHour);
            workspaceInfo.EditsHourBadge = workspaceInfo.User.Sum(u => u.EditsHourBadge);
            workspaceInfo.ViewsHour = workspaceInfo.User.Sum(u => u.ViewsHour);
            workspaceInfo.ViewsHourBadge = workspaceInfo.User.Sum(u => u.ViewsHourBadge);
            workspaceInfo.CostDay = workspaceInfo.User.Sum(u => u.CostDay);
            workspaceInfo.AverageTime = workspaceInfo.User.Sum(u => u.AverageTime);
            return workspaceInfo;
        }


        private IEnumerable<Artifact> WorkspacesWithAppInstalled()
        {
            var table = Context(-1).ExecuteSqlStatementAsDataTable(Queries.Sql.WorkspacesWithApp, new SqlParameter[] {
                new SqlParameter("ApplicationGuid", GlobalConstants.CUSTOM_PAGE_GUID)
            });
            return table.AsEnumerable().Select(row => new Artifact
            {
                ArtifactID = (int)row["ArtifactID"],
                Name = row["Name"].ToString()
            }).Where(x => x.ArtifactID > 0);
        }

        public void UpdateUserPrice(IEnumerable<UserPrice> users)
        {
            var sql = Queries.Sql.UpdateUserPrice;
            foreach (var user in users)
            {
                Context(-1).ExecuteNonQuerySQLStatement(sql, new SqlParameter[]
                {
                    new SqlParameter("UserId", user.ArtifactID),
                    new SqlParameter("PricePerHour", user.PricePerHour),
                });
            }
        }
    }
}
