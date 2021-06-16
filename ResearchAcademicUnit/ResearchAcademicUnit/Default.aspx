<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JoufUniProject.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="new_js/NewCssMenu.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="#mainDiv">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <table style="text-align: center; width: 100%; margin: 0px auto 0px auto; justify-content: center;">
            <tr>
                <td style="text-align: center">
                    <div class="BlocksHome" id="AjaxFilter">
                        <div class="Block" id="scopusDiv" runat="server">
                            <a href="Index.aspx">
                                <img src="images/scopus.png" width="255" height="370" alt="نظام الاستعلام البحثي">
                                <span class="NumberCard">
                                    <span>استعلام</span>
                                    <em>بحثي</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/scopus.png);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a href="Index.aspx">نظام الاستعلام البحثي للأبحاث المنشورة على قاعدة بيانات سكوبكس</a>
                                    <a href="Index.aspx"></a>
                                </ul>
                                <a href="Index.aspx">
                                    <h2></h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner"></div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
                                <li><a href="Index.aspx"><em>الجامعة</em></a></li>
                                <li><a href="Index.aspx"><em>الكلية</em></a></li>
                                <li><a href="Index.aspx"><em>الباحث</em></a></li>
                            </ul>
                        </div>

                        <div class="Block" runat="server" id="fileDiv">
                            <a href="PersonalInfo.aspx">
                                <img src="images/file.jpg" width="255" height="370" alt="ملف النتاج البحثي">
                                <span class="NumberCard">
                                    <span>النتاج</span>
                                    <em>بحثي</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/file.jpg);"></span>
                                </div>

                                <div class="BlockTitleDesc">
                                    <ul class="MainTerms">
                                        <a href="PersonalInfo.aspx">النتاجات البحثية</a>
                                        <a href="PersonalInfo.aspx"></a>
                                    </ul>
                                    <a href="Agreement.aspx">
                                        <h2></h2>
                                        <div class="DescPost">
                                            <div class="DescPostInner"></div>
                                        </div>
                                    </a>
                                </div>
                                <ul class="BlockDets">
                                    <li><a href="PersonalInfo.aspx"><em>البيانات الشخصية والبحثية</em></a></li>
                                    <li><a href="PersonalInfo.aspx"><em>النشاطات البحثية</em></a></li>
                                    <li><a href="PersonalInfo.aspx"><em>تحميل الملفات البحثية</em></a></li>
                                    <li runat="server" visible="false" id="Report"><a href="ReasearchFileReport.aspx"><em>تقرير النتاجات</em></a></li>
                                </ul>
                            </a>
                        </div>

                        <div class="Block" runat="server" id="trainingDiv" visible="false">
                            <a href="AvailableCourse.aspx">
                                <img src="images/training.jpg" width="255" height="370" alt="النشاطات التدريبية البحثية">
                                <span class="NumberCard">
                                    <span>النشاطات</span>
                                    <em>التدريبية</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/training.jpg);"></span>
                                </div>

                                <div class="BlockTitleDesc">
                                    <ul class="MainTerms">
                                        <a href="AvailableCourse.aspx">النشاطات التدريبية البحثية</a>
                                        <a href="AvailableCourse.aspx"></a>
                                    </ul>
                                    <a href="AvailableCourse.aspx">
                                        <h2></h2>
                                        <div class="DescPost">
                                            <div class="DescPostInner"></div>
                                        </div>
                                    </a>
                                </div>
                                <ul class="BlockDets">
                                    <li><a href="AvailableCourse.aspx"><em>النشر البحثي</em></a></li>
                                    <li><a href="AvailableCourse.aspx"><em>القراءة الاحصائية والتحليل</em></a></li>
                                    <li><a href="AvailableCourse.aspx"><em>التوثيق البحثي</em></a></li>
                                    <li><a href="AvailableCourse.aspx"><em>التدريب التقني</em></a></li>
                                    <li runat="server" id="CRLi" visible="false"><a href="CourseRegistration.aspx"><em>طلبات الدورات</em></a></li>
                                </ul>
                            </a>
                        </div>

                        <div class="Block" runat="server" id="Div1">
                            <a href="#">
                                <img src="images/form.png" width="255" height="370" alt="نماذج البحث العلمي">
                                <span class="NumberCard">
                                    <span>نماذج</span>
                                    <em></em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/form.png);"></span>
                                </div>

                                <div class="BlockTitleDesc">
                                    <ul class="MainTerms">
                                        <a href="">نماذج البحث العلمي</a>
                                        <a href=""></a>
                                    </ul>
                                    <a href="#">
                                        <h2></h2>
                                        <div class="DescPost">
                                            <div class="DescPostInner"></div>
                                        </div>
                                    </a>
                                </div>
                                <ul class="BlockDets">
                                    <li><a href="ResearchFeeForm.aspx"><em>طلب دعم نشر بحث</em></a></li>
                                    <li><a href="ResearchRewardForm.aspx"><em>طلب مكافأة نشر بحث</em></a></li>
                                    <li><a href="FollowUpFormThesis.aspx"><em>تقرير المتابعة الشهري لطلبة الدراسات العليا</em></a></li>
                                    <li><a href="Requests.aspx"><em>متابعة الطلبات</em></a></li>
                                    <li runat="server" id="Li2" visible="false"><a href="AllRequests.aspx"><em>جميع الطلبات</em></a></li>
                                    <li runat="server" id="Li1" visible="false"><a href="SecPrintRequests.aspx"><em>طباعة الطلبات</em></a></li>
                                    <li runat="server" visible="false" id="uni2"><a href="SupportAwardInfo.aspx"><em>تقرير الدعم والمكافأة</em></a></li>
                                    <li runat="server" id="FeeSettingRole" visible="false"><a href="FeeSetting.aspx"><em>اعدادات</em></a></li>
                                </ul>
                            </a>
                        </div>

                        <div class="Block" id="GSDiv" runat="server" >
                            <a href="#">
                                <img src="images/gs.png" width="255" height="370" alt="نظام الدراسات العليا">
                                <span class="NumberCard">
                                    <span>الدراسات</span>
                                    <em>العليا</em>
                                </span>
                                <div class="BottomBarOpening">
                                    <span data-style="background-image: url(images/scopus.png);"></span>
                                </div>
                            </a>
                            <div class="BlockTitleDesc">
                                <ul class="MainTerms">
                                    <a href="#"></a>
                                    <a href="#"></a>
                                </ul>
                                <a href="#">
                                    <h2></h2>
                                    <div class="DescPost">
                                        <div class="DescPostInner"></div>
                                    </div>
                                </a>
                            </div>
                            <ul class="BlockDets">
                                <li runat="server" id="ad1" visible="false"><a href="Admin_FormInfo.aspx"><em>النماذج</em></a></li>
                                <li runat="server" id="ad2" visible="false"><a href="Admin_WorkFlow.aspx"><em>مسار العمل</em></a></li>
                                <li runat="server" id="ad3" visible="false"><a href="Com_OutSupervisor.aspx"><em>المشرفين الخارجيين</em></a></li>
                                <li runat="server" id="ad4" visible="false"><a href="Com_SupervisorAdopted.aspx"><em>إعتماد المشرفين</em></a></li>
                                <li runat="server" id="ad5" visible="false"><a href="Com_JournalDataBase.aspx"><em>إعدادات النظام</em></a></li>
                                <li runat="server" id="ad6" visible="false"><a href="Com_ExceptionLoad.aspx"><em>استثناء لمشرف</em></a></li>
                                <li runat="server" id="ad7" visible="false"><a href="Com_SupervisorAdoptedReport.aspx"><em>تقرير الاعتماد</em></a></li>
                                <li runat="server" id="ad9" visible="false"><a href="Admin_SupervisorReport.aspx"><em>تقرير المشرفين</em></a></li>
                                <li runat="server" id="ad8"><a href="Admin_RequestsFollowUp.aspx"><em>متابعة الطلبات</em></a></li>
                            </ul>
                        </div>
                    </div>
                    <div style="clear: both"></div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
