using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Resources.Models;
using System;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        List<WorkspaceInfo> workspaces;
        List<User> users;
        Notification notification;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetWorkspacesData()
        {
            workspaces = new List<WorkspaceInfo>();
            users = new List<User>();
            notification = new Notification();
            workspaces = SqlRepository.GetWorkspacesInfo(DateTime.Today, DateTime.Today.AddDays(1));
            return Json(workspaces);
        }
    }
}