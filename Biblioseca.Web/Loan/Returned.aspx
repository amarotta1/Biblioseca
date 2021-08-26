﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Returned.aspx.cs" Inherits="Biblioseca.Web.Loan.Returned" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h4>Devolver Prestamos</h4>
        <hr/>        
        <div class="form-group">
            <label class="col-sm-2 control-label">Partner</label>
            <div class="col-md-10">
                <asp:DropDownList ID="partnerList" runat="server" CssClass="form-control" AutoPostBack ="True" OnSelectedIndexChanged ="OnChangeSelected"/>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Book</label>
            <div class="col-md-10">
                <asp:DropDownList ID="bookList" runat="server" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="buttonReturnLoan" runat="server" Text="Devolver Libro" OnClick="ButtonReturnLoan_Click" CssClass="btn btn-default"/>
            </div>
        </div>
    </div>
</asp:Content>
