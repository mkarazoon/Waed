<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NewResearcher.aspx.cs" Inherits="ResearchAcademicUnit.NewResearcher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <style>
        table {
            width: 100%;
            border-spacing: 5px 5px;
        }

            table input[type=text] {
                width: 100%;
                display: inline;
                box-sizing: border-box;
            }

        .validcss {
            color: red;
            font-size: large;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <div style="position: fixed; top: 50%; left: 20%; background-color: green; text-align: center; width: 50%; padding: 50px; color: white" runat="server" id="msgDiv" visible="false">--%>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="alert alert-success alert-icon" role="alert" visible="false" id="alertMsgSuccess" runat="server" style="font-family: Calibri; margin-top: 20px">
                    <i class="mdi mdi-checkbox-marked-outline"></i>
                    <asp:Label ID="Label1" runat="server" Text="تم تحميل البيانات بنجاح"></asp:Label>
                </div>
                <div class="alert alert-danger alert-icon" role="alert" visible="false" id="alertErr" runat="server" style="font-family: Calibri; margin-top: 20px">
                    <i class="mdi mdi-checkbox-marked-outline"></i>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
                <asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
<%--    </div>--%>

    <div class="TitleDiv">
        إضافة باحث
    </div>
    <div style="margin: 0px auto; padding: 2%; overflow-x: auto;">
        <table>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtRID"></asp:RequiredFieldValidator>رمز الباحث</td>
                <td>
                    <asp:TextBox ID="txtRID" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtRaName"></asp:RequiredFieldValidator>اسم الباحث-عربي</td>
                <td>
                    <asp:TextBox ID="txtRaName" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtREngName"></asp:RequiredFieldValidator>اسم الباحث-انجليزي</td>
                <td>
                    <asp:TextBox ID="txtREngName" runat="server"></asp:TextBox></td>
                <td>اسم الباحث-سكوبس</td>
                <td>
                    <asp:TextBox ID="txtReName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlFacullty" InitialValue="0"></asp:RequiredFieldValidator>الكلية</td>
                <td>
                    <asp:DropDownList ID="ddlFacullty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFacullty_SelectedIndexChanged" CssClass="ChosenSelector"></asp:DropDownList></td>
                <td>القسم<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlDept" InitialValue="0"></asp:RequiredFieldValidator></td>
                <td>
                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="ChosenSelector"></asp:DropDownList></td>
                <td>الرتبة العلمية</td>
                <td>
                    <asp:DropDownList ID="ddlRLevel" runat="server" CssClass="ChosenSelector">
                        <asp:ListItem Value="0">حدد الرتبة الأكاديمية</asp:ListItem>
                        <asp:ListItem Value="استاذ">استاذ</asp:ListItem>
                        <asp:ListItem Value="استاذ مشارك">استاذ مشارك</asp:ListItem>
                        <asp:ListItem Value="استاذ مساعد">استاذ مساعد</asp:ListItem>
                        <asp:ListItem Value="محاضر">محاضر</asp:ListItem>
                    </asp:DropDownList></td>
                <td>المؤهل العلمي</td>
                <td>
                    <asp:DropDownList ID="ddlRDegree" runat="server" CssClass="ChosenSelector">
                        <asp:ListItem Value="0">حدد المؤهل العلمي</asp:ListItem>
                        <asp:ListItem Value="دكتوراة">دكتوراة</asp:ListItem>
                        <asp:ListItem Value="ماجستير">ماجستير</asp:ListItem>
                        <asp:ListItem Value="بكالوريوس">بكالوريوس</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtHDate"></asp:RequiredFieldValidator>تاريخ التعيين</td>
                <td>
                    <asp:TextBox ID="txtHDate" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtAcdId"></asp:RequiredFieldValidator>الرقم الوظيفي</td>
                <td>
                    <asp:TextBox ID="txtAcdId" runat="server"></asp:TextBox></td>
                <td>التخصص</td>
                <td>
                    <asp:TextBox ID="txtMajor" runat="server"></asp:TextBox></td>
                <td>سنة الحصول على المؤهل</td>
                <td>
                    <asp:DropDownList ID="ddlQualYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>المسمى الوظيفي</td>
                <td>
                    <asp:TextBox ID="txtPositionName" runat="server"></asp:TextBox></td>
                <td>سنوات الخبرة</td>
                <td>
                    <asp:DropDownList ID="ddlExpYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList></td>
                <td>مؤشر الانتاجية</td>
                <td>
                    <asp:TextBox ID="txtProductivityPointer" runat="server"></asp:TextBox></td>
                <td>معدل الاستشهاد</td>
                <td>
                    <asp:TextBox ID="txtRCitationAvg" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    حالة الباحث
                </td>
                <td>
                    <asp:DropDownList ID="ddlRStatus" runat="server" CssClass="ChosenSelector">
                        <asp:ListItem Value="0">حدد حالة الباحث</asp:ListItem>
                        <asp:ListItem Value="IN">IN</asp:ListItem>
                        <asp:ListItem Value="OUT">OUT</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:Button ID="btnNew" runat="server" Text="جديد" CssClass="btn" OnClick="btnNew_Click" CausesValidation="false" />
                    <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn" OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="GridView1" runat="server" EmptyDataText="لا يوجد معلومات حاليا"
                        Caption="معلومات الباحثين" AutoGenerateColumns="false" CssClass="grd">
                        <Columns>
                            <asp:BoundField HeaderText="رمز الباحث" DataField="RId" />
                            <asp:BoundField HeaderText="اسم الباحث-عربي" DataField="RaName" />
                            <asp:BoundField HeaderText="اسم الباحث-انجليزي" DataField="REngName" />
                            <asp:BoundField HeaderText="اسم الباحث-سكوبس" DataField="REName" />
                            <asp:BoundField HeaderText="الكلية" DataField="College" />
                            <asp:BoundField HeaderText="القسم" DataField="Dept" />
                            <asp:BoundField HeaderText="الرتبة الأكاديمية" DataField="RLevel" />
                            <asp:BoundField HeaderText="المؤهل العلمي" DataField="RDegree" />
                            <asp:BoundField HeaderText="تاريخ التعيين" DataField="HDate" DataFormatString="{0:dd-MM-yyyy}"/>
                            <asp:BoundField HeaderText="الرقم الوظيفي" DataField="AcdId" />
                            <asp:BoundField HeaderText="التخصص" DataField="Major" />
                            <asp:BoundField HeaderText="سنة الحصول على الرتبة" DataField="DegreeYear" />
                            <asp:BoundField HeaderText="المسمى الوظيفي" DataField="PositionName" />
                            <asp:BoundField HeaderText="سنوات الخبرة" DataField="ExpYear" />
                            <asp:BoundField HeaderText="مؤشر الانتاجية" DataField="ProductivityPointer" />
                            <asp:BoundField HeaderText="معدل الاستشهاد" DataField="RCitationAvg" />
                            <asp:BoundField HeaderText="حالة الباحث" DataField="RStatus" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" CausesValidation="false" runat="server" OnClick="lnkEdit_Click" ToolTip="تعديل"><i class="material-icons" style="color: #E34724">border_color</i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelR" CausesValidation="false" runat="server" OnClick="lnkDelR_Click" ToolTip="حــذف" OnClientClick="return confirm('هل أنت متأكد من حذف معلومات الباحث؟')"><i class="material-icons" style="color: #E34724">clear</i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "100%" });

        }
    </script>

</asp:Content>
