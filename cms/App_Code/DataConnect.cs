using DBCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

/// <summary>
/// DataConnect 的摘要说明
/// </summary>
public class DataConnect
{
    public static DataPool Data = new DataPool(10, ConfigurationManager.ConnectionStrings["ConnString"].ToString());
    public static DataPool Data2 = new DataPool(10, ConfigurationManager.AppSettings["ConnString2"]);

}