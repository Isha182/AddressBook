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

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();
            if (Request.QueryString["CityId"] != null)
            {
                btnSave.Text = "Update";
                lblMessage.Text += "Edit mode | CityID = " + Request.QueryString["CityId"];
                FillControls(Convert.ToInt32(Request.QueryString["CityId"]));
            }

            else
                lblMessage.Text += "Add Mode";
        }
    }
    #endregion Load Event

    #region Fill Drop Down
    private void FillDropDownList()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if(objConn.State==ConnectionState.Closed)
                objConn.Open();

            #region Set the Connection and Command Objects
            SqlCommand objComm = objConn.CreateCommand();
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "PR_State_SelectForDropDownList";
            #endregion Set the Connection and Command Objects

            SqlDataReader objSDR = objComm.ExecuteReader();

            #region Read the Values
            if (objSDR.HasRows == true)
            {
                ddlStateID.DataSource = objSDR;
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                ddlStateID.DataBind();
            }
            #endregion Read the Values

            ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if(objConn.State==ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Fill Drop Down

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        #region Local Variables
        SqlInt32 strStateID = SqlInt32.Null;
        SqlString strCityName = SqlString.Null;
        SqlString strSTDCode = SqlString.Null;
        SqlString strPinCode = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            // Server Side Validation
            String strError = "";

            if (ddlStateID.SelectedIndex == 0)
            {
                strError += " - Selected State - <br />";
            }

            if (txtCityName.Text.Trim() == "")
            {
                strError += " - Enter City Name - <br />";
            }

            if (txtSTDCode.Text.Trim() == "")
            {
                strError += " - Enter STD Code - <br />";
            }

            if (txtPinCode.Text.Trim() == "")
            {
                strError += " - Enter Pin Code - <br />";
            }

            if (strError.Trim() != "")
            {
                lblMessage.Text = strError;
                return;
            }
            #endregion Server Side Validation

            #region Gather the Information
            // Gather the Information
            if (ddlStateID.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
            }

            if (txtCityName.Text.Trim() != "")
            {
                strCityName = txtCityName.Text.Trim();
            }

            if (txtSTDCode.Text.Trim() != "")
            {
                strSTDCode = txtSTDCode.Text.Trim();
            }


            if (txtPinCode.Text.Trim() != "")
            {
                strPinCode = txtPinCode.Text.Trim();
            }
            #endregion Gather the Information

            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }

            #region Set the Connection and Command Objects
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            #endregion Set the Connection and Command Objects

            #region Add Parameters
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityName", strCityName);
            objCmd.Parameters.AddWithValue("@STDCode", strPinCode);
            objCmd.Parameters.AddWithValue("@PinCode", strPinCode);
            #endregion Add Parameters

            if (Request.QueryString["CityID"] != null)
            {
                #region Update Record
                objCmd.Parameters.AddWithValue("@CityID", Request.QueryString["CityID"]);
                objCmd.CommandText = "PR_City_UpdateByPk";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/CityList.aspx");
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "PR_City_Insert";
                objCmd.ExecuteNonQuery();
                txtCityName.Text = "";
                txtPinCode.Text = "";
                txtSTDCode.Text = "";
                ddlStateID.SelectedIndex = 0;
                ddlStateID.Focus();
                lblMessage.Text = "Data Inserted Successfully";
            }
            #endregion Insert Record
            objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                if(objConn.State==ConnectionState.Open)
                    objConn.Close();
            }
        }

        
    }
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("~/AdminPanel/City/CityList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill Controls
    protected void FillControls(SqlInt32 CityID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Set Connection and Command Object
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectByPK";
            #endregion Set Connection and Command Object

            objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();
            
            #region Read the Values and Set Controls
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                   
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    break;
                }
            }
            else
                lblMessage.Text = "No data available";
            #endregion Read the Values and Set Controls
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