<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
 
    <div class="container-fluid">
         <div class="State">
        <div class="row">
        <div class="col-md-12">
            <h2>State List</h2>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAdd" runat="server" NavigateUrl="~/AdminPanel/State/StateAddEdit.aspx" CssClass="btn btn-sm btn-success">Add State</asp:HyperLink>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">
            
            <asp:GridView ID="gvState" runat="server" OnRowCommand="gvState_RowCommand" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" >
                <Columns> 
                    <asp:TemplateField HeaderText="Delete">
                       <ItemTemplate>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("StateID").ToString() %>' />
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                       <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hlEdit" Text="Edit" CssClass="btn btn-dark btn-sm" NavigateUrl= '<%# "~/AdminPanel/State/StateAddEdit.aspx?StateID=" + Eval("StateID").ToString() %>' />
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:Boundfield Datafield="StateID" HeaderText ="ID" />
                    <asp:Boundfield Datafield="CountryName" HeaderText ="Country" />
                    <asp:Boundfield Datafield="StateName" HeaderText ="State" />
                    <asp:Boundfield Datafield="StateCode" HeaderText ="Code" />
                </Columns>
            </asp:GridView>

            
        </div>
    </div>
  </div>
    </div>

</asp:Content>

