<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UploadData.aspx.cs" Inherits="ResearchAcademicUnit.UploadData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grd {
            text-align: center;
            -moz-border-radius: 20px;
            border-radius: 20px;
            Border: 1px solid black;
            margin: 10px;
            width: 100%;
        }

            .grd tr td {
                padding: 8px;
            }

            .grd th {
                background-color: #F7BA00;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--        <asp:UpdateProgress ID="updateProgress" runat="server">

        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7; font-size: xx-large; color: #FFFFFF;">
                <center>
                    Please Wait ...</center>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <div id="#mainDiv">
        <%--dir="rtl" style="width: 95%; margin: 0 auto 0 auto; box-sizing: border-box; background-color: #6d7486; padding: 15px;">--%>
        <div class="contentDiv" runat="server" id="uploadDiv" style="width: 48%; float: right; height: 325px">
            <div style="padding: 10px; box-sizing: border-box">
                <div style="margin-bottom: 20px">
                    <div class="lblDiv">معلومات الباحثين والأبحاث</div>
                    <div style="clear: both"></div>
                </div>
                <div>
                    <div class="lblDiv" style="width: 25%">
                        <asp:Label ID="lbl" runat="server" Text="تحديد الملف المراد تحميله"></asp:Label>
                    </div>
                    <div class="lblContentDiv" style="width: 74%">
                        <asp:FileUpload ID="FileUpload1" runat="server" Style="padding-right: 20px; width: 100%; box-sizing: border-box" Font-Bold="True" Font-Size="Medium" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" Display="Dynamic" Font-Size="Medium"
                            ForeColor="#F7BA00" ControlToValidate="FileUpload1" ValidationGroup="UploadI"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                            ControlToValidate="FileUpload1" ValidationGroup="UploadI"
                            ErrorMessage="ملفات اكسل من نوع xls فقط"
                            ValidationExpression="(.*\.([Xx][Ll][Ss])$)" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div style="clear: both"></div>
                    <div style="box-sizing: border-box; margin-top: 10px; text-align: center">
                        <asp:Button ID="btnUpload" ValidationGroup="UploadI"
                            OnClientClick="return confirm('هل أنت متأكد من تحميل البيانات؟ سيتم حذف البيانات السابقة؟')"
                            Style="margin-left: 20px; margin-right: 20px" runat="server" Text="تحميل" OnClick="btnUpload_Click" CssClass="btn" />
                    </div>
                </div>
            </div>
        </div>
        <div id="abstractDiv" runat="server" visible="false" class="headerDiv">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="grd">
                <Columns>
                    <asp:BoundField HeaderText="الرمز" DataField="RID" />
                    <asp:BoundField HeaderText="الخطأ" DataField="Action" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="contentDiv" runat="server" id="Div1" style="width: 48%; float: left">
            <div style="padding: 20px; box-sizing: border-box">
                <div style="margin-bottom: 20px">
                    <div class="lblDiv">معيار الحد الأقصى للدعم والمكافأة</div>
                    <div style="clear: both"></div>
                </div>
                <div>
                    <div class="lblDiv" style="width: 50%">
                        <asp:Label ID="Label1" runat="server" Text="عدد الأبحاث والنشاطات التأليفية"></asp:Label>
                    </div>
                    <div class="lblContentDiv" style="width: 49%; margin-bottom: 10px">
                        <asp:TextBox ID="txtR" runat="server" MaxLength="3" onkeypress="return isNumberKey(event)" Width="95%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" Font-Size="Medium"
                            ForeColor="#F7BA00" ControlToValidate="txtR" ValidationGroup="UploadS"></asp:RequiredFieldValidator>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div>
                    <div class="lblDiv" style="width: 50%">
                        <asp:Label ID="Label2" runat="server" Text="عدد المؤتمرات"></asp:Label>
                    </div>
                    <div class="lblContentDiv" style="width: 49%; margin-bottom: 10px">
                        <asp:TextBox ID="txtC" runat="server" MaxLength="3" onkeypress="return isNumberKey(event)" Width="95%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Display="Dynamic" Font-Size="Medium"
                            ForeColor="#F7BA00" ControlToValidate="txtC" ValidationGroup="UploadS"></asp:RequiredFieldValidator>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="box-sizing: border-box; text-align: center">
                    <asp:Button ID="btnUploadSetting" OnClientClick="return confirm('هل أنت متأكد من تحميل البيانات؟ سيتم حذف البيانات السابقة؟')" ValidationGroup="UploadS" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="حفظ" OnClick="btnUploadSetting_Click" CssClass="btn" />
                </div>
            </div>
        </div>
        <div style="clear: both"></div>
        <div class="contentDiv" runat="server" id="Div2" style="width: 48%; float: left">
            <div style="padding: 20px; box-sizing: border-box">
                <div style="margin-bottom: 20px">
                    <div class="lblDiv">السنة الحالية</div>
                    <div style="clear: both"></div>
                </div>
                <div>
                    <div class="lblDiv" style="width: 25%">
                        <asp:Label ID="Label4" runat="server" Text="من"></asp:Label>
                    </div>
                    <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                        <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="ChosenSelector">
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
                        <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div>
                    <div class="lblDiv" style="width: 25%">
                        <asp:Label ID="Label5" runat="server" Text="إلى"></asp:Label>
                    </div>
                    <div class="lblContentDiv" style="width: 74%; margin-bottom: 30px">
                        <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="ChosenSelector">
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
                        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="box-sizing: border-box; text-align: center">
                    <asp:Button ID="btnSave" OnClientClick="return confirm('هل أنت متأكد من تحميل البيانات؟ سيتم حذف البيانات السابقة؟')" CausesValidation="false" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="حفظ" OnClick="btnSave_Click" CssClass="btn" />
                </div>
            </div>
        </div>

        <div class="contentDiv" runat="server" id="Div3" style="width: 48%; float: right; height: 315px">
            <div style="padding: 20px; box-sizing: border-box">
                <div style="margin-bottom: 20px">
                    <div class="lblDiv">الصلاحيات</div>
                    <div style="clear: both"></div>
                </div>
                <div class="lblDiv" style="width: 25%">
                    <asp:Label ID="Label6" runat="server" Text="مستوى الصلاحية"></asp:Label>
                </div>
                <div>
                    <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                        <asp:DropDownList ID="ddlAuthLevel" runat="server" CssClass="ChosenSelector1" Width="95%"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlAuthLevel_SelectedIndexChanged">
                            <asp:ListItem Value="0">تحديد مستوى الصلاحية</asp:ListItem>
                            <asp:ListItem Value="1">مدير النظام</asp:ListItem>
                            <asp:ListItem Value="2">رئيس الجامعة</asp:ListItem>
                            <asp:ListItem Value="3">نائب رئيس الجامعة</asp:ListItem>
                            <asp:ListItem Value="4">عمادة الدراسات العليا والبحث العلمي</asp:ListItem>
                            <asp:ListItem Value="5">عمداء الكليات</asp:ListItem>
                            <asp:ListItem Value="7">رؤوساء الأقسام</asp:ListItem>
                            <asp:ListItem Value="6">الباحثين</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="UploadA" ControlToValidate="ddlAuthLevel"></asp:RequiredFieldValidator>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div class="lblDiv" style="width: 25%">
                    <asp:Label ID="Label7" runat="server" Text="صاحب الصلاحية"></asp:Label>
                </div>
                <div>
                    <div class="lblContentDiv" style="width: 74%; text-align: center">
                        <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" CssClass="ChosenSelector1" Style="width: 100%"></asp:ListBox>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="box-sizing: border-box; text-align: center; margin-top: 10px">
                    <asp:Button ID="btnSaveAuth" OnClientClick="return confirm('هل أنت متأكد من تحميل البيانات؟ سيتم حذف البيانات السابقة؟')" ValidationGroup="UploadA" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="حفظ" OnClick="btnSaveAuth_Click" CssClass="btn" />
                </div>
            </div>
        </div>
        <div style="clear: both"></div>
        <div class="contentDiv" runat="server" id="Div4" style="width: 48%; float: right">
            <div style="margin-bottom: 20px">
                <div class="lblDiv">معلومات السنوات السابقة</div>
                <div style="clear: both"></div>
            </div>
            <div class="lblDiv" style="width: 25%">
                <asp:Label ID="Label3" runat="server" Text="السنة الأكاديمية"></asp:Label>
            </div>
            <div class="lblContentDiv" style="width: 74%; text-align: center; margin-bottom: 10px">
                <asp:TextBox ID="txtFirstYear" runat="server"></asp:TextBox>
            </div>

            <div class="lblDiv" style="width: 25%">
                <asp:Label ID="Label8" runat="server" Text="عدد أعضاء هيئة التدريس"></asp:Label>
            </div>
            <div class="lblContentDiv" style="width: 74%; text-align: center; margin-bottom: 10px">
                <asp:TextBox ID="txtFirstInst" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </div>

            <div class="lblDiv" style="width: 25%">
                <asp:Label ID="Label9" runat="server" Text="السنة الأكاديمية"></asp:Label>
            </div>
            <div class="lblContentDiv" style="width: 74%; text-align: center; margin-bottom: 10px">
                <asp:TextBox ID="txtSecondYear" runat="server"></asp:TextBox>
            </div>
            <div class="lblDiv" style="width: 25%">
                <asp:Label ID="Label10" runat="server" Text="عدد أعضاء هيئة التدريس"></asp:Label>
            </div>
            <div class="lblContentDiv" style="width: 74%; text-align: center; margin-bottom: 10px">
                <asp:TextBox ID="txtSecondInst" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </div>
            <div class="lblDiv" style="width: 25%">
                <asp:Label ID="Label12" runat="server" Text="عدد أعضاء هيئة التدريس السنة الحالية"></asp:Label>
            </div>
            <div class="lblContentDiv" style="width: 74%; text-align: center; margin-bottom: 10px">
                <asp:TextBox ID="txtCInst" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </div>
            <div style="box-sizing: border-box; text-align: center; margin-top: 10px">
                <asp:Button ID="btnOldYearSave" runat="server" Text="حفظ" CssClass="btn"
                    OnClientClick="return confirm('هل أنت متأكد من تحميل البيانات؟ سيتم حذف البيانات السابقة؟')"
                    OnClick="btnOldYearSave_Click" />
            </div>
        </div>
    </div>
    <script>
        $('.ChosenSelector').chosen({ width: "40%" });
        $('.ChosenSelector1').chosen({ width: "95%", rtl: true });
    </script>
</asp:Content>
