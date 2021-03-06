﻿<%@ Page Title="Informe de Ventas - Totales" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="infVentasCab.aspx.vb" Inherits="hidrobio.infVentasCab" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <table border="1" class="nav-justified">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666"><span style="font-weight: bold; color: #FFFFFF"><span style="background-color: #666666">Informe Ventas Totales</span></span></td>
        </tr>
        <tr>
            <td style="height: 22px; ">Desde</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" TargetControlID="txtFecha" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; ">Hasta</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtHasta_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" BehaviorID="txtHasta_CalendarExtender" TargetControlID="txtHasta">
                        </ajaxToolkit:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                     <button type="button" runat="server"  onserverclick="btnBuscar_Click"  class="btn btn-outline-secondary">Buscar</button>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <rsweb:ReportViewer ID="rvCosecha" runat="server" Width="1000" SizeToReportContent="True">
                        </rsweb:ReportViewer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblResultado" runat="server" style="color: #FF6600; background-color: #FFFFFF" Visible="False"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
