using Relativity.CustomPages;
using Resources.Model;
using Resources.Repositories;
using Resources.Repositories.Sql;
using System.Collections.Generic;
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
        private ISqlRepository Repository
        {
            get
            {
                return new SqlRepository(Context);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(Repository.GetAllUserPrice());
        }
        
        [HttpPost]
        public IHttpActionResult UpdatePrices(List<UserPrice> users)
        {
            Repository.UpdateUserPrice(users);
            return Ok();
        }
    }
}
