<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OverAllResearchReport.aspx.cs" Inherits="ResearchAcademicUnit.OverAllResearchReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <asp:Button ID="btnApply" runat="server" Text="Get Data" CssClass="btn" OnClick="btnApply_Click" />
    </div>

    <div style="margin-bottom: 40px;">
        <asp:Button ID="btnExport" runat="server" Text="تصدير ملف اكسل" CssClass="btn" OnClick="btnExport_Click" Width="150px" />
    </div>
    <div style="margin: 0 auto; width: 95%">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="grd">
            <Columns>
                <asp:BoundField HeaderText="الرقم" DataField="ReId" />
                <asp:BoundField HeaderText="عنوان البحث" DataField="ReTitle" />
                <asp:BoundField HeaderText="المجلة" DataField="Magazine" />
                <asp:BoundField HeaderText="الربع" DataField="MClass" />
                <asp:BoundField HeaderText="السنة" DataField="ReYear" />
                <asp:BoundField HeaderText="الشهر" DataField="ReMonth" />
                <asp:BoundField HeaderText="حالة البحث" DataField="ReStatus" />
                <asp:BoundField HeaderText="التشاركية " DataField="ReParticipate" />
                <asp:BoundField HeaderText="حالة الفريق البحثي" DataField="TeamType" />
                <asp:BoundField HeaderText="القطاع البحثي" DataField="resector" />
                <asp:BoundField HeaderText="المجال البحثي" DataField="refield" />
                <asp:BoundField HeaderText="ترتيب الباحثين" DataField="Aff_Auther" />
                <asp:BoundField HeaderText="اسماء الباحثين" DataField="RNames" />
                <asp:BoundField HeaderText="العدد الكلي" DataField="TotalR" />
                <asp:BoundField HeaderText="العدد الداخلي" DataField="TotalRIn" />
                <asp:BoundField HeaderText="حاصل على دعم" DataField="InSupport" />
                <asp:BoundField HeaderText="الاسم" DataField="SupRName" />
                <asp:BoundField HeaderText="قيمة الدعم" DataField="SupAmount" />
                <asp:BoundField HeaderText="حاصل على مكافأة" DataField="Reward" />
                <asp:BoundField HeaderText="الاسم" DataField="AwardRName" />
                <asp:BoundField HeaderText="القيمة" DataField="AwardAmount" />
            </Columns>
        </asp:GridView>
    </div>


</asp:Content>
