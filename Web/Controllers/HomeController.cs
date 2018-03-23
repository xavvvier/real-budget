using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        List<Workspace> workspace;
        List<User> user;
        Notification notification;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetWorkspacesData()
        {
            workspace = new List<Workspace>();
            user = new List<User>();
            notification = new Notification();

            user.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            user.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            user.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            user.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });
            user.Add(new User { UserArtifactId = 11111, UserName = "Molina, Cristian", CostDay = 10, ViewsHour = 3, ViewsHourBadge = 1, AverageTime = 10, EditsHour = 5, EditsHourBadge = 2 });

            workspace.Add(new Workspace { WorkspaceArtifactId = 1000, WorkspaceName = "Workspace Local", User = user });
            workspace.Add(new Workspace { WorkspaceArtifactId = 1000, WorkspaceName = "Workspace Local", User = user });
            workspace.Add(new Workspace { WorkspaceArtifactId = 1000, WorkspaceName = "Workspace Local", User = user });
            workspace.Add(new Workspace { WorkspaceArtifactId = 1000, WorkspaceName = "Workspace Local", User = user });
            workspace.Add(new Workspace { WorkspaceArtifactId = 1000, WorkspaceName = "Workspace Local", User = user });

            for (int i = 0; i < workspace.Count(); i++)
            {
                for (int j = 0; j < workspace[i].User.Count(); j++)
                {
                    workspace[i].CostDay += workspace[i].User[j].CostDay;
                    workspace[i].ViewsHour += workspace[i].User[j].ViewsHour;
                    workspace[i].ViewsHourBadge += workspace[i].User[j].ViewsHourBadge;
                    workspace[i].AverageTime += workspace[i].User[j].AverageTime;
                    workspace[i].EditsHour += workspace[i].User[j].EditsHour;
                    workspace[i].EditsHourBadge += workspace[i].User[j].EditsHourBadge;
                }
            }

           

           
            return Json(workspace);
        }

        [HttpPost]
        public ActionResult GetSummary()
        {
            GetWorkspacesData();
            var list = this.workspace;
            var CostDay = 0;
            var ViewsHour = 0;
            var ViewsHourBadge = 0;
            var AverageTime = 0;
            var EditsHour = 0;
            var EditsHourBadge = 0;

            for (int i = 0; i < list.Count(); i++)
            {
                CostDay += list[i].CostDay;
                ViewsHour += list[i].ViewsHour;
                ViewsHourBadge += list[i].ViewsHourBadge;
                AverageTime += list[i].AverageTime;
                EditsHour += list[i].EditsHour;
                EditsHourBadge += list[i].EditsHourBadge;
            }
            Workspace wk = new Workspace();
            wk.CostDay = CostDay;
            wk.ViewsHour = ViewsHour;
            wk.ViewsHourBadge = ViewsHourBadge;
            wk.AverageTime = AverageTime;
            wk.EditsHour = EditsHour;
            wk.EditsHourBadge = EditsHourBadge;
            return Json(wk);
        }
    }
}