﻿<%@ Page Title="Anular Mermas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="anularMermas.aspx.vb" Inherits="hidrobio.anularMermas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <table border="1" class="nav-justified">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666"><strong><span style="color: #FFFFFF; background-color: #666666">Anular Mermas</span></strong></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 219px">Desde</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" CssClass="Cal_Theme" Format="yyyy-MM-dd" runat="server" TargetControlID="txtDesde" >
                            </cc1:CalendarExtender>
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
                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" CssClass="Cal_Theme" Format="yyyy-MM-dd" runat="server" TargetControlID="txtHasta" >
                         </cc1:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 219px; height: 22px">
                &nbsp;</td>
            <td style="height: 22px">
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
        <h5 class="modal-title" id="exampleModalLongTitle">Anulación de Mermas</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        desea anular la Merma?
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
                                <asp:BoundField DataField="mermaCod" HeaderText="Código" />
                                <asp:BoundField DataField="mermaFecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="mermaMotivo" HeaderText="Motivo" Visible="False" />
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
                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="612px">
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód." />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="mermaCant" HeaderText="Cantidad" />
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
