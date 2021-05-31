<%@ Page Title="Orden de Pago" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="op.aspx.vb" Inherits="hidrobio.op" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
  

    <table border="1" style="width:100%;">
        <tr>
            <td class="text-white" colspan="4" style="background-color: #666666"><strong>Orden de Pago</strong></td>
        </tr>
        <tr>
            <td class="text-white" style="height: 26px; width: 303px; background-color: #999999"><strong>Fecha</strong></td>
            <td class="text-white" style="height: 26px; width: 230px; background-color: #999999" colspan="2"><strong>Autorizado por</strong></td>
            <td class="text-white" style="height: 26px; background-color: #999999"><strong>Elaborado por</strong></td>
        </tr>
        <tr>
            <td style="width: 303px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender"  CssClass="Cal_Theme" Format="yyyy-MM-dd" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 230px" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlAutorizador" runat="server" Width="200px">
                            <asp:ListItem Selected="True">Maximiliano Samaniego</asp:ListItem>
                            <asp:ListItem>Marcelo Cardús</asp:ListItem>
                            <asp:ListItem>Fernando Samaniego</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlelaborado" runat="server" Width="200px">
                            <asp:ListItem>Marcelo Cardús</asp:ListItem>
                            <asp:ListItem>Yenilda Moreno</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="text-white" style="width: 303px; background-color: #999999"><strong>Tipo de Pago</strong></td>
            <td class="text-white" colspan="3" style="background-color: #999999"><strong>Detalle</strong></td>
        </tr>
        <tr>
            <td style="width: 303px">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                            <asp:ListItem>Acopio</asp:ListItem>
                            <asp:ListItem>Gastos</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="93px" Width="775px" AllowPaging="True" >
                            <Columns>
                                <asp:BoundField DataField="cod" HeaderText="Cód" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="factura" HeaderText="N° Factura" />
                                <asp:BoundField DataField="prov" HeaderText="Proveedor" />
                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:N0}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDetalle" runat="server" ImageUrl="~/imagenes/detalle.gif" CommandName="verDetalle"/>
                                         <script type="text/javascript">
                                             function ShowPopup() {
                                                 $("#ModalCenter").modal("show");
                                             }
                                        </script>               
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/imagenes/mas.gif" CommandName="AgregarDetalle"/>
                                       </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:BoundField DataField="provCod" Visible="False" />
                             
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerSettings PageButtonCount="15" />
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
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True" Width="636px">
                            <Columns>
                                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="NroFactura" HeaderText="N° Factura" />
                                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                                <asp:BoundField DataField="Monto" DataFormatString="{0:N0}" HeaderText="Total" />
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/imagenes/delete.ico" ShowDeleteButton="True">
                                <ControlStyle Height="20px" Width="20px" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                       <ContentTemplate>

                     <button type="button" class="btn btn-outline-secondary"  data-toggle="modal" data-target="#exampleModalCenter">Generar Orden de Pago</button>

                       <!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Orden de Pago</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
        desea generar la orden de Pago?
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"  onserverclick="guardar" data-dismiss="modal" class="btn btn-primary" >Guardar</button>
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
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblResultado" runat="server" style="color: #0066CC; font-size: medium" Visible="False"></asp:Label>
                              
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td colspan="2">
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                     <!-- Modal -->
<div class="modal fade" id="ModalCenter"  role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalLongTitle">Información de Movimientos</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
      <asp:GridView ID="gvDet" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                            <Columns>
                                <asp:BoundField DataField="cod" HeaderText="Cód." />
                                <asp:BoundField DataField="item" HeaderText="Item" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
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
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
          </div>
    </div>
  </div>
</div>
  </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <rsweb:ReportViewer ID="rvOp" runat="server" Width="">
                        </rsweb:ReportViewer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
