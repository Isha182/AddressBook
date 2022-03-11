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

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ContactCategoryId"] != null)
            {
                btnSave.Text = "Update";
                lblMessage.Text += "Edit mode | ContactCategoryID = " + Request.QueryString["ContactCategoryId"];
                FillControls(Convert.ToInt32(Request.QueryString["ContactCategoryId"]));
            }

            else
                lblMessage.Text += "Add Mode";
        }
        
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        #region Local Variables
        SqlString strContactCategoryName = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            // Validation
            String strError = "";

            if (txtContactCatName.Text.Trim() == "")
            {
                strError += "Enter the Name of Contact Category";
            }

            if (strError != "")
            {
                lblMessage.Text = strError;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (txtContactCatName.Text.Trim() != "")
            {
                strContactCategoryName = txtContactCatName.Text.Trim();
            }

            if (objCon.State != ConnectionState.Open)
            {
                objCon.Open();
            }
            #endregion Gather Information

            #region Set Connection and Command Objects
            SqlCommand objCom = objCon.CreateCommand();
            objCom.CommandType = CommandType.StoredProcedure;
            #endregion Set Connection and Command Objects

            objCom.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);

            if (Request.QueryString["ContactCategoryID"] != null)
            {
                #region Update Record
                objCom.Parameters.AddWithValue("@ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
                objCom.CommandText = "PR_ContactCategory_UpdateByPK";
                objCom.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCom.CommandText = "PR_ContactCategory_Insert";
                objCom.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully";
                txtContactCatName.Text = "";
                txtContactCatName.Focus();
                #endregion Insert Record
            }
            objCon.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text += ex.Message;
        }
        finally
        {
            if (objCon.State != ConnectionState.Closed)
            {
                objCon.Close();
            }
        }

    }
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill Controls
    protected void FillControls(SqlInt32 ContactCategoryID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Set the Connection and Command Objects
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_SelectByPK";
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
            #endregion Set the Connection and Command Objects

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #region Read The Values and Set the Controls
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    //if (objSDR["StateName"].Equals(DBNull.Value) != true)
                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                    {
                        txtContactCatName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                }
            }
            #endregion Read The Values and Set the Controls
            else
                lblMessage.Text = "No data available";
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text += ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Fill Controls

}