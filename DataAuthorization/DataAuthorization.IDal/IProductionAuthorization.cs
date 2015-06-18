using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DataAuthorization.IDal
{
    public interface IProductionAuthorization
    {
        /// <summary>
        /// 获得生产线组织机构信息
        /// </summary>
        /// <param name="myRootNode">根节点层次码</param>
        /// <param name="myEnabled">使能标志</param>
        /// <returns>组织机构信息表</returns>
        DataTable GetProductionOrganizationInfo(string myRootNode, bool myEnabled);
        /// <summary>
        /// 根据组织机构,获得生产线组织机构信息
        /// </summary>
        /// <param name="myOrganizationId">组织机构ID</param>
        /// <returns>组织机构信息表</returns>
        DataTable GetProductionOrganizationById(string myOrganizationId);
        /// <summary>
        /// 检查相同的层次码的节点
        /// </summary>
        /// <param name="myLevelCode">层次码</param>
        /// <returns>如果相同返回true</returns>
        bool CheckSameNode(string myOrganizationId, string myLevelCode, bool myExcludeSelf);
        /// <summary>
        /// 检查该层次码节点是否有孩子节点
        /// </summary>
        /// <param name="myLevelCode">层次码</param>
        /// <returns>如果有返回true</returns>
        bool CheckChildrenNode(string myLevelCode);
        /// <summary>
        /// 增加组织机构节点
        /// </summary>
        /// <param name="myProductionOrganizationInfo">节点信息</param>
        /// <returns>操作返回信息</returns>
        int AddProductionOrganization(Model.ProductionOrganizationInfo myProductionOrganizationInfo);
        /// <summary>
        /// 修改组织机构节点
        /// </summary>
        /// <param name="myProductionOrganizationInfo">节点信息</param>
        /// <returns>操作返回信息</returns>
        int ModifyProductionOrganization(Model.ProductionOrganizationInfo myProductionOrganizationInfo);
        /// <summary>
        /// 删除组织机构节点
        /// </summary>
        /// <param name="myOrganizationId">节点ID</param>
        /// <returns>操作返回信息</returns>
        int DeleteProductionOrganization(string myOrganizationId);
        /// <summary>
        /// 移除组织机构节点
        /// </summary>
        /// <param name="myOrganizationId">节点ID</param>
        /// <returns>操作返回信息</returns>
        int RemoveProductionOrganization(string myOrganizationId);
        /// <summary>
        /// 恢复组织机构节点
        /// </summary>
        /// <param name="myOrganizationId">节点ID</param>
        /// <param name="myLevelCode">节点层次码</param>
        /// <returns>操作返回信息</returns>
        int RestoreProductionOrganization(string myOrganizationId, string myLevelCode);
    }
}
