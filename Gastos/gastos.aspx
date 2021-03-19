<%@ Page Title="Gastos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="gastos.aspx.vb" Inherits="hidrobio.gastos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;" border="1">
        <tr>
            <td class="text-left" style="color: #FFFFFF; background-color: #666666; height: 27px; font-size: large;" colspan="4"><strong>GASTOS</strong></td>
        </tr>
        <tr>
            <td class="text-left" style="color: #FFFFFF; width: 358px; background-color: #999999"><strong>Fecha</strong></td>
            <td class="text-left" style="color: #FFFFFF; background-color: #999999" colspan="2"><strong>Nro Factura</strong></td>
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
            <td colspan="2">
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
            <td class="text-left" style="height: 26px; background-color: #999999; font-weight: bold;" colspan="2">Rubro</td>
            <td class="text-left" style="height: 26px; background-color: #999999">&nbsp;</td>
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
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblRubros" runat="server" RepeatColumns="2" AutoPostBack="True">
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                &nbsp;</td>
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
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                     
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" rowspan="2">
                <asp:Panel ID="Panel1" runat="server" Width="504px">
                    <table style="width:93%;">
                        <tr>
                            <td style="width: 174px"><strong>Exentas</strong></td>
                            <td style="width: 197px">
                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtE" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="text-white" style="background-color: #999999"><strong>IVA</strong></td>
                        </tr>
                        <tr>
                            <td style="width: 174px"><strong>Gravadas 10%</strong></td>
                            <td style="width: 197px">
                                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt10" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                        <asp:Label ID="lbl10" runat="server" Visible="False"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 174px"><strong>Gravadas 5%</strong></td>
                            <td style="width: 197px">
                                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt5" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                        <asp:Label ID="lbl5" runat="server" Visible="False"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 174px"><strong>Total</strong></td>
                            <td style="width: 197px">
                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtTotal" runat="server" Enabled="False"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                    <ContentTemplate>
                                        <strong>
                                        <asp:Label ID="lblTotIva" runat="server"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td colspan="2" style="background-color: #999999">
                <span class="text-white" style="font-weight: bold">Observación</span></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                                               <script type="text/javascript">
                                                   function contadorTexto(campo, cuentaCampos, limiteMaximo) {
                                                       if (campo.value.length > limiteMaximo) //Si muy largo, cortar.
                                                           campo.value = campo.value.substring(0, limiteMaximo);
                                                       else
                                                           cuentaCampos.value = (limiteMaximo - campo.value.length);
                                                   }
     </script>
                  
                        <asp:TextBox ID="txtObs" runat="server" Height="52px" TextMode="MultiLine" Width="298px" onkeydown="contadorTexto(txtObs, Txt_Cont, 100);" 
                            onkeyup="contadorTexto(txtObs, Txt_Cont, 100);"></asp:TextBox>
                        <input id="Txt_Cont" type="text" size="3" disabled="disabled">

   </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <button class="btn btn-outline-secondary" data-target="#ModalGuardarGasto" data-toggle="modal" type="button">
                    Guardar Gastos</button>

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
            <td colspan="4">
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
