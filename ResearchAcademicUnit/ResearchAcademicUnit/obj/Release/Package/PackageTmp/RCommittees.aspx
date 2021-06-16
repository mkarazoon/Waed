<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="RCommittees.aspx.cs" Inherits="ResearchAcademicUnit.RCommittees" %>

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
                    <div class="smalltitle">
                        <asp:Label ID="Label1" CssClass="lblTitle" runat="server" Text="إضافة عضوية"></asp:Label>
                    </div>
                    <table dir="rtl" style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="Label33" runat="server" Text="اسم الجهة ( الجمعية ) البحثية"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="نوع العضوية"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlActType" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد...</asp:ListItem>
                                    <asp:ListItem Value="1">عضو</asp:ListItem>
                                    <asp:ListItem Value="2">رئيس لجنة</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlActType" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label29" runat="server" Text="تاريخ الحصول على العضوية"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:UpdatePanel ID="UpFromDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDate" runat="server" placeholder="dd-mm-yyyy" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" CausesValidation="true"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="تاريخ خطأ" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="save"></asp:CompareValidator>
                                        <asp:Label ID="lblFDateError" runat="server" Text="يجب أن يبدأ التاريخ من 01-01-2014" ForeColor="Red" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="الدولة"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                            </td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="ملاحظات"></asp:Label>
                            </td>
                            <td> <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="5" Columns="40"></asp:TextBox></td>
                            <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة" ValidationGroup="save">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                        </asp:LinkButton>

                        <%--<asp:Button ID="btnTrainSave" runat="server" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                    </td>
                </tr>
                    </table>
                    <div>
                        <div class="sec">
                            <asp:Label ID="Label13" runat="server" Text="الجهات المضافة"></asp:Label>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="3%" ItemStyle-Width="3%" />
                                <asp:BoundField DataField="CommName" HeaderText="اسم الجهة البحثية" />
                                <asp:BoundField DataField="TypeI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="TypeS" HeaderText="نوع العضوية" />
                                <asp:BoundField DataField="CommDate" HeaderText="تاريخ العضوية" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField DataField="CountryI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                <asp:BoundField DataField="Notes" HeaderText="ملاحظات" />
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
                <div class="sec">
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
