using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_Project6.Admin_Panel
{
    public partial class EncryptionDescrytion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodeData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodeData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }

        protected void btnEncryption_Click(object sender, EventArgs e)
        {
            lblEncryption.Text = Base64Encode(txtEncryption.Text.Trim());
        }

        protected void btnDecryption_Click(object sender, EventArgs e)
        {
            lblDecryption.Text = Base64Decode(txtDecryption.Text.Trim());
        }
    }
}