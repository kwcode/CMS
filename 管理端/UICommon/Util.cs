using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UICommon
{
    public class Util
    {
        public static int ConvertToInt32(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    int Num = 0;
                    Int32.TryParse(o.ToString(), out Num);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public static decimal ConvertToDecimal(object o)
        {
            try
            {
                if (o != DBNull.Value && o != null && o.ToString() != String.Empty)
                {
                    decimal Num = 0;
                    decimal.TryParse(o.ToString().Trim(), out Num);
                    return Num;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

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


        #region 防止后退
        /// <summary>
        /// 防止后退
        /// </summary>
        public static void No_Back()
        {
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";//IE
            System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Buffer = true;
        }
        #endregion

        private static int _seed = int.Parse(DateTime.Now.Ticks.ToString().Substring(10));
        private static int seed { get { return _seed; } set { _seed = value; } }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="RangeString">字符串范围，如："0123456789ABCDEF"</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetRandString(string RangeString, int len)
        {
            char[] _a = RangeString.ToCharArray();

            string VNum = "";
            seed++;
            Random rand = new Random(seed);
            for (int i = 0; i < len; i++)
            {
                VNum += _a[rand.Next(_a.Length - 1)];
            }
            return VNum;
        }

        /// <summary>
        /// 获取随机数字
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetRandNumber(int len)
        {
            return GetRandString("0123456789", len);
        }

    }
}
