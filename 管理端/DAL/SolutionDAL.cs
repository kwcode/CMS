using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model;
namespace DAL
{
    /// <summary>
    /// Create By Tool :TCode
    /// Create Time:2016-12-04 14:47
    /// </summary>
    public class SolutionDAL
    {
        /// <summary>
        ///表名
        /// <summary>
        private static string TableName = "Solution";

        #region 新增
        /// <summary>
        ///新增
        /// <summary>
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
        public static int Modify(SqlParameter[] pramsModify, SqlParameter[] pramsWhere)
        {
            return DBCommon.DBHelper.Modify(DALUtil.ConnString, TableName, pramsModify, pramsWhere);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pramsModify">修改参数集合</param>
        /// <param name="id">ID</param>
        /// <returns>成功返回影响行数,失败返回0</returns>
        public static int Modify(SqlParameter[] pramsModify, int id)
        {
            SqlParameter[] pramsWhere =
			{
				DALUtil.MakeInParam("@ID",SqlDbType.Int,4,id)
			};
            return DBCommon.DBHelper.Modify(DALUtil.ConnString, TableName, pramsModify, pramsWhere);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pramsWhere">条件集合</param>
        /// <returns>成功返回影响行数,失败返回0</returns>
        public static int Delete(SqlParameter[] pramsWhere)
        {
            return DBCommon.DBHelper.Delete(DALUtil.ConnString, TableName, pramsWhere);
        }
        #endregion




        #region 获取一条数据
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T">返回实体</typeparam>
        /// <param name="SelectIF">需要查询的字段</param>
        /// <param name="pramsWhere">条件集合</param>
        public static T Get1<T>(string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            DataTable dt = DBCommon.DBHelper.GetDataTable1(DALUtil.ConnString, TableName, SelectIF, pramsWhere, OrderName);
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
        /// <param name="pramsWhere">条件(AND a=1 and b=2)</param>
        public static List<T> GetPageList<T>(int PageIndex, int PageSize, string SelectIF, string strWhere, string OrderName = "ID")
        {
            DataTable dt = DBCommon.DBHelper.GetDataTablePage(DALUtil.ConnString, TableName, SelectIF, PageIndex, PageSize, strWhere, OrderName);
            return DALUtil.ConvertDataTableToEntityList<T>(dt);
        }

        /// <summary>
        /// 获取数据总数量
        /// </summary>
        /// <param name="pramsWhere">条件(AND a=1 and b=2)</param>
        public static int GetRecordCount(string strWhere)
        {
            string SelectIF = " count(1) ";
            object obj = DBCommon.DBHelper.GetSingle(DALUtil.ConnString, TableName, SelectIF, strWhere);
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
        #region 获取自定义参数数据
        /// <summary>
        /// 获取自定义参数数据
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="SelectIF">查询字段</param>
        public static SolutionEntity Get_99(int ID, string SelectIF)
        {
            try
            {
                //参数Where条件
                SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@ID", SqlDbType.Int, 4, ID)
				};
                return Get1<SolutionEntity>(SelectIF, pramsWhere);
            }
            catch { return null; }
        }
        #endregion
        #region 根据删除ID
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>成功返回影响行数,失败返回0</returns>
        public static int Delete_1(int ID)
        {
            SqlParameter[] pramsWhere =
			{
				DALUtil.MakeInParam("@ID",SqlDbType.Int,4,ID)
			 };
            return DBCommon.DBHelper.Delete(DALUtil.ConnString, TableName, pramsWhere);
        }
        #endregion

        #region 获取一个数据[判断是否存在,获取最大值]
        /// <summary>
        /// 获取一个数据[判断是否存在,获取最大值]
        /// </summary>
        /// <param name="SelectIF">查询数据</param>
        /// <returns>返回数据</returns>
        public static int GetSingle(string SelectIF)
        {
            string sqlWhere = " 1=1 ";
            object obj = DBCommon.DBHelper.GetSingle(DALUtil.ConnString, TableName, SelectIF, sqlWhere);
            return DALUtil.ConvertToInt32(obj);
        }
        /// <summary>
        /// 获取一个数据[判断是否存在,获取最大值]
        /// </summary>
        /// <param name="SelectIF">查询数据</param>
        /// <param name="sqlWhere">条件 a=1</param>
        /// <returns>返回数据</returns>
        public static int GetSingle(string SelectIF, string sqlWhere)
        {
            object obj = DBCommon.DBHelper.GetSingle(DALUtil.ConnString, TableName, SelectIF, sqlWhere);
            return DALUtil.ConvertToInt32(obj);
        }

        #endregion




    }
}

