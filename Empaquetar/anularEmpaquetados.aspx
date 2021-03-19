<%@ Page Title="Anular Empaquetados" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="anularEmpaquetados.aspx.vb" Inherits="hidrobio.anularEmpaquetados" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <table border="1" class="nav-justified" style="width: 780px">
        <tr>
            <td colspan="2" style="font-size: x-large; background-color: #666666"><span style="color: #FFFFFF; background-color: #FF6600"><strong><span style="background-color: #666666">Anular E</span></strong><span style="font-weight: bold"><span style="background-color: #666666">mpaquetados</span></span></span></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 101px">Desde</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtDesde" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtDesde_CalendarExtender" format="yyyy-MM-dd" runat="server" TargetControlID="txtDesde" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 22px; width: 101px">Hasta</td>
            <td style="height: 22px">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtHasta" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtHasta_CalendarExtender" format="yyyy-MM-dd" runat="server" TargetControlID="txtHasta" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 101px; height: 22px">
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
        <h5 class="modal-title" id="exampleModalLongTitle">Anulación de Empaquetados</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        desea anular el Empaquetado?
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
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="768px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                            <Columns>
                                <asp:BoundField DataField="eCod" HeaderText="Código" />
                                <asp:BoundField DataField="eFch" HeaderText="Fecha" />
                                <asp:BoundField DataField="NA" HeaderText="Anterior" />
                                <asp:BoundField DataField="eCant" HeaderText="Cant Ant" />
                                <asp:BoundField DataField="NB" HeaderText="Empaquetado" />
                                <asp:BoundField DataField="eCantEmp" HeaderText="Cant Emp" />
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
