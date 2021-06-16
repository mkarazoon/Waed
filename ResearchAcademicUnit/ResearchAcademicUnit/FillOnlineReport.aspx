<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FillOnlineReport.aspx.cs" Inherits="HelpDeskIT.FillOnlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />--%>
    <script src="sweet-alert2/sweetalert2.min.js"></script>
    <link href="sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script>
        function executeExample(errMsg, sentIcon) {
            Swal.fire({
                icon: sentIcon,
                text: errMsg,
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,

            })
        }
    </script>
    <style>
        .hidden {
            display: none;
        }

        .active {
            background-color: #921a1d;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <asp:UpdatePanel ID="id1" runat="server">
        <ContentTemplate>--%>
            <section class="wrapper site-min-height">
                <h3><i class="fa fa-angle-left"></i>تقرير المحاضرة</h3>
                <!-- BASIC FORM ELELEMNTS -->
                <div class="row mt">
                    <div class="col-lg-12">
                        <div class="form">
                            <div class="form-panel">
                                <div class="row">
                                    <div class="form-group col-sm-3">
                                        <label class="text-danger">*</label><label>الرقم الوظيفي</label>
                                        <asp:TextBox ID="txtInstId" runat="server" CssClass="form-control" required AutoPostBack="true" OnTextChanged="txtInstId_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-sm-3" runat="server" visible="false" id="instNameDiv">
                                        <label>الاسم</label>
                                        <asp:Label ID="lblInstName" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="form-group col-sm-3" runat="server" visible="false" id="instFacultyDiv">
                                        <label>الكلية</label>
                                        <asp:Label ID="lblFaculty" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="form-group col-sm-3" runat="server" visible="false" id="instDeptDiv">
                                        <label>القسم</label>
                                        <asp:Label ID="lblDept" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <ul class="nav nav-pills nav-justified mb" id="mytabs" runat="server" visible="false">
                                    <li class="nav-item waves-effect waves-light">
                                        <asp:LinkButton class="nav-link active" ID="lnkTab1" runat="server" OnClick="lnkTab1_Click">الجدول الدراسي</asp:LinkButton>
                                    </li>
                                    <li class="nav-item waves-effect waves-light">
                                        <asp:LinkButton class="nav-link" ID="lnkTab2" runat="server" OnClick="lnkTab2_Click">المحاضرات المدخلة</asp:LinkButton>
                                    </li>
                                </ul>
                                <div id="tab1" runat="server" class="mt-auto">

                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                                OnDataBound="GridView1_DataBound"
                                                CssClass="table table-bordered table-striped col-md-12" Width="100%" EmptyDataText="لا يوجد جدول حاليا">
                                                <HeaderStyle CssClass="text-right" BackColor="#000" ForeColor="White" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="الوقت" DataField="time_desc" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:TemplateField HeaderText="تفاصيل المادة">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <div class="form-group col-md-4">
                                                                    <label>اسم المادة</label>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("Course_Name") %>' CssClass="form-control"></asp:Label>
                                                                    <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("Course_No") %>' CssClass="form-control" Visible="false"></asp:Label>
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <label>رقم الشعبة</label>
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("crs_section") %>' CssClass="form-control"></asp:Label>
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <label>عدد الطلبة المسجلين</label>
                                                                    <asp:Label ID="lblRegStud" runat="server" Text='<%# Eval("reg_students") %>' CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group col-md-6 col-sm-12">
                                                                    <label>الأيام</label>
                                                                    <asp:RadioButtonList ID="rdDaysNew" runat="server" Width="100%" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="س">السبت</asp:ListItem>
                                                                        <asp:ListItem Value="ح">الأحد</asp:ListItem>
                                                                        <asp:ListItem Value="ن">الاثنين</asp:ListItem>
                                                                        <asp:ListItem Value="ث">الثلاثاء</asp:ListItem>
                                                                        <asp:ListItem Value="ر">الاربعاء</asp:ListItem>
                                                                        <asp:ListItem Value="خ">الخميس</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="form-group col-md-6 col-sm-12">
                                                                    <label>وقت المحاضرة</label>
                                                                    <asp:RadioButtonList ID="rdtimesNew" runat="server" Width="100%" RepeatDirection="Horizontal">
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                            </div>
                                                            <%--</ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="row col-md-3">
                                            <ItemTemplate>--%>
                                                            <div class="row center-block" runat="server" id="detailsDiv">
                                                                <div class="form-group col-md-3 col-sm-12">
                                                                    <label>تاريخ المحاضرة</label>
                                                                    <input type="date" runat="server" id="txtlecDate" class="form-control" />
                                                                </div>
                                                                <div class="form-group col-md-3 col-sm-12">
                                                                    <label>عدد الحضور</label>
                                                                    <asp:TextBox ID="txtPresentStud" runat="server" class="form-control" data-mask="099" AutoPostBack="true" OnTextChanged="txtPresentStud_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-md-6 col-sm-12">
                                                                    <label class="col-sm-12">التطبيق المستخدم</label>
                                                                    <asp:CheckBoxList ID="chkApps" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                                        <asp:ListItem Value="1">Microsoft Teams</asp:ListItem>
                                                                        <asp:ListItem Value="3">Moodle</asp:ListItem>
                                                                        <asp:ListItem Value="2">Zoom</asp:ListItem>
                                                                        <asp:ListItem Value="7">Youtube</asp:ListItem>
                                                                        <asp:ListItem Value="4">Facebook</asp:ListItem>
                                                                        <asp:ListItem Value="6">WhatsApp</asp:ListItem>
                                                                        <asp:ListItem Value="5">Google Classroom</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="form-group col-12 col-sm-12">
                                                                    <label class="col-sm-12">الملاحظات</label>
                                                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="10" Width="100%" Style="resize: none"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-3 col-sm-3">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="ارسال" CssClass="btn btn-primary btn-block" OnClick="btnSubmit_Click" />
                                                                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="2500"></asp:Timer>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-primary btn-block" OnClick="lnkView_Click">عرض</asp:LinkButton>
                                                <asp:CheckBox ID="chkLec" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab2" runat="server" class="p-3" visible="false">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                OnRowDataBound="GridView2_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="Autoid" HeaderText="AutoId" />
                                                    <asp:BoundField DataField="CourseName" HeaderText="اسم المادة" />
                                                    <asp:BoundField DataField="SectionId" HeaderText="الشعبة" />
                                                    <asp:BoundField DataField="LectureDate" HeaderText="تاريخ المحاضرة" DataFormatString="{0:dd-MM-yyyy}" />
                                                    <asp:BoundField DataField="LectTime" HeaderText="وقت المحاضرة" />
                                                    <asp:BoundField DataField="LectDay" HeaderText="يوم المحاضرة" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script src="jquery.min.js"></script>
    <script src="lib/form-validation-script.js"></script>
    <script src="jquery-mask-input/jquery.mask.min.js"></script>
</asp:Content>
