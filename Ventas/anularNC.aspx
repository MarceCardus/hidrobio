﻿<%@ Page Title="Anular Nota de Crédito" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="anularNC.aspx.vb" Inherits="hidrobio.anularNC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <table border="1" class="nav-justified">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666"><span style="color: #FFFFFF; background-color: #FF6600"><strong><span style="background-color: #666666">Anular Nota de Crédito</span></strong></span></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 219px">Desde</td>
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
            <td style="height: 22px; width: 219px">Hasta</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtHasta_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" TargetControlID="txtHasta" />
                                           </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                     <button type="button" runat="server"  onserverclick="btnBuscar_Click"  class="btn btn-outline-secondary">Buscar</button>
                        &nbsp;
                        <button type="button" class="btn btn-outline-secondary"  data-toggle="modal" data-target="#exampleModalCenter">Eliminar</button>

                       <!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Anulación de Nota de Crédito</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        desea anular la Nota de Crédito?
           <br />
          <asp:TextBox ID="txtMotivo" runat="server"></asp:TextBox>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"  onserverclick="btnAceptar_Click" data-dismiss="modal" class="btn btn-primary" >Guardar</button>
      </div>
    </div>
  </div>
</div>
                      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="620px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                            <Columns>
                                <asp:BoundField DataField="ncCod" HeaderText="Código" />
                                <asp:BoundField DataField="ncFchFact" HeaderText="Fecha" />
                                <asp:BoundField DataField="ventaCod" HeaderText="Cód Venta" />
                                <asp:BoundField DataField="accNombre" HeaderText="Cargado por" />
                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/imagenes/flecha1.png" >
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
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvNCDet" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="581px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="ncDetCod" HeaderText="Cód" />
                                        <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                        <asp:BoundField DataField="ncCant" HeaderText="Cantidad" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
