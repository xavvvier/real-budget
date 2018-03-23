using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Relativity.API;
using RSO = Relativity.Services.Objects;
using ServiceProxy = Relativity.Services.ServiceProxy;
using Resources.Repositories.ObjectManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relativity.Services.ArtifactGuid;
using Relativity.Services.Pipeline;
using Resources.Constants;

namespace Resources.Repositories.ObjectManager.Tests
{
    [TestClass()]
    public class ObjectManagerRepositoryTests
    {
        IHelper _Helper;
        IDataRepository _ObjectManagerRepo;        

        [TestInitialize]
        public void Initialize()
        {
            int c_WorkspaceId = 1027620;
            const string c_ServicesURL = "http://relativityvm/relativity.services";
            const string c_KeplerUri = "http://relativityvm/relativity.rest/api";
            const string c_RelativityUser = "relativity.admin@relativity.com";
            const string c_RelativityPassword = "Nserio.1";
            const string c_SQLServerName = "relativityvm";
            const string c_SQLUser = "eddsdbo";
            const string c_SQLPassword = "Nserio.1";

            Mock<IHelper> mockHelper = new Mock<IHelper>();
            mockHelper.Setup(p => p.GetDBContext(It.IsAny<int>()))
            .Returns<int>(workspaceID =>
            {
                if (workspaceID == 0)
                    throw new ArgumentException();
                return new DBContext(new kCura.Data.RowDataGateway.Context(c_SQLServerName, string.Format("EDDS{0}", workspaceID == -1 ? string.Empty : workspaceID.ToString()), c_SQLUser, c_SQLPassword)); ;
            });
            mockHelper.Setup(p => p.GetServicesManager())
                .Returns(() =>
                {
                    Mock<IServicesMgr> svcMgr = new Mock<IServicesMgr>();
                    svcMgr.Setup(p => p.CreateProxy<RSO.IObjectManager>(It.IsAny<ExecutionIdentity>()))
                      .Returns<ExecutionIdentity>(eid =>
                      {
                          ServiceProxy.Credentials credentials = new ServiceProxy.UsernamePasswordCredentials(c_RelativityUser, c_RelativityPassword);
                          var factorySettings = new KeplerServiceFactorySettings(new Uri(c_KeplerUri), credentials.GetAuthenticationHeaderValue(), WireProtocolVersion.V2);
                          var factory = new KeplerServiceFactory(factorySettings);
                          return factory.GetClient<RSO.IObjectManager>();
                      });
                    svcMgr.Setup(p => p.CreateProxy<IArtifactGuidManager>(It.IsAny<ExecutionIdentity>()))
                      .Returns<ExecutionIdentity>(eid =>
                      {
                          ServiceProxy.Credentials credentials = new ServiceProxy.UsernamePasswordCredentials(c_RelativityUser, c_RelativityPassword);
                          var factorySettings = new KeplerServiceFactorySettings(new Uri(c_KeplerUri), credentials.GetAuthenticationHeaderValue(), WireProtocolVersion.V2);
                          var factory = new KeplerServiceFactory(factorySettings);
                          return factory.GetClient<IArtifactGuidManager>();
                      });
                    return svcMgr.Object;
                });

            _Helper = mockHelper.Object;
            _ObjectManagerRepo = new ObjectManagerRepository(_Helper, c_WorkspaceId);            
        }

        [TestMethod()]
        public void InsertDocumentViewTest()
        {
            //_ObjectManagerRepo.InsertDocumentActivity(1045766, 9, ActionChoices.View, 0);
            var t = _ObjectManagerRepo.GetSecondsSinceLastView(1045766, 9);
        }
    }
}