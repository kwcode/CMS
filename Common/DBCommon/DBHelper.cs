using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBCommon
{
    /// <summary>
    /// 比较通用的sql帮助类，但是没加入事务
    /// </summary>
    public class DBHelper
    {
        #region 新增

        public static int Add(string connString, string Table, SqlParameter[] pramsAdd)
        {
            try
            {
                if (pramsAdd != null && pramsAdd.Length > 0)
                {
                    using (SqlConnection MyConn = new SqlConnection(connString))
                    {
                        Object obj = new object();
                        MyConn.Open();
                        SqlCommand cmd = new SqlCommand();
                        StringBuilder sqlParameterName = new StringBuilder();
                        StringBuilder sqlValue = new StringBuilder();
                        foreach (SqlParameter pram in pramsAdd)
                        {
                            //Value为null值不添加
                            if (pram.Value != null)
                            {
                                //生成ParameterName 表()表字段
                                sqlParameterName.Append(pram.ParameterName.Replace("@", "") + ",");

                                //生成ParameterName 表()表字段
                                sqlValue.Append(pram.ParameterName + ",");

                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                        if (sqlParameterName.ToString().Trim() != "")
                        {
                            //删除最后多余的 ,(逗号)
                            sqlParameterName.Remove(sqlParameterName.Length - 1, 1);

                            //删除最后多余的 ,(逗号)
                            sqlValue.Remove(sqlValue.Length - 1, 1);
                        }
                        string sqlText = "insert into " + Table + " (" + sqlParameterName + ") Values(" + sqlValue + ") ;select @@IDENTITY";
                        cmd.Connection = MyConn;
                        //初始Sql语句
                        cmd.CommandText = sqlText;
                        obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return DBUtil.ConvertToInt32(obj.ToString());
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region 修改
        /// <summary>
        ///  修改记录
        /// </summary>      
        /// <param name="Table">表名</param>
        /// <param name="pramsAdd">表字段值</param>
        /// <returns>影响数</returns>
        public static int Modify(string connString, string Table, SqlParameter[] pramsModify, SqlParameter[] pramsWhere)
        {
            try
            {
                if (pramsModify != null && pramsModify.Length > 0)
                {
                    using (SqlConnection MyConn = new SqlConnection(connString))
                    {
                        Object obj = new object();
                        MyConn.Open();
                        SqlCommand cmd = new SqlCommand();

                        StringBuilder sqlParameterSet = new StringBuilder();
                        foreach (SqlParameter pram in pramsModify)
                        {
                            if (pram.Value != null)
                            {
                                //格式如name=@name,time=@time
                                sqlParameterSet.Append(pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName + ",");
                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                        //删除最后多余的 ,(逗号)
                        if (sqlParameterSet.ToString().Trim() != "")
                        {
                            sqlParameterSet.Remove(sqlParameterSet.Length - 1, 1);
                        }

                        StringBuilder sqlWhere = new StringBuilder();
                        sqlWhere.Append(" 1=1 ");//保证pramsWhere参数为空的时候 正常使用
                        if (pramsWhere != null && pramsWhere.Length > 0)
                        {
                            string ParameterNameLeft = "";
                            foreach (SqlParameter pram in pramsWhere)
                            {
                                ParameterNameLeft = pram.ParameterName.ToString().Replace("@", "");
                                pram.ParameterName = pram.ParameterName + "_Where";//避免和修改的参数冲突 加_Where
                                //格式如：AND id=@id__Where
                                sqlWhere.Append(" AND " + ParameterNameLeft + "=" + pram.ParameterName);
                                cmd.Parameters.Add(pram);
                            }
                        }
                        //Sql语句
                        string sqlText = " update  " + Table + " set " + sqlParameterSet + " where " + sqlWhere;

                        //初始连接Conn
                        cmd.Connection = MyConn;
                        //初始Sql语句
                        cmd.CommandText = sqlText;
                        obj = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return DBUtil.ConvertToInt32(obj.ToString());
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 删除
        public static int Delete(string connString, string Table, SqlParameter[] pramsWhere)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    MyConn.Open();
                    SqlCommand cmd = new SqlCommand();
                    StringBuilder sqlWhere = new StringBuilder();
                    if (pramsWhere != null && pramsWhere.Length > 0)
                    {
                        string ParameterNameLeft = "";
                        foreach (SqlParameter pram in pramsWhere)
                        {
                            ParameterNameLeft = pram.ParameterName.ToString().Replace("@", "");
                            pram.ParameterName = pram.ParameterName + "_Where";//避免和修改的参数冲突 加_Where
                            //格式如：AND id=@id__Where
                            sqlWhere.Append(" AND " + ParameterNameLeft + "=" + pram.ParameterName);
                            cmd.Parameters.Add(pram);
                        }
                    }
                    if (string.IsNullOrEmpty(sqlWhere.ToString().Trim()))
                    {
                        return 0;
                    }
                    else
                    {
                        sqlWhere.Insert(0, " 1=1 ");
                    }
                    //Sql语句
                    string sqlText = " DELETE " + Table + " WHERE " + sqlWhere;

                    //初始连接Conn
                    cmd.Connection = MyConn;
                    //初始Sql语句
                    cmd.CommandText = sqlText;
                    obj = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return DBUtil.ConvertToInt32(obj.ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 查询

        #region 获取一条数据
        /// <summary>
        /// 获取一条数据的DataTable
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="Table"></param>
        /// <param name="SelectIF"></param>
        /// <param name="pramsWhere"></param>
        /// <returns></returns>
        public static DataTable GetDataTable1(string connString, string Table, string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            try
            {
                if (SelectIF.ToLower().IndexOf("top") < 0)
                {
                    SelectIF = " top 1 " + SelectIF;
                }
                DataTable dataTable = GetDataTable2(connString, Table, SelectIF, pramsWhere, OrderName);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 取得集合
        /// <summary>
        /// 返回Object(多条记录数据源) 
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="SelectIF">条件：如ID,Title 以逗号分隔</param>
        /// <param name="pramsWhere">SqlParameter参数传值</param>
        /// <returns>返回Object</returns>
        public static DataTable GetDataTable2(string connString, string Table, string SelectIF, SqlParameter[] pramsWhere, string OrderName = "")
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    SqlCommand cmd = new SqlCommand();
                    MyConn.Open();
                    StringBuilder sqlWhere = new StringBuilder();
                    if (pramsWhere != null)
                    {
                        foreach (SqlParameter pram in pramsWhere)
                        {
                            if (pram.Value != null)
                            {
                                sqlWhere.Append(" AND " + pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName);
                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                    }
                    string sqlText = " SELECT " + SelectIF + " FROM  " + Table + " WHERE 1=1 " + sqlWhere.ToString();
                    if (!string.IsNullOrEmpty(OrderName))
                    {
                        sqlText += " ORDER BY " + OrderName;
                    }
                    cmd.Connection = MyConn;
                    cmd.CommandText = sqlText;
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable("yes");
                    MyAdapter.Fill(dataTable);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region 分页获取数据
        public static DataTable GetDataTablePage(string connString, string Table, string SelectIF, int PageIndex, int PageSize, SqlParameter[] pramsWhere, string OrderName = "ID")
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    SqlCommand cmd = new SqlCommand();
                    MyConn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT " + SelectIF + " FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    if (!string.IsNullOrEmpty(OrderName.Trim()))
                    {
                        strSql.Append("order by T." + OrderName);
                    }
                    strSql.Append(")AS Row, T.*  from " + Table + " T ");
                    StringBuilder sqlWhere = new StringBuilder();
                    if (pramsWhere != null)
                    {
                        foreach (SqlParameter pram in pramsWhere)
                        {
                            if (pram.Value != null)
                            {
                                sqlWhere.Append(" AND " + pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName);
                                //添加到 cmd 中参数
                                cmd.Parameters.Add(pram);
                            }
                        }
                    }
                    strSql.Append(" WHERE 1=1 " + sqlWhere.ToString());

                    strSql.Append(" ) TT");
                    strSql.Append(" WHERE TT.Row between " + PageIndex + " and " + (PageIndex * PageSize));
                    cmd.Connection = MyConn;
                    cmd.CommandText = strSql.ToString();
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable("yes");
                    MyAdapter.Fill(dataTable);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetDataTablePage(string connString, string Table, string SelectIF, int PageIndex, int PageSize, string strWhere, string OrderName = "ID")
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    SqlCommand cmd = new SqlCommand();
                    MyConn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT " + SelectIF + " FROM ( ");
                    strSql.Append(" SELECT ROW_NUMBER() OVER (");
                    if (!string.IsNullOrEmpty(OrderName.Trim()))
                    {
                        strSql.Append("order by T." + OrderName);
                    }
                    strSql.Append(")AS Row, T.*  from " + Table + " T ");
                    strSql.Append(" WHERE " + strWhere.ToString());
                    strSql.Append(" ) TT");
                    strSql.Append(" WHERE TT.Row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize));
                    cmd.Connection = MyConn;
                    cmd.CommandText = strSql.ToString();
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable("yes");
                    MyAdapter.Fill(dataTable);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region 获取结果的第一行的第一列

        public static object GetSingle(string connString, string Table, string SelectIF, SqlParameter[] pramsWhere)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        MyConn.Open();
                        StringBuilder sqlWhere = new StringBuilder();
                        if (pramsWhere != null)
                        {
                            foreach (SqlParameter pram in pramsWhere)
                            {
                                if (pram.Value != null)
                                {
                                    sqlWhere.Append(" AND " + pram.ParameterName.Replace("@", "") + "=" + pram.ParameterName);
                                    //添加到 cmd 中参数
                                    cmd.Parameters.Add(pram);
                                }
                            }
                        }
                        string sqlText = " SELECT " + SelectIF + " FROM  " + Table + " WHERE 1=1 " + sqlWhere.ToString();
                        cmd.Connection = MyConn;
                        cmd.CommandText = sqlText;
                        Object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static object GetSingle(string connString, string Table, string SelectIF, string sqlWhere)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        MyConn.Open();
                        string sqlText = " SELECT " + SelectIF + " FROM  " + Table + " WHERE " + sqlWhere;
                        cmd.Connection = MyConn;
                        cmd.CommandText = sqlText;
                        Object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        MyConn.Close();
                        MyConn.Dispose();
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        public static DataSet GetDataSet(string connString, string sqlText)
        {
            try
            {
                using (SqlConnection MyConn = new SqlConnection(connString))
                {
                    Object obj = new object();
                    SqlCommand cmd = new SqlCommand();
                    MyConn.Open();
                    cmd.Connection = MyConn;
                    cmd.CommandText = sqlText;
                    SqlDataAdapter MyAdapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    MyAdapter.Fill(ds);
                    MyAdapter.Dispose();
                    cmd.Parameters.Clear();
                    MyConn.Close();
                    MyConn.Dispose();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string connString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string connString, string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion

    }
}
