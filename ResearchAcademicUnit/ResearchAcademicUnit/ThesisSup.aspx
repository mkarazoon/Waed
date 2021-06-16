<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="ThesisSup.aspx.cs" Inherits="ResearchAcademicUnit.ThesisSup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 33px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="div" id="ResearchDiv" runat="server">
                <div class="div">
                    <div class="sec">
                        <asp:Label ID="Label1" CssClass="lblTitle" runat="server" Text="إضافة رسالة"></asp:Label>
                    </div>
                    <table dir="rtl" style="width: 100%">
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="Label28" runat="server" Text="عنوان الرسالة-عربي"></asp:Label></td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtThTitleA" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtThTitleA" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="Label9" runat="server" Text="عنوان الرسالة-انجليزي"></asp:Label></td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtThTitleE" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtThTitleE" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="لغة الرسالة"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlLang" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد لغة الرسالة</asp:ListItem>
                                    <asp:ListItem Value="1">عربي</asp:ListItem>
                                    <asp:ListItem Value="2">انجليزي</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLang" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="الحالة"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPubStatus" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">الحالة</asp:ListItem>
                                    <asp:ListItem Value="1">مناقش</asp:ListItem>
                                    <asp:ListItem Value="2">مشرف منفرد</asp:ListItem>
                                    <asp:ListItem Value="3">مشرف رئيسي</asp:ListItem>
                                    <asp:ListItem Value="4">مشرف مشارك</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save" runat="server" ControlToValidate="ddlPubStatus" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="الجامعة"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtUni" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtUni" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="العام الجامعي"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtAcdYear" runat="server" placeholder="مثال:2019-2020"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtAcdYear" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="اسم الطالب-اختياري"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtStudName" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="save" runat="server" ControlToValidate="txtStudName" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة" ValidationGroup="save">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                                </asp:LinkButton>

                                <%--                        <asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="+" CssClass="btn1" OnClick="btnTrainSave_Click" />--%>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div class="sec">
                            <asp:Label ID="Label13" runat="server" Text="الرسائل العلمية المضافة"></asp:Label>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Serial" HeaderText="م" />
                                <asp:BoundField DataField="ThesisTitleA" HeaderText="عنوان الرسالة-عربي" />
                                <asp:BoundField DataField="ThesisTitleE" HeaderText="عنوان الرسالة-انجليزي" />
                                <asp:BoundField DataField="ThesisLangInt" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ThesisLang" HeaderText="لغة الرسالة" />
                                <asp:BoundField DataField="University" HeaderText="الجامعة" />
                                <asp:BoundField DataField="AcdYear" HeaderText="العام الجامعي" />
                                <asp:BoundField DataField="typeint" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="typeName" HeaderText="الحالة" />
                                <asp:BoundField DataField="StudName" HeaderText="اسم الطالب" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false">حذف</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" OnClick="lnkUpdate_Click" runat="server" CausesValidation="false">تعديل</asp:LinkButton>
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
