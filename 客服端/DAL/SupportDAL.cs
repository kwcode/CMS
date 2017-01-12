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
    /// Create Time:2017-01-12 10:36
    /// </summary>
    public class SupportDAL
    {
        /// <summary>
        ///表名
        /// <summary>
        private static string TableName = "Support";

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
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public static int Add(Model.SupportEntity Support)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Support(Title,Content,Keysword,LookCount,Status)");
            strSql.Append(" values(@Title,@Content,@Keysword,@LookCount,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,200,Support.Title), 
                DAL.DALUtil.MakeInParam("@Keysword",System.Data.SqlDbType.NVarChar,200,Support.Keysword), 
                DAL.DALUtil.MakeInParam("@Content",System.Data.SqlDbType.NVarChar,200,Support.Content), 
                DAL.DALUtil.MakeInParam("@LookCount",System.Data.SqlDbType.Int,8,Support.LookCount),
                DAL.DALUtil.MakeInParam("@Status",System.Data.SqlDbType.Int,8,Support.Status)
            };
            object obj = DBCommon.DbHelperSQL.GetSingle(DALUtil.ConnString, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
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
        /// <returns></returns>
        public static int Modify(Model.SupportEntity Support)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Support set ");
            strSql.Append("Title=@Title");
            strSql.Append(",Content=@Content");
            strSql.Append(",Keysword=@Keysword");
            strSql.Append(",LookCount=@LookCount");
            strSql.Append(",Status=@Status");
            strSql.Append(" where KeyID=@KeyID");
            SqlParameter[] pramsModify =
            {
                DAL.DALUtil.MakeInParam("@KeyID",System.Data.SqlDbType.Int,0,Support.KeyID),
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,200,Support.Title), 
                DAL.DALUtil.MakeInParam("@Keysword",System.Data.SqlDbType.NVarChar,200,Support.Keysword), 
                DAL.DALUtil.MakeInParam("@Content",System.Data.SqlDbType.NVarChar,200,Support.Content), 
                DAL.DALUtil.MakeInParam("@LookCount",System.Data.SqlDbType.Int,0,Support.LookCount),
                DAL.DALUtil.MakeInParam("@Status",System.Data.SqlDbType.Int,0,Support.Status)
            };
            return DBCommon.DbHelperSQL.ExecuteSql(DALUtil.ConnString, strSql.ToString(), pramsModify);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public static int Modify(int KeyID, string Title = null, string Content = null, string Keysword = null, int? LookCount = null, int? Status = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Support set ");
            if (Title != null)
                strSql.Append("Title=@Title,");
            if (Content != null)
                strSql.Append("Content=@Content,");
            if (Keysword != null)
                strSql.Append("Keysword=@Keysword,");
            if (LookCount != null)
                strSql.Append("LookCount=@LookCount,");
            if (Status != null)
                strSql.Append("Status=@Status,");
            if (strSql.ToString().Substring(strSql.Length - 1, 1) == ",")
                strSql.Remove(strSql.Length - 1, 1);
            else
                return 0;
            strSql.Append(" where KeyID=@KeyID");
            SqlParameter[] pramsModify =
            {
                DAL.DALUtil.MakeInParam("@KeyID",System.Data.SqlDbType.Int,0,KeyID),
                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,200,Title), 
                DAL.DALUtil.MakeInParam("@Keysword",System.Data.SqlDbType.NVarChar,200,Keysword), 
                DAL.DALUtil.MakeInParam("@Content",System.Data.SqlDbType.NVarChar,200,Content), 
                DAL.DALUtil.MakeInParam("@LookCount",System.Data.SqlDbType.Int,0,LookCount),
                DAL.DALUtil.MakeInParam("@Status",System.Data.SqlDbType.Int,0,Status)
            };
            return DBCommon.DbHelperSQL.ExecuteSql(DALUtil.ConnString, strSql.ToString(), pramsModify);
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
				DALUtil.MakeInParam("@KeyID",SqlDbType.Int,4,id)
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
        public static SupportEntity Get_99(int ID, string SelectIF)
        {
            try
            {
                //参数Where条件
                SqlParameter[] pramsWhere =
				{
					DALUtil.MakeInParam("@KeyID", SqlDbType.Int, 4, ID)
				};
                return Get1<SupportEntity>(SelectIF, pramsWhere);
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
				DALUtil.MakeInParam("@KeyID",SqlDbType.Int,4,ID)
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

