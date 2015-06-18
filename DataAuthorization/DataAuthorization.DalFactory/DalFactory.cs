using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAuthorization.IDal;
using System.Configuration;

namespace DataAuthorization.DalFactory
{
    public class DalFactory
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static object GetDataObject(string m_Type)
        {
            try
            {
                return DataFactory.DataFactory.GetObject(m_Type);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 产生一个实例
        /// </summary>
        /// <returns>返回实例类型</returns>
        public static ICommon GetCommonInstance()
        {
            string m_Type = ConfigurationManager.AppSettings["DataAuthorization"];
            ICommon m_IMainFrameObj = (ICommon)GetDataObject(m_Type + ".Common");
            //m_IMainFrameObj.InitializeDbConn();
            return m_IMainFrameObj;

        }
        /// <summary>
        /// 产生一个实例
        /// </summary>
        /// <returns>返回实例类型</returns>
        public static IProductionAuthorization GetProductionAuthorizationInstance()
        {
            string m_Type = ConfigurationManager.AppSettings["DataAuthorization"];
            IProductionAuthorization m_IMainFrameObj = (IProductionAuthorization)GetDataObject(m_Type + ".ProductionAuthorization");    //命名空间加上类名 DataAuthorization.Dal.ProductionAuthorization
            //m_IMainFrameObj.InitializeDbConn();
            return m_IMainFrameObj;
        }
    }
}
