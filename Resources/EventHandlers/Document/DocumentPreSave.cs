﻿using kCura.EventHandler;
using Resources.Constants;
using Resources.Repositories;
using Resources.Repositories.ObjectManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.EventHandlers.Document
{
    class DocumentPreSave : PreSaveEventHandler
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
                int? Seconds = _Repo.GetSecondsSinceLastView(this.ActiveArtifact.ArtifactID, this.Helper.GetAuthenticationManager().UserInfo.ArtifactID);
                string Action = _Repo.DocumentHasBeenModified(this.ActiveArtifact.ArtifactID, this.ActiveArtifact.Fields) ? ActionChoices.Edit : ActionChoices.View;
                _Repo.InsertDocumentActivity(this.ActiveArtifact.ArtifactID, this.Helper.GetAuthenticationManager().UserInfo.ArtifactID, Action, Seconds);
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
