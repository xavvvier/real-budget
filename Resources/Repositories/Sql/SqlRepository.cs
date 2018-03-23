using Relativity.API;
using Resources.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Resources.Repositories.Sql
{
    public class SqlRepository : ISqlRepository
    {
        public SqlRepository(IDBContext context)
        {
            this.Context = context;
        }

        public IDBContext Context { get; private set; }

        public IEnumerable<UserPrice> GetAllUserPrice()
        {
            var table = this.Context.ExecuteSqlStatementAsDataTable(Queries.Sql.GetAll);
            return table.AsEnumerable().Select(row => new UserPrice
            {
                ArtifactID = (int)row["ArtifactID"],
                UserName = row["UserName"].ToString(),
                PricePerHour = (decimal)row["PricePerHour"]
            });
        }

        public void UpdateUserPrice(IEnumerable<UserPrice> users)
        {
            var sql = Queries.Sql.UpdateUserPrice;
            foreach (var user in users)
            {
                Context.ExecuteNonQuerySQLStatement(sql, new SqlParameter[]
                {
                    new SqlParameter("UserId", user.ArtifactID),
                    new SqlParameter("PricePerHour", user.PricePerHour),
                });
            }
        }
    }
}
