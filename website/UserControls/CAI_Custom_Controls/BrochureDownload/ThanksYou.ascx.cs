using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.BrochureDownload
{
    public partial class ThanksYou : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void DownloadButton_Click(Object sender, EventArgs e)
        {

            try
            {
                string filename = Server.MapPath("~/App_Data/Brochure.pdf");

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
                if (fileInfo.Exists)
                {

                    {
                        //context.Response.ContentType = "application/octet-stream";
                        //context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(file));
                        //context.Response.WriteFile(context.Server.MapPath("~/ App_Data/" + file));
                        //context.Response.WriteFile(context.Server.MapPath(file));

                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Brochure.pdf");
                        // Response.TransmitFile(Server.MapPath("~/App_Data/pdf1.pdf"));
                        Response.TransmitFile(filename);
                        // Response.End();
                    }



                }
                else
                {
                    throw new Exception("File not found");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }
            finally
            {
                Response.End();
            }

        }
    }
}
