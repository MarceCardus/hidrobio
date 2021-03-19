<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="hidrobio.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style8 {
            color: #FFFFFF;
            font-weight: 700;
            text-align: center;
            font-size: xx-large;
            background-color: #009933;
        }
        .auto-style9 {
            font-size: large;
            color: #006600;
        }
        .auto-style10 {
            font-size: xx-large;
        }
        .auto-style11 {
            font-size: x-large;
        }
        .auto-style14 {
            font-weight: bold;
        }
        .auto-style15 {
            font-size: large;
            font-family: Arial, Helvetica, sans-serif;
        }
        .auto-style17 {
            font-size: large;
            font-style: normal;
            color: #FFFFFF;
        }
       
        .auto-style18 {
            font-size: large;
            color: #FFFFFF;
        }
        .auto-style19 {
            font-size: xx-large;
            color: #FFFFFF;
            font-weight: bold;
        }
       
        .auto-style20 {
            font-size: xx-large;
            width: 246px;
        }
        .auto-style21 {
            font-size: xx-large;
            width: 420px;
            height: 125px;
        }
       
    </style>
   


</head>

<body background="imagenes/4-bg-top.jpg">
    <form id="form1" runat="server">
        <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:Panel ID="Panel1" runat="server">
                <table  align="center" border="1" 
            style="margin-left: 310px; " class="auto-style21">
                    <tr>
                        <td class="auto-style8" colspan="2">
                            <h3 class="auto-style15"><span class="auto-style11"><strong>Acceso - </strong>Hidrobio S.A.</span></h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style19">
                            <h3 class="auto-style17"><strong>Usuario</strong></h3>
                        </td>
                        <td class="auto-style20">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="auto-style9">
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="auto-style14"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style19">
                            <h3 class="auto-style17"><strong>Contraseña</strong></h3>
                        </td>
                        <td class="auto-style20">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="auto-style9">
                                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="auto-style14"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <b>
                                    <asp:Label ID="lblLogin" runat="server" CssClass="auto-style18" Visible="False"></asp:Label>
                                    </b>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="auto-style20">
                            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" Height="26px" CssClass="auto-style14" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
    
        </div>
    </form>
</body>
</html>
