<%@ Page Title="Actualizar Ventas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ActualizarVentas.aspx.vb" Inherits="hidrobio.ActualizarVentas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <table border="1" class="nav-justified">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666"><strong><span style="color: #FFFFFF; background-color: #666666">Actualizar Ventas</span></strong></td>
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
            <td style="width: 219px; height: 22px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div class="text-center">
                                    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                              <asp:TemplateField HeaderText="Cód">
                                                <EditItemTemplate>
                                                   <asp:TextBox ID="txtventaCod" Width="50px" Enabled ="false"  Text='<%# Bind("ventaCod") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblventaCod"  Text='<%# Bind("ventaCod") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Cliente">
                                                <EditItemTemplate>
                                                   <asp:TextBox ID="txtclieNombre" Width="250px" Enabled ="false"  Text='<%# Bind("clieNombre") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclieNombre"  Text='<%# Bind("clieNombre") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        
                                             <asp:TemplateField HeaderText="Fecha">
                                                <EditItemTemplate>
                                                   <asp:TextBox ID="txtFecha" Width="100px" Enabled ="false"  Text='<%# Bind("ventaFchFact") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFecha"  Text='<%# Bind("ventaFchFact") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                                      
                                            <asp:TemplateField HeaderText="Nro Factura">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNroFact" Width="150px" Text='<%# Bind("ventaNroFact") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNroFactura"  Text='<%# Bind("ventaNroFact") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Total">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTotal" Width="100px" Enabled ="false"  Text='<%# Bind("ventaTotal") %>' DataFormatString="{0:N0}" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal"  Text='<%# Bind("ventaTotal") %>' DataFormatString="{0:N0}" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pagado">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlPagadoE" runat="server">
                                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                                        <asp:ListItem>Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlPagado0" runat="server">
                                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                                        <asp:ListItem>Si</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Estado">
                                                
                                                <EditItemTemplate>
                                                   <asp:TextBox ID="txtEstado" Width="50px" Enabled ="false"  Text='<%# Bind("ventaEstado") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEstado"  Text='<%# Bind("ventaEstado") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            
                                              </asp:TemplateField>
                                             <asp:CommandField ButtonType="Image" EditImageUrl="~/imagenes/edit.ico" ShowEditButton="True" CancelImageUrl="~/imagenes/Cancel_2_B.gif" HeaderText="Actualizar" UpdateImageUrl="~/imagenes/descarga.jpg">
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
