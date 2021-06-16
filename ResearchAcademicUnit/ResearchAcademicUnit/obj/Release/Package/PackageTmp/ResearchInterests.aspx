<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="ResearchInterests.aspx.cs" Inherits="ResearchAcademicUnit.ResearchInterests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table, th, td {
            padding: 5px;
        }

        table {
            border-spacing: 10px;
            width: 100%;
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
                        <asp:Label ID="Label1" runat="server" Text="إضافة اهتمام بحثي ( على الأقل واحد)"></asp:Label>
                    </div>
                    <table dir="rtl">
                        <tr>
                            <td>
                                <asp:Label ID="lblInterestErr" runat="server" Text="يجب تعبئة اهتمام واحد على الأقل" ForeColor="Red" Visible="false"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label28" runat="server" Text="القطاع البحثي"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label29" runat="server" Text="المجال البحثي"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label27" runat="server" Text="الاهتمام البحثي"></asp:Label></td>
                        </tr>
                        <tr>

                            <td>
                                <asp:DropDownList ID="ddlRSector" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlRSector_SelectedIndexChanged">
                                    <asp:ListItem Value="0">حدد القطاع المعرفي</asp:ListItem>
                                    <asp:ListItem Value="1">العلوم الاجتماعية والانسانية</asp:ListItem>
                                    <asp:ListItem Value="2">العلوم الأساسية</asp:ListItem>
                                    <asp:ListItem Value="3">العلوم الحياتية</asp:ListItem>
                                    <asp:ListItem Value="4">العلوم الصحية</asp:ListItem>
                                    <asp:ListItem Value="5">العلوم الهندسية</asp:ListItem>
                                    <asp:ListItem Value="6">تخصصات متعددة</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlRSector" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="care"></asp:RequiredFieldValidator>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlField" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlField" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="care"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtRInter" runat="server" Width="95%" placeholder="مثال: الاهتمام البحثي الاول ; الاهتمام البحثي الثاني ; ..."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" ControlToValidate="txtRInter" Display="Dynamic" ForeColor="Red" ValidationGroup="care"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة" ValidationGroup="care">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                                </asp:LinkButton>

                                <%--<asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div class="sec">
                            <asp:Label ID="Label13" runat="server" Text="الاهتمامات البحثية المضافة"></asp:Label>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="grd">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="RSector" HeaderText="القطاع البحثي" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="RSectorN" HeaderText="القطاع البحثي" />
                                <asp:BoundField DataField="RField" HeaderText="القطاع البحثي" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="RFieldN" HeaderText="المجال البحثي" />
                                <asp:BoundField DataField="RInterest" HeaderText="الاهتمام البحثي" />
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                                        <%--</ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="2%" ItemStyle-Width="2%">
                            <ItemTemplate>--%>
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
                    <div class="sec">
                        <asp:Label ID="Label2" runat="server" Text="الاحتياجات التدريبية المطلوبة في المجال البحثي ( على الأقل واحد)"></asp:Label>
                    </div>
                    <table dir="rtl" style="width: 100%">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlField1" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد مجال الاحتياجات التدريبية</asp:ListItem>
                                    <asp:ListItem Value="1">التعريفي</asp:ListItem>
                                    <asp:ListItem Value="2">النشر البحثي</asp:ListItem>
                                    <asp:ListItem Value="3">التوثيق</asp:ListItem>
                                    <asp:ListItem Value="4">القراءة الاحصائية والتحليل</asp:ListItem>
                                    <asp:ListItem Value="5">منهجية البحث العلمي</asp:ListItem>
                                    <asp:ListItem Value="6">التقني</asp:ListItem>
                                    <asp:ListItem Value="7">أخرى</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlField1" Display="Dynamic" ForeColor="Red" ValidationGroup="need" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNeed1" runat="server" placeholder="اكتب هنا احدى الاحتياجات التدريبية وفقا للمجال" Width="95%" Style="text-align: right"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtNeed1" Display="Dynamic" ForeColor="Red" ValidationGroup="need"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUpdateNeed" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnAddNeeded_Click" ToolTip="إضافة" ValidationGroup="need">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                                </asp:LinkButton>

                                <%--<asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                            </td>
                        </tr>

                        <%--                            <td style="text-align: left">
                                <asp:Button ID="btnAddNeeded" runat="server" Text="إضافة" Width="50px" OnClick="btnAddNeeded_Click" CssClass="btn" ValidationGroup="need" /></td>
                        </tr>--%>
                    </table>
                    <div>
                        <div class="sec">
                            <asp:Label ID="Label3" runat="server" Text="الاحتياجات البحثية المضافة"></asp:Label>
                        </div>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="NeedFieldI" HeaderText="القطاع البحثي" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="NeedFieldS" HeaderText="المجال البحثي" />
                                <asp:BoundField DataField="NeedText" HeaderText="الاحتياج البحثي" />
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                                        <%--</ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="2%" ItemStyle-Width="2%">
                            <ItemTemplate>--%>
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
                    <div id="hhh" runat="server" visible="false">
                        <div class="sec">
                            <asp:Label ID="Label4" runat="server" Text="عناوين مقترحة لرسائل بحثية ( على الأقل واحد)"></asp:Label>
                        </div>
                        <table dir="rtl" style="width: 100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtThesisTitle" runat="server" placeholder="اكتب عنوان مقترح لرسالة جامعية" Width="95%" Style="text-align: right"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtThesisTitle" Display="Dynamic" ForeColor="Red" ValidationGroup="thesis"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblUpdateThesis" runat="server" Text="0" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnAddthesis_Click" ToolTip="إضافة">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                                    </asp:LinkButton>

                                    <%--<asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                                </td>
                            </tr>

                            <%--                            <td style="text-align: left">
                                <asp:Button ID="btnAddthesis" runat="server" Text="إضافة" Width="50px" OnClick="btnAddthesis_Click" CssClass="btn" ValidationGroup="thesis" /></td>
                        </tr>--%>
                        </table>
                        <div>
                            <div class="sec">
                                <asp:Label ID="Label6" runat="server" Text="عناوين الرسائل المضافة"></asp:Label>
                            </div>
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="ThesisTitle" HeaderText="عنوان الرسالة" />
                                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                                            <%--</ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="2%" ItemStyle-Width="2%">
                            <ItemTemplate>--%>
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
