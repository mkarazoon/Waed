<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="WorkExperience.aspx.cs" Inherits="ResearchAcademicUnit.WorkExperience" %>

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
                        <asp:Label ID="Label1" runat="server" Text="الخبرات العملية"></asp:Label>
                    </div>
                    <table dir="rtl" style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="Label28" runat="server" Text="مكان العمل"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtWorkPlace" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtWorkPlace" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="الدولة"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save" runat="server" ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label29" runat="server" Text="وصف الوظيفة"></asp:Label>
                            </td>
                            <td class="auto-style1" colspan="3">
                                <asp:TextBox ID="txtWorkDesc" runat="server" Width="95%" TextMode="MultiLine" Rows="10"></asp:TextBox>
<%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtWorkDesc" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="من تاريخ"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" Width="95%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="save" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="الى تاريخ"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server" Width="95%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="save" runat="server" ControlToValidate="txtToDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="الحالة الوظيفية"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCType" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">اختيار</asp:ListItem>
                                    <asp:ListItem Value="1">استقالة</asp:ListItem>
                                    <asp:ListItem Value="2">استغناء عن خدمات</asp:ListItem>
                                    <asp:ListItem Value="3">على رأس عمله</asp:ListItem>
                                    <asp:ListItem Value="4">بلوغ السن القانونية للتقاعد</asp:ListItem>
                                </asp:DropDownList>
<%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlCType" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة"  ValidationGroup="save">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div class="sec">
                            <asp:Label ID="Label13" runat="server" Text="الخبرات العملية المضافة"></asp:Label>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%"
                            EmptyDataText="لا يوجد معلومات حاليا">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="5%" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="JobPlace" HeaderText="مكان العمل" />
                                <asp:BoundField DataField="JobCountryId" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="JobCountry" HeaderText="الدولة" />
                                <asp:BoundField DataField="JobDesc" HeaderText="وصف الوظيفة" />
                                <asp:BoundField DataField="FromDate" HeaderText="من تاريخ" />
                                <asp:BoundField DataField="toDate" HeaderText="الى تاريخ"/>
                                <asp:BoundField DataField="LeaveReasonInt" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="LeaveReason" HeaderText="الحالة الوظيفية" />
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkUpdate" OnClick="lnkUpdate_Click" runat="server" CausesValidation="false" ToolTip="تعديل" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">&#xE254;</i></asp:LinkButton>
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
            $('.ChosenSelector1').chosen({ width: "30%" });

        }
    </script>
</asp:Content>
