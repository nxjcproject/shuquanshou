using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DataAuthorization.IDal;


namespace DataAuthorization.Dal
{
    public class ProductionAuthorization : IProductionAuthorization
    {
        private static readonly WebStyleBaseForEnergy.DbDataAdapter m_DbDataAdapter = new WebStyleBaseForEnergy.DbDataAdapter("ConnNXJC");

        public DataTable GetProductionOrganizationInfo(string myRootNode, bool myEnabled)
        {
            string m_Enabled = myEnabled == true ? "1" : "0";
             string m_Sql = @"select 
                    A.OrganizationID as OrganizationId, 
                    A.LevelCode as LevelCode, 
                    A.DatabaseID as DataBaseId, 
                    A.Name as Name, 
                    A.Type as Type, 
                    A.LegalRepresentative as LegalRepresentative, 
                    A.Address as Address, 
                    A.Contacts as Contacts, 
                    A.ContactInfo as ContactInfo, 
                    A.CommissioningDate as CommissioningDate, 
                    A.Products as Products, 
                    A.Remarks as Remarks, 
                    A.Enabled as Enabled 
                    from system_Organization A 
                    where A.ENABLED = {1}  
                    {0}";
            try
            {
                string m_Condition = "";
                if (myRootNode != "")
                {
                    m_Condition = m_Condition + string.Format("and A.LevelCode like '{0}%'", myRootNode);
                }
                m_Sql = string.Format(m_Sql, m_Condition, m_Enabled);

                DataSet mDataSet_ProductionOrganizationInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "ProductionOrganizationTable");
                return mDataSet_ProductionOrganizationInfo.Tables["ProductionOrganizationTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetProductionOrganizationById(string myOrganizationId)
        {
            string m_Sql = @"select 
                    A.OrganizationID as OrganizationId, 
                    A.LevelCode as LevelCode, 
                    A.DatabaseID as DataBaseId, 
                    A.Name as Name, 
                    A.Type as Type, 
                    A.LegalRepresentative as LegalRepresentative, 
                    A.Address as Address, 
                    A.Contacts as Contacts, 
                    A.ContactInfo as ContactInfo, 
                    A.CommissioningDate as CommissioningDate, 
                    A.Products as Products, 
                    A.Remarks as Remarks, 
                    A.Enabled as Enabled 
                    from system_Organization A 
                    where A.OrganizationID = '{0}'";
            try
            {
                m_Sql = string.Format(m_Sql, myOrganizationId);

                DataSet mDataSet_ProductionOrganizationInfo = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "ProductionOrganizationTable");
                return mDataSet_ProductionOrganizationInfo.Tables["ProductionOrganizationTable"];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CheckSameNode(string myOrganizationId, string myLevelCode, bool myExcludeSelf)
        {
            string m_Sql = @"select count(A.LevelCode) as NodeRowCount from system_Organization A  where (A.OrganizationID = '{0}' or A.LevelCode = '{1}') {2}";
            try
            {
                string m_Condition = "";
                if(myExcludeSelf ==true)
                {
                    m_Condition = string.Format(" and A.OrganizationID <> '{0}'", myOrganizationId);
                }
                m_Sql = string.Format(m_Sql, myOrganizationId, myLevelCode, m_Condition);

                DataSet mDataSet_RowCount = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "RowCountTable");
                if (mDataSet_RowCount != null)
                {
                    int m_RowCount = (int)mDataSet_RowCount.Tables["RowCountTable"].Rows[0]["NodeRowCount"];
                    if (m_RowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool CheckChildrenNode(string myLevelCode)
        {
            string m_Sql = @"select count(A.LevelCode) as NodeRowCount from system_Organization A  where A.LevelCode like '{0}%' and A.Enabled = 1";
            try
            {
                m_Sql = string.Format(m_Sql, myLevelCode);

                DataSet mDataSet_RowCount = m_DbDataAdapter.MySqlDbDataAdaper.Fill(null, m_Sql, "RowCountTable");
                if (mDataSet_RowCount != null)
                {
                    int m_RowCount = (int)mDataSet_RowCount.Tables["RowCountTable"].Rows[0]["NodeRowCount"];
                    if (m_RowCount > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public int AddProductionOrganization(Model.ProductionOrganizationInfo myProductionOrganizationInfo)
        {
            string m_Sql = @" Insert into system_Organization 
                ( OrganizationID, LevelCode, Name, Type, LegalRepresentative, Address, Contacts, ContactInfo, CommissioningDate, Products, Remarks) 
                values
                ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');";
            m_Sql = m_Sql.Replace("{0}", myProductionOrganizationInfo.OrganizationID);
            m_Sql = m_Sql.Replace("{1}", myProductionOrganizationInfo.LevelCode);
            m_Sql = m_Sql.Replace("{2}", myProductionOrganizationInfo.Name);
            m_Sql = m_Sql.Replace("{3}", myProductionOrganizationInfo.Type);
            m_Sql = m_Sql.Replace("{4}", myProductionOrganizationInfo.LegalRepresentative);
            m_Sql = m_Sql.Replace("{5}", myProductionOrganizationInfo.Address);
            m_Sql = m_Sql.Replace("{6}", myProductionOrganizationInfo.Contacts);
            m_Sql = m_Sql.Replace("{7}", myProductionOrganizationInfo.ContactInfo);
            m_Sql = m_Sql.Replace("{8}", myProductionOrganizationInfo.CommissioningDate);
            m_Sql = m_Sql.Replace("{9}", myProductionOrganizationInfo.Products);
            m_Sql = m_Sql.Replace("{10}", myProductionOrganizationInfo.Remarks);
            try
            {
                int m_ReturnValue = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return m_ReturnValue >= 1 ? 1 : m_ReturnValue; ;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int ModifyProductionOrganization(Model.ProductionOrganizationInfo myProductionOrganizationInfo)
        {
            string m_Sql = @"Update system_Organization set
                            LevelCode = '{1}', 
                            Name = '{2}',
                            Type = '{3}',
                            LegalRepresentative = '{4}',
                            Address = '{5}',
                            Contacts = '{6}', 
                            ContactInfo = '{7}', 
                            CommissioningDate = '{8}', 
                            Products = '{9}', 
                            Remarks = '{10}'
                            where OrganizationID = '{0}'";
            m_Sql = m_Sql.Replace("{0}", myProductionOrganizationInfo.OrganizationID);
            m_Sql = m_Sql.Replace("{1}", myProductionOrganizationInfo.LevelCode);
            m_Sql = m_Sql.Replace("{2}", myProductionOrganizationInfo.Name);
            m_Sql = m_Sql.Replace("{3}", myProductionOrganizationInfo.Type);
            m_Sql = m_Sql.Replace("{4}", myProductionOrganizationInfo.LegalRepresentative);
            m_Sql = m_Sql.Replace("{5}", myProductionOrganizationInfo.Address);
            m_Sql = m_Sql.Replace("{6}", myProductionOrganizationInfo.Contacts);
            m_Sql = m_Sql.Replace("{7}", myProductionOrganizationInfo.ContactInfo);
            m_Sql = m_Sql.Replace("{8}", myProductionOrganizationInfo.CommissioningDate);
            m_Sql = m_Sql.Replace("{9}", myProductionOrganizationInfo.Products);
            m_Sql = m_Sql.Replace("{10}", myProductionOrganizationInfo.Remarks);
            try
            {
                int ModifyRows = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return ModifyRows >= 1 ? 1 : ModifyRows;
            }
            catch
            {
                return -1;
            }
        }

        public int DeleteProductionOrganization(string myOrganizationId)
        {
            string m_Sql = @"Delete from system_Organization where OrganizationID = '{0}'";
            m_Sql = m_Sql.Replace("{0}", myOrganizationId);
            try
            {
                int m_ExecuteFlag = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return m_ExecuteFlag >= 1 ? 1 : m_ExecuteFlag;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int RemoveProductionOrganization(string myOrganizationId)
        {
            string m_Sql = @"Update system_Organization set
                            Enabled = 0
                            where OrganizationID = '{0}'";
            m_Sql = m_Sql.Replace("{0}", myOrganizationId);
            try
            {
                int m_ExcuteFlag = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return m_ExcuteFlag >= 1 ? 1 : m_ExcuteFlag;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int RestoreProductionOrganization(string myOrganizationId, string myLevelCode)
        {
            string m_Sql = @"Update system_Organization set
                            Enabled = 1,
                            LevelCode = '{1}' 
                            where OrganizationID = '{0}'";
            m_Sql = m_Sql.Replace("{0}", myOrganizationId);
            m_Sql = m_Sql.Replace("{1}", myLevelCode);
            try
            {
                int m_ExcuteFlag = m_DbDataAdapter.MySqlDbDataAdaper.ExecuteNonQuery(m_Sql);
                return m_ExcuteFlag >= 1 ? 1 : m_ExcuteFlag;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
