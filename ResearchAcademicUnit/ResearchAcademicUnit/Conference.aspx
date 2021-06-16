<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="Conference.aspx.cs" Inherits="ResearchAcademicUnit.Conference" %>

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
        <%--<asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>

    <div class="div" id="ResearchDiv" runat="server">
        <div class="div">
                    <div class="sec">
                        <asp:Label ID="Label1" runat="server" Text="إضافة مؤتمر"></asp:Label>
                    </div>
            <table dir="rtl" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblUpdate" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="Label36" runat="server" Text="نوع التمثيل بالمؤتمر"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCType" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlCType_SelectedIndexChanged">
                            <asp:ListItem Value="0">نوع التمثيل</asp:ListItem>
                            <asp:ListItem Value="1">حضور</asp:ListItem>
                            <asp:ListItem Value="2">مشاركة</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ValidationGroup="save" runat="server" ControlToValidate="ddlCType" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="عنوان المشاركة - الورقة"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtRTitle" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtRTitle" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label29" runat="server" Text="اسم المؤتمر"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtCName" runat="server" Width="95%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCName" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label31" runat="server" Text="مجلة النشر"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMagId" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtMagId" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label32" runat="server" Text="نوع المؤتمر"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCPlace" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlCPlace_SelectedIndexChanged">
                            <asp:ListItem Value="0">حدد نوع المؤتمر</asp:ListItem>
                            <asp:ListItem Value="1">مؤتمرات جامعة الشرق الأوسط</asp:ListItem>
                            <asp:ListItem Value="2">محلي</asp:ListItem>
                            <asp:ListItem Value="3">عربي</asp:ListItem>
                            <asp:ListItem Value="4">دولي</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ValidationGroup="save" runat="server" ControlToValidate="ddlCPlace" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:RequiredFieldValidator>
                        <div id="CountryDiv" runat="server" visible="false" style="margin:5px">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                            </div>
                    </td>
                    <td>
                        <asp:Label ID="Label30" runat="server" Text="قاعدة البيانات"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDBType" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlDBType_SelectedIndexChanged">
                            <asp:ListItem Value="0">حدد قاعدة البيانات</asp:ListItem>
                            <asp:ListItem Value="1">مجلة علمية محكمة</asp:ListItem>
                            <asp:ListItem Value="2">مجلة علمية محكمة مصنفة عالميا</asp:ListItem>
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlDBType" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                        <div id="dbInfoDiv" runat="server" visible="false">
                            <asp:TextBox ID="txtJorMag" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtJorMag" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                            <div class="tooltip">
                                <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                                <span class="tooltiptext">ملاحظة: أن تصدر عن مؤسسة علمية أو المجلات الأردنية المدعومة من صندوق البحث العلمي الأردني</span>
                            </div>
                        </div>
                        <div id="globaldbDiv" runat="server" visible="false">
                            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" CssClass="ChosenSelector1" Style="width: 100%">
                                <asp:ListItem Value="1">Science Citation Index - SCI</asp:ListItem>
                                <asp:ListItem Value="2">Science Citation Index Extended - SCIE</asp:ListItem>
                                <asp:ListItem Value="3">Thomson Reuters</asp:ListItem>
                                <asp:ListItem Value="4">Scopus</asp:ListItem>
                                <asp:ListItem Value="5">ERA</asp:ListItem>
                                <asp:ListItem Value="6">EBSCO Abstract</asp:ListItem>
                                <asp:ListItem Value="7">EconLit</asp:ListItem>
                            </asp:ListBox>

                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlDB" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                        </div>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="سنة النشر"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="ChosenSelector">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="save" runat="server" ControlToValidate="ddlFYear" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="الشهر"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ChosenSelector">
                            <asp:ListItem Value="0">حدد الشهر</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="save" runat="server" ControlToValidate="ddlMonth" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="حالة النشر"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPubStatus" runat="server" CssClass="ChosenSelector">
                            <asp:ListItem Value="0">الحالة</asp:ListItem>
                            <asp:ListItem Value="1">محلي (داخل الاردن)</asp:ListItem>
                            <asp:ListItem Value="2">اقليمي (دول عربية)</asp:ListItem>
                            <asp:ListItem Value="3">دولي</asp:ListItem>
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save" runat="server" ControlToValidate="ddlPubStatus" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        <asp:Label ID="Label33" runat="server" Text="ترتيب الباحث"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrder" runat="server" CssClass="ChosenSelector">
                            <asp:ListItem Value="0">حدد ترتيب الباحث</asp:ListItem>
                            <asp:ListItem Value="1">باحث رئيسي</asp:ListItem>
                            <asp:ListItem Value="2">باحث فردي</asp:ListItem>
                            <asp:ListItem Value="3">باحث ثاني</asp:ListItem>
                            <asp:ListItem Value="4">باحث ثالث واكثر</asp:ListItem>
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ValidationGroup="save" runat="server" ControlToValidate="ddlOrder" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label34" runat="server" Text="عدد الباحثين &lt;br&gt;المشاركين الكلي"></asp:Label>
                        <div class="tooltip">
                            <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                            <span class="tooltiptext">ممن فيهم الباحث نفسه</span>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAllR" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlAllR_SelectedIndexChanged">
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ValidationGroup="save" runat="server" ControlToValidate="ddlAllR" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>
                        <asp:Label ID="Label35" runat="server" Text="عدد الباحثين من&lt;br&gt; داخل الجامعة"></asp:Label>
                        <div class="tooltip">
                            <asp:Image ID="Image2" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                            <span class="tooltiptext">ممن فيهم الباحث نفسه</span>
                        </div>


                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInR" runat="server" CssClass="ChosenSelector">
                        </asp:DropDownList>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="save" runat="server" ControlToValidate="ddlInR" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
<tr>
                    <td>
                        <label style="cursor: pointer">
                <div class="divFile" style="background-color:lightgray">
                    <div class="titlebg">
                        <asp:Label ID="Label9" runat="server" Text="تحميل البحث"></asp:Label>
                    </div>

                    <span><strong>اختيار الملف</strong></span>
                    <asp:FileUpload ID="fluRScopus" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluRScopus" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="save" runat="server" ControlToValidate="fluRScopus" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblUp1" runat="server" Text=""></asp:Label>
                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image4" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">نسخة من البحث - PDF  فقط
                    </span>
                </div>
                    
                    </center>
                </div>
            </label>
                    </td>
                </tr>
                <tr>
                    <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnTrainSave_Click" ToolTip="إضافة" ValidationGroup="save">
                                    <i class="material-icons" style="color: #27C46B;border:1px solid;padding:5px;border-radius:10px;background-color:#27C46B;color:white">إضافة جديد &#xE03B;</i>
                                </asp:LinkButton>

                        <%--<asp:Button ID="btnTrainSave" runat="server" Text="إضافة" CssClass="btn" OnClick="btnTrainSave_Click" />--%>
                    </td>
                </tr>
            </table>
            <div>
                        <div class="sec">
                            <asp:Label ID="Label13"  runat="server" Text="المؤتمرات المضافة"></asp:Label>
                        </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%"
                    OnDataBound="GridView1_DataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="AutoId" HeaderText="AutoId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Serial" HeaderText="م" HeaderStyle-Width="5%" ItemStyle-Width="5%"/>
                        <asp:BoundField DataField="CTitle" HeaderText="اسم المؤتمر" />
                        <asp:BoundField DataField="CTypeI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CTypeS" HeaderText="نوع التمثيل في المؤتمر" />
                        <asp:BoundField DataField="PaperTitle" HeaderText="عنوان المشاركة - الورقة" />
                        <asp:BoundField DataField="MagName" HeaderText="مجلة المشاركة" />
                        <asp:BoundField DataField="PubStatusI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="PubStatusS" HeaderText="حالة النشر" />
                        <asp:BoundField DataField="DBTypeI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="DBTypeS" HeaderText="المجلة المحكمة" />
                        <asp:BoundField DataField="GlobalDBI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="قاعدة البيانات">
                            <ItemTemplate>
                                <asp:Label ID="lblRName" runat="server" Text='<%# Eval("JorDB") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PYear" HeaderText="السنة" />
                        <asp:BoundField DataField="PMonth" HeaderText="الشهر" />
                        <asp:BoundField DataField="CPlaceI" HeaderText=""  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CPlaceS" HeaderText="نوع المؤتمر" />
                        <asp:BoundField DataField="CountryI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CountryS" HeaderText="مكان المؤتمر" />
                        <asp:BoundField DataField="ROrderI" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ROrderS" HeaderText="ترتيب الباحث" />
                        <asp:BoundField DataField="AllR" HeaderText="عدد الباحثين الخارجي" />
                        <asp:BoundField DataField="InR" HeaderText="عدد الباحثين الداخلي" />
                        <asp:BoundField DataField="JorDB" HeaderText="الشهر" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="filepath" HeaderText="مسار الملف" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  />
                        <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" style="margin:0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkUpdate" OnClick="lnkUpdate_Click" runat="server" CausesValidation="false" ToolTip="تعديل" style="margin:0 5px"><i class="material-icons" style="color: #FFC107">&#xE254;</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server" OnClientClick="SetTarget();" CausesValidation="false" ToolTip="عرض" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">open_in_new</i></asp:LinkButton>
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
<%--</ContentTemplate>
            </asp:UpdatePanel>--%>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "80%" });
            $('.ChosenSelector1').chosen({ width: "30%" });

        }
    </script>
</asp:Content>
