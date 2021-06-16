<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeuRCV.aspx.cs" Inherits="ResearchAcademicUnit.MeuRCV" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>النتاج البحثي لعضو هيئة التدريس - جامعة الشرق الأوسط MEU</title>
    <link href="css/ExportStyle.css" rel="stylesheet" />
    <link rel="alternate" media="print" href="printversion.doc" />

    <script language="javascript" type="text/javascript">
        function test(divName, flag) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            document.location.href = document.URL;
            //if (flag == 1) {
            //    __doPostBack("btn");
            //}
        }
    </script>


    <%--    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printDiv');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
         </script>--%>

    <style>
        @media print {
            .pr {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <div class="rela-block page" id="printDiv" runat="server">

            <div class="side-bar">
<%--                <div class="mugshot">
                    <div class="logo">
                        <img id="imgR" runat="server" class="rela-block logo-svg"
                            src="~/images/default-avatar.jpg" />
                    </div>
                </div>--%>
                
                <p class="rela-block side-header1" id="name" runat="server"></p>
                <p class="rela-block side-header1" id="Degree" runat="server"></p>
                <div style="background-color:#3d5b74;width:350px;left:0;top:320px;position:absolute;padding:40px 30px 500px 0px;">
                <p class="rela-block side-header">المعلومات الشخصية</p>
                <p id="BOD" runat="server"></p>
                <p id="Nat" runat="server"></p>
                <p id="Address" runat="server"></p>
                <p id="Mobile" runat="server"></p>
                <p id="Email" runat="server"></p>
                <p class="rela-block caps side-header">المعلومات الوظيفية</p>
                <p class="rela-block list-thing" id="PosName" runat="server"></p>
                <p class="rela-block list-thing" id="College_Dept" runat="server"></p>
                <p class="rela-block list-thing" id="Qual" runat="server"></p>
                <p class="rela-block list-thing" id="Major" runat="server"></p>
                <p class="rela-block list-thing" id="Minor" runat="server"></p>
                <p class="rela-block list-thing" id="Exp" runat="server"></p>
                </div>
            </div>
            <div class="rela-block content-container">
                <div class="pr" style="float: left; top: 0; left: 0">
                    <input type="button" onclick="test('printDiv', 1);" value="    طباعة    " class="btn"/>
                    <asp:Button ID="btnBack" runat="server" Text="رجوع" OnClick="btnBack_Click" CssClass="btn"/>
                </div>

                <div id="QualDiv" runat="server" visible="false">
                    <div class="rela-inline separator">
                        <div class="floated">
                            المؤهلات العلمية
                        </div>
                    </div>
                    <div class="rela-inline justified">
                        <ul class="timeline timeline-split">
                            <li class="timeline-item" id="PhDMain" runat="server" visible="false">
                                <div class="timeline-info">
                                    <span id="PhDYear" runat="server">2016</span>
                                </div>
                                <div class="timeline-marker"></div>
                                <div class="timeline-content">
                                    الدكتوراة
                                    <p id="PhDInfo" runat="server">
                                        <span id="PhDUni" runat="server"></span>
                                        <br />
                                        <span id="PhDCollege" runat="server"></span>
                                        <br />
                                        <span id="PhDCountry" runat="server"></span>
                                        <br />
                                        <span id="PhDMajor" runat="server"></span>
                                        <br />
                                        <span id="PhDMinor" runat="server"></span>
                                        <br />
                                        <span id="PhDDegree" runat="server"></span>
                                    </p>
                                </div>
                            </li>
                            <li class="timeline-item" id="MsMain" runat="server" visible="false">
                                <div class="timeline-info">
                                    <span id="MsYear" runat="server">2016</span>
                                </div>
                                <div class="timeline-marker"></div>
                                <div class="timeline-content">
                                    الماجستير
                                    <p id="MsInfo" runat="server">
                                        <span id="MsUni" runat="server"></span>
                                        <br />
                                        <span id="MsCollege" runat="server"></span>
                                        <br />
                                        <span id="MsCountry" runat="server"></span>
                                        <br />
                                        <span id="MsMajor" runat="server"></span>
                                        <br />
                                        <span id="MsMinor" runat="server"></span>
                                        <br />
                                        <span id="MsDegree" runat="server"></span>

                                    </p>
                                </div>
                            </li>
                            <li class="timeline-item" id="BscMain" runat="server" visible="false">
                                <div class="timeline-info">
                                    <span id="BscYear" runat="server">2016</span>
                                </div>
                                <div class="timeline-marker"></div>
                                <div class="timeline-content">
                                    البكالوريوس
                                    <p id="BscInfo" runat="server">
                                        <span id="BscUni" runat="server"></span>
                                        <br />
                                        <span id="BscCollege" runat="server"></span>
                                        <br />
                                        <span id="BscCountry" runat="server"></span>
                                        <br />
                                        <span id="BscMajor" runat="server"></span>
                                        <br />
                                        <span id="BscMinor" runat="server"></span>
                                        <br />
                                        <span id="BscDegree" runat="server"></span>

                                    </p>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>

                <div id="AllResearchDiv" runat="server" visible="false">
                    <div class="rela-inline separator justified">
                        <div class="floated">
                            النتاج البحثي
                        </div>
                    </div>
                    <div id="PublishedReDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            الأبحاث المنشورة
                        </div>

                        <div class="rela-inline justified">
                            <asp:GridView ID="grdPublishedResearch" runat="server" CssClass="grd" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="RTitle" HeaderText="عنوان البحث" />
                                    <asp:BoundField DataField="MagName" HeaderText="اسم المجلة" />
                                    <asp:BoundField DataField="ISSN" HeaderText="ISSN" />
                                    <asp:BoundField DataField="PubStatusS" HeaderText="حالة النشر" />
                                    <asp:TemplateField HeaderText="قاعدة البيانات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%#(Eval("DBTypeI").ToString()=="1"?Eval("JorDB") :Eval("GlobalDBS")) %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PYear" HeaderText="سنة النشر" />
                                    <asp:BoundField DataField="ROrderS" HeaderText="ترتيب الباحث" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="ConfDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            المؤتمرات
                        </div>

                        <div class="rela-inline justified">
                            <asp:GridView ID="grdConf" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="CTitle" HeaderText="اسم المؤتمر" />
                                    <asp:BoundField DataField="CTypeS" HeaderText="نوع التمثيل في المؤتمر" />
                                    <asp:BoundField DataField="MagName" HeaderText="مجلة المشاركة" />
                                    <asp:BoundField DataField="CPlaceS" HeaderText="نوع المؤتمر" />
                                    <asp:TemplateField HeaderText="قاعدة البيانات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%#(Eval("DBTypeI").ToString()=="1"?Eval("JorDB") :Eval("GlobalDBS")) %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PYear" HeaderText="السنة" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="BookDiv" runat="server" visible="false" class="rela-inline justified">

                        <div class="rela-block greyed">
                            النشاطات التأليفية
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdBook" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="BookTitle" HeaderText="العنوان" />
                                    <asp:BoundField DataField="Publisher" HeaderText="اسم الناشر" />
                                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                                    <asp:BoundField DataField="BookYear" HeaderText="سنة النشر" />
                                    <asp:BoundField DataField="PubStatusS" HeaderText="حالة النشر" />
                                    <asp:BoundField DataField="AuthOrderS" HeaderText="ترتيب الباحث" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div id="OtherActivitiesDiv" runat="server" visible="false">
                    <div class="rela-inline separator justified">
                        <div class="floated">
                            النشاطات البحثية الأخرى
                        </div>
                    </div>
                    <div id="SeminarDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            الندوات البحثية
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdSeminar" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ActTitle" HeaderText="عنوان النشاط البحثي" />
                                    <asp:BoundField DataField="HoursCount" HeaderText="عدد الساعات" />
                                    <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                    <asp:BoundField DataField="StatusS" HeaderText="الحالة" />
                                    <asp:TemplateField HeaderText="قاعدة البيانات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%#Eval("FDate") + " | " + Eval("TDate") %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="WorkShopDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            ورش العمل البحثية
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdWorkShop" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ActTitle" HeaderText="عنوان النشاط البحثي" />
                                    <asp:BoundField DataField="HoursCount" HeaderText="عدد الساعات" />
                                    <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                    <asp:BoundField DataField="StatusS" HeaderText="الحالة" />
                                    <asp:TemplateField HeaderText="قاعدة البيانات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%#Eval("FDate") + " | " + Eval("TDate") %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="TrainDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            الدورات البحثية
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdTrain" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="ActTitle" HeaderText="عنوان النشاط البحثي" />
                                    <asp:BoundField DataField="HoursCount" HeaderText="عدد الساعات" />
                                    <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                    <asp:BoundField DataField="StatusS" HeaderText="الحالة" />
                                    <asp:TemplateField HeaderText="قاعدة البيانات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%# Convert.ToDateTime(Eval("FDate")).ToString("dd/MM/yyyy") + " | " + Convert.ToDateTime(Eval("TDate")).ToString("dd/MM/yyyy") %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="EvaluationDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            خبرات التحكيم والتقييم
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdExp" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="الدور">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDB" runat="server"><%#(Eval("RoleNameI").ToString()=="4"?Eval("RoleNameS") + "-"+ Eval("OtherRole"):Eval("RoleNameS") ) %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MagazineName" HeaderText="المجلة" />
                                    <asp:TemplateField HeaderText="نوع قاعدة البيانات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%#(Eval("DBNameI").ToString()=="1"?Eval("DBNameS") + "-"+ Eval("MgzText"):Eval("DBNameS") + "-"+ Eval("DbListS")) %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                    <asp:TemplateField HeaderText="الفترة بالسنوات">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server"><%# Convert.ToInt16(Eval("TYear")) - Convert.ToInt16(Eval("FYear")) %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div id="CommitteeDiv" runat="server" visible="false">
                    <div class="rela-inline separator justified">
                        <div class="floated">
                            اللجان البحثية
                        </div>
                    </div>
                    <div class="rela-inline justified">
                        <div class="rela-block greyed">
                            العضوية في اللجان البحثية
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdCommittee" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="CommName" HeaderText="اسم الجهة البحثية" />
                                    <asp:BoundField DataField="TypeS" HeaderText="نوع العضوية" />
                                    <asp:BoundField DataField="CommDate" HeaderText="تاريخ العضوية" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div id="AchDiv" runat="server" visible="false">
                    <div class="rela-inline separator justified">
                        <div class="floated">
                            الانجازات البحثية                    
                        </div>
                    </div>

                    <div id="AchievementDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            الانجازات الابتكارية
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdAchievement" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="TypeS" HeaderText="نوع الانجاز" />
                                    <asp:BoundField DataField="AName" HeaderText="الاسم بالعربي" />
                                    <asp:BoundField DataField="AAbstract" HeaderText="موجز بالعربي" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="CertificateDiv" runat="server" visible="false" class="rela-inline justified">
                        <div class="rela-block greyed">
                            شهادات التميز
                        </div>
                        <div class="rela-inline justified">
                            <asp:GridView ID="grdCertificate" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="100%">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="CName" HeaderText="عنوان الشهادة" />
                                    <asp:BoundField DataField="FromName" HeaderText="الجهة المانحة" />
                                    <asp:BoundField DataField="CommDate" HeaderText="تاريخ الشهادة" DataFormatString="{0:dd-MM-yyyy}" />
                                    <asp:BoundField DataField="CountryS" HeaderText="الدولة" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script data-cfasync="false" src="/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js"></script>
        <script src="https://static.codepen.io/assets/common/stopExecutionOnTimeout-de7e2ef6bfefd24b79a3f68b414b87b8db5b08439cac3f1012092b2290c719cd.js"></script>
        <script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
        <script id="rendered-js">
// Some assembly required, batteries sold separately

// Inspiration
// https://www.pinterest.com/pin/556687203921295406/
//# sourceURL=pen.js
        </script>
    </form>
</body>
</html>
