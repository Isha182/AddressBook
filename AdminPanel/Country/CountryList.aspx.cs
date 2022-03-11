using System;
using System.Collections.Generic;
using System.Configuration;
// Step-5 CommandType
using System.Data;
// Step-1 Imported
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Country_CountryList : System.Web.UI.Page
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
        // Step-1 Establish the Connection
        // Create empty connection Object
        // SqlConnection objConn = new SqlConnection(); // Blank Connection Object

        // Step-2 Information About Database

        // To read the connection string from web.config file
        // objConn.ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;

        //can also write above both statement as 
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);  //for shortcut


        //objConn.ConnectionString = "data source=DESKTOP-7LDVBT7;    initial catalog=AddressBook;  Integrated Security=True;";


        // data source=DESKTOP-7LDVBT7 --> Source of Data/Server Name
        // initial catalog=AddressBook; --> Name of the Database
        // If Integrated Security is False you have to provide Username and Password
        // IS is true if its Local database

        try
        {
            // Step-3 To Open the connection
            if(objConn.State == ConnectionState.Closed)
                objConn.Open();
            // data Work

            #region Set Connection and Command Object
            // Step-5 Prepare the Command Object
            SqlCommand objComm = new SqlCommand(); // Blank Command Object
            // Specify with which connection you want to work
            objComm.Connection = objConn;
            // Type of Command you want to execute

            // Short Form for above Two Statement is --->  SqlCommand objComm = objConn.CreateCommand();

            objComm.CommandType = CommandType.StoredProcedure;
            // objComm.CommandType = CommandType.Text;
            // objComm.CommandType = CommandType.TableDirect;

            // Specify the Stored Procedure
            objComm.CommandText = "PR_Country_SelectAll";

            // To Execute the Command
            // objComm.ExecuteNonQuery();  //Insert,Update,Delete
            // objComm.ExecuteReader();    //Select
            // objComm.ExecuteScalar();    //Only one Scalar value is being return
            // objComm.ExecuteXmlReader(); //XML type of data
            #endregion Set Connection and Command Object
            // Step-6 To read the data
            SqlDataReader objSDR = objComm.ExecuteReader();

            // Step-7 To Display Data
            gvCountry.DataSource = objSDR;
            // To bind the data in Table Form
            gvCountry.DataBind();

            // Step-4 At last Close the connection
            objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "ex.Message";
        }
        finally
        {
            if(objConn.State==ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Fill GridView

    #region RowCommand
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteRecord(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete Record
    }
    #endregion RowCommand

    #region Delete Record
    private void DeleteRecord(SqlInt32 CountryID)
    {
        SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        try
        {
            if(objCon.State==ConnectionState.Closed)
                objCon.Open();

            #region Set Connection and Command Object

            SqlCommand objCom = objCon.CreateCommand();
            objCom.CommandType = CommandType.StoredProcedure;
            objCom.CommandText = "PR_Country_DeleteByPK";
            #endregion Set Connection and Command Object

            objCom.Parameters.AddWithValue("CountryID", CountryID.ToString().Trim());
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