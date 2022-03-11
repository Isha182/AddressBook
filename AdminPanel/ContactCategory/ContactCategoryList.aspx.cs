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

public partial class AdminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
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
            objComm.CommandText = "PR_ContactCategory_SelectAll";
            #endregion Set Connection and Command Objects

            SqlDataReader objSDR = objComm.ExecuteReader();
            gvContactCategory.DataSource = objSDR;
            gvContactCategory.DataBind();
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
    protected void gvContactCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteRecord(Convert.ToInt32((e.CommandArgument.ToString().Trim())));
            }
        }
        #endregion Delete Record
    }
    #endregion RowCommand

    #region Delete Record
    private void DeleteRecord(SqlInt32 ContactCategoryID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if(objConn.State==ConnectionState.Closed)
                objConn.Open();

            #region Set Connection and Command Objects
            SqlCommand objComm = objConn.CreateCommand();
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "PR_ContactCategory_DeleteByPK";
            #endregion Set Connection and Command Objects

            objComm.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());

            objComm.ExecuteNonQuery();

            objConn.Close();
            FillGridView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objConn.Close();
        }
    }
    #endregion Delete Record
}