<%@ Page Title="Cobros" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="cobros.aspx.vb" Inherits="hidrobio.cobros" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <table border="1" style="width:100%;">
        <tr>
            <td colspan="4" style="height: 22px; color: #FFFFFF; font-size: xx-large; background-color: #808080;"><strong>Cobros</strong></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 319px"><strong>N° Venta/Factura</strong></td>
            <td style="height: 22px; width: 298px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtVenta" runat="server" Enabled="False"></asp:TextBox>
                                              
                          <button type="button" class="btn btn-info" data-toggle="modal" data-target="#ModalBuscar">Buscar</button>
                                                  </ContentTemplate>
                </asp:UpdatePanel>  
                                  
                              
                        <div class="modal fade"  id="ModalBuscar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true" >
  <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalLongBuscar">Buscar Facturas</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:UpdatePanel ID="UpdatePanel11" runat="server">
           <ContentTemplate>
            <div class="input-group mb-3">
  <div class="input-group-prepend">
    <button class="btn btn-outline-secondary" runat="server" onserverclick="btnBuscaClientes" type="button">Buscar</button>
  </div>
  <asp:TextBox ID="txtClie" runat="server"></asp:TextBox>
</div>                       
            </ContentTemplate>
                </asp:UpdatePanel>     
          <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                     <ContentTemplate>
                        <asp:GridView ID="gvBuscar" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="93px" Width="400px">
                            <Columns>
                                <asp:BoundField DataField="clieCod" HeaderText="Cód" />
                                <asp:BoundField DataField="clieRuc" HeaderText="Ruc" />
                                <asp:BoundField DataField="clieNombre" HeaderText="Cliente" />
                                <asp:CommandField  ShowSelectButton="True"  runat="server"  ButtonType="Image" SelectImageUrl="~/imagenes/flecha1.png" >
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
                    
                       
                        <%--Grilla Facturas--%>

                            <asp:GridView ID="gvVentas" runat="server" Width="600px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" style="border-style: solid; border-width: 1px; padding: 1px">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="ventaCod" HeaderText="Cód" />
                                                <asp:BoundField DataField="ventaFchFact" HeaderText="Fecha" />
                                                <asp:BoundField DataField="ventaNroFact" HeaderText="Factura" />
                                                <asp:BoundField DataField="ventaTotal" HeaderText="Total" />
                                                <asp:BoundField DataField="ventaSaldo" HeaderText="Saldo" />
                                                <asp:BoundField DataField="ventaEstado" HeaderText="Estado" />
                                                <asp:CommandField runat="server" ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/imagenes/flecha1.png"  />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <RowStyle BackColor="#F7F7DE" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                            <SortedAscendingHeaderStyle BackColor="#848384" />
                                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                            <SortedDescendingHeaderStyle BackColor="#575357" />
                                        </asp:GridView>
                       
                          </ContentTemplate>
                </asp:UpdatePanel>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"   onserverclick="TraerFacturas"  data-dismiss="modal" class="btn btn-primary" >Seleccionar</button>
      </div>
    </div>
  </div>
</div>
    </div> 
    
                                                          
            </td>
            <td style="height: 22px" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblCliente" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 319px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblFechaFact" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 298px">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <strong>
                        Total Fact:
                        <asp:Label ID="lblTotalFact" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <strong>
                        
                        &nbsp;<asp:Label ID="lblTipopago" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblEstado" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 319px">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblReparto" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 298px">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <strong>
                        Saldo :
                        <asp:Label ID="lblSaldo" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblTotalIVa" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblCodVenta" runat="server"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 319px">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True">
                            <asp:ListItem>Tipo de Pago.....</asp:ListItem>
                            <asp:ListItem Value="1">Efectivo</asp:ListItem>
                            <asp:ListItem Value="2">Cheque</asp:ListItem>
                            <asp:ListItem Value="4">Transferencia</asp:ListItem>
                            <asp:ListItem Value="3">Boleta de Depósito</asp:ListItem>
                            <asp:ListItem Value="5">Nota Crédito</asp:ListItem>
                            <asp:ListItem Value="6">Retención</asp:ListItem>
                        </asp:DropDownList>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 298px">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:DropDownList ID="ddlBanco" runat="server">
                        </asp:DropDownList>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <strong>Monto : </strong>
                        <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <strong>Recibo :
                        <asp:TextBox ID="txtRecibo" runat="server"></asp:TextBox>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="text" style="width: 319px; height: 22px">
 
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <strong>N° Cheque/Depósito </strong><strong>
                        <asp:TextBox ID="txtCheque" runat="server"></asp:TextBox>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
 
                    </td>
            <td style="width: 298px; height: 22px">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <strong>Fecha/Vencimiento </strong><strong>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        </strong>
                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender"  CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha">
                        </ajaxToolkit:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFechaCobro" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFechaCobro_CalendarExtender"  CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" BehaviorID="txtFechaCobro_CalendarExtender" TargetControlID="txtFechaCobro">
                        </ajaxToolkit:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" colspan="4">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <button type="button" class="btn btn-outline-secondary"  data-toggle="modal" data-target="#ModalGuardarCobro">Guardar Cobro</button>

                       <!-- Modal -->
<div class="modal fade" id="ModalGuardarCobro" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalLongGuardarCobro">Cobros</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
        desea guardar el Cobro?
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"  onserverclick="btnGuardar_Click" data-dismiss="modal" class="btn btn-primary" >Guardar</button>
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
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvCobros" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                            <Columns>
                                <asp:BoundField DataField="cobroCod" HeaderText="Cód" />
                                <asp:BoundField DataField="cobroFecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="tPagoDesc" HeaderText="Tipo" />
                                <asp:BoundField DataField="banNombre" HeaderText="Banco" />
                                <asp:BoundField DataField="cobroCheqNro" HeaderText="N°" />
                                <asp:BoundField DataField="cobroFchCheq" HeaderText="Fecha" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="cobroMonto" DataFormatString="{0:C}" HeaderText="Monto" />
                                <asp:BoundField DataField="cobroRecibo" HeaderText="Recibo" />
                                <asp:BoundField DataField="cobroSaldo" HeaderText="Saldo" DataFormatString="{0:C}" />
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
                        <strong>
                        <asp:Label ID="lblResultado" runat="server" Visible="False"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
