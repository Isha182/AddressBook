<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
   <div class="Contact1">
        <div class="row">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="col-md-4">
            Country :
        </div>
        <div class="col-md-8">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" ></asp:DropDownList>
        </div>

        <div class="col-md-4">
            State :
        </div>
        <div class="col-md-8">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-4">
            City :
        </div>
        <div class="col-md-8">
            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>

        <div class="col-md-4">
            Contact Category :
        </div>
        <div class="col-md-8">
            <asp:DropDownList ID="ddlContactCategory" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            Contact Name :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtContactName" runat="server" Placeholder="Enter Contact Name" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            Contact Number :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtContactNo" runat="server" Placeholder="Enter Contact Number" CssClass="form-control" ></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtContactNo" Display="None" ErrorMessage="Enter Valid Phone No" ForeColor="Red" ValidationExpression="\+?\d[\d -]{8,12}\d"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            Birth Date :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtBirthDate" runat="server" Placeholder="Enter Birth Date" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
             <asp:CompareValidator ID="cvBirthDate" runat="server" ControlToValidate="txtBirthDate" Display="None" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            WhatsApp Number :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtWhatsApp" runat="server" Placeholder="Enter WhatsApp Number" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:RegularExpressionValidator ID="revWhatsAppNumber" runat="server" ControlToValidate="txtWhatsApp" Display="None" ErrorMessage="Enter Valid Number" ForeColor="Red" ValidationExpression="\+?\d[\d -]{8,12}\d"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            Email :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Enter Validate Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            Address :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtAddress" runat="server" Placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            Age :
        </div>
        <div class="col-md-8">
            <asp:TextBox ID="txtAge" runat="server" Placeholder="Enter Age" CssClass="form-control"></asp:TextBox>
             
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:RangeValidator ID="rvAge" runat="server" ControlToValidate="txtAge" Display="None" ErrorMessage="Enter Validate Age" ForeColor="Red" MaximumValue="60" MinimumValue="12" Type="Integer"></asp:RangeValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            Blood Group :
        </div>
    <div class="col-md-8">
        <asp:TextBox ID="txtBloodGroup" runat="server" Placeholder="Enter Blood Group" CssClass="form-control"></asp:TextBox>
    </div>
</div>
    <div class="row">
        <div class="col-md-4">
            Facebook ID :
        </div>
    <div class="col-md-8">
        <asp:TextBox ID="txtFacebook" runat="server" Placeholder="Enter Facebook ID" CssClass="form-control"></asp:TextBox>
    </div>
        </div>
    <div class="row">
        <div class="col-md-4">
            LinkedIn ID :
        </div>
    <div class="col-md-8">
        <asp:TextBox ID="txtLinkedIn" runat="server" Placeholder="Enter LinkedIn ID" CssClass="form-control"></asp:TextBox>
    </div>
    </div>

    <div class="row">
        <div class="col-md-2 col-lg-push-4">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-group-sm btn-success" OnClick="btnSave_Click1" />
        </div>
        <div class="col-md-1 col-lg-push-5">
            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click1" />
        </div>
    </div>

   </div>
    </asp:Content>

