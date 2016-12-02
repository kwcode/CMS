using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Web;


/// <summary>
/// 网站Http请求
/// </summary>
public class HTTPS
{
    /// <summary>
    /// 获取网站上一页地址
    /// </summary>
    public static string ComeUrl
    {
        get { return System.Web.HttpContext.Current.Request.UrlReferrer.ToString(); }
    }

    /// <summary>
    /// 获取网站绝对地址
    /// </summary>
    public static string SiteUrl
    {
        get
        {
            if (System.Web.HttpContext.Current.Request.Url.Port == 80)
            {
                return "http://" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath;
            }
            return "http://" + System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + System.Web.HttpContext.Current.Request.ApplicationPath;
        }
    }

    /// <summary>
    /// 获取站点物理路径
    /// </summary>
    public static string PhyPath
    {
        get { return HttpContext.Current.Request.PhysicalApplicationPath; }
    }

    /// <summary>
    /// 获取应用程序的虚拟根路径
    /// </summary>
    public static string VisPath
    {
        get { return HttpContext.Current.Request.ApplicationPath; }
    }

    public static string GetPathNoParams
    {
        get { return HttpContext.Current.Request.RawUrl; }
    }

    /// <summary>
    /// 穿过代理服务器取远程用户真实IP地址
    /// </summary>
    public static string GetIP
    {
        get
        {
            try
            {

                //return IPAddress2;
                //if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                //    return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
                //else
                //    return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; 
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    {
                        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Length > 0)
                        {
                            return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "127.0.0.1";
                    }
                }
                else
                    return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //return TripRequest.GetIP();
                //string user_IP = "";
                //if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                //{
                //    user_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                //}
                //else
                //{
                //    user_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                //}
                //return user_IP;
            }
            catch
            {
                return "127.0.0.1";
            }
        }

    }

    private string GetClientIP()
    {
        string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        return result;
    }


    /**/
    /// <summary> 
    /// 取得客户端真实IP。如果有代理则取第一个非内网地址 
    /// </summary> 
    public static string IPAddress2
    {
        get
        {
            string result = String.Empty;

            result = GetIP;// HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理 
                if (result.IndexOf(".") == -1)     //没有“.”肯定是非IPv4格式 
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];     //找到不是内网的地址 
                            }
                        }
                    }
                    else if (IsIPAddress(result)) //代理即是IP格式 
                        return result;
                    else
                        result = null;     //代理中的内容 非IP，取IP 
                }

            }

            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];



            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }
    }
    //// <summary>
    /// 判断是否是IP地址格式 0.0.0.0
    /// </summary>
    /// <param name="str1">待判断的IP地址</param>
    /// <returns>true or false</returns>
    public static bool IsIPAddress(string str1)
    {
        if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

        string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

        Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
    }



    /// <summary>
    /// 获取IP地址
    /// </summary>
    public static string IPAddress
    {
        get
        {
            string userIP;
            // HttpRequest Request = HttpContext.Current.Request;
            HttpRequest Request = HttpContext.Current.Request; // ForumContext.Current.Context.Request;
            // 如果使用代理，获取真实IP
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                userIP = Request.ServerVariables["REMOTE_ADDR"];
            else
                userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (userIP == null || userIP == "")
                userIP = Request.UserHostAddress;
            return userIP;
        }
    }


    public static string GetIPAddress2(HttpContext context = null)
    {
        if (context == null)
        {
            context = HttpContext.Current;
        }

        string result = String.Empty;

        result = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (result != null && result != String.Empty)
        {
            //可能有代理 
            if (result.IndexOf(".") == -1)     //没有“.”肯定是非IPv4格式 
                result = null;
            else
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];     //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;     //代理中的内容 非IP，取IP 
            }

        }

        string IpAddress = (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : context.Request.ServerVariables["REMOTE_ADDR"];



        if (null == result || result == String.Empty)
            result = context.Request.ServerVariables["REMOTE_ADDR"];

        if (result == null || result == String.Empty)
            result = context.Request.UserHostAddress;

        return result;

    }

    public static bool IsLocalIP(HttpContext context = null)
    {
        if (context == null)
        {
            context = HttpContext.Current;
        }
        HttpRequest _request = context.Request;
        string ip = _request.UserHostAddress;
        return ip.Equals("127.0.0.1") || ip.StartsWith("192.168.") || ip.StartsWith("::1");
    }

    /// <summary>
    /// 获得用户IP
    /// </summary>
    public static string GetUserIP()
    {
        string ip;
        string[] temp;
        bool isErr = false;
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        else
            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
        if (ip.Length > 15)
            isErr = true;
        else
        {
            temp = ip.Split('.');
            if (temp.Length == 4)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Length > 3) isErr = true;
                }
            }
            else
                isErr = true;
        }

        if (isErr)
            return "1.1.1.1";
        else
            return ip;
    }

    /// <summary>
    /// 格式化IP
    /// </summary>
    public string FormatIP(string ipStr)
    {
        string[] temp = ipStr.Split('.');
        string format = "";
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].Length < 3) temp[i] = Convert.ToString("000" + temp[i]).Substring(Convert.ToString("000" + temp[i]).Length - 3, 3);
            format += temp[i].ToString();
        }
        return format;
    }
    /// <summary>
    /// 当前地址
    /// </summary>
    public static string GetCurrentUrl()
    {
        string strUrl;
        strUrl = HttpContext.Current.Request.ServerVariables["Url"];
        if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
            return strUrl;
        else
        {
            return strUrl + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
        }

    }

    /// <summary>
    /// 获得当前程序名
    /// </summary>
    public string GetScriptName()
    {
        return HttpContext.Current.Request.ServerVariables["Script_Name"];
    }
    public static string GetRootURI()
    {
        string AppPath = "";
        HttpContext HttpCurrent = HttpContext.Current;
        HttpRequest Req;
        if (HttpCurrent != null)
        {
            Req = HttpCurrent.Request;

            string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
            if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                //直接安装在   Web   站点   
                AppPath = UrlAuthority;
            else
                //安装在虚拟子目录下   
                AppPath = UrlAuthority + Req.ApplicationPath;
        }
        if (AppPath.IndexOf("/", AppPath.Length) < 0)
        {
            AppPath = AppPath + "/";
        }
        return AppPath;
    }

    public static string GetRootPath()
    {
        string AppPath = "";
        HttpContext HttpCurrent = HttpContext.Current;
        HttpRequest Req;
        if (HttpCurrent != null)
        {
            Req = HttpCurrent.Request;

            string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
            if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                //直接安装在   Web   站点   
                AppPath = "/";
            else
                //安装在虚拟子目录下   
                AppPath = Req.ApplicationPath;
        }
        if (AppPath.Length > 1)
        {
            if (AppPath.IndexOf("/", AppPath.Length) < 0)
            {
                AppPath = AppPath + "/";
            }
        }
        return AppPath;
    }
    public static HttpContext Current
    {
        get { return HttpContext.Current; }
    }

    public static HttpRequest Request
    {
        get { return Current.Request; }
    }

    public static HttpResponse Response
    {
        get { return Current.Response; }
    }

}
