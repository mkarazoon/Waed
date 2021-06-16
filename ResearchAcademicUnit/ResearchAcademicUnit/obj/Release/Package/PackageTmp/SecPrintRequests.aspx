<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SecPrintRequests.aspx.cs" Inherits="ResearchAcademicUnit.SecPrintRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TitleDiv">
        متابعة الطلبات
    </div>
    <div style="padding: 2%">
        <div id="SecPrint" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 50%">
            <asp:GridView ID="GridView6" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>الطلبات الحالية للكلية</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewReDir" runat="server" OnClick="lnkViewReDir_Click" Width="100%" ToolTip="عرض"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="طباعة كتابة التغطية">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPrintCover" runat="server" OnClick="lnkPrintCover_Click" Width="100%" ToolTip="طباعة وارسال"><i class="material-icons" style="color: #E34724;margin-top:5px">print</i></asp:LinkButton>
                            <asp:TextBox ID="txtFacultyReqNo" runat="server" Text='<%# (Eval("FacultyReqNo").ToString()) %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
        <div id="GSSec" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 75%">
            <asp:GridView ID="GridView1" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>الطلبات الحالية لعمادة الدراسات العليا</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false"
                OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
<%--                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewReDir" Visible="false" runat="server" OnClick="lnkViewReDir_Click" Width="100%" ToolTip="عرض"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="طباعة">
                        <ItemTemplate>
                            <div id="divWared" runat="server" visible="false">
                                <asp:Label ID="Label1" runat="server" Text="رقم الوارد"></asp:Label>
                                <asp:TextBox ID="txtGSOutNo" runat="server" Text='<%# (Eval("GSInNo").ToString()) %>' Width="50%"></asp:TextBox>
                                <asp:LinkButton ID="lnkPrintCoverRe" runat="server" OnClick="lnkPrintCoverRe_Click" Width="15%" ToolTip="تخزين وارسال لرئيس قسم البحث العلمي"><i class="material-icons" style="color: #E34724;margin-top:5px">save</i></asp:LinkButton>
                            </div>
                            <div id="gsSaderDiv" runat="server">
                                <asp:Label ID="Label3" runat="server" Text="رقم الصادر :"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text=" ع د ع / د / 8 /"></asp:Label>
                                <asp:TextBox ID="txtGSInNo" runat="server" Text='<%# (Eval("GSOutNo").ToString()) %>' Width="20%" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                <asp:LinkButton ID="lnkGenerateForm" runat="server" OnClick="lnkGenerateForm_Click" Width="15%" ToolTip="تخزين وارسال لرئيس قسم البحث العلمي"><i class="material-icons" style="color: #E34724;margin-top:5px">save</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Width="15%" ToolTip="عرض البحث"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="حالة الطلب" DataField="RequestFinalStatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkOk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:LinkButton ID="lnkSaveAll" runat="server" OnClick="lnkSaveAll_Click" CssClass="btn" style="padding:10px;text-decoration:none">حفظ الجميع</asp:LinkButton>
        </div>


        <div id="RectorOfficeDiv" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 75%">
            <div>
                <div style="display:inline-block;font-size:large" class="lblContentDiv">عدد طلبات الدعم لرسوم نشر بحث علمي <asp:Label ID="lblCountSupport" runat="server" Text=""></asp:Label></div>
                <div style="display:inline-block;font-size:large" class="lblContentDiv">عدد طلبات المكافأة على نشر بحث علمي <asp:Label ID="lblCountReward" runat="server" Text=""></asp:Label></div>
            </div>
            <asp:GridView ID="GridView2" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>الطلبات الحالية لنائب الرئيس الغير منجزة</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            1. <asp:LinkButton ID="lnkViewReDir" runat="server" OnClick="lnkViewReDir_Click" Width="80%" ToolTip="عرض"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="طباعة">
                        <ItemTemplate>
                            <%--2. <asp:LinkButton ID="lnkPrintCoverRe" runat="server" OnClick="lnkPrintCoverRe_Click" Width="80%" ToolTip="كتاب التغطية"><i class="material-icons" style="color: #E34724;margin-top:5px">book</i></asp:LinkButton>--%>
                            <div>
                                <asp:TextBox ID="txtGSOutNo" runat="server" Visible="false" Text='<%# (Eval("GSInNo").ToString()) %>'></asp:TextBox>
                            </div>
                            <div>
                                <asp:TextBox ID="txtGSInNo" Visible="false" runat="server" Text='<%# (Eval("GSOutNo").ToString()) %>'></asp:TextBox>
                                2. <asp:LinkButton ID="lnkGenerateFormRector" runat="server" OnClick="lnkGenerateFormRector_Click" Width="80%" ToolTip="كتاب التوصية"><i class="material-icons" style="color: #E34724;margin-top:5px">collections_bookmark</i></asp:LinkButton>
                                3. <asp:LinkButton ID="lnkGenerateDecision" runat="server" OnClick="lnkGenerateDecision_Click" Width="80%" ToolTip="القرار"><i class="material-icons" style="color: #E34724;margin-top:5px">email</i></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="حالة الطلب" DataField="RequestFinalStatus" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDone" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div>
                <asp:Button ID="btnDone" runat="server" Text="أنجزت" OnClick="btnDone_Click" CssClass="btn"/>
            </div>

            <asp:GridView ID="GridView3" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td> الطلبات الحالية لنائب الرئيس المنجزة</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            1. <asp:LinkButton ID="lnkViewReDir" runat="server" OnClick="lnkViewReDir_Click" Width="80%" ToolTip="عرض"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="طباعة">
                        <ItemTemplate>
<%--                            2. <asp:LinkButton ID="lnkPrintCoverRe" runat="server" OnClick="lnkPrintCoverRe_Click" Width="80%" ToolTip="كتاب التغطية"><i class="material-icons" style="color: #E34724;margin-top:5px">book</i></asp:LinkButton>--%>
                            <div>
                                <asp:TextBox ID="txtGSOutNo" runat="server" Visible="false" Text='<%# (Eval("GSInNo").ToString()) %>'></asp:TextBox>
                            </div>
                            <div>
                                <asp:TextBox ID="txtGSInNo" Visible="false" runat="server" Text='<%# (Eval("GSOutNo").ToString()) %>'></asp:TextBox>
                                2. <asp:LinkButton ID="lnkGenerateFormRector" runat="server" OnClick="lnkGenerateFormRector_Click" Width="80%" ToolTip="كتاب التوصية"><i class="material-icons" style="color: #E34724;margin-top:5px">collections_bookmark</i></asp:LinkButton>
                                3. <asp:LinkButton ID="lnkGenerateDecision" runat="server" OnClick="lnkGenerateDecision_Click" Width="80%" ToolTip="القرار"><i class="material-icons" style="color: #E34724;margin-top:5px">email</i></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="حالة الطلب" DataField="RequestFinalStatus" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
