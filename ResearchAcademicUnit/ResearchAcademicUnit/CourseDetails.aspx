<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="ResearchAcademicUnit.CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/home_ar.css" rel="stylesheet" />
    <style>
        .div:hover {
            background: #e8e8e8;
            /*color:#fff;*/
            box-shadow: 2px 2px 2px 2px gray;
        }

        hr { 
  display: block;
  margin-top: 0.5em;
  margin-bottom: 0.5em;
  margin-left: auto;
  margin-right: auto;
  border-style: outset;
  border-width: 2px;
} 
    </style>



    <script> 
        function displayRadioValue1() {
            var ele = document.getElementsByName('cdate_value');
            for (i = 0; i < ele.length; i++) {
                if (ele[i].checked)
                    alert(ele[i].title);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home-container">
        <div class="spotlight">
            <div class="spot-block" id="coursename" runat="server">
                <div id="courseDetails" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
