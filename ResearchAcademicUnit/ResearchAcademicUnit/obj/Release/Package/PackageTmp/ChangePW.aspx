<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChangePW.aspx.cs" Inherits="ResearchAcademicUnit.ChangePW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .headerDiv {
            border-radius: 15px;
            border: 1px solid #8A91A1;
            color: white;
            width: 100%;
            margin: 0 auto 10px auto;
        }

        .contentDiv {
            border-radius: 15px;
            border: 1px solid #8A91A1;
            /*color: white;*/
            width: 100%;
            margin: 0 auto 10px auto;
            box-sizing: border-box;
        }

        .lblDiv {
            float: right;
            background-color: #F7BA00;
            padding: 10px;
            border-radius: 35px;
            width: 100px;
            text-align: center;
        }

        .lblContentDiv {
            float: right;
            border: 1px solid #8A91A1;
            padding: 10px;
            margin: 0 2px 0 2px;
            border-radius: 35px;
            width: 100px;
            text-align: center;
            color: white;
        }

        .btn {
            border-radius: 6px;
            width: 100px;
            padding: 10px 10px;
            color: white;
            background-color: #F7BA00;
            font-size: medium;
            font-family: Calibri;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            cursor: pointer;
            /*box-shadow: 0 9px #999;*/
        }
        
        input[type=text], input[type=password] {
            outline: none;
            display: block;
            width: auto;
            padding-bottom: 7.5px;
            padding-top: 7.5px;
            border: 1px solid black;
            border-radius: 6px;
            text-align: center;
            margin: 1px 5px 1px 5px;
            font-size: 14px;
            autocomplete: off;
            font-family: Calibri;
            width:90%;
        }

            .btn:hover {
                box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            }

        .grd {
            text-align: center;
            -moz-border-radius: 20px;
            border-radius: 20px;
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" style="width: 95%; margin: 0 auto 0 auto; min-width: 1000px; box-sizing: border-box; background-color: #6d7486; padding: 15px;">
<%--        <div class="headerDiv">
            <div style="float: right; width: 30%; padding-right: 10px; padding-top: 5px">
                <img src="images/newlogo.png" width="250" height="80" style="max-width: 90%" />
            </div>
            <div style="float: right; width: 35%; text-align: center; padding-top: 20px;">
                ملخص الأبحاث
            </div>
            <div style="float: left; width: 30%; text-align: left; padding-top: 20px; padding-left: 10px">
                عمادة الدراسات والبحث العلمي<br />
                قسم البحث العلمي
            </div>
            <div style="clear: both"></div>
        </div>--%>
<%--        <div class="headerDiv" style="float: right; box-sizing: border-box; padding: 10px; width: 100%">--%>
            <center>
            <div style="margin: 0 auto 20px auto; width: 40%; box-sizing: border-box">
                <div class="lblDiv" style="margin-top:10px;margin-left:20px">كلمة المرور القديمة</div>
                <div class="lblContentDiv" style="width: 250px; text-align: right; margin: 0 auto 20px auto;">
                    <asp:TextBox ID="txtOldPW" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Yellow" ControlToValidate="txtOldPW" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div style="clear:both"></div>
                <div class="lblDiv" style="margin-top:10px;margin-left:20px">كلمة المرور الجديدة</div>
                <div class="lblContentDiv" style="width: 250px; text-align: right; margin: 0 auto 20px auto;">
                    <asp:TextBox ID="txtNewPW" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Yellow" ControlToValidate="txtNewPW" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="لا يوجد تطابق بكلمة المرور" ControlToCompare="txtNewPW" ControlToValidate="txtConfirmPW" Display="Dynamic" ForeColor="Yellow"></asp:CompareValidator>
                </div>
                <div style="clear:both"></div>
                <div class="lblDiv" style="margin-top:10px;margin-left:20px">تأكيد كلمة المرور</div>
                <div class="lblContentDiv" style="width: 250px; text-align: right; margin: 0 auto 20px auto;">
                    <asp:TextBox ID="txtConfirmPW" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Yellow" ControlToValidate="txtConfirmPW" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div style="clear:both"></div>
            </div>
                <div style="clear: both"></div>
                </center>
<%--        </div>--%>
        <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="contentDiv" style="padding: 20px;text-align:center">
<%--            <asp:Button ID="btnBack" CausesValidation="false" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="رجوع" OnClick="btnBack_Click" CssClass="btn" />--%>
            <asp:Button ID="btnChange"  Style="margin-left: 20px; margin-right: 20px" runat="server" Text="تغيير" OnClick="btnChange_Click" CssClass="btn" />
        </div>

    </div>
</asp:Content>
