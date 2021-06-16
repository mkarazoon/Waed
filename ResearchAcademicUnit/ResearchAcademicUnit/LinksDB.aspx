<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="LinksDB.aspx.cs" Inherits="ResearchAcademicUnit.LinksDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
table, th, td {
  padding: 5px;
}
table {
  border-spacing: 10px;
}
        </style>
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>

            <div class="div" id="ResearchDiv" runat="server">
                <div class="div">
                            <div class="sec">
                                <asp:Label ID="Label1"  runat="server" Text="إضافة قاعدة بيانات"></asp:Label>
                            </div>
                    <table dir="rtl" style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label28" runat="server" Text="قاعدة البيانات"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label27" runat="server" Text="الرابط"></asp:Label></td>
                        </tr>
                        <tr>

                            <td>
                                <asp:DropDownList ID="ddlRSector" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد قاعدة البيانات</asp:ListItem>
                                    <asp:ListItem Value="1">Google Scholar</asp:ListItem>
                                    <asp:ListItem Value="2">Research Gate</asp:ListItem>
                                    <asp:ListItem Value="3">ORCID</asp:ListItem>
                                    <asp:ListItem Value="4">RESN</asp:ListItem>
                                    <asp:ListItem Value="5">Research rid</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="save" runat="server" ControlToValidate="ddlRSector" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                            </td>
                            <td>
                                <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtLink" runat="server" Style="text-align: left; direction: ltr" Width="95%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" ControlToValidate="txtLink" Display="Dynamic" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة" ValidationGroup="save" >
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                        </asp:LinkButton>

                        <%--<asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                    </td>
                </tr>
                    </table>
                    <div>
                                <div class="sec">
                                    <asp:Label ID="Label13" runat="server" Text="قواعد البيانات والروابط المضافة"></asp:Label>
                                </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="10%" ItemStyle-Width="10%"/>
                                <asp:BoundField DataField="DBNameS" HeaderText="اسم قاعدة البيانات" />
                                <asp:BoundField DataField="DBNameI" HeaderText="رقم قاعدة البيانات" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                
                                <asp:TemplateField HeaderText="الرابط" >
                                    <ItemTemplate>
                                        <a href='<%# Eval("LinkName") %>' target="_blank"><%# Eval("LinkName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" style="margin:0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                            <%--</ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="2%" ItemStyle-Width="2%">
                            <ItemTemplate>--%>
                                <asp:LinkButton ID="lnkUpdate" OnClick="lnkUpdate_Click" runat="server" CausesValidation="false" ToolTip="تعديل" style="margin:0 5px"><i class="material-icons" style="color: #FFC107">&#xE254;</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="sec" style="text-align: center">
                    <asp:Button ID="btnPrev" runat="server" CausesValidation="False" CssClass="btn" Height="40px" OnClick="btnPrev_Click" Text="السابق" Width="100px" />
                    <asp:Button ID="btnSaveCert" runat="server" Text="متابعة" CssClass="btn" Width="100px" Height="40px" OnClick="btnSaveCert_Click" CausesValidation="False" Visible="False" />
                    <asp:Button ID="btnNext" runat="server" CausesValidation="False" CssClass="btn" Height="40px" OnClick="btnNext_Click" Text="التالي" Width="100px" />
                </div>
            </div>
            <div class="divTitle">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <center>
                    <asp:Label runat="server" Visible="false" Text="تم التخزين بنجاح" CssClass="lblMsg" ID="lblMsg"></asp:Label>
                    <asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False" ></asp:Timer>
                        </center>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "80%" });
        }
    </script>

</asp:Content>
