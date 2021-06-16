<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Admin_SupervisorReport.aspx.cs" Inherits="ResearchAcademicUnit.Admin_SupervisorReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
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
                    متابعة الطلبات
                </div>
            </div>
            <div class="form-group col-md-4 d-sm-none d-md-block"></div>
        </div>
        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label></label>
                            <asp:DropDownList ID="ddlSupervisor" runat="server" CssClass="jsSelect" AutoPostBack="true" OnSelectedIndexChanged="ddlSupervisor_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>اسماء المشرفين</label>
                            <asp:GridView ID="GridView1" runat="server">
                                <Columns>
<%--                                    <asp:BoundField DataField="Thesis_semester" HeaderText="Thesis_semester" />
                                    <asp:BoundField DataField="Main_Supervisor" HeaderText="Main_Supervisor" />
                                    <asp:BoundField DataField="Co_Supervisor" HeaderText="Co_Supervisor" />
                                    <asp:BoundField DataField="Single_supervisor" HeaderText="Single_supervisor" />
                                    <asp:BoundField DataField="FACULITY_AGREE_DATE" HeaderText="Faculty_agree_Date" />
                                    <asp:BoundField DataField="start_agree_date" HeaderText="start_agree_date" />--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.jsSelect').select2();
    </script>
</asp:Content>
