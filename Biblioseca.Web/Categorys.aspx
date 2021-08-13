<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorys.aspx.cs" Inherits="Biblioseca.Web.Categorys" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Categorias</h1>
        <asp:GridView ID ="GridViewCategory" runat="server">
        </asp:GridView>
    </div>  

</asp:Content>