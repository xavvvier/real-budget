using kCura.EventHandler;
using kCura.EventHandler.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Resources.EventHandlers.Application
{
    [Description("Real Budget Post Install Event Handler")]
    [Guid("DD7D3DA2-592C-4259-A285-8711EE547A0E")]
    public class PostInstall : PostInstallEventHandler
    {
        public override Response Execute()
        {
            var response = new Response();
            try
            {
                var context = Helper.GetDBContext(-1);
                context.ExecuteNonQuerySQLStatement(Queries.Sql.CreateInstanceLevelTab);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }
    }
}
