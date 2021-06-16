<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ResearchAcademicUnit.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="new_js/NewCssMenu.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="#mainDiv">
        <%--dir="rtl" style="width: 95%; margin: 0 auto 0 auto;  overflow-x: auto; box-sizing: border-box; background-color: #6d7486; padding: 15px;">--%>
        <table style="text-align: center; width: 100%; margin: 0px auto 0px auto; justify-content: center">
            <tr>
                <td style="text-align: center">
                    <div class="BlocksHome" id="AjaxFilter">
                        <div class="Block" id="upDiv" runat="server" visible="false">
                            <a>
                                <img src="images/setting.jpg" width="255" height="370" alt="تحميل بيانات الباحثين">
                                <span class="NumberCard">
                                    <span>مدير</span>
                                    <em>النظام</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/setting.jpg);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a href="UploadData.aspx">اعدادات النظام</a>
                                    <a href="UploadData.aspx"></a>
                                </ul>
                                <a href="UploadData.aspx">
                                    <h2>خاص بعمادة الدراسات العليا والبحث العلمي</h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner"></div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
                                <li><a href="NewResearcher.aspx"><em>تحميل بيانات الباحثين</em></a></li>
                                <li><a href="NewResearch.aspx"><em>تحميل بيانات الابحاث</em></a></li>
                                <li><a href="ResearchSearch.aspx"><em>استعلام عن الابحاث</em></a></li>
                            </ul>
                        </div>

                        <div class="Block" id="UDiv" runat="server">
                            <a>
                                <img src="images/uni.jpg" width="255" height="370" alt="الجامعة">
                                <span class="NumberCard">
                                    <span>استعلام</span>
                                    <em>الجامعة</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/uni.jpg);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a>الجامعة</a>
                                    <a></a>
                                </ul>
                                <a>
                                    <h2>الاستعلام على مستوى الجامعة</h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner"></div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
<%--                                <li runat="server" visible="false" id="uni1"><a href="UniAbstract.aspx"><em>ملخص الابحاث</em></a></li>--%>
                                
                                <li runat="server" visible="false" id="uni3"><a href="UniResearcher.aspx"><em>البطاقة البحثية</em></a></li>
<%--                                <li runat="server" visible="false" id="uni4"><a href="CollegeComp.aspx"><em>الأداء البحثي للكليات</em></a></li>--%>
                                <li runat="server" visible="false" id="uni5"><a href="ResearchRank.aspx"><em>MEU SR RANK</em></a></li>
                                <li runat="server" visible="false" id="uni6"><a href="FullReport.aspx"><em>Full Report</em></a></li>
                                <li runat="server" visible="false" id="uni7"><a href="RStatusRep.aspx"><em>Research Status</em></a></li>
                                <li runat="server" visible="false" id="uni8"><a href="ResearchRankCertificate.aspx"><em>Certificate</em></a></li>
                                <li><a href="UniversityInfo.aspx"><em>أبحاث الجامعة</em></a></li>
                            </ul>
                        </div>

                        <div class="Block" id="CDiv" runat="server" visible="false">
                            <a>
                                <img src="images/college.jpg" width="255" height="370" alt="الكلية">
                                <span class="NumberCard">
                                    <span>استعلام</span>
                                    <em>الكلية</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/college.png);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a>الكلية</a>
                                    <a></a>
                                </ul>
                                <a>
                                    <h2>الاستعلام على مستوى الكلية والقسم</h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner"></div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
                                <%--<li><a href="CollegeAbstract.aspx"><em>ملخص الأبحاث</em></a></li>--%>
                                <li><a href="CollegeResearcher.aspx"><em>البطاقة البحثية</em></a></li>
                                <li><a href="CollegeInfo.aspx"><em>أبحاث الكلية</em></a></li>
                            </ul>
                        </div>

                        <div class="Block" id="RDiv" runat="server" visible="false">
                            <a>
                                <img src="images/res.jpg" width="255" height="370" alt="الباحث">
                                <span class="NumberCard">
                                    <span>استعلام</span>
                                    <em>الباحث</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/res.jpg);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a>الباحث</a>
                                    <a></a>
                                </ul>
                                <a>
                                    <h2>الاستعلام على مستوى الباحث</h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner"></div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
                                <li><a href="Researcher.aspx"><em>البطاقة البحثية</em></a></li>
                                <li><a href="Info.aspx"><em>أبحاث عضو هيئة التدريس</em></a></li>
                            </ul>
                        </div>

<%--                        <div class="Block">
                            <a href="RFileMenu.aspx">
                                <img src="images/ntag2.png" width="255" height="370" alt="ملف النتاج البحثي">
                                <span class="NumberCard">
                                    <span>النتاج</span>
                                    <em>بحثي</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/ntag2.png);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a href="RFileMenu.aspx">ملف النتاج البحثي</a>
                                    <a href="RFileMenu.aspx"></a>
                                </ul>
                                <a href="RFileMenu.aspx">
                                    <h2>ملف النتاج البحثي</h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner">ملف النتاج البحثي</div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
                                <li><a href="RFileMenu.aspx"><em></em></a></li>
                                <li><a href="RFileMenu.aspx"><em></em></a></li>
                                <li><em>النوع</em></li>
                            </ul>
                        </div>--%>
                    </div>
                    <div style="clear: both"></div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
