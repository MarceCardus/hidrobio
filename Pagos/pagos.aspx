<%@ Page Title="Pagos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pagos.aspx.vb" Inherits="hidrobio.pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;" border="1">
        <tr>
            <td class="text-white" style="width: 184px; background-color: #999999"><strong>Fecha de Pago</strong></td>
            <td class="text-white" style="width: 274px; background-color: #999999"><strong>Cuenta Corriente Débito</strong></td>
            <td class="text-white" style="background-color: #999999" colspan="2"><strong>OP</strong></td>
        </tr>
        <tr>
            <td style="width: 184px; height: 33px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 274px; height: 33px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlBanco" runat="server">
                            <asp:ListItem Value="1">Continental - 14-417245-08</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 33px" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <strong>NRO : </strong>
                        <asp:TextBox ID="txtOP" runat="server"></asp:TextBox>
                        &nbsp;
                        <button type="button" class="btn btn-outline-secondary" Height="28px" ImageUrl="~/imagenes/buscar2.png" Width="29px" runat="server" data-toggle="modal"  onserverclick="BuscarOP"  data-target="#ModalBuscarOP">Buscar</button>
                        <br />
                        <strong>
                        <asp:Label ID="lblCod" runat="server" CssClass="bg-white" style="font-size: small;"></asp:Label>
                        &nbsp;-
                        <asp:Label ID="lblTipo" runat="server" style="font-size: small"></asp:Label>
                        &nbsp;-
                        <asp:Label ID="lblMonto" runat="server" style="font-size: small" Text="Label"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal fade"  id="ModalBuscarOP" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Buscar Op's</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
                                   
                               
           <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvOp" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="93px" Width="361px">
                            <Columns>
                                <asp:BoundField DataField="opCod" HeaderText="Cód" />
                                <asp:BoundField DataField="opTipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="opTotal" HeaderText="Total OP" />
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
        <button type="button" runat="server"   onserverclick="gvOp_seleccionar"  data-dismiss="modal" class="btn btn-primary" >Seleccionar</button>
      </div>
    </div>
  </div>
</div>
    </div>
            </td>
        </tr>
        <tr>
            <td style="width: 184px; background-color: #666666;" class="text-white"><strong>Tipo de Pago</strong></td>
            <td style="width: 274px; background-color: #666666;" class="text-white"><strong>N° Cheque/Transferencia</strong></td>
            <td class="text-white" style="background-color: #666666; width: 391px;"><strong>Fecha Cheque/Transferencia</strong></td>
            <td class="text-white" style="background-color: #666666"><strong>Recibo</strong></td>
        </tr>
        <tr>
            <td style="width: 184px">
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlTipo" runat="server">
                            <asp:ListItem Selected="True" Value="4">Transferencia</asp:ListItem>
                            <asp:ListItem Value="2">Cheque</asp:ListItem>
                            <asp:ListItem Value="1">Efectivo</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 274px">
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNroCheque" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFechaCheque" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFechaCheque_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" CssClass="Cal_Theme" format="yyyy-MM-dd" TargetControlID="txtFechaCheque" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNroRecibo" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                         <button type="button" class="btn btn-outline-secondary"  data-toggle="modal" data-target="#ModalGuardarVenta">Guardar Pago</button>

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
        desea guardar el Pago?
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
                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblResultado" runat="server" style="color: #FF9900" Visible="False"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
