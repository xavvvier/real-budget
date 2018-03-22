using Relativity.CustomPages;
using Resources.Repositories;
using Resources.Repositories.ObjectManager;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected IDataRepository _Repo
        {
            get
            {
                if (_repo == null)
                    _repo = new ObjectManagerRepository(ConnectionHelper.Helper(), WorkspaceID);
                return _repo;
            }
        }
        private IDataRepository _repo;

        public int WorkspaceID
        {
            get
            {
                return int.Parse(Request.Params["AppID"] ?? Request.Params["WorkspaceID"] ?? Request.Headers["AppID"]);
            }
        }
    }
}