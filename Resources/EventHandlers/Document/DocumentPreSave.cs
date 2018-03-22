using kCura.EventHandler;
using Resources.Repositories;
using Resources.Repositories.ObjectManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Event_Handlers.Document
{
    class DocumentPreSave : PreSaveEventHandler
    {
        public override FieldCollection RequiredFields
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override Response Execute()
        {
            throw new NotImplementedException();
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
