using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using lib;

public partial class change_config : System.Web.UI.Page
{
    protected Config configManager;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        configManager = new Config(Server.MapPath(VirtualPathUtility.GetDirectory(Request.Path)));

        if (configManager.getConfig("admin.password") == null)
        {
            Response.Redirect("setup.aspx");
            Response.End();
        }

        if (Request["SAVE_CONFIG"] != null)
        {
            configManager.setConfig("path.pdf", Request["PDF_Directory"]);
            configManager.setConfig("path.swf", Request["SWF_Directory"]);
            configManager.setConfig("licensekey", Request["LICENSEKEY"]);
            configManager.setConfig("splitmode", Request["SPLITMODE"]);
            configManager.setConfig("renderingorder.primary", Request["RenderingOrder_PRIM"]);
            configManager.setConfig("renderingorder.secondary", Request["RenderingOrder_SEC"]);

            configManager.SaveConfig(Server.MapPath(VirtualPathUtility.GetDirectory(Request.Path)));

            // wipe old converted files
            if (System.IO.Directory.Exists(configManager.getConfig("path.swf")))
            {
                foreach (string filename in System.IO.Directory.GetFiles(configManager.getConfig("path.swf"))) 
                {
                    System.IO.File.Delete(filename);
                }
            }

            Response.Redirect("Default.aspx?msg=Configuration%20saved!");
        }

        if (Session["FLEXPAPER_AUTH"] == null)
        {
            Response.Redirect("Default.aspx");
            Response.End();
        }
    }
}