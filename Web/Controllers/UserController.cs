using Relativity.CustomPages;
using System.Web.Http;

namespace Web.Controllers
{
    public class UserController : ApiController
    {
        private Relativity.API.IDBContext Context
        {
            get
            {
                return ConnectionHelper.Helper().GetDBContext(-1);
            }
        }

        [HttpGet]
        public void GetAll()
        {
            //return Context.EI
        }
    }
}
