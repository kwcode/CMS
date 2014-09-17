using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_PM_PagePermiTallot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/");
        List<string> str = Directory.GetFiles(path + "admin/", "*.aspx", SearchOption.AllDirectories).ToList();
        for (int i = 0; i < str.Count; i++)
        {
            str[i] = str[i].Substring(str[i].LastIndexOf("admin/"), str[i].Length - str[i].LastIndexOf("admin/")).Replace("\\", "/");
        }
        Repeater1.DataSource = str;
        Repeater1.DataBind();
    }
}