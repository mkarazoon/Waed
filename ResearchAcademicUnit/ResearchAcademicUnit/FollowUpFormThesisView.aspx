<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FollowUpFormThesisView.aspx.cs" Inherits="ResearchAcademicUnit.FollowUpFormThesisView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="plugins/select2/select2.min.js"></script>
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
    <script language="javascript" type="text/javascript">
        function test1(divName, flag) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            document.location.href = document.URL;// "PrintStudSeatsFinal.aspx";
            if (flag == 1) {
                __doPostBack("btn");
            }
        }
    </script>
    <style>
        @media print {
            body {
                background-color: white;
            }
            .newpage{
                page-break-after: always;
                page-break-inside: avoid;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="button" id="btnPrint" runat="server" style="margin-left: 10px; margin-right: 10px" class="btn" onclick="test1('printDiv1', 1);" value="    طباعة    " />
    <div id="printDiv1">
        <div class="container text-right">
            <div class="row text-center mt-2">
                <div class="form-group col-md-4 d-sm-none">333</div>
                <div class="form-group col-md-4 d-sm-none">
                    <%--style="text-align: center; margin-top: 10px">--%>
                    <img src="images/MEU.png" style="width: 240px" />
                    <%--<div style="font-family: 'Khalid Art'; font-size: 22px">--%>
                عمادة الدراسات العليا والبحث العلمي
            <%--            </div>
            <div style="font-family: 'Tw Cen MT'; font-size: 18px">--%>
                Deanship of Graduate Studies & Scientific Research
                </div>
                <div class="form-group col-md-4 d-sm-none">333</div>
                <hr />
            </div>

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
                            <div class="form-group col-md-3 col-sm-12">
                                <label>الرقم الجامعي</label>
                                <asp:Label ID="lblStudId" runat="server" Text="" CssClass="form-control col-12"></asp:Label>
                            </div>
                            <div class="form-group col-md-9 col-sm-12">
                                <label>اسم الطالب - رباعي</label>
                                <asp:Label ID="lblStudName" runat="server" Text="Label" CssClass="form-control col-12"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6 col-sm-12">
                                <label>برنامج ماجستير</label>
                                <asp:Label ID="lblMs" runat="server" Text="" CssClass="form-control col-12"></asp:Label>
                            </div>
                            <div class="form-group col-md-3 col-sm-12">
                                <label>الكلية</label>
                                <asp:Label ID="lblStudFaculty" runat="server" Text="" CssClass="form-control col-12"></asp:Label>
                            </div>
                            <div class="form-group col-md-3 col-sm-12">
                                <label>القسم</label>
                                <asp:Label ID="lblStudDept" runat="server" Text="" CssClass="form-control col-12"></asp:Label>
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
                                <asp:Label ID="lblSupName" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-md-6 col-sm-12">
                                <label>الرتبة</label>
                                <asp:Label ID="lblSupDegree" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6 col-sm-12">
                                <label>المشرف المشارك - ان وجد</label>
                                <asp:Label ID="lblCoSupName" runat="server" Text="" CssClass="form-control col-12"></asp:Label>
                            </div>
                            <div class="form-group col-md-6 col-sm-12">
                                <label>التخصص</label>
                                <asp:Label ID="lblSupMajor" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-3 col-sm-12">
                                <label>الكلية</label>
                                <asp:Label ID="lblFaculty" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-md-3 col-sm-12">
                                <label>القسم</label>
                                <asp:Label ID="lblSupDept" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-md-6 col-sm-12">
                                <label>تاريخ قرار التكليف</label>
                                <asp:Label ID="lblSupDate" runat="server" Text="" CssClass="form-control col-12"></asp:Label>
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
                                <div runat="server" id="ArabicThesisDiv" class="form-control"></div>
                            </div>
                            <div class="form-group col-sm-12">
                                <label>عنوان الرسالة باللغة الانجليزية</label>
                                <div runat="server" id="EngThesisDiv" class="form-control"></div>
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
                                <asp:Label ID="lblMeetingMonth" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-sm-12 col-md-3">
                                <label>عدد المرات</label>
                                <asp:Label ID="lblMeetingCount" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 mb-2 newpage">
                <div class="card">
                    <div class="card-body">
                        <div class="card-header mb-2">
                            أهم انجازات الطالب في موضوع رسالته
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div runat="server" id="StudAchDiv" class="form-control"></div>
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
                                <div runat="server" id="SupOpinionDiv" class="form-control"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 mb-2">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label>اسم الطالب</label>
                                <asp:Label ID="lblStudSign" runat="server" Text="" class="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-sm-6">
                                <label>اسم المشرف</label>
                                <asp:Label ID="lblSuperSign" runat="server" Text="" class="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-sm-6">
                                <label>رئيس القسم</label>
                                <asp:Label ID="Label1" runat="server" Text="" class="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-sm-6">
                                <label>عميد الكلية</label>
                                <asp:Label ID="Label2" runat="server" Text="" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="width: 100%; text-align: left; position: fixed; bottom: 0">
                <hr />
                <div class="form-group col-sm-4">
                    <img src="images/footer.png" width="250px" />
                </div>
                <div class="form-group col-sm-4">
                    F506, Rev. c<br />
                    Ref.: Deans' Council Session (07/2018-2019) Decision No: 05, Date: 18/11/2018
                </div>
            </div>
        </div>
    </div>
</asp:Content>
