<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Admin_RequestsFollowUp.aspx.cs" Inherits="ResearchAcademicUnit.Admin_RequestsFollowUp" %>

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
                    <ul class="nav nav-pills nav-justified mb" id="mytabs" runat="server">
                        <li class="nav-item">
                            <asp:LinkButton class="nav-link active" ID="lnkTab1" runat="server" OnClick="lnkTab1_Click">الطلبات الحالية</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton class="nav-link" ID="lnkTab2" runat="server" OnClick="lnkTab2_Click">جميع طلباتي</asp:LinkButton>
                        </li>
                    </ul>
                    <div class="row" id="tab1" runat="server">
                        <div class="form-group col-md-12 col-sm-12">
                            <asp:GridView ID="grdCurrentRequest" runat="server" AutoGenerateColumns="false" EmptyDataText="لا يوجد معاملات حاليا"
                                CssClass="table text-center" OnRowDataBound="grdCurrentRequest_RowDataBound">
                                <HeaderStyle CssClass="card-header" />
                                <Columns>
                                    <asp:BoundField HeaderText="رقم النموذج" DataField="FormId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField HeaderText="اسم النموذج" DataField="FormAName" />
                                    <asp:BoundField HeaderText="رقم المعاملة" DataField="AutoId" />
                                    <asp:BoundField HeaderText="تاريخ المعاملة" DataField="SentDate" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="رقم مقدم الطلب" DataField="UserId" />
                                    <asp:BoundField HeaderText="اسم مقدم الطلب" DataField="SupName" />
                                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="SentDate" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="الحالة" DataField="Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField HeaderText="الحالة" DataField="Status" />
                                    <asp:BoundField HeaderText="رابط النموذج" DataField="FormLink" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField HeaderText="جدول النموذج" DataField="OriginalTable" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField HeaderText="صاحب الصلاحية" DataField="ReqToId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:TemplateField HeaderText="الإجراء">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewForm" OnClick="lnkViewForm_Click" runat="server" ToolTip="عرض النموذج"><i class="material-icons text-success">visibility</i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkFollow" runat="server" ToolTip="متابعة النموذج" OnClick="lnkFollow_Click"><i class="material-icons text-info">fast_rewind</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row" id="tab2" runat="server" visible="false">
                        <div class="form-group col-md-12 col-sm-12">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered text-center" Font-Size="Small" AutoGenerateColumns="false"
                                EmptyDataText="لا يوجد نماذج متاحة حاليا" OnRowDataBound="GridView1_RowDataBound">
                                <HeaderStyle CssClass="thead-light" />
                                <AlternatingRowStyle CssClass="progress-bar-striped" />
                                <Columns>
                                    <asp:BoundField DataField="FormId" HeaderText="رقم النموذج" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="AutoId" HeaderText="رقم المعاملة" />
                                    <asp:BoundField DataField="FormAName" HeaderText="اسم النموذج" />
                                    <asp:BoundField HeaderText="رقم مقدم الطلب" DataField="UserId" />
                                    <asp:BoundField DataField="SupName" HeaderText="اسم مقدم الطلب" />
                                    <asp:BoundField DataField="Status" HeaderText="حالة الطلب" />
                                    <asp:BoundField DataField="FormLink" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="OriginalTable" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewFollowup" runat="server" OnClick="lnkFollow_Click" ToolTip="عرض المعاملة"><i class="material-icons text-info">visibility</i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkViewFormToPrint" runat="server" OnClick="lnkViewFormToPrint_Click" ToolTip="طباعة الطلب" Visible="false"><i class="material-icons text-info">visibility</i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkViewDecision" runat="server" OnClick="lnkViewDecision_Click" ToolTip="طباعة القرار" Visible="false"><i class="material-icons text-info">visibility</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            function LoginFail1() {
                $('#FollowUpModal').modal();
            }
        </script>
        <div class="modal fade" id="FollowUpModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="FollowUpLabel" runat="server">تعليمات النموذج</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group col-md-12">
                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle CssClass="card-header" />
                                    <Columns>
                                        <asp:BoundField DataField="ReqFromName" HeaderText="المرسل" />
                                        <asp:BoundField DataField="RquToName" HeaderText="المستقبل" />
                                        <asp:BoundField DataField="ReqStatus" HeaderText="الحالة" />
                                        <asp:BoundField DataField="Notes" HeaderText="الملاحظات" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">اغلاق</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
