using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace DataAuthorization.Web.UI_DataAuthorization
{
    public partial class ProductionOrganization : WebStyleBaseForEnergy.webStyleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.InitComponts();
#if DEBUG
            ////////////////////调试用,自定义的数据授权
            List<string> m_DataValidIdItems = new List<string>() { "zc_nxjc_byc_byf" };
            AddDataValidIdGroup("ProductionOrganization", m_DataValidIdItems);
            //页面操作权限控制
            mPageOpPermission = "0100";
#endif
        }
        /// <summary>
        /// 增删改查权限控制
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static char[] AuthorityControl()
        {
            return mPageOpPermission.ToArray();
        }
        [WebMethod]
        public static string GetProductionOrganizationInfo()
        {
            string m_ProductionOrganizationInfoJson = DataAuthorization.Bll.ProductionAuthorization.GetProductionOrganizationInfo("", true);
            return m_ProductionOrganizationInfoJson;
        }
        [WebMethod]
        public static string GetProductionOrganizationById(string myOrganizationId)
        {
            string m_ProductionOrganizationInfoJson = DataAuthorization.Bll.ProductionAuthorization.GetProductionOrganizationById(myOrganizationId);
            return m_ProductionOrganizationInfoJson;
        }
        [WebMethod]
        public static string AddProductionOrganization(string myOrganizationId, string myName, string myLevelCode, string myType, string myCommissioningDate,
                                                       string myAddress, string myCoefficientAltitude, string myContacts, string myLegalRepresentative, string myContactInfo, string myProducts, string myRemark)
        {
            if (mPageOpPermission.ToArray()[1] == '1')
            {
                if (mUserId != "")
                {
                    Model.ProductionOrganizationInfo m_ProductionOrganizationInfo = new Model.ProductionOrganizationInfo();
                    m_ProductionOrganizationInfo.OrganizationID = myOrganizationId;
                    m_ProductionOrganizationInfo.Name = myName;
                    m_ProductionOrganizationInfo.LevelCode = myLevelCode;
                    m_ProductionOrganizationInfo.DatabaseID = "";
                    m_ProductionOrganizationInfo.Type = myType;
                    m_ProductionOrganizationInfo.CommissioningDate = myCommissioningDate;
                    m_ProductionOrganizationInfo.Address = myAddress;
                    if (myCoefficientAltitude == "")
                    {
                        m_ProductionOrganizationInfo.CoefficientAltitude = "0";
                    }
                    else
                    {
                        m_ProductionOrganizationInfo.CoefficientAltitude = myCoefficientAltitude;
                    }
                    m_ProductionOrganizationInfo.Contacts = myContacts;
                    m_ProductionOrganizationInfo.LegalRepresentative = myLegalRepresentative;
                    m_ProductionOrganizationInfo.ContactInfo = myContactInfo;
                    m_ProductionOrganizationInfo.Products = myProducts;
                    m_ProductionOrganizationInfo.Remarks = myRemark;

                    string m_ProductionOrganizationInfoJson = DataAuthorization.Bll.ProductionAuthorization.AddProductionOrganization(m_ProductionOrganizationInfo);
                    return m_ProductionOrganizationInfoJson;
                }
                else
                {
                    return "非法的用户操作!";
                }
            }
            else
            {
                return "该用户没有增加权限！";
            }
        }
        [WebMethod]
        public static string ModifyProductionOrganization(string myOrganizationId, string myName, string myLevelCode, string myType, string myCommissioningDate,
                                                       string myAddress, string myCoefficientAltitude, string myContacts, string myLegalRepresentative, string myContactInfo, string myProducts, string myRemark)
        {
            if (mPageOpPermission.ToArray()[2] == '1')
            {
                if (mUserId != "")
                {
                    Model.ProductionOrganizationInfo m_ProductionOrganizationInfo = new Model.ProductionOrganizationInfo();
                    m_ProductionOrganizationInfo.OrganizationID = myOrganizationId;
                    m_ProductionOrganizationInfo.Name = myName;
                    m_ProductionOrganizationInfo.LevelCode = myLevelCode;
                    m_ProductionOrganizationInfo.DatabaseID = "";
                    m_ProductionOrganizationInfo.Type = myType;
                    m_ProductionOrganizationInfo.CommissioningDate = myCommissioningDate;
                    m_ProductionOrganizationInfo.Address = myAddress;
                    if (myCoefficientAltitude == "")
                    {
                        m_ProductionOrganizationInfo.CoefficientAltitude = "0";
                    }
                    else
                    {
                        m_ProductionOrganizationInfo.CoefficientAltitude = myCoefficientAltitude;
                    }
                    m_ProductionOrganizationInfo.Contacts = myContacts;
                    m_ProductionOrganizationInfo.LegalRepresentative = myLegalRepresentative;
                    m_ProductionOrganizationInfo.ContactInfo = myContactInfo;
                    m_ProductionOrganizationInfo.Products = myProducts;
                    m_ProductionOrganizationInfo.Remarks = myRemark;
                    string m_ProductionOrganizationInfoJson = DataAuthorization.Bll.ProductionAuthorization.ModifyProductionOrganization(m_ProductionOrganizationInfo);
                    return m_ProductionOrganizationInfoJson;
                }
                else
                {
                    return "非法的用户操作!";
                }
            }
            else
            {
                return "该用户没有修改权限！";
            }
        }
        [WebMethod]
        public static string RemoveProductionOrganization(string myOrganizationId, string myLevelCode)
        {
            if (mPageOpPermission.ToArray()[3] == '1')
            {
                if (mUserId != "")
                {
                    string m_ProductionOrganizationInfoJson = DataAuthorization.Bll.ProductionAuthorization.RemoveProductionOrganization(myOrganizationId, myLevelCode);
                    return m_ProductionOrganizationInfoJson;
                }
                else
                {
                    return "非法的用户操作!";
                }
            }
            else
            {
                return "该用户没有删除权限！";
            }
        }
    }
}