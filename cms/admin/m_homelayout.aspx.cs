using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_m_homelayout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { // p_comm_layoutlist_get
        DataTable dt = DataConnect.Data.ExecuteDataTable("p_Act_ActInfo", new object[] { 0 });
        t_page.DataSource = dt;
        t_page.DataBind();
    }
}