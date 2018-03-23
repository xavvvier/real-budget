using kCura.EventHandler;
using NSerio.Relativity.WebAuthentication;
using Relativity.API;
using Resources.Constants;
using Resources.Repositories;
using Resources.Repositories.ObjectManager;
using RestSharp;
using System;

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
            IRestRequest request = new RestRequest($"Relativity/CustomPages/{custompageGuid}/api/Notification");
            request.Method = Method.POST;
            string jsonToSend = @"{
                    ""UserName"": ""Relativity Admin"",
                    ""Action"": ""Edit"",
                    ""TotalEdits"": 4
                }";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var rsp = auth.Request(request);
        }

        protected IDBContext GetCaseDBContext(int appID)
        {
            return this.Helper.GetDBContext(appID);
        }

        protected IDataRepository _Repo
        {
            get
            {
                if (_repo == null)
                    _repo = new ObjectManagerRepository(this.Helper, this.Helper.GetActiveCaseID());
                return _repo;
            }
        }
        private IDataRepository _repo;
    }
}
