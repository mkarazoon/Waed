<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="ResearchAcademicUnit.Requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <style>
        .noData {
            border: none;
            font-size: 36px;
        }

        a:hover {
            background-color: #7f7f7f;
            color: white;
        }
    </style>
    <script>
        function notifyMe() {
            // Let's check if the browser supports notifications
            if (!("Notification" in window)) {
                alert("This browser does not support desktop notification");
            }

            // Let's check whether notification permissions have already been granted
            else if (Notification.permission === "granted") {
                // If it's okay let's create a notification
                var notification = new Notification("Hi there!");
            }

            // Otherwise, we need to ask the user for permission
            else if (Notification.permission !== "denied") {
                Notification.requestPermission().then(function (permission) {
                    // If the user accepts, let's create a notification
                    if (permission === "granted") {
                        var notification = new Notification("Hi there!");
                    }
                });
            }

            // At last, if the user has denied notifications, and you 
            // want to be respectful there is no need to bother them any more.
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TitleDiv">
        متابعة الطلبات
    </div>
    <%--    <button onclick="notifyMe()">Notify me!</button>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="notifyMe()" />--%>
    <div style="padding: 2%">
        <div id="ResearcherDiv" runat="server" style="margin: 1% auto; text-align: center; width: 50%">
            <div>
                <a href="ResearchFeeForm.aspx" style="float: left; text-decoration: none; font-size: x-large; padding: 5px; border-top-left-radius: 5px; border-top-right-radius: 5px; background-color: #7f7f7f; color: white">تقديم طلب جديد</a>
            </div>
            <asp:GridView ID="GridView1" runat="server"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td style="padding:5px">طلبات الباحث الحالية </td></tr></table>'
                EmptyDataText="لا يوجد طلبات حاليا" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="noData"
                CssClass="grd" Width="100%" OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="ReqId" />
                    <asp:BoundField HeaderText="تاريخ التقديم" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:TemplateField HeaderText="نوع الطلب">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# (Eval("RequestType").ToString()=="S"?"طلب دعم رسوم نشر بحث علمي":(Eval("RequestType").ToString()=="T"?"طلب مكافأة على نشر بحث علمي":(Eval("RequestType").ToString()=="NS"?"طلب دعم رسوم نشر بحث علمي غير مكتمل":"طلب مكافأة على نشر بحث علمي غير مكتمل"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="حالة الطلب" DataField="RequestFinalStatus" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="متابعة الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFollowUp" runat="server" OnClick="lnkFollowUp_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">fast_rewind</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="حذف الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDeleteRequest" runat="server" OnClick="lnkDeleteRequest_Click" Width="100%" OnClientClick="return confirm('هل أنت متأكد من حذف الطلب - لا يمكن استعادة البيانات في حال الحذف؟')"><i class="material-icons" style="color: #E34724;margin-top:5px">delete</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div id="DirectorDiv" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 50%">
            <asp:GridView ID="GridView3" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>طلبات رئيس القسم الحالية</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="ReqFromName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="SentDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="رقم السجل" DataField="AutoId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="متابعة الطلب" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFollowUp" runat="server" OnClick="lnkFollowUp_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">fast_rewind</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <div id="DeanDiv" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 50%">
            <asp:GridView ID="GridView4" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>طلبات عميد الكلية الحالية</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="ReqFromName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="SentDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewDean" runat="server" OnClick="lnkViewDean_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="رقم السجل" DataField="AutoId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="متابعة الطلب" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFollowUp" runat="server" OnClick="lnkFollowUp_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">fast_rewind</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <div id="RDeptDiv" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 50%">
            <asp:GridView ID="GridView6" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>طلبات رئيس قسم البحث العلمي الحالية</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="ReqFromName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="SentDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:BoundField HeaderText="حالة الطلب" DataField="ReqStatus" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewReDir" runat="server" OnClick="lnkViewReDir_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="رقم السجل" DataField="AutoId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="متابعة الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFollowUp" runat="server" OnClick="lnkFollowUp_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">fast_rewind</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <div id="RDeanDiv" runat="server" visible="false" style="margin: 1% auto; text-align: center; width: 50%">
            <asp:GridView ID="GridView5" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>طلبات عميد الدراسات العليا والبحث العلمي الحالية</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="RequestId" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="ReqFromName" />
                    <asp:BoundField HeaderText="تاريخ الطلب" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="SentDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="نوع الطلب" DataField="type" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewReDean" runat="server" OnClick="lnkViewReDean_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="رقم السجل" DataField="AutoId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="متابعة الطلب" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFollowUp" runat="server" OnClick="lnkFollowUp_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">fast_rewind</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <div id="FollowUpDiv" visible="false" runat="server" style="margin: 1% auto; text-align: center; width: 50%; height: auto; border: 5px outset; background-color: white">
            <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click"><i class="material-icons" style="color: #E34724;margin-top:5px">clear</i></asp:LinkButton>
            <asp:GridView ID="GridView2" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>الحركات التي تمت على الطلب</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="من" DataField="ReqFromName" />
                    <asp:BoundField HeaderText="إلى" DataField="RquToName" />
                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="RequestDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="حالة الطلب" DataField="ReqStatus" />
                    <asp:BoundField HeaderText="ملاحظات" DataField="Notes" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
