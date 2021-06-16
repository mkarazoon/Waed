<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.Master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="ResearchAcademicUnit.PersonalInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
table, th, td {
  padding: 5px;
}
table {
  border-spacing: 10px;
}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Bootstrap -->

    <%--<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
    <%--            <div runat="server" class="img">
                <asp:Image runat="server" ID="imgF" ImageUrl="~/images/default-avatar.jpg" Width="200px" Height="200px" ImageAlign="Middle" style="border-radius:10px"/>
                <br />
                <label style="font-size: small;color:blue;cursor:pointer">
                    <span><strong><u>تحميل الصورة الشخصية</u></strong></span>
                    <asp:FileUpload ID="flCV" runat="server" onchange="this.form.submit()" Style="display: none" />
                    <asp:Label ID="lblImageName" runat="server" Text="Label" Visible="false"></asp:Label>
                </label>
            </div>--%>
    <div class="div" id="PersonalInfoDiv" runat="server">
        <article style="width:98%">
            
            <div class="sec">
                <asp:Label ID="Label79" CssClass="lblTitle" runat="server" Text="المعلومات الشخصية"></asp:Label>
            </div>
            <div runat="server" class="img">
                <asp:Image runat="server" ID="imgF" ImageUrl="~/images/default-avatar.jpg" Width="200px" Height="200px" ImageAlign="Middle" style="border-radius:10px"/>
                <br />
                <label style="font-size: small;color:blue;cursor:pointer">
                    <span><strong><u>تحميل الصورة الشخصية</u></strong></span>
                    <asp:FileUpload ID="flCV" runat="server" onchange="this.form.submit()" Style="display: none" />
                    <asp:Label ID="lblImageName" runat="server" Text="Label" Visible="false"></asp:Label>
                </label>
            </div>
            <div style="color:red;">
                * يجب تعبئة جميع الحقول
            </div>
            <table style="width: 95%;" >
                <tr>
                    <td>
                        <asp:Label CssClass="label" ID="Label4" runat="server" Text="الاسم باللغة العربية"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtAName" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAName" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                    </td>

                    <td>
                        <asp:Label CssClass="label" ID="Label81" runat="server" Text="الاسم باللغة الانجليزية"></asp:Label>
                    </td>

                    <td>
                        <asp:TextBox ID="txtEName" runat="server" Style="text-align: left; direction: ltr"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtEName" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                    </td>

                    <td >
                        <asp:Label CssClass="label" ID="Label5" runat="server" Text="الاسم كما يكتب في النشر ضمن سكوبس"></asp:Label></td>
                    <td >
                        <asp:TextBox ID="txtSName" runat="server"  Style="text-align: left; direction: ltr" placeholder="ex. :  Smith, J"></asp:TextBox>
                        <div class="tooltip">
                            <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                            <span class="tooltiptext">اذا لم يكن لديك نشر بسكوبس بعد الرجاء كتابة لا يوجد</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label CssClass="label" ID="Label6" runat="server" Text="تاريخ الميلاد"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtBOD" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtBOD" Display="Dynamic" ErrorMessage="تاريخ خطأ" Font-Bold="False" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="PersonalInfo"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="txtBOD" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label CssClass="label" ID="Label7" runat="server" Text="الجنسية"></asp:Label>
                    </td>
                    <td colspan="2" >
                        <asp:DropDownList ID="ddlNat" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlNat" InitialValue="0" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label CssClass="label" ID="Label8" runat="server" Text="الجنس"></asp:Label></td>
                    <td >
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="ChosenSelector">
                            <asp:ListItem Value="0">اختار</asp:ListItem>
                            <asp:ListItem Value="M">ذكر</asp:ListItem>
                            <asp:ListItem Value="F">انثى</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGender" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                        <asp:Label CssClass="label" ID="Label9" runat="server" Text="العنوان"></asp:Label>
                    </td>
                    <td colspan="2" >
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" Columns="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                    </td>

                </tr>
                <tr>
                    <td style="margin-right: 40px">
                        <asp:Label CssClass="label" ID="Label10" runat="server" Text="البريد الإلكتروني"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Style="text-align: left; direction: ltr" placeholder="ex: ahmad@gmail.com"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="خطأ في البريد الإلكتروني" Font-Bold="False" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>

                    <td>
                        &nbsp;</td>

                    <td>
                        <asp:Label CssClass="label" ID="Label11" runat="server" Text="رقم الجوال"></asp:Label>
                    </td>

                    <td colspan="2">
                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)" Style="text-align: left; direction: ltr"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="خطأ في رقم الجوال" ForeColor="Red" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                </tr>
            </table>
        </article>


        <article style="width:98%">
            <div class="sec">
                <asp:Label ID="Label80" CssClass="lblTitle" runat="server" Text="المعلومات الوظيفية"></asp:Label>
            </div>
            <div style="color:red;">
                * يجب تعبئة جميع الحقول
            </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>

                    <table style="width: 95%">
                        <tr>
                            <td class="auto-style1">
                                <asp:Label CssClass="label" ID="Label2" runat="server" Text="الرقم الوظيفي"></asp:Label></td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtJobId" runat="server" MaxLength="5" onkeypress="return isNumberKey(event)" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="auto-style2">
                                <asp:Label CssClass="label" ID="Label3" runat="server" Text="المسمى الوظيفي"></asp:Label></td>
                            <td>
                                <asp:UpdatePanel ID="PositionPanel" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlPositionName" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlPositionName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">حدد المسمى الوظيفي</asp:ListItem>
                                            <asp:ListItem Value="1">عميد</asp:ListItem>
                                            <asp:ListItem Value="2">رئيس قسم</asp:ListItem>
                                            <asp:ListItem Value="3">عضو هيئة تدريس</asp:ListItem>
                                            <asp:ListItem Value="4">أخرى</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPositionName" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                        <div id="otherPosDiv" runat="server" visible="false">
                                            <asp:TextBox ID="txtOtherPos" runat="server" Width="80%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtOtherPos" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <td class="auto-style2">
                                        <asp:Label CssClass="label" ID="Label15" runat="server" Text="الكلية"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCollege" runat="server" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" CssClass="ChosenSelector">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCollege" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:Label CssClass="label" ID="Label16" runat="server" Text="القسم العلمي"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlDept" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                    </td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label CssClass="label" ID="Label14" runat="server" Text="الرتبة العلمية"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد الرتبة العلمية</asp:ListItem>
                                    <asp:ListItem Value="1">استاذ</asp:ListItem>
                                    <asp:ListItem Value="2">استاذ مشارك</asp:ListItem>
                                    <asp:ListItem Value="3">استاذ مساعد</asp:ListItem>
                                    <asp:ListItem Value="4">مدرس</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDegree" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label CssClass="label" ID="Label1" runat="server" Text="سنة الحصول على الرتبة"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDegreeYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label CssClass="label" ID="Label17" runat="server" Text="تاريخ التعيين بالجامعة"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHDate" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtHDate" Display="Dynamic" ErrorMessage="تاريخ خطأ" Font-Bold="False" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="PersonalInfo"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtHDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style2">
                                <asp:Label CssClass="label" ID="Label18" runat="server" Text="المؤهل العلمي"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlQual" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد المؤهل العلمي</asp:ListItem>
                                    <asp:ListItem Value="1">دكتوراة</asp:ListItem>
                                    <asp:ListItem Value="2">ماجستير</asp:ListItem>
                                    <asp:ListItem Value="3">بكالوريوس</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlQual" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="التخصص العام"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMajor" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMajor" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="التخصص الدقيق"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMinor" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtMinor" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <asp:UpdatePanel ID="RSectorPanel" runat="server">
                                <ContentTemplate>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="القطاع المعرفي"></asp:Label>
                                    </td>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlRSector" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="المجال المعرفي"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlField" runat="server" CssClass="ChosenSelector">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlField" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                    </td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </tr>
                        <tr>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="عدد سنوات الخبرة الكلي"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAllExpYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlAllExpYear" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="عدد سنوات الخبرة تحت مظلة الجامعة"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlUExp" runat="server" CssClass="ChosenSelector" ToolTip="ملاحظة: التعيين بالعام الحالي يختار 1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlUExp" Display="Dynamic" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" InitialValue="0" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                                    </td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </tr>
                    </table>


                    <div class="sec">
                        
                        <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn" OnClick="btnSave_Click" Width="150px" Height="40px"/>
                    
                        <asp:Button ID="btnNext" runat="server" CssClass="btn" Height="40px" OnClick="btnNext_Click" Text="التالي" ValidationGroup="PersonalInfo" Width="150px" />
                    
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </article>
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
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "60%" });
        }
    </script>
    <script>
        $(document).ready(function () {
            $('#txtBOD').datepicker({
                dateFormat: 'dd/mm/yy'
            });
        });
    </script>
</asp:Content>
