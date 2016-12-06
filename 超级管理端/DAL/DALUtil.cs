using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL
{
    public class DALUtil
    {
        #region 获取数据库连接字符串
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public static string ConnString
        {
            get
            {
                string connString = ConfigurationManager.AppSettings["ConnString1"];
                return connString;
            }
        }

        #endregion

        #region  实体转换
        /// <summary>
        /// DataTable 转换成实体 List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToEntityList<T>(DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name.ToLower() == dt.Columns[i].ColumnName.ToLower());
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                info.SetValue(s, item[i], null);
                            }
                        }
                        catch { }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        public static T ConvertDataTableToEntity<T>(DataTable dt)
        {

            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            T s = System.Activator.CreateInstance<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow item = dt.Rows[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name.ToLower() == dt.Columns[i].ColumnName.ToLower());
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                info.SetValue(s, item[i], null);
                            }
                        }
                        catch { }
                    }
                }
            }
            return s;

        }
        #endregion

        #region 构造 param
        /// <summary>
        /// 构造 param.
        /// </summary>
        /// <param name="ParamName">参数名称.</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数长</param>
        /// <param name="Value">参数值</param>
        /// <returns>New parameter.</returns>
        public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            SqlParameter param;
            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);
            if (Value != null)
                param.Value = Value;
            return param;
        }
        #endregion

        #region  GetParams

        public static SqlParameter[] GetParams<T>(T t)
        {
            List<SqlParameter> lstParams = new List<SqlParameter>();
            Type type = typeof(T);
            List<PropertyInfo> plist = new List<PropertyInfo>(typeof(T).GetProperties());
            if (plist != null && plist.Count > 0)
            {
                foreach (PropertyInfo item in plist)
                {
                    if (item.Name.ToLower() != "id")
                    {
                        string dataType = item.PropertyType.Name.ToLower();
                        SqlDbType _dbType = SqlDbType.NVarChar;
                        switch (dataType)
                        {
                            case "string":
                                _dbType = SqlDbType.NVarChar;
                                break;
                            case "int32":
                                _dbType = SqlDbType.Int;
                                break;
                            case "datetime":
                                _dbType = SqlDbType.DateTime;
                                break;
                            case "decimal":
                                _dbType = SqlDbType.Decimal;
                                break;
                        }
                        object obj = item.GetValue(t, null);
                        lstParams.Add(MakeInParam("@" + item.Name, _dbType, 0, obj));
                    }
                }
            }
            return lstParams.ToArray();
        }

        public static SqlParameter[] GetParams<T>(T t, int UserID)
        {
            List<SqlParameter> lstParams = new List<SqlParameter>();
            Type type = typeof(T);
            List<PropertyInfo> plist = new List<PropertyInfo>(typeof(T).GetProperties());
            if (plist != null && plist.Count > 0)
            {
                foreach (PropertyInfo item in plist)
                {
                    if (item.Name.ToLower() != "id")
                    {
                        if (item.Name.ToLower() == "userid")
                        {
                            lstParams.Add(MakeInParam("@UserID", SqlDbType.Int, 4, UserID));
                        }
                        else
                        {
                            string dataType = item.PropertyType.Name.ToLower();
                            SqlDbType _dbType = SqlDbType.NVarChar;
                            switch (dataType)
                            {
                                case "string":
                                    _dbType = SqlDbType.NVarChar;
                                    break;
                                case "int32":
                                    _dbType = SqlDbType.Int;
                                    break;
                                case "datetime":
                                    _dbType = SqlDbType.DateTime;
                                    break;
                                case "decimal":
                                    _dbType = SqlDbType.Decimal;
                                    break;
                            }
                            object obj = item.GetValue(t, null);
                            lstParams.Add(MakeInParam("@" + item.Name, _dbType, 0, obj));
                        }
                    }
                }
            }
            return lstParams.ToArray();
        }
        #endregion

        #region Object转换为Int32
        /// <summary>
        /// Object转换为Int32
        /// </summary>
        /// <param name="o">Object</param>
        /// <returns>int 报错也返回0</returns>
        public static int ConvertToInt32(object o)
        {
            try
            {
                int obj = 0;
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    int.TryParse(o.ToString(), out obj);
                }
                return obj;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        public static string ConvertToString(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        #region 转换为可以保持的sql字符串

        public static string ConverToSqlTxt(object o)
        {
            try
            {
                string str = ConvertToString(o);
                str = "'" + str.Replace("'", "''") + "'";
                return str;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
