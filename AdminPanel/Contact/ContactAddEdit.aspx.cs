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

public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {        
                 FillCountryDropDownList();
                 FillContactCategoryDropDownList();

                if (Request.QueryString["ContactID"] != null)
                {
                    btnSave.Text = "Update";
                    lblMessage.Text += "Edit mode | ContactID = " + Request.QueryString["ContactID"];
                    FillControls(Convert.ToInt32(Request.QueryString["ContactID"]));
               
            }
                else
                    lblMessage.Text += "Add Mode";          
        }

    }
    #endregion Load Event

    #region  Fill ContactCategory DropDownList

    private void FillContactCategoryDropDownList()
    {
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {
            if (objCon.State == ConnectionState.Closed)
                objCon.Open();

            #region Set Connection and Command Objects

            SqlCommand objCom = objCon.CreateCommand();
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.CommandText = "PR_ContactCategory_SelectForDropDownList";
            #endregion Set Connection and Command Objects

            SqlDataReader objSDR3 = objCom.ExecuteReader();


            #region Read the Values and Set the Controls

            if (objSDR3.HasRows == true)
            {
                ddlContactCategory.DataSource = objSDR3;
                ddlContactCategory.DataValueField = "ContactCategoryID";
                ddlContactCategory.DataTextField = "ContactCategoryName";
                ddlContactCategory.DataBind();
            }
            objSDR3.Close();    
            #endregion Read the Values and Set the Controls

            objCon.Close();
            
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objCon.Close();
            ddlContactCategory.Items.Insert(0, new ListItem(" Select Contact Category", "-1"));
        }
        

    }
    #endregion  Fill ContactCategory DropDownList

    #region Fill Country DropDown List

    private void FillCountryDropDownList()
    {
        SqlConnection objconn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            #region Set Connection & Command Object

            if (objconn.State != ConnectionState.Open)
                objconn.Open();

            SqlCommand objcmd = objconn.CreateCommand();

            objcmd.CommandType = CommandType.StoredProcedure;

            #endregion Set Connection & Command Object

            objcmd.CommandText = "PR_Country_SelectForDropDrownList";

            SqlDataReader objSDR = objcmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                
                ddlCountry.DataSource = objSDR;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
            }

            ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

            if (objconn.State != ConnectionState.Closed)
                objconn.Close();

        }

        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        finally
        {
            if (objconn.State == ConnectionState.Open)
                objconn.Close();
        }
    }


    #endregion FillCountry DropDown List

    #region FillStateDropDown
    public void FillStateDropDown()
    {
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        SqlInt32 strCountryID = SqlInt32.Null;

        if (ddlCountry.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }

        objCon.Open();

        SqlCommand objCom = objCon.CreateCommand();
        objCom.CommandType = CommandType.StoredProcedure;

        objCom.CommandText = "PR_Contact_StateDDByCountry";
        objCom.Parameters.AddWithValue("@CountryID", strCountryID);

        SqlDataReader objSDR1 = objCom.ExecuteReader();        
        if (objSDR1.HasRows == true)
        {
            ddlState.DataSource = objSDR1;
            ddlState.DataValueField = "StateID";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
        }
        
        ddlState.Items.Insert(0, new ListItem("Select State ", "-1"));
        objCon.Close();
        
    }

    #endregion FillStateDropDown

    #region FillCityDropDown
    public void FillCityDropDown()
    {
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

       
        objCon.Open();

        SqlString strStateID = SqlString.Null;
        if (ddlState.SelectedIndex > 0)
            strStateID = ddlState.SelectedValue;

        SqlCommand objCom = objCon.CreateCommand();
        objCom.CommandType = CommandType.StoredProcedure;

        objCom.CommandText = "PR_Contact_CityDDByState";
        objCom.Parameters.AddWithValue("@StateID", strStateID);

        SqlDataReader objSDR2 = objCom.ExecuteReader();
        
        if (objSDR2.HasRows == true)
        {
            ddlCity.DataSource = objSDR2;
            ddlCity.DataValueField = "CityID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
        }
        objSDR2.Close();
        objCon.Close();

        ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
    }

    #endregion FillCityDropDown

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        FillStateDropDown();
        FillCityDropDown();
      
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Items.Clear();
        FillCityDropDown();
        
    }

    #region Button : Save
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        #region Local Variables
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlInt32 strContactCatID = SqlInt32.Null;
        SqlString strContactName = SqlString.Null;
        SqlString strContactNumber = SqlString.Null;
        SqlString strWhatsApp = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlInt32 strAge = SqlInt32.Null;
        SqlDateTime strBirthDate = SqlDateTime.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strFacebookID = SqlString.Null;
        SqlString strLinkedInID = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            string strErrorMessage = "";

            if (ddlCountry.SelectedIndex == 0)
            {
                strErrorMessage += "Select Country  <br/>";
            }
            if (ddlState.SelectedIndex == 0)
            {
                strErrorMessage += "Select State  <br/>";
            }
            if (ddlCity.SelectedIndex == 0)
            {
                strErrorMessage += "Select City  <br/>";
            }
            if (ddlContactCategory.SelectedIndex == 0)
            {
                strErrorMessage += "Select ContactCategory  <br/>";
            }
            if (txtContactName.Text.Trim() == "")
            {
                strErrorMessage += "Enter Contact Name  <br/>";
            }
            if (txtContactNo.Text.Trim() == "")
            {
                strErrorMessage += "Enter ContactNo  <br/>";
            }
            if (txtEmail.Text.Trim() == "")
            {
                strErrorMessage += "Enter Email  <br/>";
            }
            if (txtAddress.Text.Trim() == "")
            {
                strErrorMessage += "Enter Address  <br/>";
            }
            if (strErrorMessage.Trim() != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (ddlCountry.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            if (ddlState.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlState.SelectedValue);
            }
            if (ddlCity.SelectedIndex > 0)
            {
                strCityID = Convert.ToInt32(ddlCity.SelectedValue);
            }
            if (ddlContactCategory.SelectedIndex > 0)
            {
                strContactCatID = Convert.ToInt32(ddlContactCategory.SelectedValue);
            }
            if (txtContactName.Text.Trim() != "")
            {
                strContactName = txtContactName.Text.Trim();
            }
            if (txtContactNo.Text.Trim() != "")
            {
                strContactNumber = txtContactNo.Text.Trim();
            }
            if (txtWhatsApp.Text.Trim() != "")
            {
                strWhatsApp = txtWhatsApp.Text.Trim();
            }
            if (txtEmail.Text.Trim() != "")
            {
                strEmail = txtEmail.Text.Trim();
            }
            if (txtAddress.Text.Trim() != "")
            {
                strAddress = txtAddress.Text.Trim();
            }
            if (txtAge.Text.Trim() != "")
            {
                strAge = Convert.ToInt32(txtAge.Text.Trim());
            }
            if (txtBirthDate.Text.Trim() != "")
            {
                strBirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim());
            }
            if (txtBloodGroup.Text.Trim() != "")
            {
                strBloodGroup = txtBloodGroup.Text.Trim();
            }
            if (txtFacebook.Text.Trim() != "")
            {
                strFacebookID = txtFacebook.Text.Trim();
            }
            if (txtLinkedIn.Text.Trim() != "")
            {
                strLinkedInID = txtLinkedIn.Text.Trim();
            }
            #endregion Gather Information


            if (objConn.State == ConnectionState.Closed)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            #region Add Parameters
            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityID", strCityID);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCatID);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNumber);
            objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsApp);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
            objCmd.Parameters.AddWithValue("@FacebookID", strFacebookID);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedInID);
            #endregion Add Parameters

            if (Request.QueryString["ContactID"] != null)
            {
                objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                objCmd.CommandText = "PR_Contact_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Contact/ContactList.aspx",true);
            }

            else
            {
                objCmd.CommandText = "PR_Contact_Insert";
                objCmd.ExecuteNonQuery();
                ddlCountry.SelectedIndex = 0;
                ddlState.SelectedIndex = 0;
                ddlCity.SelectedIndex = 0;
                ddlContactCategory.SelectedIndex = 0;
                txtContactName.Text = "";
                txtAddress.Text = "";
                txtAge.Text = "";
                txtBirthDate.Text = "";
                txtBloodGroup.Text = "";
                txtContactNo.Text = "";
                txtEmail.Text = "";
                txtFacebook.Text = "";
                txtLinkedIn.Text = "";
                txtWhatsApp.Text = "";
                ddlCountry.Focus();
                lblMessage.Text = "Data Inserted Successfully";
            }
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
    #endregion Button : Save

    #region Button : Cancel
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Contact/ContactList.aspx", true);
    }
    #endregion Button : Cancel

    #region Fill Controls
    protected void FillControls(SqlInt32 ContactID)
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
            objCmd.CommandText = "PR_Contact_SelectByPK";
            #endregion Set Connection and Command Objects

            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            #region Read the Values and Set Controls
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        FillStateDropDown();
                        ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        FillCityDropDown();
                        ddlCity.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                    {
                        ddlContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                    }
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                    {
                        txtWhatsApp.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        txtBirthDate.Text = Convert.ToDateTime(objSDR["BirthDate"].ToString().Trim()).ToString("yyyy-MM-dd").Trim();
                      //  txtBirthDate.Attributes["min"] = DateTime.Now.ToString("MM-dd-yyyy");
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFacebook.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                    {
                        txtLinkedIn.Text = objSDR["LinkedINID"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available";
            }              
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