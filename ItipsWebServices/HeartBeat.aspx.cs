using System;

namespace ItipsWebServices
{
    public partial class HeartBeat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
        }
    }
}