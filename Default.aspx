<%@ Page Title="Principal" Language="VB" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="hidrobio._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width:100%;">
        <tr>
            <td class="text-white" style="text-align: center; background-color: #666666"><strong>Lechugas</strong></td>
        </tr>
        <tr>
            <td class="text-white" style="text-align: center; background-color: #FFFFFF">
                            <asp:GridView ID="gvStockLechugas1" runat="server" AutoGenerateColumns="False" CellPadding="3" style="font-size: small" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CssClass="bg-white" Width="688px">
                                <AlternatingRowStyle BorderColor="#666666" />
                                <Columns>
                                    <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                    <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
<asp:BoundField HeaderText="Inicial" DataField="inicial"></asp:BoundField>
                                    <asp:BoundField DataField="merma" HeaderText="Merma" />
                                    <asp:BoundField HeaderText="Ventas Depósito" DataField="Deposito" />
                                    <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                    <asp:BoundField HeaderText="Ventas Reparto" DataField="Reparto" />
                                </Columns>
                                <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
                        </td>
        </tr>
        <tr>
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Verdeos</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvStockVerdeos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CellPadding="3" CssClass="bg-white" style="font-size: small" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField DataField="inicial" HeaderText="Inicial" />
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField DataField="Deposito" HeaderText="Ventas Depósito" />
                                <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField DataField="Reparto" HeaderText="Ventas Reparto" />
                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Locotes</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvLocotes" runat="server" AutoGenerateColumns="False" CellPadding="3" style="font-size: small" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CssClass="bg-white" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField HeaderText="Inicial" DataField="inicial"></asp:BoundField>
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField HeaderText="Ventas Depósito" DataField="Deposito" />
                                <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField HeaderText="Ventas Reparto" DataField="Reparto" />
                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Tomates</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTomate" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CellPadding="3" CssClass="bg-white" style="font-size: small" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField DataField="inicial" HeaderText="Inicial" />
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField DataField="Deposito" HeaderText="Ventas Depósito" />
                                <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField DataField="Reparto" HeaderText="Ventas Reparto" />
                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Frescos</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvFrescos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CellPadding="3" CssClass="bg-white" style="font-size: small" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField DataField="inicial" HeaderText="Inicial" />
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField DataField="Deposito" HeaderText="Ventas Depósito" />
                                <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField DataField="Reparto" HeaderText="Ventas Reparto" />
                                
                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Frutas</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvFrutas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CellPadding="3" CssClass="bg-white" style="font-size: small" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField DataField="inicial" HeaderText="Inicial" />
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField DataField="Deposito" HeaderText="Ventas Depósito" />
                                 <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField DataField="Reparto" HeaderText="Ventas Reparto" />
                               
                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Curcubitáceas</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvCurcu" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CellPadding="3" CssClass="bg-white" style="font-size: small" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField DataField="inicial" HeaderText="Inicial" />
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField DataField="Deposito" HeaderText="Ventas Depósito" />
                                <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField DataField="Reparto" HeaderText="Ventas Reparto" />
                                                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
            <td style="background-color: #666666 !important; height: 26px; text-align: center;" class="bg-white">
                <span style="font-weight: bold" class="text-white">Otros</span></td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" class="bg-white">
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvOtros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#666666" BorderStyle="None" BorderWidth="0px" CellPadding="3" CssClass="bg-white" style="font-size: small" Width="688px">
                            <AlternatingRowStyle BorderColor="#666666" />
                            <Columns>
                                <asp:BoundField DataField="prodCod" HeaderText="Cód" />
                                <asp:BoundField DataField="prodNombre" HeaderText="Producto" />
                                <asp:BoundField DataField="empaNombre" HeaderText="Empaque" />
                                <asp:BoundField DataField="inicial" HeaderText="Inicial" />
                                <asp:BoundField DataField="merma" HeaderText="Merma" />
                                <asp:BoundField DataField="Deposito" HeaderText="Ventas Depósito" />
                                 <asp:BoundField DataField="actual" HeaderText="Stock Actual" />
                                <asp:BoundField DataField="Reparto" HeaderText="Ventas Reparto" />
                               
                            </Columns>
                            <EditRowStyle BorderColor="#666666" BorderWidth="1px" />
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
        </table>

</asp:Content>
