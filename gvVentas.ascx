<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gvVentas.ascx.vb" Inherits="hidrobio.gvVentas" %>
 <td style="height: 22px" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True" Width="885px">
                                                <Columns>
                                                    <asp:BoundField DataField="Linea" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                                    <asp:BoundField DataField="Stock" DataFormatString="{0:N2}" HeaderText="Stock" />
                                                    <asp:TemplateField HeaderText="Cantidad">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvCant" runat="server" AutoPostBack="True" onfocus="Javascript:this.focus();this.select();" OnTextChanged="txtgvCant_TextChanged" Text='<%# Bind("Cantidad") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unitario">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvUnit" runat="server" AutoPostBack="True" onfocus="Javascript:this.focus();this.select();" OnTextChanged="txtgvUnit_TextChanged" Text='<%# Bind("Unitario") %>' Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Impuesto">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlImpuesto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlImpuesto_SelectedIndexChanged" SelectedValue='<%#Eval("Impuesto") %>'>
                                                                <asp:ListItem Value="5">5%</asp:ListItem>
                                                                <asp:ListItem Value="10">10%</asp:ListItem>
                                                                <asp:ListItem Value="0">Exenta</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Excenta" DataFormatString="{0:N0}" HeaderText="Exentas" />
                                                    <asp:BoundField DataField="5" DataFormatString="{0:N0}" HeaderText="5%" />
                                                    <asp:BoundField DataField="10" DataFormatString="{0:N0}" HeaderText="10%" />
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