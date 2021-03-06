﻿using KRCDTOs = kCura.Relativity.Client.DTOs;
using KRC = kCura.Relativity.Client;
using Relativity.API;
using Relativity.Services.Objects;
using Relativity.Services.Objects.DataContracts;
using Resources.DTOs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Resources.Constants;
using KEH = kCura.EventHandler;

namespace Resources.Repositories.ObjectManager
{
    public class ObjectManagerRepository : BaseRepository, IDataRepository
    {
        public ObjectManagerRepository(IHelper helper, int workspaceID)
        {
            _helper = helper;
            _workspaceID = workspaceID;
        }

        #region CRUD
        private Exception HandleCRUDException(Exception ex, string Operation, string caller, string ObjectTypeIdentifier, List<FieldRef> fields, string Condition, List<Sort> sorts, int? objectArtifactId = null, List<FieldRefValuePair> fieldsRef = null, int? ArtifactID = null)
        {
            JavaScriptSerializer coder = new JavaScriptSerializer();
            string ErrorParams = $"{Operation}. " + Environment.NewLine +
                $"Method: {caller}" + Environment.NewLine;

            if (Operation == "ReadAsync")
            {
                ErrorParams += $"objectArtifactId: {objectArtifactId}" + Environment.NewLine +
                $"Fields: {coder.Serialize(fields)}";
            }
            if (Operation == "CreateAsync")
            {
                ErrorParams += $"ObjectTypeIdentifier: {ObjectTypeIdentifier}" + Environment.NewLine +
                $"Fields: {coder.Serialize(fieldsRef)}";
            }
            if (Operation == "UpdateAsync")
            {
                ErrorParams += $"ArtifactID: {ArtifactID}" + Environment.NewLine +
                $"Fields: {coder.Serialize(fieldsRef)}";
            }
            if (Operation == "DeleteAsync")
            {
                ErrorParams += $"ArtifactID: {ArtifactID}";
            }
            if (Operation == "QueryAsync" || Operation == "QuerySlimAsync")
            {
                ErrorParams += $"ObjectType: {ObjectTypeIdentifier}" + Environment.NewLine +
                $"Fields: {coder.Serialize(fields)}" + Environment.NewLine +
                $"Condition: {Condition}" + Environment.NewLine +
                $"Sorts: {coder.Serialize(sorts)}";
            }
            var _newEx = new Exception(ErrorParams, ex);
            LogRepo.LogException<ObjectManagerRepository>(_newEx);
            return _newEx;
        }
        private async Task<ReadResult> ReadAsync(int objectArtifactId, IEnumerable<FieldRef> fieldRefs, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, string caller = "")
        {
            using (IObjectManager objectManager = _helper.GetServicesManager().CreateProxy<IObjectManager>(identity))
            {
                try
                {
                    var request = new ReadRequest
                    {
                        Object = new RelativityObjectRef { ArtifactID = objectArtifactId },
                        Fields = fieldRefs
                    };
                    return await objectManager.ReadAsync(_workspaceID, request).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw (HandleCRUDException(ex, "ReadAsync", caller, null, fieldRefs.ToList(), null, null, objectArtifactId));
                }
            }
        }
        private async Task<CreateResult> CreateAsync(object ObjectTypeIdentifier, IEnumerable<FieldRefValuePair> fields, int ParentArtifactID = 0, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, string caller = "")
        {
            using (IObjectManager objectManager = _helper.GetServicesManager().CreateProxy<IObjectManager>(identity))
            {
                try
                {
                    CreateRequest Obj = new CreateRequest();
                    if (ParentArtifactID != 0)
                    {
                        Obj.ParentObject = new RelativityObjectRef() { ArtifactID = ParentArtifactID };
                    }
                    Obj.ObjectType = GetObjectTypeRef(ObjectTypeIdentifier);
                    Obj.FieldValues = fields;
                    return await objectManager.CreateAsync(_workspaceID, Obj).ConfigureAwait(false); ;
                }
                catch (Exception ex)
                {
                    throw (HandleCRUDException(ex, "CreateAsync", caller, ObjectTypeIdentifier.ToString(), null, null, null, null, fields.ToList()));
                }
            }
        }
        private async Task<UpdateResult> UpdateAsync(int _ArtifactID, IEnumerable<FieldRefValuePair> fields, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, string caller = "")
        {
            using (IObjectManager objectManager = _helper.GetServicesManager().CreateProxy<IObjectManager>(identity))
            {
                try
                {
                    var updateRequest = new UpdateRequest
                    {
                        Object = new RelativityObjectRef { ArtifactID = _ArtifactID },
                        FieldValues = fields
                    };
                    return await objectManager.UpdateAsync(_workspaceID, updateRequest).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw (HandleCRUDException(ex, "UpdateAsync", caller, null, null, null, null, null, fields.ToList(), _ArtifactID));
                }
            }
        }
        private async Task<DeleteResult> DeleteAsync(int _ArtifactID, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, string caller = "")
        {
            using (IObjectManager objectManager = _helper.GetServicesManager().CreateProxy<IObjectManager>(identity))
            {
                try
                {
                    var deleteRequest = new DeleteRequest
                    {
                        Object = new RelativityObjectRef { ArtifactID = _ArtifactID }
                    };
                    return await objectManager.DeleteAsync(_workspaceID, deleteRequest).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw (HandleCRUDException(ex, "DeleteAsync", caller, null, null, null, null, null, null, _ArtifactID));
                }
            }
        }
        private async Task<QueryResult> QueryAsync(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, string caller = "", int WS = 0)
        {
            using (IObjectManager objectManager = _helper.GetServicesManager().CreateProxy<IObjectManager>(identity))
            {
                try
                {
                    var queryRequest = new QueryRequest()
                    {
                        Condition = Condition,
                        Fields = fields,
                        Sorts = sorts,
                        ObjectType = GetObjectTypeRef(ObjectTypeIdentifier)
                    };
                    return await objectManager.QueryAsync((WS == 0 ? _workspaceID : WS), queryRequest, start, length).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw (HandleCRUDException(ex, "QueryAsync", caller, ObjectTypeIdentifier.ToString(), fields, Condition, sorts));
                }
            }
        }
        private async Task<QueryResultSlim> QuerySlimAsync(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, string caller = "")
        {
            using (IObjectManager objectManager = _helper.GetServicesManager().CreateProxy<IObjectManager>(identity))
            {
                try
                {
                    var queryRequest = new QueryRequest()
                    {
                        Condition = Condition,
                        Fields = fields,
                        Sorts = sorts,
                        ObjectType = GetObjectTypeRef(ObjectTypeIdentifier),
                        IncludeIDWindow = false,
                        RelationalField = null,
                        SampleParameters = null,
                        SearchProviderCondition = null
                    };
                    return await objectManager.QuerySlimAsync(_workspaceID, queryRequest, 1, 100).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw (HandleCRUDException(ex, "QuerySlimAsync", caller, ObjectTypeIdentifier.ToString(), fields, Condition, sorts));
                }
            }
        }

        public RelativityObject Create(object ObjectTypeIdentifier, IEnumerable<FieldRefValuePair> fields, int ParentArtifactID = 0, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            Task<CreateResult> task = CreateAsync(ObjectTypeIdentifier, fields, ParentArtifactID, identity, caller);
            task.Wait();
            return task.Result.Object;
        }
        public List<RelativityObject> Create(List<ArtifactRequest> req, int ParentArtifactID = 0, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            List<RelativityObject> _res = new List<RelativityObject>();
            foreach (var item in req)
            {
                var _thisRes = Create(item.ObjectTypeIdentifier, item.Fields, ParentArtifactID, identity, caller);
                _res.Add(_thisRes);
            }
            return _res;
        }
        public RelativityObject Read(int objectArtifactId, IEnumerable<FieldRef> fieldRefs, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            Task<ReadResult> task = ReadAsync(objectArtifactId, fieldRefs, identity, caller);
            task.Wait();
            return task.Result.Object;
        }
        public void Update(int ArtifactID, IEnumerable<FieldRefValuePair> fields, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            Task<UpdateResult> task = UpdateAsync(ArtifactID, fields, identity, caller);
            task.Wait();
        }
        public void Update(List<ArtifactRequest> req, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            req.ForEach(item =>
            {
                Update(item.ArtifactID, item.Fields, identity, caller);
            });
        }
        public void Delete(int ArtifactID, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            Task<DeleteResult> task = DeleteAsync(ArtifactID, identity, caller);
            task.Wait();
        }
        public List<RelativityObject> Query(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "", int WS = 0)
        {
            Task<QueryResult> task = QueryAsync(ObjectTypeIdentifier, Condition, fields, sorts, start, length, identity, caller, WS);
            task.Wait();
            return task.Result.Objects;
        }
        public QueryResult QueryFullResponse(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            Task<QueryResult> task = QueryAsync(ObjectTypeIdentifier, Condition, fields, sorts, start, length, identity, caller);
            task.Wait();
            return task.Result;
        }
        public List<RelativityObjectSlim> QuerySlim(object ObjectTypeIdentifier, string Condition, List<FieldRef> fields, List<Sort> sorts = null, int start = 1, int length = int.MaxValue, ExecutionIdentity identity = ExecutionIdentity.CurrentUser, [CallerMemberName]string caller = "")
        {
            Task<QueryResultSlim> task = QuerySlimAsync(ObjectTypeIdentifier, Condition, fields, sorts, start, length, identity, caller);
            task.Wait();
            return task.Result.Objects;
        }
        #endregion

        public void InsertDocumentActivity(int DocumentAI, int UserAI, string actionChoice, int? Seconds)
        {
            Create(ObjectTypes.ActivityLog, new List<FieldRefValuePair>(){
                new FieldRefValuePair() {
                    Field = new FieldRef() { Guid = new Guid(ActivityLogFields.User) },
                    Value = UserAI
                },
                new FieldRefValuePair() {
                    Field = new FieldRef() { Guid = new Guid(ActivityLogFields.Document) },
                    Value = new RelativityObjectRef() { ArtifactID = DocumentAI }
                },
                new FieldRefValuePair() {
                    Field = new FieldRef() { Guid = new Guid(ActivityLogFields.DateTime) },
                    Value = DateTime.Now
                },
                new FieldRefValuePair() {
                    Field = new FieldRef() { Guid = new Guid(ActivityLogFields.Action) },
                    Value = new ChoiceRef() { Guid = new Guid(actionChoice)}
                },
                new FieldRefValuePair() {
                    Field = new FieldRef() { Guid = new Guid(ActivityLogFields.Seconds) },
                    Value = Seconds
                }
            });
        }
        public bool DocumentHasBeenModified(int ArtifactID, KEH.FieldCollection _fields)
        {
            List<FieldRef> QFields = new List<FieldRef>();
            List<kCura.EventHandler.Field> ObjectFields = new List<kCura.EventHandler.Field>();
            var _fEnum = _fields.GetEnumerator();
            while (_fEnum.MoveNext())
            {
                var _f = _fEnum.Current as kCura.EventHandler.Field;
                QFields.Add(new FieldRef()
                {
                    ArtifactID = _f.ArtifactID
                });
                ObjectFields.Add(_f);
            }
            var DocumentData = Query(KRC.ArtifactType.Document, $"'Artifact ID' == {ArtifactID}", QFields).FirstOrDefault().FieldValues;
            foreach (var _field in ObjectFields)
            {
                var StoredFieldValue = DocumentData.FirstOrDefault(s => s.Field.ArtifactID == _field.ArtifactID)?.Value;
                var UpdatedFieldValue = (_field.Value as KEH.FieldValue)?.Value;
                switch ((KRC.FieldType)_field.FieldTypeID)
                {
                    case KRC.FieldType.FixedLengthText:
                    case KRC.FieldType.LongText:
                    case KRC.FieldType.WholeNumber:
                        if (StoredFieldValue?.ToString() != UpdatedFieldValue?.ToString())
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.Decimal:
                    case KRC.FieldType.Currency:
                        if (Convert.ToDecimal(StoredFieldValue) != Convert.ToDecimal(UpdatedFieldValue))
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.Date:
                        if (Convert.ToDateTime(StoredFieldValue) != Convert.ToDateTime(UpdatedFieldValue))
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.YesNo:
                        if (Convert.ToBoolean(StoredFieldValue) != Convert.ToBoolean(UpdatedFieldValue))
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.SingleChoice:
                        var _UpdatedFieldValue = (_field.Value as KEH.ChoiceFieldValue)?.Value as KEH.ChoiceCollection;
                        var StoredField = (StoredFieldValue as Choice);
                        KEH.Choice UpdatedField = null;
                        var _updEnum = _UpdatedFieldValue.GetEnumerator();
                        while (_updEnum.MoveNext())
                        {
                            UpdatedField = _updEnum.Current as KEH.Choice;
                        }
                        if (StoredField?.ArtifactID != UpdatedField?.ArtifactID)
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.MultipleChoice:
                        var __UpdatedFieldValue = (_field.Value as KEH.ChoiceFieldValue)?.Value as KEH.ChoiceCollection;
                        var _StoredField = (StoredFieldValue as List<Choice>);
                        List<KEH.Choice> _UpdatedField = new List<KEH.Choice>();
                        var __updEnum = __UpdatedFieldValue.GetEnumerator();
                        while (__updEnum.MoveNext())
                        {
                            _UpdatedField.Add(__updEnum.Current as KEH.Choice);
                        }
                        if (_StoredField?.Count() != _UpdatedField?.Count())
                        {
                            return true;
                        }
                        else
                        {
                            for (int i = 0; i < _StoredField.Count(); i++)
                            {
                                if (_StoredField[i]?.ArtifactID != _UpdatedField[i]?.ArtifactID)
                                {
                                    return true;
                                }
                            }
                        }
                        break;
                    case KRC.FieldType.SingleObject:
                        if ((StoredFieldValue as RelativityObjectValue)?.ArtifactID != Convert.ToInt32(UpdatedFieldValue))
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.MultipleObject:
                        var _MOStored = (StoredFieldValue as List<RelativityObjectValue>).Select(s => s.ArtifactID).ToArray();
                        var _MOUpdated = (UpdatedFieldValue as int[]);
                        if (_MOStored.Length != _MOUpdated.Length)
                        {
                            return true;
                        }
                        else
                        {
                            for (int i = 0; i < _MOStored.Length; i++)
                            {
                                if (_MOStored[i] != _MOUpdated[i])
                                {
                                    return true;
                                }
                            }
                        }
                        break;
                    case KRC.FieldType.User:
                        if (StoredFieldValue?.ToString() != UpdatedFieldValue?.ToString())
                        {
                            return true;
                        }
                        break;
                    case KRC.FieldType.File:
                        if (StoredFieldValue?.ToString() != UpdatedFieldValue?.ToString())
                        {
                            return true;
                        }
                        break;
                    default:
                        if (StoredFieldValue?.ToString() != UpdatedFieldValue?.ToString())
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }
        public int GetSecondsSinceLastView(int ArtifactID, int UserArtifactID)
        {
            DateTime dt = DateTime.Now;
            int seconds = 0;
            var DocumentData = Query(ObjectTypes.ActivityLog, $"'{ActivityLogFields.Document}' IN OBJECT [{ArtifactID}] AND '{ActivityLogFields.User}' == {UserArtifactID}", new List<FieldRef>() {
                new FieldRef() { Guid = new Guid(ActivityLogFields.DateTime)}
            }, new List<Sort>() {
                new Sort() {
                    FieldIdentifier = new FieldRef() { Guid = new Guid(ActivityLogFields.DateTime)},
                    Direction = SortEnum.Descending
                }
            }, 1, 2).ToArray();

            if (DocumentData.Length > 0)
            {
                var dtLastView = Convert.ToDateTime(DocumentData[0].FieldValues.First().Value);
                if((dt - dtLastView).TotalSeconds > 2)
                {
                    seconds = Convert.ToInt32((dt - dtLastView).TotalSeconds);
                }
                else if(DocumentData.Length > 1)
                {
                    dtLastView = Convert.ToDateTime(DocumentData[1].FieldValues.First().Value);
                    if ((dt - dtLastView).TotalSeconds > 2)
                    {
                        seconds = Convert.ToInt32((dt - dtLastView).TotalSeconds);
                        Delete(DocumentData[0].ArtifactID);
                    }
                }
            }
            return seconds;
        }

        //PRIVATE
        private ObjectTypeRef GetObjectTypeRef(object ObjectTypeIdentifier)
        {
            ObjectTypeRef ObjectType = new ObjectTypeRef();
            if (ObjectTypeIdentifier is Guid[])
            {
                ObjectType.Guid = ObjectTypeIdentifier as Guid?;
            }
            else if (ObjectTypeIdentifier is int)
            {
                ObjectType.ArtifactTypeID = Convert.ToInt32(ObjectTypeIdentifier);
            }
            else if (ObjectTypeIdentifier is string)
            {
                ObjectType.Name = ObjectTypeIdentifier.ToString();
            }
            else if (ObjectTypeIdentifier is KRC.ArtifactType)
            {
                ObjectType.ArtifactTypeID = (int)(ObjectTypeIdentifier);
            }
            else
            {
                throw new Exception("Unexpected ObjectTypeIdentifier format");
            }
            return ObjectType;
        }
    }
}
