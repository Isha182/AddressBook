using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_ContactList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGridView();
        }
    }
    #endregion Load Event

    #region Fill GridView
    private void FillGridView()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if(objConn.State==ConnectionState.Closed)
                objConn.Open();

            #region Set Connection and Command Objects
            SqlCommand objComm = new SqlCommand();
            objComm.Connection = objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "PR_Contact_SelectAll";
            #endregion Set Connection and Command Objects

            SqlDataReader objSDR = objComm.ExecuteReader();
            gvContact.DataSource = objSDR;
            gvContact.DataBind();
            objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
        }
    }
    #endregion Fill GridView

    #region RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region  Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteRecord(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion  Delete Record
    }
    #endregion RowCommand

    #region Delete Record
    private void DeleteRecord(SqlInt32 ContactID)
    {
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        try
        {
            if(objCon.State == ConnectionState.Closed)
                objCon.Open();

            #region Set Connection And Command Object
            SqlCommand objCom = objCon.CreateCommand();
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.CommandText = "PR_Contact_DeleteByPK";
            #endregion Set Connection And Command Object

            objCom.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            objCom.ExecuteNonQuery();

            objCon.Close();
            FillGridView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objCon.Close();
        }
    }
    #endregion Delete Record
}