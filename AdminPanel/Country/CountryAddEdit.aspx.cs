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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CountryId"] != null)
            {
                btnSave.Text = "Update";
                lblMessage.Text += "Edit mode | CountryID = " + Request.QueryString["CountryId"];
                FillControls(Convert.ToInt32(Request.QueryString["CountryId"]));
            }

            else
                lblMessage.Text += "Add Mode";
        }  
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Establishing Connection
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        #region Local Variables
        // Declaring Variables to Insert Data
        SqlString strCountryCode = SqlString.Null;
        SqlString strCountryName = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validations
            // Validation
            String strError = "";

            if (txtCountryCode.Text.Trim() == "" && txtCountryName.Text.Trim() == "")
            {
                strError += "Enter the Name and Code of Country";
            }

            if (strError != "")
            {
                lblMessage.Text = strError;
                return;
            }
            #endregion Server Side Validations

            #region Gather Information

            if (txtCountryCode != null)
            {
                strCountryCode = txtCountryCode.Text.Trim();
            }

            if (txtCountryName != null)
            {
                strCountryName = txtCountryName.Text.Trim();
            }

            if (objCon.State != ConnectionState.Open)
            {
                objCon.Open();
            }
            #endregion Gather Information

            #region Command Object
            // Prepare the Command
            SqlCommand objCmd = objCon.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            #endregion Command Object

            #region Passing Parameters
            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);
            objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
            #endregion Passing Parameters

            if (Request.QueryString["CountryID"] != null)
            {
                #region Update record
                objCmd.Parameters.AddWithValue("@CountryID", Request.QueryString["CountryID"].ToString().Trim());
                // Pass the Parameters
                objCmd.CommandText = "PR_Country_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/CountryList.aspx");
                #endregion Update record
            }
            else
            {
                #region Insert record
                // Pass the Parameters
                objCmd.CommandText = "PR_Country_Insert";
                objCmd.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully";
                txtCountryName.Text = "";
                txtCountryCode.Text = "";
                txtCountryName.Focus();
                #endregion Insert record
            }
            objCon.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text += ex.Message;
        }
        finally
        {
            if (objCon.State == ConnectionState.Open)
            {
                if(objCon.State==ConnectionState.Open)
                    objCon.Close();
            }
        }
    }
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Country/CountryList.aspx", true);
    }
    #endregion Button : Cancel

    #region FillControl
    protected void FillControls(SqlInt32 CountryID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Set Connection and Command Objects
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            #endregion Set Connection and Command Objects

            objCmd.CommandText = "PR_Country_SelectByPK";
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #region Read the value and Set the Controls
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    //if (objSDR["StateName"].Equals(DBNull.Value) != true)
                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
                lblMessage.Text = "No data available";

            #endregion Read the value and Set the Controls

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
    #endregion FillControl
}