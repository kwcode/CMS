using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserControl uc_top = (UserControl)this.Page.LoadControl("~/communal/uc_top.ascx");
        UserControl uc_home = (UserControl)this.Page.LoadControl("~/communal/uc_home.ascx");
        UserControl uc_header = (UserControl)this.Page.LoadControl("~/communal/uc_header.ascx");
        UserControl uc_footer = (UserControl)this.Page.LoadControl("~/communal/uc_footer.ascx");
        this.Controls.Add(uc_top);
        this.Controls.Add(uc_home);
        this.Controls.Add(uc_header);
        this.Controls.Add(uc_footer);
    }
}