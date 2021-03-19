<%@ Page Title="Informe de Acopios" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="infAcopioProv.aspx.vb" Inherits="hidrobio.infAcopioProv" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="1" class="nav-justified" style="width: 558px">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666"><span style="font-weight: bold; color: #FFFFFF">I<span style="background-color: #666666">nforme de </span></span><strong><span style="color: #FFFFFF; background-color: #666666">Acopios</span></strong></td>
        </tr>
        <tr>
            <td style="height: 22px; ">Desde</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtDesde_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" TargetControlID="txtDesde" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; ">Hasta</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txthasta" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txthasta_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" TargetControlID="txthasta">
                        </ajaxToolkit:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; ">Proveedor</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                       <ContentTemplate>
                               <asp:TextBox ID="txtprov" runat="server"></asp:TextBox>
                               &nbsp;&nbsp;
                            <button runat="server" class="btn btn-outline-secondary" data-target="#exampleModalCenter" data-toggle="modal" onserverclick="btnBuscaCl_Click" type="button">
                                Buscar Proveedor
                            </button>
                         
                        </ContentTemplate>
                </asp:UpdatePanel>
                 <!-- Modal -->
<div class="modal fade"  id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Buscar Proveedor</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
                                   
                               
           <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                       <asp:GridView ID="gvProveedor" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="93px" Width="361px">
                            <Columns>
                                <asp:BoundField DataField="provCod" HeaderText="Cód" />
                                <asp:BoundField DataField="provRuc" HeaderText="Ruc" />
                                <asp:BoundField DataField="provNombre" HeaderText="Proveedor" />
                                <asp:CommandField  ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/imagenes/flecha1.png" >
                                <ControlStyle Height="30px" Width="30px" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                       </div>
                          </ContentTemplate>
                </asp:UpdatePanel>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"   onserverclick="gvProveedor_seleccionar"  data-dismiss="modal" class="btn btn-primary" >Seleccionar</button>
      </div>
    </div>
  </div>
</div>
    </div>
            </td>
        </tr>
        <tr>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblCodProv" runat="server" Visible="False"></asp:Label>
                        </strong>&nbsp;-
                        <strong>
                        <asp:Label ID="lblProv" runat="server" Visible="False"></asp:Label>
                        </strong>
                    </ContentTemplate>
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
                        <rsweb:ReportViewer ID="rvCosecha" runat="server" Width="1000px" SizeToReportContent="True" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
                           
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
