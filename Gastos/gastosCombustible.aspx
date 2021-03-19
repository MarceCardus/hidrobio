<%@ Page Title="Gastos  - Combustibles" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="gastosCombustible.aspx.vb" Inherits="hidrobio.gastosCombustible" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;" border="1">
        <tr>
            <td class="text-left" style="color: #FFFFFF; background-color: #666666; font-size: large;" colspan="3"><strong>CARGA DE COMBUSTIBLE</strong></td>
        </tr>
        <tr>
            <td class="text-left" style="color: #FFFFFF; width: 358px; background-color: #999999"><strong>Fecha</strong></td>
            <td class="text-left" style="color: #FFFFFF; background-color: #999999"><strong>Nro Factura</strong></td>
            <td class="text-left" style="color: #FFFFFF; background-color: #999999"><strong>Tipo de Compra</strong></td>
        </tr>
        <tr>
            <td style="width: 358px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNroFact" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Contado</asp:ListItem>
                            <asp:ListItem>Crédito</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="text-white">
            <td class="text-left" style="height: 26px; width: 358px; background-color: #999999"><strong>P</strong><span style="font-weight: bold">roveedor</span></td>
            <td class="text-left" style="height: 26px; background-color: #999999; font-weight: bold;">Chofer</td>
            <td class="text-left" style="height: 26px; background-color: #999999"><b>M</b><span style="font-weight: bold">óvil</span></td>
        </tr>
        <tr>
            <td style="width: 358px">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <button type="button" class="btn btn-outline-secondary" runat="server" data-toggle="modal"  onserverclick="btnBuscaProv_Click"  data-target="#ModalProveedor">Buscar</button>
                        <asp:TextBox ID="txtProveedor" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal fade"  id="ModalProveedor" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalProveedorLongTitle">Buscar Proveedor</h5>
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
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlChofer" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlMovil" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 358px">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblCodProv" runat="server" style="font-size: large; color: #0066CC" Visible="False"></asp:Label>
                        </strong>-<strong><asp:Label ID="lblProv" runat="server" style="font-size: large; color: #0066CC" Visible="False"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                      
                        <asp:RadioButtonList ID="rblRubros" runat="server" AutoPostBack="True" RepeatColumns="2">
                        </asp:RadioButtonList>
                      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                     
                        <asp:RadioButtonList ID="rblItem" runat="server" AutoPostBack="True" RepeatColumns="1" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                     
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                     <ContentTemplate>
                      <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="939px" ShowFooter="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="Linea" />
                                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                        <asp:BoundField DataField="Item" HeaderText="Item" />
                                        <asp:BoundField DataField="Chofer" HeaderText="Chofer" />
                                        <asp:BoundField DataField="Movil" HeaderText="Móvil" />
                                        <asp:TemplateField HeaderText="Cantidad">
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtgvCant" Text='<%# Bind("Cantidad") %>' runat="server" AutoPostBack="True" Width="50px" OnTextChanged="txtgvCant_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unitario">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUnit" Text='<%# Bind("Unitario") %>' runat="server" AutoPostBack="True" Width="80px" OnTextChanged="txtgvUnit_TextChanged"></asp:TextBox>
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
                                        <asp:BoundField DataField="movCod" HeaderText="CodMovil" Visible="False" />
                                        <asp:BoundField DataField="perCod" HeaderText="PerCod" Visible="False" />
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
            <td colspan="3">
                <button class="btn btn-outline-secondary" data-target="#ModalGuardarGasto" data-toggle="modal" type="button">Guardar Gasto</button>      
                <!-- Modal -->
                <div id="ModalGuardarGasto" aria-hidden="true" aria-labelledby="exampleModalCenterTitle" class="modal fade" role="dialog" tabindex="-1">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 id="ModalLongGuardarGasto" class="modal-title">Gastos</h5>
                                <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                    <span aria-hidden="true">x</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                desea guardar el gasto?
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-secondary" data-dismiss="modal" type="button">
                                    Cerrar
                                </button>
                                <button runat="server" class="btn btn-primary" data-dismiss="modal" onserverclick="btnGuardar_Click" type="button">
                                    Guardar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblResultado" runat="server" style="color: #FF9900; font-size: large;" Visible="False"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
