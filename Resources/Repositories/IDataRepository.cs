using kCura.EventHandler;
using Relativity.API;
using Relativity.Services.Objects.DataContracts;
using Resources.DTOs.Repositories;
using Resources.Repositories.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Repositories
{
    public interface IDataRepository
    {
        ILoggingRepository LogRepo { get; }

        #region CRUD       
        RelativityObject Create(object ObjectTypeIdentifier, IEnumerable<FieldRefValuePair> fields, int ParentArtifactID = 0, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        List<RelativityObject> Create(List<ArtifactRequest> req, int ParentArtifactID = 0, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        RelativityObject Read(int objectArtifactId, IEnumerable<FieldRef> fieldRefs, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        void Update(int ArtifactID, IEnumerable<FieldRefValuePair> fields, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        void Update(List<ArtifactRequest> req, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        void Delete(int ArtifactID, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        List<RelativityObject> Query(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "", int WS = 0);
        QueryResult QueryFullResponse(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        List<RelativityObjectSlim> QuerySlim(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "");
        #endregion

        void InsertDocumentActivity(int DocumentAI, int UserAI, string actionChoice, int? Seconds);
        bool DocumentHasBeenModified(int ArtifactID, FieldCollection _fields);
    }
}
