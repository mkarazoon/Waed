<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="IndexResearcher.aspx.cs" Inherits="ResearchAcademicUnit.IndexResearcher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" style="width: 95%; margin: 0 auto 0 auto; overflow-x: auto; box-sizing: border-box; background-color: #6d7486; padding: 15px;">
        <div runat="server" id="mainDiv">
            <table style="text-align: center; width: 100%; margin: 0px auto 0px auto; justify-content: center;">
                <tr>
                    <td style="text-align: center">

                        <div class="poster">
                            <span class="ribbon">البطاقة البحثية</span>
                            <a href="Researcher.aspx">
                                <img src="" alt=""
                                    title="البطاقة البحثية"
                                    width="200" height="150" />
                                <div class="card_content"></div>
                                <div class="info">
                                    <h3>تقرير الأداء البحثي للباحث</h3>
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
                            <span class="ribbon">أبحاث عضو هيئة التدريس </span>
                            <a href="Info.aspx">
                                <img src="" alt=""
                                    title="معلومات الباحث"
                                    width="200" height="150" />
                                <div class="card_content"></div>
                                <div class="info">
                                    <h3>تقرير النشاطات البحثية </h3>
                                    <center>
                                <div class="rate">
                                    <div class="gerne">
                                    </div>
                                    <span class="greyinfo">للباحث
                                    </span>
                                </div>
                            </center>
                                </div>
                            </a>
                        </div>

                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
