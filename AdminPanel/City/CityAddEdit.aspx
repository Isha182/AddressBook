<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="AdminPanel_City_CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
   <div class="City1">
        <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-4">
                    State :
                </div>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlStateID" runat="server" CssClass="form-control" Width="405px"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            City Name :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" Placeholder="Enter City Name"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            STD Code :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtSTDCode" runat="server" CssClass="form-control" Placeholder="Enter STD Code"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
           Pin Code :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" Placeholder="Enter Pin Code"></asp:TextBox>
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

