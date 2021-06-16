<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="IndexUni.aspx.cs" Inherits="ResearchAcademicUnit.IndexUni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <div runat="server" id="mainDiv">
            <table style="text-align: center; width: 100%; margin: 0px auto 0px auto; justify-content: center;">
                <tr>
                    <td style="text-align: center">
                        <div runat="server" id="OtherDiv" visible="true">
                            <div class="poster">
                                <span class="ribbon">تقرير ملخص عن الأبحاث</span>
                                <a href="UniAbstract.aspx">
                                    <img src="" alt=""
                                        title=""
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير عن جميع الأبحاث على مستوى الجامعة</h3>
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
                                <span class="ribbon">أبحاث العام الحالي</span>
                                <a href="UniversityInfoCurrentYear.aspx">
                                    <img src="" alt=""
                                        title=""
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير عن جميع أبحاث العام الحالي للجامعة</h3>
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
                                <a href="UniResearcher.aspx">
                                    <img src="" alt=""
                                        title="البطاقة البحثية"
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير الأداء البحثي للجامعة</h3>
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
                                <span class="ribbon">الأداء البحثي للكليات</span>
                                <a href="CollegeComp.aspx">
                                    <img src="" alt=""
                                        title="الأداء البحثي للكليات"
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير الأداء البحثي للكليات </h3>
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
                        </div>
                        <div runat="server" id="AllReDiv">
                            <div class="poster">
                                <span class="ribbon">أبحاث الجامعة </span>
                                <a href="UniversityInfo.aspx">
                                    <img src="" alt=""
                                        title="أبحاث الجامعة"
                                        width="200" height="150" />
                                    <div class="card_content"></div>
                                    <div class="info">
                                        <h3>تقرير النشاطات البحثية </h3>
                                        <center>
                                <div class="rate">
                                    <div class="gerne">
                                    </div>
                                    <span class="greyinfo">على مستوى الجامعة
                                    </span>
                                </div>
                            </center>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
