<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CourseCertificate.aspx.cs" Inherits="ResearchAcademicUnit.CourseCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media print{
            body{
                background-color:white;
                margin:0 auto;
            }
        }
    </style>

    <link href="https://fonts.googleapis.com/css?family=Playfair+Display&display=swap" rel="stylesheet">
    <script language="javascript" type="text/javascript">
        function test1(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            document.location.href = document.URL;// "PrintStudSeatsFinal.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cert" id="pr">
        <div id="EText" runat="server" class="cert-left">
            
            Middle East University
        </div>

        <div id="AText" runat="server" class="cert-right">
            تشهد جامعة الشرق الأوسط بأن
        </div>
        
<%--        <div id="aPresDiv" runat="server" class="apres">
            <p>رئيس الجامعة</p>
            <p>أ.د. محمد الحيلة</p>
        </div>
        <div id="ePresDiv" runat="server" class="apres">
            <p>MEU President</p>
            <p>Prof. Mohammad Al Hileh</p>
        </div>--%>
    </div>
    <%--<input type="button" id="btnPrint" runat="server" style="margin-left: 10px; margin-right: 10px" class="btn" onclick="test1('pr');" value="    طباعة    "/>--%>
</asp:Content>
