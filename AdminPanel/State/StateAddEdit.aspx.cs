using System;
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

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();
            if (Request.QueryString["StateId"] != null)
            {
                btnSave.Text = "Update";
                lblMessage.Text += "Edit mode | StateID = " + Request.QueryString["StateId"];
                FillControls(Convert.ToInt32(Request.QueryString["StateId"]));
            }

            else
                lblMessage.Text += "Add Mode";
        }   
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        #region Local Variables
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlString strStateName = SqlString.Null;
        SqlString strStateCode = SqlString.Null;
        #endregion Local Variables

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Server Side validation
            // Server Side Validation
            String strError = "";

            if (ddlCountryID.SelectedIndex == 0)
            {
                strError += " - Selected Country - <br />";
            }

            if (txtStateName.Text.Trim() == "")
            {
                strError += " - Enter State Name - <br />";
            }

            if (txtStateCode.Text.Trim() == "")
            {
                strError += " - Enter State Code - <br />";
            }

            if (strError.Trim() != "")
            {
                lblMessage.Text += strError;
                return;
            }
            #endregion Server Side validation

            #region Gather Information
            // Gather the Information
            if (ddlCountryID.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
            }

            if (txtStateName.Text.Trim() != "")
            {
                strStateName = txtStateName.Text.Trim();
            }

            if (txtStateCode.Text.Trim() != "")
            {
                strStateCode = txtStateCode.Text.Trim();
            }
            #endregion Gather Information

            #region Set Connection And Command Object
            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;


            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateName", strStateName);
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);

            #endregion Set Connection And Command Object

           
            if (Request.QueryString["StateID"] != null)
            {
                #region Update Record
                // Edit Mode
                objCmd.Parameters.AddWithValue("@StateID", Request.QueryString["StateID"].ToString().Trim());
                objCmd.CommandText = "PR_State_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/State/StateList.aspx", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                // Add Mode
                objCmd.CommandText = "PR_State_Insert";
                objCmd.ExecuteNonQuery();
                txtStateName.Text = "";
                txtStateCode.Text = "";
                ddlCountryID.SelectedIndex = 0;
                ddlCountryID.Focus();
                lblMessage.Text = "Data Inserted Successfully";
                #endregion Insert Record
            }
            objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text += ex.Message;
        }
        finally
        {
            if(objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        
    }
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("~/AdminPanel/State/StateList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill DropDownList
    private void FillDropDownList()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            #region Connection and Command Object
            objConn.Open();
            SqlCommand objComm = objConn.CreateCommand();
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "PR_Country_SelectForDropDrownList";
            SqlDataReader objSDR = objComm.ExecuteReader();
            #endregion Connection and Command Object

            if (objSDR.HasRows == true)
            {
                ddlCountryID.DataSource = objSDR;
                ddlCountryID.DataValueField = "CountryID";
                ddlCountryID.DataTextField = "CountryName";
                ddlCountryID.DataBind();
            }

            ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));
            objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text += ex.Message;
        }
        finally
        {
            objConn.Close();
        }
    }
    #endregion Fill DropDownList

    #region Fill Controls
    protected void FillControls(SqlInt32 StateID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {

            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }

            #region Set Conection and Command Object

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectByPK";
            objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());
            #endregion Set Conection and Command Object

            #region Read the Values and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    //if (objSDR["StateName"].Equals(DBNull.Value) != true)
                    if (!objSDR["StateName"].Equals(DBNull.Value))
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();
                    }
                    if (!objSDR["StateCode"].Equals(DBNull.Value))
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
                    }
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    break;
                }
            }
            else
                lblMessage.Text = "No data available";
            #endregion Read the Values and Set the Controls

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