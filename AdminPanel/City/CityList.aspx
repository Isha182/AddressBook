<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CityList.aspx.cs" Inherits="AdminPanel_City_CityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">

    <div class="container-fluid">
         <div class="City">
        <div class="row">
        <div class="col-md-12">
            <asp:Label runat="server" Text="" ID="lblMessage" EnableViewState="false"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2>City List</h2>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAdd" runat="server" NavigateURL="~/AdminPanel/City/CityAddEdit.aspx" CssClass="btn btn-lg btn-success">Add City</asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
           
            <asp:GridView ID="gvCity" runat="server" OnRowCommand="gvCity_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("CityID").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-success btn-sm" NavigateUrl='<%# "~/AdminPanel/City/CityAddEdit.aspx?CityID=" + Eval("CityID").ToString() %>'>Edit</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CityID" HeaderText="ID" /> 
                    <asp:BoundField DataField="StateName" HeaderText="State" />
                    <asp:BoundField DataField="CityName" HeaderText="City" />
                    <asp:BoundField DataField="PinCode" HeaderText="Pin Code" />
                    <asp:BoundField DataField="STDCode" HeaderText="STD Code" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
 </div>

    </div>


</asp:Content>

