using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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
        public static bool IsNull(object o)
        {
            try
            {
                string str = ConvertToString(o);
                return string.IsNullOrEmpty(str);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region 获取当前登录用户

        public static int GetUserID()
        {
            int UserID = 0;
            try
            {
                Model.UserInfoEntity userInfo = System.Web.HttpContext.Current.Session["UserInfo"] as Model.UserInfoEntity;
                UserID = userInfo.ID;
            }
            catch { }
            return UserID;

        }
        #endregion

        #region 按字节截取字符串
        /// <summary>
        /// 按字节截取字符串
        /// </summary>
        /// <param name="strInput">字符串</param>
        /// <param name="intLen">长度</param>
        /// <returns>字符串</returns>
        public static string SubString(object obj, int length)
        {
            string Input = ConvertToString(obj);
            try
            {
                if (string.IsNullOrEmpty(Input))
                {
                    return "";
                }

                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(Input);
                int n = 0;  //  表示当前的字节数
                int i = 0;  //  要截取的字节数
                for (; i < bytes.GetLength(0) && n < length; i++)
                {
                    //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                    if (i % 2 == 0)
                    {
                        n++;      //  在UCS2第一个字节时n加1
                    }
                    else
                    {
                        //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                        if (bytes[i] > 0)
                        {
                            n++;
                        }
                    }
                }
                //  如果i为奇数时，处理成偶数
                if (i % 2 == 1)
                {
                    //  该UCS2字符是汉字时，去掉这个截一半的汉字
                    if (bytes[i] > 0)
                        i = i - 1;
                    //  该UCS2字符是字母或数字，则保留该字符
                    else
                        i = i + 1;
                }
                return System.Text.Encoding.Unicode.GetString(bytes, 0, i);
            }
            catch (Exception ex)
            {
                return Input;
            }

        }
        #endregion

        #region 清除HTML函数
        /// <summary>
        /// 清除HTML函数 
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string ChearHTML(string Htmlstring)
        {
            if (!string.IsNullOrEmpty(Htmlstring))
            {
                //删除脚本 
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //删除HTML 
                Htmlstring = Htmlstring.Replace("&mdash;", "-");
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Htmlstring = Htmlstring.Replace("<", "");
                Htmlstring = Htmlstring.Replace(">", "");
                Htmlstring = Htmlstring.Replace("\r\n", "");
                Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
                Htmlstring = Htmlstring.Replace("&amp;ldquo;", "“");
                Htmlstring = Htmlstring.Replace("&amp;rdquo;", "”");
            }
            return Htmlstring;
        }
        #endregion
    }
}

