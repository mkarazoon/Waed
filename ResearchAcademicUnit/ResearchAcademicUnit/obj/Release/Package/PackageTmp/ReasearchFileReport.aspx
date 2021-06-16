<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReasearchFileReport.aspx.cs" Inherits="ResearchAcademicUnit.ReasearchFileReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-bottom:40px;">
        <asp:Button ID="btnExport" runat="server" Text="تصدير ملف اكسل" CssClass="btn" OnClick="btnExport_Click" Width="150px"/></div>
    <div style="margin:0 auto;width:95%">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="grd"
        OnDataBound="GridView1_DataBound">
        <Columns>
            <asp:BoundField HeaderText="الرقم الوظيفي" DataField="JobId" />
            <asp:BoundField HeaderText="الاسم" DataField="Name" />
            <asp:BoundField HeaderText="الكلية" DataField="College" />
            <asp:BoundField HeaderText="القسم" DataField="Dept" />
            <asp:BoundField HeaderText="المعلومات الشخصية" DataField="PInfo" />
            <asp:BoundField HeaderText="المؤهلات العلمية" DataField="Qual" />
            <asp:BoundField HeaderText="الاهتمامات البحثية" DataField="Interest" />
            <asp:BoundField HeaderText="روابط الباحث في " DataField="DbLink" />
            <asp:BoundField HeaderText="خبرات التحكيم والتقييم" DataField="Exp" />
            <asp:BoundField HeaderText="الابحاث المنشورة" DataField="Research" />
            <asp:BoundField HeaderText="المؤتمرات" DataField="Conf" />
            <asp:BoundField HeaderText="الكتب" DataField="Book" />
            <asp:BoundField HeaderText="الندوات والدورات وورش العمل" DataField="Seminar" />
            <asp:BoundField HeaderText="الانجازات الابتكارية" DataField="Ach" />
            <asp:BoundField HeaderText="العضوية في اللجان" DataField="Comm" />
            <asp:BoundField HeaderText="شهادات التميز البحثي" DataField="Cert" />
            <asp:BoundField HeaderText="الرسائل المناقشة/المشرف عليها" DataField="Thesis" />
            <asp:BoundField HeaderText="ملفات الابحاث" DataField="UploadR" />
            <asp:BoundField HeaderText="ملفات المؤتمرات" DataField="UploadC" />
            <asp:BoundField HeaderText="ملفات الكتب" DataField="UploadB" />
            <asp:BoundField HeaderText="ملفات الورش" DataField="UploadW" />
            <asp:BoundField HeaderText="ملفات شهادات التميز" DataField="UploadCR" />
            <asp:BoundField HeaderText="Last Seen" DataField="lastSeen" />
        </Columns>
    </asp:GridView>
        </div>


</asp:Content>
