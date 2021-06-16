<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="IndexCollege.aspx.cs" Inherits="ResearchAcademicUnit.IndexCollege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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

        .headerDiv {
            border-radius: 15px;
            border: 1px solid #8A91A1;
            color: white;
            width: 100%;
            margin: 0 auto 10px auto;
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

            .btn:hover {
                box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            }
    </style>
        <script language="Javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" style="width: 95%; margin: 0 auto 0 auto; overflow-x: auto; box-sizing: border-box; background-color: #6d7486; padding: 15px;">
<%--        <div class="headerDiv">
            <div style="float: right; width: 30%; padding-right: 10px; padding-top: 5px">
                <img src="images/newlogo.png" width="250" height="80" style="max-width: 90%" />
            </div>
            <div style="float: right; width: 35%; text-align: center; padding-top: 20px;">
                نظام الاستعلام البحثي
            </div>
            <div style="float: left; width: 30%; text-align: left; padding-top: 20px; padding-left: 10px">
                عمادة الدراسات والبحث العلمي<br />
                قسم البحث العلمي
            </div>
            <div style="clear: both"></div>
        </div>--%>
<%--        <div class="headerDiv" runat="server" id="logDiv">
            <div style="padding: 10px; box-sizing: border-box">
                <asp:TextBox ID="txtUser" runat="server" onkeypress="return isNumberKey(event)" placeholder="اسم المستخدم" AutoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="yellow" Font-Size="Medium" ControlToValidate="txtUser"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="أرقام فقط" ControlToValidate="txtUser" Font-Size="Medium" ForeColor="Yellow" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            </div>
            <div style="padding: 10px; box-sizing: border-box">
                <asp:TextBox TextMode="Password" ID="txtPW" runat="server" placeholder="كلمة المرور"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="yellow" Font-Size="Medium" ControlToValidate="txtPW"></asp:RequiredFieldValidator>
            </div>
            <div style="padding: 10px; box-sizing: border-box">
                <asp:Button ID="btnLogin" runat="server" Text="دخول" OnClick="btnLogin_Click" CssClass="btn" />
                <asp:Button ID="btnBack1" CausesValidation="false" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="رجوع" OnClick="btnBack_Click" CssClass="btn" />

            </div>
        </div>--%>
        <div runat="server" id="mainDiv">
<%--            <div class="headerDiv" style="height: 300px">--%>
                <table style="text-align: center; width: 100%; margin: 0px auto 0px auto; justify-content: center;">
                    <tr>
                        <td style="text-align: center">
                            <div class="poster">
                                <span class="ribbon">ملخص الأبحاث </span>
                                <a href="CollegeAbstract.aspx">
                                    <img src="" alt=""
                                        title="ملخص الأبحاث"
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>ملخص الأبحاث على مستوى الكلية</h3>
                                        <center>
                                <div class="rate">
                                    <div class="gerne">
                                        
                                    </div>
                                    <span class="greyinfo">
                                    </span>
                                </div>
                            </center>
                                    </div>
                                </a>
                            </div>
                            <div class="poster">
                                <span class="ribbon">البطاقة البحثية</span>
                                <a href="CollegeResearcher.aspx">
                                    <img src="" alt=""
                                        title="البطاقة البحثية"
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير الأداء البحثي للكلية</h3>
                                        <center>
                                <div class="rate">
                                    <div class="gerne">
                                    </div>
                                    <span class="greyinfo">البطاقة البحثية
                                    </span>
                                </div>
                            </center>
                                    </div>
                                </a>
                            </div>
                            <div class="poster">
                                <span class="ribbon">أبحاث الكلية </span>
                                <a href="CollegeInfo.aspx">
                                    <img src="" alt=""
                                        title="أبحاث الكلية"
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير النشاطات البحثية </h3>
                                        <center>
                                <div class="rate">
                                    <div class="gerne">
                                    </div>
                                    <span class="greyinfo">للكلية
                                    </span>
                                </div>
                            </center>
                                    </div>
                                </a>
                            </div>

                        </td>
                    </tr>
                </table>
<%--            </div>--%>
<%--            <div class="contentDiv" style="padding: 20px">
                <asp:Button ID="btnBack" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="رجوع" OnClick="btnBack_Click" CssClass="btn" />

                <asp:Button ID="btnChangePW" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="تغيير كلمة المرور" OnClick="btnChangePW_Click" CssClass="btn" Width="150px" />

                <asp:Button ID="btnLogout" CausesValidation="false" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="تسجيل خروج" OnClick="btnLogout_Click" CssClass="btn" />
            </div>--%>
        </div>
    </div>
</asp:Content>
