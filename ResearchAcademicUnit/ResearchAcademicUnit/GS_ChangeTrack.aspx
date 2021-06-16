<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GS_ChangeTrack.aspx.cs" Inherits="ResearchAcademicUnit.GS_ChangeTrack" %>

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
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    نموذج تحويل مسار
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>
        <div class="col-12">
            <div class="card mb-2">
                <div class="card-body">
                    <div class="card-header mb-2">
                        معلومات الطالب
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12 col-md-4">
                            <label>الرقم الجامعي</label>
                            <asp:TextBox ID="txtStudId" runat="server" CssClass="form-control" required Width="100%" data-mask="000000000" pattern="[0-9]{9}" onblur="getData()" ReadOnly="true"></asp:TextBox>
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

                        <div class="form-group col-sm-12 col-md-4">
                            <label>اسم الطالب رباعي</label>
                            <asp:Label ID="lblStudName" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-2">
                            <label>المعدل التراكمي</label>
                            <asp:Label ID="lblStudAvg" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-2">
                            <label>حالة الطالب</label>
                            <asp:Label ID="lblStudStatus" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>الكلية</label>
                            <asp:Label ID="lblStudFaculty" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <asp:Label ID="lblStudFacultyNo" runat="server" Text="" CssClass="form-control" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>القسم</label>
                            <asp:Label ID="lblStudDept" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <asp:Label ID="lblStudDeptNo" runat="server" Text="" CssClass="form-control" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>التخصص</label>
                            <asp:Label ID="lblStudMajor" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>

                    </div>

                </div>
            </div>

            <div class="card mb-2">
                <div class="card-body">
                    <div class="card-header mb-2 col-md-6">
                        مسار الطالب الحالي : 
                            <span class="text-danger font-weight-bold" style="font-size: 24px" id="currentTrack" runat="server"></span>
                    </div>
                    <div class="card-header mb-2 col-md-6">
                        المسار المطلوب التحويل إليه : 
                            <span class="text-danger font-weight-bold" style="font-size: 24px" id="newTrack" runat="server"></span>
                    </div>
                </div>
            </div>

            <div class="card mb-2">
                <div class="card-body">
                    <div class="card-header mb-2">
                        بيانات دائرة القبول والتسجيل
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6">
                            <label>فصل التحاق الطالب بالجامعة</label>
                            <asp:Label ID="lblJoinSemester" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-6">
                            <label>الساعات التي اجتازها الطالب بنجاح</label>
                            <asp:Label ID="lblHourSuccess" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>

            <div class="card mb-2" runat="server" id="buttonsDiv">
                <div class="card-body">
                    <div class="row">
                        <div class="form-group col-md-3 col-sm-12">
                            <asp:Button ID="btnSend" runat="server" Text="ارسال" CssClass="btn btn-primary" OnClick="btnSend_Click" />
                            <button type="button" class="btn btn-gradient-purple" data-toggle="modal" data-animation="bounce" data-target="#exampleModal" formnovalidate="formnovalidate" runat="server" id="btnShowDec">التنسيب</button>
                            <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="2500" OnTick="Timer1_Tick"></asp:Timer>
                            <asp:Timer ID="Timer3" runat="server" Enabled="false" Interval="2500" OnTick="Timer3_Tick"></asp:Timer>
                        </div>
                    </div>

                </div>
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
                    <div class="row" id="decisionInfoDiv" runat="server">
                        <div class="form-group col-md-4">
                            <label>رقم القرار</label>
                            <asp:TextBox ID="txtDecNo" runat="server" CssClass="form-control col-12" type="number"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>رقم الجلسة</label>
                            <asp:TextBox ID="txtDecMeetNo" runat="server" CssClass="form-control col-12" type="number"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>تاريخ الجلسة</label>
                            <asp:TextBox ID="txtDecMeetDate" runat="server" CssClass="form-control col-12" type="date"></asp:TextBox>
                        </div>
                    </div>
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
                            <asp:Button ID="btnDirAgree" runat="server" Text="موافق" CssClass="btn btn-success" OnClick="btnDirAgree_Click" formnovalidate="formnovalidate" ValidationGroup="dirdec" />
                            <asp:Button ID="btnDisAgree" runat="server" Text="غير موافق" CssClass="btn btn-danger" OnClick="btnDisAgree_Click" formnovalidate="formnovalidate" ValidationGroup="dirdec" />
                            <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Enabled="false" Interval="2500"></asp:Timer>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js"></script>
    <script>
        $('.jsSelect').select2();
    </script>
</asp:Content>
