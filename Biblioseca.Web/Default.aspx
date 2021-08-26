<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Biblioseca.Web._Default" %>
<%@ Import Namespace="Biblioseca.Web" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Biblioseca</h1>
        <p class="lead">Sistema de prestamos de libros para Academia CyS</p>
        <p><a href="Loan/Create.aspx" class="btn btn-primary btn-lg">Prestar libro &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Autores</h2>
            <p>
                <a class="btn btn-default" href="Author/List">Ver autores &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Ve nuestros Libros</h2>
            <p>
                <a class="btn btn-default" href="Book/AvailableBooks.aspx">Libros disponibles &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Crear un nuevo socio!</h2>
            <p>
                Se parte de esta maravillosa Academia
            </p>
            <p>
                <a class="btn btn-default" href="Partner/Create.aspx">Nuevo socio &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
