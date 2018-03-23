using Relativity.API;
using Relativity.Services.ArtifactGuid;
using Resources.Repositories.Logging;

namespace Resources.Repositories.ObjectManager
{
    public class BaseRepository
    {
        public IHelper _helper;
        public int _workspaceID;

        public ILoggingRepository LogRepo
        {
            get
            {
                if (_LogRepo == null)
                    _LogRepo = new LoggingRepository(this._helper, this._workspaceID);
                return _LogRepo;
            }
        }
        private ILoggingRepository _LogRepo;

        public int WorkspaceID
        {
            get
            {
                return this._workspaceID;
            }
        }

        public IArtifactGuidManager ArtifactGuidClient
        {
            get
            {
                if (_ArtifactGuidClient == null)
                    _ArtifactGuidClient = _helper.GetServicesManager().CreateProxy<IArtifactGuidManager>(ExecutionIdentity.CurrentUser);
                return _ArtifactGuidClient;
            }
        }
        private IArtifactGuidManager _ArtifactGuidClient;
    }
}
