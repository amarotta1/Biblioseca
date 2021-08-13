<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Authors.aspx.cs" Inherits="Biblioseca.Web.Authors" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Autores</h1>
        <asp:GridView ID ="GridViewAuthors" runat="server">

        </asp:GridView>
    </div>  

</asp:Content>
