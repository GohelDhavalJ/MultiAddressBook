using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_Project6
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //Check for selection of file
                //Name,File Size , type ,Extension
                //50 MB File Upload
                if (fuFile.HasFile)
                {
                    /*lblDisplay.Text = "File name: " +
                    fuFile.PostedFile.FileName.ToString() + "<br> File Size:" +
                    fuFile.PostedFile.ContentLength.ToString() + " kb<br>" +
                    "Content type: " +
                    fuFile.PostedFile.ContentType.ToString();*/

                    String FolderPath = "~/UserContent/";
                    String AbsolutePath = Server.MapPath(FolderPath);

                    lblDisplay.Text = "File Will be Uploaded  at the location = " + AbsolutePath;

                    if(!Directory.Exists(AbsolutePath))
                    {
                        Directory.CreateDirectory(AbsolutePath);
                    }

                    fuFile.SaveAs(AbsolutePath + fuFile.PostedFile.FileName.ToString());
                }
                else
                {
                    lblDisplay.Text = "File not Selected";
                }
            }
            catch (Exception ex)
            {
                lblDisplay.Text = ex.Message;
            }
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            String FilePath = "~/UserContent/sinnnnn.jpg";

            FileInfo file = new FileInfo(Server.MapPath(FilePath));

            if(file.Exists)
            {
                file.Delete();
            }
            else
            {
                lblDisplay.Text = "File not avalibale";
            }
        }
    }
}