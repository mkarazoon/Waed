<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="EvaluationExp.aspx.cs" Inherits="ResearchAcademicUnit.EvaluationExp" %>

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
    <%--    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>

    <div class="div" id="ResearchDiv" runat="server">
        <div class="div">
            <div class="sec">
                <asp:Label ID="Label1" runat="server" Text="إضافة خبرات"></asp:Label>
            </div>
            <table dir="rtl" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="Label28" runat="server" Text="الدور"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="ChosenSelector"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                            <asp:ListItem Value="0">حدد الدور</asp:ListItem>
                            <asp:ListItem Value="1">رئيس هيئة تحرير</asp:ListItem>
                            <asp:ListItem Value="2">عضو هيئة تحرير</asp:ListItem>
                            <asp:ListItem Value="3">محكًم</asp:ListItem>
                            <asp:ListItem Value="4">أخرى</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlRole" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>
                        <div runat="server" id="oRoleDiv" visible="false">
                            <asp:TextBox ID="txtORole" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtORole" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>

                    </td>
                    <td>

                        <asp:Label ID="Label3" runat="server" Text="المجلة"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtMgz" runat="server" Width="95%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtMgz" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="قواعد البيانات"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlDBType" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlDBType_SelectedIndexChanged">
                            <asp:ListItem Value="0">حدد قاعدة البيانات</asp:ListItem>
                            <asp:ListItem Value="1">مجلة علمية محكمة</asp:ListItem>
                            <asp:ListItem Value="2">مجلة علمية محكمة مصنفة عالميا</asp:ListItem>
                        </asp:DropDownList>
                        <div id="dbDiv" runat="server" visible="false">
                            <asp:TextBox ID="txtDBmgz" runat="server"></asp:TextBox>
                            <div class="tooltip">
                                <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                                <span class="tooltiptext">ملاحظة: أن تصدر عن مؤسسة علمية أو المجلات الأردنية المدعومة من صندوق البحث العلمي الأردني</span>
                            </div>
                        </div>
                        <asp:DropDownList ID="ddlDB" runat="server" CssClass="ChosenSelector" Visible="false">
                            <asp:ListItem Value="0">حدد قاعدة البيانات</asp:ListItem>
                            <asp:ListItem Value="1">Science Citation Index - SCI</asp:ListItem>
                            <asp:ListItem Value="2">Science Citation Index Extended - SCIE</asp:ListItem>
                            <asp:ListItem Value="3">Thomson Reuters</asp:ListItem>
                            <asp:ListItem Value="4">Scopus</asp:ListItem>
                            <asp:ListItem Value="5">ERA</asp:ListItem>
                            <asp:ListItem Value="6">EBSCO Abstract</asp:ListItem>
                            <asp:ListItem Value="7">EconLit</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="save" runat="server" ControlToValidate="ddlDB" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="الدولة"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ChosenSelector">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save" runat="server" ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="الفترة بالسنوات"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="ChosenSelector">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="save" runat="server" ControlToValidate="ddlFYear" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="إلى"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlTYear" runat="server" CssClass="ChosenSelector">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="save" runat="server" ControlToValidate="ddlTYear" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة"  ValidationGroup="save">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                        </asp:LinkButton>

                        <%--<asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                    </td>
                </tr>
            </table>
            <%--                    <div class="divTitle" style="text-align: center">
                    </div>--%>
            <div>
                <div class="sec">
                    <asp:Label ID="Label13" runat="server" Text="خبرات التحكيم والتقييم المضافة"></asp:Label>
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="RoleNameI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="الدور">
                            <ItemTemplate>
                                <asp:Label ID="lblDB" runat="server"><%#(Eval("RoleNameI").ToString()=="4"?Eval("RoleNameS") + "-"+ Eval("OtherRole"):Eval("RoleNameS") ) %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CommitteeName" HeaderText="اللجنة" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="MagazineName" HeaderText="المجلة" />
                        <asp:BoundField DataField="DBNameI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <%--                                <asp:BoundField DataField="DBNameS" HeaderText="نوع قاعدة البيانات" />--%>
                        <asp:TemplateField HeaderText="نوع قاعدة البيانات">
                            <ItemTemplate>
                                <asp:Label ID="lblRName" runat="server"><%#(Eval("DBNameI").ToString()=="1"?Eval("DBNameS") + "-"+ Eval("MgzText"):Eval("DBNameS") + "-"+ Eval("DbListS")) %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CountryI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                        <asp:BoundField DataField="FYear" HeaderText="من" />
                        <asp:BoundField DataField="TYear" HeaderText="إلى" />
                        <asp:BoundField DataField="OtherRole" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="MgzText" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="DbListI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="DbListS" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
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
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "80%" });
        }
    </script>
</asp:Content>
