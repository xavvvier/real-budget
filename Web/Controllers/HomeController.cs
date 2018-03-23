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
#if !DEBUG
            workspaces = SqlRepository.GetWorkspacesInfo(DateTime.Today, DateTime.Today.AddDays(1));
#else
            users.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            users.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            users.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            users.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            users.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });

            workspaces.Add(new WorkspaceInfo { WorkspaceArtifactId = 1000, WorkspaceName = "Workspace Local", User = users });
            workspaces.Add(new WorkspaceInfo { WorkspaceArtifactId = 1001, WorkspaceName = "Workspace Local", User = users });
            workspaces.Add(new WorkspaceInfo { WorkspaceArtifactId = 1002, WorkspaceName = "Workspace Local", User = users });
            workspaces.Add(new WorkspaceInfo { WorkspaceArtifactId = 1003, WorkspaceName = "Workspace Local", User = users });
            workspaces.Add(new WorkspaceInfo { WorkspaceArtifactId = 1004, WorkspaceName = "Workspace Local", User = users });
#endif
            for (int i = 0; i < workspaces.Count(); i++)
            {
                for (int j = 0; j < workspaces[i].User.Count(); j++)
                {
                    workspaces[i].CostDay += workspaces[i].User[j].CostDay;
                    workspaces[i].ViewsHour += workspaces[i].User[j].ViewsHour;
                    workspaces[i].ViewsHourBadge += workspaces[i].User[j].ViewsHourBadge;
                    workspaces[i].AverageTime += workspaces[i].User[j].AverageTime;
                    workspaces[i].EditsHour += workspaces[i].User[j].EditsHour;
                    workspaces[i].EditsHourBadge += workspaces[i].User[j].EditsHourBadge;
                }
            }

            return Json(workspaces);
        }
    }
}