using Relativity.Services.Objects.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.DTOs.Repositories
{
    public class Artifact
    {
        public int ArtifactID { get; set; }
        public int? ArtifactTypeID { get; set; }
        public string ArtifactTypeName { get; set; }
        public DateTime? SystemCreatedOn { get; set; }
        public DateTime? SystemLastModifiedOn { get; set; }
        public string TextIdentifier { get; set; }
    }
    public class ArtifactRequest
    {
        private object _objectType { get; set; }
        private int _artifactID { get; set; }
        private List<FieldRefValuePair> _fields { get; set; }

        public int ArtifactID
        {
            get
            {
                return this._artifactID;
            }
            set
            {
                this._artifactID = value;
            }
        }
        public List<FieldRefValuePair> Fields
        {
            get
            {
                return this._fields;
            }
            set
            {
                this._fields = value;
            }
        }
        public object ObjectTypeIdentifier
        {
            get
            {
                return this._objectType;
            }
            set
            {
                this._objectType = value;
            }
        }

        public ArtifactRequest()
        {
            this.Fields = new List<FieldRefValuePair>();
        }
        public ArtifactRequest(object ObjectType)
        {
            this._objectType = ObjectType;
            this.Fields = new List<FieldRefValuePair>();
        }
    }
}
