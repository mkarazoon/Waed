<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FollowUpFormThesis.aspx.cs" Inherits="ResearchAcademicUnit.FollowUpFormThesis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="plugins/select2/select2.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css">
    <script>
        function executeExample(errMsg, sentIcon) {
            Swal.fire({
                icon: sentIcon,
                text: errMsg,
                showConfirmButton: false,
                timer: 2500,
                timerProgressBar: true,

            })
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-right">
        <div class="row">
            <div class="form-group col-md-4 d-sm-none d-md-block"></div>
            <div class="form-group col-md-4 col-sm-12 text-center bg-secondary text-white">
                <div class="card-header">
                    نموذج المتابعة الشهري لطلبة الدراسات العليا "مسار الرسالة"
                </div>
            </div>
            <div class="form-group col-md-4 d-sm-none d-md-block"></div>
        </div>
        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        معلومات عامة
                    </div>
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label>الرقم الجامعي</label>
                            <asp:TextBox ID="txtStudId" runat="server" CssClass="form-control col-12" required Width="100%" data-mask="000000000" pattern="[0-9]{9}" onblur="getData()"></asp:TextBox>
                            <asp:Button ID="btnGetData" runat="server" Text="Button" OnClick="btnGetData_Click" formnovalidate="formnovalidate" Style="display: none" />
                            <script>
                                function getData() {
                                    var btnHidden = $('#<%= btnGetData.ClientID %>');
                                    if (btnHidden != null) {
                                        btnHidden.click();
                                    }
                                }
                            </script>
                        </div>
                        <div class="form-group col-md-9">
                            <label>اسم الطالب - رباعي</label>
                            <%--<asp:TextBox ID="txtStudName" runat="server" CssClass="form-control col-md-12" Width="100%" required></asp:TextBox>--%>
                            <asp:Label ID="lblStudName" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6">
                            <label>برنامج ماجستير</label>
                            <%--<asp:TextBox ID="txtMs" runat="server" CssClass="form-control col-12" Width="100%" required></asp:TextBox>--%>
                            <asp:Label ID="lblMs" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-3">
                            <label>الكلية</label>
                            <%--<asp:DropDownList ID="ddlFaculty" runat="server" CssClass="jsSelect col-12" Width="100%" required AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>--%>
                            <asp:Label ID="lblStudFaculty" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <asp:Label ID="lblStudFacultyNo" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-3">
                            <label>القسم</label>
                            <%--<asp:DropDownList ID="ddlDept" runat="server" CssClass="jsSelect col-12" Width="100%" required></asp:DropDownList>--%>
                            <asp:Label ID="lblStudDept" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <asp:Label ID="lblStudDeptNo" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        معلومات عن المشرف
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6 col-sm-12">
                            <label>اسم المشرف</label>
                            <%--<asp:TextBox ID="txtSupName" runat="server" CssClass="form-control" Width="100%" required ReadOnly="true"></asp:TextBox>--%>
                            <asp:Label ID="lblSupName" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <label>الرتبة</label>
                            <asp:Label ID="lblSupDegree" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <%--                            <asp:DropDownList ID="ddlSupDegree" runat="server" CssClass="jsSelect" Width="100%" required>
                                <asp:ListItem Value="">اختيار</asp:ListItem>
                                <asp:ListItem Value="1">استاذ</asp:ListItem>
                                <asp:ListItem Value="2">استاذ مشارك</asp:ListItem>
                                <asp:ListItem Value="3">استاذ مساعد</asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6 col-sm-12">
                            <label>المشرف المشارك - ان وجد</label>
                            <asp:TextBox ID="txtCoSupName" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <label>التخصص</label>
                            <%--<asp:TextBox ID="txtSupMajor" runat="server" CssClass="form-control" Width="100%" required></asp:TextBox>--%>
                            <asp:Label ID="lblSupMajor" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-3 col-sm-12">
                            <label>الكلية</label>
                            <%--<asp:DropDownList ID="ddlSupFaculty" runat="server" CssClass="jsSelect" Width="100%" required AutoPostBack="true" OnSelectedIndexChanged="ddlSupFaculty_SelectedIndexChanged"></asp:DropDownList>--%>
                            <asp:Label ID="lblFaculty" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-3 col-sm-12">
                            <label>القسم</label>
                            <%--<asp:DropDownList ID="ddlSupDept" runat="server" CssClass="jsSelect" Width="100%" required></asp:DropDownList>--%>
                            <asp:Label ID="lblSupDept" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <label>تاريخ قرار التكليف</label>
                            <asp:TextBox ID="txtSupDate" runat="server" CssClass="form-control" required Width="100%" type="date"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        عنوان الرسالة باللغتين العربية والانجليزية
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <label>عنوان الرسالة باللغة العربية</label>
                            <asp:TextBox ID="txtArabicThesis" runat="server" CssClass="form-control" required TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-12">
                            <label>عنوان الرسالة باللغة الانجليزية</label>
                            <asp:TextBox ID="txtEngThesis" runat="server" CssClass="form-control" required TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header">
                        عدد مرات لقاء الطالب لمناقشته في موضوع الرسالة
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12 col-md-3">
                            <label>شهر</label>
                            <asp:DropDownList ID="ddlMeetingMonth" runat="server" CssClass="jsSelect" required Width="100%">
                                <asp:ListItem Value="">اختيار</asp:ListItem>
                                <asp:ListItem Value="1">كانون الثاني</asp:ListItem>
                                <asp:ListItem Value="2">شباط</asp:ListItem>
                                <asp:ListItem Value="3">اذار</asp:ListItem>
                                <asp:ListItem Value="4">نيسان</asp:ListItem>
                                <asp:ListItem Value="5">أيار</asp:ListItem>
                                <asp:ListItem Value="6">حزيران</asp:ListItem>
                                <asp:ListItem Value="7">تموز</asp:ListItem>
                                <asp:ListItem Value="8">آب</asp:ListItem>
                                <asp:ListItem Value="9">أيلول</asp:ListItem>
                                <asp:ListItem Value="10">تشرين الأول</asp:ListItem>
                                <asp:ListItem Value="11">تشرين الثاني</asp:ListItem>
                                <asp:ListItem Value="12">كانون الأول</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-sm-12 col-md-3">
                            <label>عدد المرات</label>
                            <asp:TextBox ID="txtMeetingCount" runat="server" CssClass="form-control" Width="100%" required data-mask="09"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        أهم انجازات الطالب في موضوع رسالته
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <asp:TextBox ID="txtStudAch" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        رأي المشرف وتقييمه لإنجاز الطالب
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <asp:TextBox ID="txtSupOpinion" runat="server" TextMode="MultiLine" Rows="15" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <div class="row">
            <div class="form-group col-md-3 col-sm-12">
                <asp:Button ID="btnSend" runat="server" Text="ارسال" CssClass="btn btn-primary" OnClick="btnSend_Click" />
                <%--<asp:Button ID="btnDir" runat="server" Text="تنسيب المسؤول" OnClick="btnDir_Click" />--%>
                <button type="button" class="btn btn-gradient-purple" data-toggle="modal" data-animation="bounce" data-target="#exampleModal" formnovalidate="formnovalidate" runat="server" id="btnShowDec">تنسيب المسؤول</button>
                <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="2500" OnTick="Timer1_Tick"></asp:Timer>
            </div>
        </div>

    </div>
    <div class="modal fade bs-pq-modal-lg text-right" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">تنسيب المسؤول</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <label>الملاحظات</label>
                            <asp:TextBox ID="txtDirNote" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtDirNote" ValidationGroup="dirdec"></asp:RequiredFieldValidator>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="form-group col-12">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">اغلاق</button>
<%--                            <button type="button"></button>
                            <button type="button"></button>--%>
                            <asp:Button ID="btnDirAgree" runat="server" Text="تم الاطلاع"  CssClass="btn btn-success" OnClick="btnDirAgree_Click" formnovalidate="formnovalidate" ValidationGroup="dirdec"/>
                            <%--<asp:Button ID="btnDirDisAgree" runat="server" Text="غير موافق" CssClass="btn btn-danger" OnClick="btnDirDisAgree_Click" formnovalidate="formnovalidate" ValidationGroup="dirdec"/>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script src="plugins/jquery-mask-input/jquery.mask.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js"></script>
<script>
  $(document).ready(function() {
     $("#MyModal").modal();
     $('#myModal').on('shown.bs.modal', function() {
        $('#myInput').focus();
     });
  });
    </script>
    <script>
        $('.jsSelect').select2();
    </script>
</asp:Content>
