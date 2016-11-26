using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCommon
{
    internal class DBUtil
    {
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
    }
}
