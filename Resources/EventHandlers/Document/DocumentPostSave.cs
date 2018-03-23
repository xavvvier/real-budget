using kCura.EventHandler;
using NSerio.Relativity.WebAuthentication;
using Relativity.API;
using Resources.Constants;
using Resources.Repositories;
using Resources.Repositories.ObjectManager;
using Resources.Repositories.Sql;
using RestSharp;
using System;
using System.Web.Script.Serialization;

namespace Resources.EventHandlers.Document
{
    public class DocumentPostSave : PostSaveEventHandler
    {
        public override FieldCollection RequiredFields
        {
            get
            {
                return new FieldCollection();
            }
        }

        public override Response Execute()
        {
            Response response = new Response { Success = true };
            try
            {
                InvokeWebService();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _Repo.LogRepo.LogException<DocumentPreload>(ex);
                throw;
            }
            return response;
        }

        private void InvokeWebService()
        {
            var myUri = this.Helper.GetUrlHelper().GetApplicationURL(GlobalConstants.CUSTOM_PAGE_GUID);
            IWebAuthenticationHelper webAuthenticationHelper = new WebAuthenticationHelper
            {
                GetDBContext = GetCaseDBContext,
                RelativityUrl = myUri,
                UserArtifactID = 777,
            };
            var auth = new NSerio.Relativity.WebAuthentication.Core.Authentication(webAuthenticationHelper);
            var custompageGuid = GlobalConstants.CUSTOM_PAGE_GUID.ToString();
            IRestRequest request = new RestRequest($"Relativity/CustomPages/{custompageGuid}/api/Notification/WorkspacesNotification");
            request.Method = Method.POST;
            JavaScriptSerializer coder = new JavaScriptSerializer();
            var lstWSInfo = SqlRepository.GetWorkspacesInfo(DateTime.Today, DateTime.Today.AddDays(1));
            string jsonToSend = coder.Serialize(lstWSInfo);
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var rsp = auth.Request(request);
        }

        protected IDBContext GetCaseDBContext(int appID)
        {
            return this.Helper.GetDBContext(appID);
        }

        private IDataRepository _repo;
        protected IDataRepository _Repo
        {
            get
            {
                if (_repo == null)
                    _repo = new ObjectManagerRepository(this.Helper, this.Helper.GetActiveCaseID());
                return _repo;
            }
        }        
        private ISqlRepository _SqlRepository;
        public ISqlRepository SqlRepository
        {
            get
            {
                if (_SqlRepository == null)
                {
                    _SqlRepository = new SqlRepository(this.Helper.GetDBContext);
                }
                return _SqlRepository;
            }
        }
    }
}
