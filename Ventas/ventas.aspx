<%@ Page Title="Ventas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="ventas.aspx.vb" Inherits="hidrobio.ventas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



     <table border="1" class="nav-justified">
        <tr>
            <td colspan="3" style="font-size: x-large; background-color: #666666; color: #FFFFFF;"><span style="font-weight: bold">Ventas</span></td>
        </tr>
        <tr class="text-white">
            <td style="height: 22px; width: 324px; background-color: #999999;"><strong>Nro Factura</strong></td>
            <td style="height: 22px; background-color: #999999;">
                <strong>Fecha</strong></td>
            <td style="height: 22px; background-color: #999999; width: 391px;">
                <strong>Tipo de Venta</strong></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 324px">
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblCodVenta" runat="server" Visible="False"></asp:Label>
                        </strong>&nbsp;<asp:TextBox ID="txtFactura" runat="server" Width="166px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtfecha" runat="server" Width="166px" style="margin-left: 32"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtfecha_CalendarExtender"  CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" TargetControlID="txtfecha" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px; width: 391px;">
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rbTipo" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                            <asp:ListItem Selected="True">Contado</asp:ListItem>
                            <asp:ListItem>Crédito</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; width: 324px; background-color: #999999;" class="text-white"><strong>Cliente</strong></td>
            <td style="height: 22px; background-color: #999999;" class="text-white">
                <strong>Lugar de Entrega</strong></td>
            <td style="height: 22px; background-color: #999999; width: 391px;" class="text-white">
                <strong>Forma de Pago</strong></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 324px">
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblclieCod" runat="server" style="color: #003399; font-size: small;"></asp:Label>
                        -<asp:Label ID="lblRuc" runat="server" style="color: #003399; font-size: small;"></asp:Label>
                        - <asp:Label ID="lblCliente" runat="server" style="color: #003399; font-size: small;"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rbEntrega" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="S" Selected="True">Reparto</asp:ListItem>
                            <asp:ListItem Value="N">Depósito</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px; width: 391px;">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblForma" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">Efectivo</asp:ListItem>
                            <asp:ListItem Value="4">Transferencia</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 324px">
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                       
                           
                        <button type="button" class="btn btn-outline-secondary" runat="server" data-toggle="modal"  onserverclick="btnBuscaCl_Click"  data-target="#exampleModalCenter">Buscar</button>
                           &nbsp;
                           <asp:TextBox ID="txtcliente" runat="server"></asp:TextBox>
                           </ContentTemplate>
                </asp:UpdatePanel>
                       <!-- Modal -->
<div class="modal fade"  id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Buscar Clientes</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
                                   
                               
           <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvCliente" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="93px" Width="361px">
                            <Columns>
                                <asp:BoundField DataField="clieCod" HeaderText="Cód" />
                                <asp:BoundField DataField="clieRuc" HeaderText="Ruc" />
                                <asp:BoundField DataField="clieNombre" HeaderText="Cliente" />
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
        <button type="button" runat="server"   onserverclick="gvCliente_seleccionar"  data-dismiss="modal" class="btn btn-primary" >Seleccionar</button>
      </div>
    </div>
  </div>
</div>
    </div>
            </td>
            <td colspan="2">
               

                 
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                      <ContentTemplate>
                    <button type="button" runat="server" onserverclick="imprimirVentas" class="btn btn-info">Imprimir</button>
                                 <button type="button" class="btn btn-info" data-toggle="modal" data-target="#ModalBuscar">Buscar</button>
                                 <button type="button" runat="server" onserverclick="btnNuevo_Click" class="btn btn-info">Nuevo</button>
                                 <button type="button" runat="server" onserverclick="Actualizar" class="btn btn-info">Actualizar</button>
                                                 

                                    <button type="button" data-toggle="modal" data-target="#ActualizarVenta" class="btn btn-info">Guardar</button>
                          <!-- Modal -->
<div class="modal fade" id="ActualizarVenta" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ActualizarVentaLong">Actualizar Ventas</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
        desea actualizar la venta?
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"  onserverclick="GuardarActualizar" data-dismiss="modal" class="btn btn-primary" >Guardar</button>
      </div>
    </div>
  </div>
</div>
                            <button type="button" data-toggle="modal" data-target="#AnularVenta" class="btn btn-info">Anular Venta</button>


                          <div class="modal fade" id="AnularVenta" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="AnularVentaLong">Anular Ventas</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
         desea anular la Venta?
            <br />
     <asp:TextBox ID="txtMotivo" runat="server" Height="65px" TextMode="MultiLine" Width="226px"></asp:TextBox>
           </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"  onserverclick="Anular" data-dismiss="modal" class="btn btn-primary" >Guardar</button>
      </div>
    </div>
  </div>
</div>
                             </ContentTemplate>
                </asp:UpdatePanel>
                    <!-- Modal Buscar-->
<div class="modal fade"  id="ModalBuscar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalLongBuscar">Buscar Facturas</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
           <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
            <div class="input-group mb-3">
  <div class="input-group-prepend">
    <button class="btn btn-outline-secondary" runat="server" onserverclick="btnBuscaClientes" type="button">Buscar</button>
  </div>
  <asp:TextBox ID="txtClie" runat="server"></asp:TextBox>
</div>                       
            </ContentTemplate>
                </asp:UpdatePanel>                     
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvBuscar" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="93px" Width="361px">
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

                            <asp:GridView ID="gvVentas" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" style="border-style: solid; border-width: 1px; padding: 1px">
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
        </tr>
        <tr>
            <td style="height: 22px; width: 324px; background-color: #666666;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <strong>
                        <span class="text-white" style="font-size: large">
                        <asp:Label ID="lblCredito" runat="server" CssClass="bg-white" style="color: #17A2B8 !important; font-size: medium" Text="Días Crédito"></asp:Label>
                        </span><span style="font-size: large"> &nbsp;</span><asp:TextBox ID="txtDias" runat="server" Width="50px"></asp:TextBox>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px; color: #FFFFFF; font-size: large; background-color: #666666 !important;" colspan="2" class="bg-white">
                <strong>Producto</strong></td>
        </tr>
        <tr>
            <td style="width: 324px; height: 22px">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                     <ContentTemplate>
                        <asp:RadioButtonList ID="rblSt" runat="server" AutoPostBack="True" RepeatColumns="2">
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px; " colspan="2">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblVerduras" runat="server" AutoPostBack="True" RepeatColumns="2">
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="885px" ShowFooter="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="Linea" />
                                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="Stock" DataFormatString="{0:N2}" HeaderText="Stock" />
                                        <asp:TemplateField HeaderText="Cantidad">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtgvCant" onfocus="Javascript:this.focus();this.select();" Text='<%# Bind("Cantidad") %>' runat="server" AutoPostBack="True" Width="50px" OnTextChanged="txtgvCant_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unitario">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUnit" onfocus="Javascript:this.focus();this.select();" Text='<%# Bind("Unitario") %>' runat="server" AutoPostBack="True" Width="80px" OnTextChanged="txtgvUnit_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Impuesto">
                                            <ItemTemplate>
                                                <asp:DropDownList SelectedValue='<%#Eval("Impuesto") %>' ID="ddlImpuesto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlImpuesto_SelectedIndexChanged">
                                                    <asp:ListItem Value="5">5%</asp:ListItem>
                                                    <asp:ListItem Value="10">10%</asp:ListItem>
                                                    <asp:ListItem Value="0">Exenta</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Excenta" HeaderText="Exentas" DataFormatString="{0:N0}" />
                                        <asp:BoundField DataField="5" HeaderText="5%" DataFormatString="{0:N0}" />
                                        <asp:BoundField DataField="10" HeaderText="10%" DataFormatString="{0:N0}" />
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/imagenes/delete.ico" >
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
            <td style="height: 22px" colspan="3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                         <button type="button" class="btn btn-outline-secondary"  data-toggle="modal" data-target="#ModalGuardarVenta">Guardar Venta</button>

                       <!-- Modal -->
<div class="modal fade" id="ModalGuardarVenta" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalLongGuardarVenta">Ventas</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
        desea guardar la Venta?
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
        <button type="button" runat="server"  onserverclick="btnGuardar_Click" data-dismiss="modal" class="btn btn-primary" >Guardar</button>
          <button type="button" runat="server"  onserverclick="btnGuardarImprimir_Click" data-dismiss="modal" class="btn btn-primary" >Guardar e Imprimir</button>
      </div>
    </div>
  </div>
</div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblResultado" runat="server" style="color: #FF6600; background-color: #FFFFFF" Visible="False"></asp:Label>
                        &nbsp;&nbsp;&nbsp;</strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
