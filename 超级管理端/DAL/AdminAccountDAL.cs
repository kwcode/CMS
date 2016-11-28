using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class AdminAccountDAL
    {
        /// <summary>
        /// 表名
        /// </summary>
        private static string TableName = "AdminAccount";

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="pramsAdd">参数</param>
        /// <returns>成功返回自增ID</returns>
        public static int Add(SqlParameter[] pramsAdd)
        {
            return DBCommon.DBHelper.Add(DALUtil.ConnString, TableName, pramsAdd);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pramsModify">修改参数集合</param>
        /// <param name="pramsWhere">条件集合</param>
        /// <returns>成功返回影响行数,失败返回0</returns>
        public static int Add(SqlParameter[] pramsModify, SqlParameter[] pramsWhere)
        {
            return DBCommon.DBHelper.Modify(DALUtil.ConnString, TableName, pramsModify, pramsWhere);
        }
        #endregion

        #region 获取一条数据
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T">返回实体</typeparam>
        /// <param name="SelectIF">需要查询的字段</param>
        /// <param name="pramsWhere">条件集合</param> 
        public static T Get1<T>(string SelectIF, SqlParameter[] pramsWhere)
        {
            DataTable dt = DBCommon.DBHelper.GetDataTable1(DALUtil.ConnString, TableName, SelectIF, pramsWhere);
            return DALUtil.ConvertDataTableToEntity<T>(dt);
        }
        #endregion

        #region 获取集合
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T">集合实体</typeparam>
        /// <param name="SelectIF">需要查询的字段</param>
        /// <param name="pramsWhere">条件集合</param>
        /// <param name="OrderName">排序无需带Order by</param> 
        public static List<T> GetList<T>(string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            DataTable dt = DBCommon.DBHelper.GetDataTable2(DALUtil.ConnString, TableName, SelectIF, pramsWhere, OrderName);
            return DALUtil.ConvertDataTableToEntityList<T>(dt);
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary> 
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">数量</param>
        /// <param name="SelectIF">查询字段</param>
        /// <param name="pramsWhere">条件</param>
        /// <param name="OrderName">排序无需带Order by</param> 
        public static List<T> GetPageList<T>(int PageIndex, int PageSize, string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            DataTable dt = DBCommon.DBHelper.GetDataTablePage(DALUtil.ConnString, TableName, SelectIF, PageIndex, PageSize, pramsWhere, OrderName);
            return DALUtil.ConvertDataTableToEntityList<T>(dt);
        }
        /// <summary>
        /// 获取数据总数量
        /// </summary>
        /// <param name="pramsWhere">条件</param> 
        public static int GetRecordCount(SqlParameter[] pramsWhere)
        {
            string SelectIF = " count(1) ";
            object obj = DBCommon.DBHelper.GetSingle(DALUtil.ConnString, TableName, SelectIF, pramsWhere);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return DALUtil.ConvertToInt32(obj);
            }
        }
        #endregion

        #region 登录
        //public static int Login(string name, string pwd)
        //{
        //    try
        //    {
        //        SqlParameter[] pramsWhere =
        //        {
        //           DALUtil.MakeInParam("@LoginName", SqlDbType.NVarChar, 100,name)
        //        };
        //        AdminAccountEntity adminAccount = Get1<AdminAccountEntity>("LoginName,Password,UserID", pramsWhere);
        //        if (adminAccount == null)
        //        {
        //            return 1;
        //        }
        //        else
        //        {
        //            if (adminAccount.Password == pwd)
        //            {
        //                return 8;
        //            }
        //            else
        //            {
        //                return 2;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //记录日志
        //        //BLLUtil.Log(ex.ToString(), "Error.txt");
        //        return -9;
        //    }
        //}

        //public static int GetUserID(string LoginName)
        //{
        //    try
        //    {
        //        SqlParameter[] pramsWhere =
        //        {
        //           DALUtil.MakeInParam("@LoginName", SqlDbType.NVarChar, 100,LoginName)
        //        };
        //        AdminAccountEntity adminAccount = Get1<AdminAccountEntity>("UserID", pramsWhere);
        //        return adminAccount.UserID;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}
        #endregion
        public static int GetLoginName(int UserID)
        {
            try
            {
                SqlParameter[] pramsWhere =
                {
                   DALUtil.MakeInParam("@LoginName", SqlDbType.NVarChar, 100,LoginName)
                };
                AdminAccountEntity adminAccount = Get1<AdminAccountEntity>("UserID", pramsWhere);
                return adminAccount.UserID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
