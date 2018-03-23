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
            foreach (var workspace in workspacesWithApp)
            {
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
                workspaceInfo.User = table.AsEnumerable().Select(row => new User
                {
                    EditsHour = (int)row["Edits"],
                    EditsHourBadge = (int)row["DistinctEdits"],
                    ViewsHour = (int)row["Views"],
                    ViewsHourBadge = (int)row["DistinctViews"],
                }).ToList();
            }
            return infoList;
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
            });
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
