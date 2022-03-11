<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryAddEdit.aspx.cs" Inherits="AdminPanel_Country_CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
   <div class="Country1">
        <div class="row">
        <div class="col-md-8">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>
  
    <div class="row">
        <div class="col-md-2">
            Country Name :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtCountryName" runat="server" CssClass="form-control" Placeholder="Enter Country Name"></asp:TextBox>
        </div>
    </div>

        <div class="row">
        <div class="col-md-2">
            Country Code :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtCountryCode" runat="server" CssClass="form-control" Placeholder="Enter Country Code"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2 col-lg-push-2">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-sm btn-default" OnClick="btnSave_Click" />
        </div>
        <div class="col-md-1 col-lg-push-1">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>
   </div>
</asp:Content>

