using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DataAuthorization.IDal;
namespace DataAuthorization.Bll
{
    public class ProductionAuthorization
    {
        private static readonly IProductionAuthorization dal_IProductionAuthorization = DalFactory.DalFactory.GetProductionAuthorizationInstance();

        public static string GetProductionOrganizationInfo(string myRootNode, bool myEnabled)
        {
            DataTable m_ProductionOrganizationInfo = dal_IProductionAuthorization.GetProductionOrganizationInfo(myRootNode, myEnabled);
            string m_ProductionOrganizationInfoJson = EasyUIJsonParser.TreeJsonParser.DataTableToJsonByLevelCode(m_ProductionOrganizationInfo, "LevelCode", "Name", new string[] { "OrganizationId",
                "DatabaseId", "Type", "LegalRepresentative", "Address", "Contacts", "ContactInfo","CommissioningDate", "Products", "Remarks" });
            return m_ProductionOrganizationInfoJson;
        }
        public static string GetProductionOrganizationById(string myOrganizationId)
        {
            DataTable m_ProductionOrganizationInfo = dal_IProductionAuthorization.GetProductionOrganizationById(myOrganizationId);
            string m_ProductionOrganizationInfoJson = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(m_ProductionOrganizationInfo);
            return m_ProductionOrganizationInfoJson;
        }
        public static string AddProductionOrganization(Model.ProductionOrganizationInfo myProductionOrganizationInfo)
        {

            bool m_CheckSameNode = dal_IProductionAuthorization.CheckSameNode(myProductionOrganizationInfo.OrganizationID, myProductionOrganizationInfo.LevelCode, false);
            if (m_CheckSameNode == true)
            {
                return "系统内存在相同层次码的节点!";
            }
            else
            {
                int m_ReturnValue = dal_IProductionAuthorization.AddProductionOrganization(myProductionOrganizationInfo);
                return m_ReturnValue.ToString();
            }
        }
        public static string ModifyProductionOrganization(Model.ProductionOrganizationInfo myProductionOrganizationInfo)
        {
            bool m_CheckSameNode = dal_IProductionAuthorization.CheckSameNode(myProductionOrganizationInfo.OrganizationID, myProductionOrganizationInfo.LevelCode, true);
            if (m_CheckSameNode == true)
            {
                return "系统内存在相同层次码的节点!";
            }
            else
            {
                int m_ReturnValue = dal_IProductionAuthorization.ModifyProductionOrganization(myProductionOrganizationInfo);
                return m_ReturnValue.ToString();
            }
        }
        public static string RemoveProductionOrganization(string myOrganizationId, string myLevelCode)
        {
            bool m_CheckChildrenNode = dal_IProductionAuthorization.CheckChildrenNode(myLevelCode);
            if (m_CheckChildrenNode == true)
            {
                return "该组织机构有下属组织机构,无法移除!";
            }
            else
            {
                int m_ReturnValue = dal_IProductionAuthorization.RemoveProductionOrganization(myOrganizationId);
                return m_ReturnValue.ToString();
            }
        }
    }
}
