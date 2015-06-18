using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAuthorization.Model
{
    public class ProductionOrganizationInfo
    {
        private string _OrganizationID;
        private string _LevelCode;
        private string _DatabaseID;
        private string _Name;
        private string _Type;
        private string _LegalRepresentative;
        private string _Address;
        private string _Contacts;
        private string _ContactInfo;
        private string _CommissioningDate;
        private string _Products;
        private string _Remarks;
        private bool _Enabled;

        public ProductionOrganizationInfo()
        {
            _OrganizationID = "";
            _LevelCode = "";
            _DatabaseID = "";
            _Name = "";
            _Type = "";
            _LegalRepresentative = "";
            _Address = "";
            _Contacts = "";
            _ContactInfo = "";
            _CommissioningDate = "NUll";
            _Products = "";
            _Remarks = "";
            _Enabled = true;
        }
        public string OrganizationID
        {
            get
            {
                return _OrganizationID;
            }
            set
            {
                _OrganizationID = value;
            }
        }
        public string LevelCode
        {
            get
            {
                return _LevelCode;
            }
            set
            {
                _LevelCode = value;
            }
        }
        public string DatabaseID
        {
            get
            {
                return _DatabaseID;
            }
            set
            {
                _DatabaseID = value;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        public string LegalRepresentative
        {
            get
            {
                return _LegalRepresentative;
            }
            set
            {
                _LegalRepresentative = value;
            }
        }
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
        public string Contacts
        {
            get
            {
                return _Contacts;
            }
            set
            {
                _Contacts = value;
            }
        }
        public string ContactInfo
        {
            get
            {
                return _ContactInfo;
            }
            set
            {
                _ContactInfo = value;
            }
        }
        public string CommissioningDate
        {
            get
            {
                return _CommissioningDate;
            }
            set
            {
                _CommissioningDate = value;
            }
        }
        public string Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
            }
        }
        public string Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                _Remarks = value;
            }
        }
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }
    }
}
