<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupportAwardRep.aspx.cs" Inherits="ResearchAcademicUnit.SupportAwardRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <%--        <asp:UpdatePanel ID="getDataPanel" runat="server">
        <ContentTemplate>--%>
    <div class="contentDiv2" style="direction: ltr; width: 98%; margin: 0 auto 0 auto">
        <asp:Label ID="lblP" runat="server" Text="Period"></asp:Label>
        <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="ChosenSelector" OnSelectedIndexChanged="ddlFromYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlFromYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlFromMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlToYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlToMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlType" runat="server" CssClass="ChosenSelector">
            <asp:ListItem Value="1" Selected="True">طلب دعم رسوم</asp:ListItem>
            <asp:ListItem Value="2">طلب مكافأة</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnApply" runat="server" Text="Get Data" CssClass="btn" OnClick="btnApply_Click" />
    </div>
    <%--        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlFromYear" />
            <asp:PostBackTrigger ControlID="btnApply" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <div style="margin:10px auto; width: 90%;border:1px solid">
        <asp:UpdatePanel ID="getDataPanel" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" CssClass="grd" Width="100%" AutoGenerateColumns="false" Font-Size="small"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="رمز البحث" DataField="ReId" />
                        <asp:BoundField HeaderText="عنوان البحث" DataField="ReTitle" />
                        <asp:BoundField HeaderText="اسم المجلة" DataField="Magazine"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="الربع" DataField="MClass"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="السنة" DataField="ReYear" />
                        <asp:BoundField HeaderText="الشهر" DataField="ReMonth" />
                        <asp:BoundField HeaderText="حالة البحث" DataField="ReStatus"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="حالة النشر" DataField="ReParticipate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="حالة الفريق" DataField="TeamType"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="ترتيب الباحثين" DataField="Aff_Auther"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="اسماء الباحثين" DataField="RNames"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="العدد الكلي للباحثين" DataField="TotalR" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField HeaderText="العدد الداخلي للباحثين" DataField="TotalRIn"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                        <asp:BoundField HeaderText="حصل على دعم رسوم نشر" DataField="InSupport" />
                        <asp:BoundField HeaderText="اسم صاحب البحث" DataField="SupRName" />
                        <asp:BoundField HeaderText="قيمة دعم رسوم نشر" DataField="SupAmount" />
                        <asp:TemplateField HeaderText="شيك رسوم نشر">
                            <ItemTemplate>
                                <asp:TextBox ID="SChequeNo" runat="server" placeholder="رقم الشيك" Width="30%"></asp:TextBox>
                                <input type="date" id="SChequeD" runat="server" placeholder="تاريخ الشيك" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="حصل على دعم نشر بحث" DataField="Reward" />
                        <asp:BoundField HeaderText="اسم صاحب البحث" DataField="AwardRName" />
                        <asp:BoundField HeaderText="قيمة دعم نشر البحث" DataField="AwardAmount" />
                        <asp:TemplateField HeaderText="شيك نشر بحث">
                            <ItemTemplate>
                                <asp:TextBox ID="AChequeNo" runat="server" placeholder="رقم الشيك" Width="30%"></asp:TextBox>
                                <input type="date" id="AChequeD" runat="server" placeholder="تاريخ الشيك" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk" runat="server" OnClick="lnk_Click">حفظ</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="رقم الباحث" DataField="AcdId" />
                    </Columns>
                </asp:GridView>
                <div style="position: fixed; top: 50%; left: 20%; background-color: #5f5f5f; border-radius: 10px; text-align: center; width: 50%; padding: 50px; color: white" runat="server" id="msgDiv" visible="false">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
                    <asp:Button ID="btnOk" runat="server" Text="عودة" OnClick="btnOk_Click" CssClass="btn" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <asp:GridView ID="GridView2" runat="server" CssClass="grd" Width="100%" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="حالة النشر" DataField="RType" />
                <asp:BoundField HeaderText="عدد الابحاث" DataField="RCount" />
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:GridView ID="GridView3" runat="server" CssClass="grd" Width="100%" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="" DataField="RType" />
                <asp:BoundField HeaderText="دعم" DataField="RSupport" HtmlEncode="False" />
                <asp:BoundField HeaderText="مكافأة" DataField="RAward" HtmlEncode="False" />
            </Columns>
        </asp:GridView>
    </div>

    <div>
        <asp:GridView ID="GridView4" runat="server" CssClass="grd" Width="100%" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="الربع" DataField="qrt" />
                <asp:BoundField HeaderText="عدد الأبحاث" DataField="RCount" HtmlEncode="False" />
                <asp:BoundField HeaderText="مجموع الدعم" DataField="SupportAmount" HtmlEncode="False" />
                <asp:BoundField HeaderText="مجموع المكافأة" DataField="AwardAmount" HtmlEncode="False" />
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:GridView ID="GridView5" runat="server" CssClass="grd" Width="100%" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="الحالة" DataField="status" />
                <asp:BoundField HeaderText="الترتيب" DataField="RCount" HtmlEncode="False" />
                <asp:BoundField HeaderText="عدد الأبحاث" DataField="order" HtmlEncode="False" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
