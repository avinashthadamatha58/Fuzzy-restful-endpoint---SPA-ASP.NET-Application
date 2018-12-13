using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        RestClient rClient = new RestClient();
        public string spr { get; set; }
        DataTable dt1 = new DataTable();

        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = false;", true);
                DataTableCreate();
            }

          else
            {
                for (int i = 0; i < rptCustomers.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)rptCustomers.Items[i].FindControl("CheckBox1");
                    cb.Enabled = true;
                }
            }
        }
        #region loading list<T> events to ASP.NET DataTable and Binding it to Repeater
        private void DataTableCreate()
        {
           
            rClient.EndPoint = "http://dev.dragonflyathletics.com:1337/api/dfkey/events";
            List<RootObject> test1 = rClient.makeRequest();
            //check error
            if (test1 == null)
            {
                String msg = "alert('Could not make request to the server, Click OK to reload')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", msg + ";window.location='WebForm1.aspx';", true);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Id"), new DataColumn("EName"), new DataColumn("Description"), new DataColumn("Location"), new DataColumn("EventID"), new DataColumn("ImageID"), new DataColumn("Name"), new DataColumn("Comments"), new DataColumn("Date") });
                int count = 0;

                foreach (RootObject ro in test1)
                {
                    DateTime datetime = ro.Date;
                    string location = "Name: " + ro.Location.Name + "\n" + "Address: " +"\r\n" + ro.Location.Address + "City: " + "\r\n" + ro.Location.City + "State: " + ro.Location.State;
                    location = "@" + location;
                    string comments = string.Empty;
                    foreach (Comment com in ro.Comments)
                    {
                        comments += "<b>" +com.From + " : </b>" + com.Text + "\r\n\r\n\r\n<br />" + "<br/ >" + "<br />";

                    }
                    comments = "@"+comments;
                 
                    if ((ro.Images != null) && (ro.Images.Any()))
                    {
                        dt.Rows.Add(count, ro.Name, ro.Description, location, ro.Id, ro.Images[0].Id, ro.Name, comments, datetime);

                    }
                    else
                    {
                        dt.Rows.Add(count, ro.Name, ro.Description, location, ro.Id, "", ro.Name, comments);
                    }
                    count++;
                }
                dt1 = dt;
                rptCustomers.DataSource = dt;
                rptCustomers.DataBind();
            }
        }
        #endregion

    }
}