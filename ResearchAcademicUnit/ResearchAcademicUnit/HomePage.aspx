<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ResearchAcademicUnit.HomePage" %>

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
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    معلومات النماذج
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <%--<div class="row">
                    <div class="col-12 mb-2">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-header mb-2">
                                    معلومات النماذج
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-3">
                                        <label>رقم النموذج</label>
                                        <asp:Label ID="lblFormId" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="form-group col-sm-3">
                                        <label>النوع</label>
                                        <asp:DropDownList ID="ddlFormType" runat="server" CssClass="jsSelect col-12">
                                            <asp:ListItem Value="0">البحث العلمي</asp:ListItem>
                                            <asp:ListItem Value="1">الدراسات العليا</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-sm-3">
                                        <label>اسم النموذج عربي</label>
                                        <asp:TextBox ID="txtAName" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-sm-3">
                                        <label>اسم النموذج انجليزي</label>
                                        <asp:TextBox ID="txtEName" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12 col-md-4">
                                        <label class="col-12">متاح من تاريخ</label>
                                        <input type="date" runat="server" id="txtOpenDate" class="form-control" width="100%" />
                                    </div>
                                    <div class="form-group col-sm-12 col-md-4">
                                        <label class="col-12">يغلق بتاريخ</label>
                                        <input type="date" runat="server" id="txtCloseDate" class="form-control" width="100%" />
                                    </div>
                                    <div class="form-group col-sm-12 col-md-4 mt-auto">
                                        <asp:CheckBox ID="chkDirector" runat="server" class="form-control col-12" Text="مؤشر رئيس القسم" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-12 col-sm-12">
                                        <label>تعليمات واجراءات النموذج</label>
                                        <asp:TextBox ID="txtInstruction" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-3 col-md-3 col-sm-12">
                                        <asp:Button ID="btnSubmit" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnNew" runat="server" Text="جديد" CssClass="btn btn-info" OnClick="btnNew_Click" />
                                        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="2500"></asp:Timer>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <div class="col-sm-12 mb-2" runat="server" id="currentDiv">
                        <div class="card">
                            <div class="card-body">
                                <ul class="nav nav-pills nav-justified mb" id="mytabs" runat="server">
                                    <li class="nav-item">
                                        <asp:LinkButton class="nav-link active" ID="lnkTab1" runat="server" OnClick="lnkTab1_Click">النماذج الحالية</asp:LinkButton>
                                    </li>
                                    <li class="nav-item">
                                        <asp:LinkButton class="nav-link" ID="lnkTab2" runat="server" OnClick="lnkTab2_Click">طلباتي</asp:LinkButton>
                                    </li>
                                </ul>

                                <%--                                <div class="card-header mb-2">
                                    النماذج الحالية
                                </div>--%>
                                <div class="row" id="tab1" runat="server">
                                    <div class="form-group col-md-12 col-sm-12">
                                        <asp:GridView ID="grdFormInfo" runat="server" CssClass="table table-bordered text-center" Font-Size="Small" AutoGenerateColumns="false"
                                            OnRowDataBound="grdFormInfo_RowDataBound" EmptyDataText="لا يوجد نماذج متاحة حاليا">
                                            <HeaderStyle CssClass="thead-light" />
                                            <AlternatingRowStyle CssClass="progress-bar-striped" />
                                            <Columns>
                                                <asp:BoundField DataField="FormId" HeaderText="رقم النموذج" />
                                                <asp:BoundField DataField="type" HeaderText="القسم" />
                                                <asp:BoundField DataField="FormAName" HeaderText="اسم النموذج - عربي" />
                                                <asp:BoundField DataField="FormEName" HeaderText="اسم النموذج - انجليزي" />
                                                <asp:BoundField DataField="OpenFrom" HeaderText="مفتوح من تاريخ" DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="ClosedAt" HeaderText="متاح لغاية تاريخ" DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="FromHeadEnter" HeaderText="المصدر" />
                                                <asp:BoundField DataField="FormInstruction" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FormLink" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="OriginalTable" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewInst" runat="server" OnClick="lnkViewInst_Click" ToolTip="عرض التعليمات"><i class="material-icons text-info">visibility</i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click" ToolTip="تقديم طلب"><i class="material-icons text-success">fiber_new</i></asp:LinkButton>
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
                                                <asp:BoundField DataField="Status" HeaderText="حالة الطلب" />
                                                <asp:BoundField DataField="FormLink" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="OriginalTable" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewFollowup" runat="server" OnClick="lnkViewFollowup_Click" ToolTip="عرض المعاملة"><i class="material-icons text-info">visibility</i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" ToolTip="تعديل طلب" Visible="false"><i class="material-icons text-success">edit</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-md-4 col-sm-12 mb-2" runat="server" visible="false" id="instructionDiv">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-header mb-2">
                                    تعليمات واجراءات النموذج<span class="float-left"><asp:ImageButton ID="imgClose" runat="server" CssClass="img-fluid" OnClick="imgClose_Click" ImageUrl="~/images/Close.png" Height="25" /></span>
                                </div>
                                <div class="form-group col-sm-12">
                                    <div class="row">
                                        <div class="col-md-12 h-100" id="viewInstDiv" runat="server">
                                            <asp:TextBox ID="txtInstructionView" runat="server" ReadOnly="true" CssClass="form-control" Font-Size="Small" TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <script type="text/javascript">
                    function LoginFail() {
                        $('#exampleModal').modal();
                    }
                </script>
                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel" runat="server">تعليمات النموذج</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                <div class="form-group col-md-12">
                                    <asp:TextBox ID="txtInstructionView" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="10" Width="100%" Style="resize: none"></asp:TextBox>
                                </div>
                                    </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">اغلاق</button>
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
                                <div class="form-group row">
                                    <div class="form-group col-md-12">
                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false"
                                            OnRowDataBound="GridView2_RowDataBound">
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:GridView ID="GridView3" runat="server"></asp:GridView>
    <script src="plugins/jquery-mask-input/jquery.mask.min.js"></script>

    <script>
        $('.jsSelect').select2();
    </script>
</asp:Content>
