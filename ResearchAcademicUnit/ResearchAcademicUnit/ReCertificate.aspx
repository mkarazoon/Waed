<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReCertificate.aspx.cs" Inherits="ResearchAcademicUnit.ReCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media print {
            * {
                -webkit-print-color-adjust: exact;
            }
        }
        /*@media print {
            body {
                background-color: white;
                margin: 0 auto;
            }
        }*/
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
    <div class="cert1" id="pr">
        <div id="EText" runat="server" class="cert1-left" style="font-family: sans-serif">
        </div>

        <div style="position: fixed; top: 780px; left: 800px; text-align: left; font-family: sans-serif">
            <p>Prof. Dr. Alaa Al-Halhouli</p>
            <p>MEU President</p>
        </div>
        <div style="position: fixed; top: 780px; left: 100px; text-align: left; font-family: sans-serif">
            
            <p><%= DateTime.Now.ToString("MMMM dd, yyyy") %></p>
            <p>Date</p>
        </div>
    </div>

    <input type="button" id="btnPrint" runat="server" style="margin-left: 10px; margin-right: 10px" class="btn" onclick="test1('pr');" value="    طباعة    " />
</asp:Content>
