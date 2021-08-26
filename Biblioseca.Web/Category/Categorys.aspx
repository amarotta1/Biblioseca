<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorys.aspx.cs" Inherits="Biblioseca.Web.Categorys" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Categorias</h1>
        <asp:GridView ID ="GridViewCategory" runat="server" CssClass="table table-striped table-dark" BackColor="SkyBlue"
            AutoGenerateColumns="false">
            <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="labelID" runat="server" Text='<%# Eval("Id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category Name">
                <ItemTemplate>
                    <asp:Label ID="labelCategoryName" runat="server" Text='<%# Eval("name") %>'/>
                </ItemTemplate>      
            </asp:TemplateField>            
        </Columns>
        </asp:GridView>
    </div>  

</asp:Content>