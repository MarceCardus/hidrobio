<%@ Page Title="Nota de Crédito" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="notaCredito.aspx.vb" Inherits="hidrobio.notaCredito" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <table border="1" class="nav-justified" style="width: 853px">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666; color: #FFFFFF;"><span style="font-weight: bold">Nota Crédito</span></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 295px">Fecha</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtfecha" runat="server" Width="166px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtfecha_CalendarExtender" CssClass="Cal_Theme" Format="yyyy/MM/dd" runat="server" TargetControlID="txtfecha" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; width: 295px">Nro Nota Crédito</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFactura" runat="server" Width="166px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; width: 295px">Motivo</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtMotivo" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; width: 295px">Cliente</td>
            <td style="height: 22px">
            
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
        </tr>
        <tr>
            <td style="height: 22px; " colspan="2">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Label ID="lblclieCod" runat="server" style="color: #1C5E55"></asp:Label>
                        &nbsp;-
                        <asp:Label ID="lblRuc" runat="server" style="color: #1C5E55"></asp:Label>
                        &nbsp;-
                        <asp:Label ID="lblCliente" runat="server" style="color: #1C5E55"></asp:Label>
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="text-white">
            <td style="width: 295px; height: 22px; background-color: #666666;" class="text-center">
                <strong>Sub Producto</strong></td>
            <td style="height: 22px; background-color: #666666;" class="text-center">
                <strong>Producto</strong></td>
        </tr>
        <tr>
            <td style="width: 295px; height: 22px">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                       <ContentTemplate>
                        <asp:RadioButtonList ID="rblSt" runat="server" AutoPostBack="True" RepeatColumns="2">
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 22px; ">
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblVerduras" runat="server" AutoPostBack="True" RepeatColumns="2">
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="636px" ShowFooter="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="Linea" />
                                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
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
                                        <asp:BoundField DataField="Excenta" HeaderText="Excentas" DataFormatString="{0:N0}" />
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
            <td style="height: 22px" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                          <button type="button" class="btn btn-outline-secondary"  data-toggle="modal" data-target="#ModalGuardarNC">Guardar NC</button>

                       <!-- Modal -->
<div class="modal fade" id="ModalGuardarNC" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="ModalLongGuardarNC">Nota de Crédito</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">x</span>
        </button>
      </div>
      <div class="modal-body">
        desea guardar la Nota de Crédito?
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
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
        </tr>
    </table>

</asp:Content>
