<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Admin_WorkFlow.aspx.cs" Inherits="ResearchAcademicUnit.Admin_WorkFlow" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-right">
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    مسار العمل للنماذج
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>
        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        مسار العمل للنماذج
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-5">
                            <label>اسم النموذج</label>
                            <asp:DropDownList ID="ddlFormsInfo" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlFormsInfo_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="form-group col-sm-5">
                            <label>الاجراء</label>
                            <asp:DropDownList ID="ddlJobtitle" runat="server" CssClass="jsSelect col-12"></asp:DropDownList>
                        </div>
                        <div class="form-group col-sm-2 mt-auto">
                            <asp:Button ID="btnAddStep" runat="server" Text="إضافة" CssClass="btn btn-primary" OnClick="btnAddStep_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        مسار العمل الحالي
                    </div>
        <div class="row">
            <div class="form-group col-sm-12">
                <asp:GridView ID="GridView1" runat="server" OnDataBound="GridView1_DataBound" CssClass="table table-bordered text-center"
                    AutoGenerateColumns="false" EmptyDataText="لا يوجد معلومات لهذا النموذج">
                    <HeaderStyle CssClass="thead-light" />
                    <Columns>
                        <asp:BoundField DataField="StepNo" HeaderText="التسلسل" />
                        <asp:BoundField DataField="PrivName" HeaderText="الصلاحية" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"><i class="material-icons text-danger">delete</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkDown" runat="server" OnClick="lnkDown_Click"><i class="material-icons">arrow_downward</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkUp" runat="server" OnClick="lnkUp_Click"><i class="material-icons">arrow_upward</i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row">
            <div class="form-group col-sm-3">
                <asp:Button ID="btnSave" runat="server" Text="حفظ" OnClick="btnSave_Click" CssClass="btn btn-primary" Visible="false" />
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
