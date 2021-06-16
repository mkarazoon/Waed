<%@ Page Title="" ValidateRequest="False" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AvailableCourse.aspx.cs" Inherits="ResearchAcademicUnit.AvailableCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/home_ar.css" rel="stylesheet" />
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>

    <style>
        .grd {
            text-align: center;
            color: black;
            Border: 1px solid black;

        }

            .grd th {
                padding: 8px;
                background-color: #7f7f7f;
                color: white;
                Border: 1px solid black;
            }
            .grd td{
                Border: 1px solid black;
                height:40px;
                vertical-align:middle;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-block">
        <%--<div class="menu-background"></div>--%>
        <div class="container">
            <div class="row">
                <div class="wrap">
                    <div class="content">
                        <div class="home-container">
                            <div class="homesub-nav">
                                <asp:LinkButton runat="server" ID="lnkCurrent" class="active" OnClick="lnkCurrent_Click">الدورات الحالية</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkMyCourses" OnClick="lnkMyCourses_Click">الدورات المسجلة</asp:LinkButton>
                            </div>
                            <div class="spotlight" id="currentCourses" runat="server">
                                <div class="spot-block" id="coursename" runat="server">
                                </div>
                                <div id="myCourses" runat="server" visible="false">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                        CssClass="grd" Width="100%" GridLines="Both" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="النشاط البحثي" DataField="CourseName" />
                                            <asp:BoundField HeaderText="نوع النشاط" DataField="Type" />
                                            <asp:BoundField HeaderText="مستوى النشاط" DataField="Level" />
                                            <asp:BoundField HeaderText="اليوم" DataField="day" />
                                            <asp:BoundField HeaderText="التاريخ / الوقت" DataField="Details" />
                                            <asp:BoundField HeaderText="اسم المدرب" DataField="Trainer" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:TemplateField HeaderText="اسم المدرب">
                                                <ItemTemplate>
                                                    <div id="trainerDiv" runat="server"></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="مكان النشاط" DataField="Place" />
                                            <asp:BoundField HeaderText="الحالة" DataField="Status" />
                                            <asp:TemplateField HeaderText="تقييم الدورة">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEvalCourse" runat="server" OnClick="lnkEvalCourse_Click" >تقييم الدورة</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="رقم الدورة" DataField="CourseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:TemplateField HeaderText="الشهادة" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCert" runat="server" Visible="false" OnClick="lnkCert_Click" ><i class="material-icons" style="color: #E34724">description</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="مدة الدورة" DataField="CourseHour" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField HeaderText="اسم الدورة بالانجليزي" DataField="CourseNameE" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField HeaderText="حالة تقييم الدورة" DataField="EvalStatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
