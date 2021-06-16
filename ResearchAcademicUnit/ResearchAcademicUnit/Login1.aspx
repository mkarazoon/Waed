<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="ResearchAcademicUnit.Login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="background: url(images/bg.jpg) no-repeat;background-size:100% 100%;position:fixed;top:0;left:0;width: 100%; height: 100%;text-align:center">
    <div class="headerDiv" runat="server" id="logDiv" style="padding-bottom:10px; text-align: center; width: 25%; margin-top: 150px;background-color:white">
        <div style="clear: both"></div>
        <div>
            
        </div>
        <div style="display:flex;direction:ltr">
            <img src="images/graduate.png" style="float:left;width:50%"/>
            <div style="background-color: #e6e6e6; padding-top: 15%;float:left;width:50%;font-size:x-large">بوابة البحث العلمي</div>
        </div>
        <div style="clear: both"></div>
        <div style="box-sizing: border-box;width:100%;margin:30px auto 10px auto">
            <asp:TextBox ID="txtUser" style="margin:0px auto 0px auto;width:50%;" runat="server" onkeypress="return isNumberKey(event)" placeholder="اسم المستخدم" AutoComplete="off"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="txtUser" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div style="box-sizing: border-box;width:100%;margin:10px auto 10px auto">
            <asp:TextBox TextMode="Password" style="margin:0px auto 0px auto;width:50%" ID="txtPW" runat="server" placeholder="كلمة المرور"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="txtPW" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div style="box-sizing: border-box;">
            <asp:Button ID="btnLogin" style="margin:0px auto 0px auto;width:25%;font-size:100%" runat="server" Text="دخول" OnClick="btnLogin_Click" CssClass="btn" />
        </div>
<%--        <div style="box-sizing: border-box;width:100%;margin:10px auto 10px auto;text-align:right;padding:5px;">
            اسم المستخدم: الرقم الوظيفي
            <br />
            كلمة المرور: تاريخ الميلاد على شكل DDMMYYYY
        </div>
        <hr />--%>
        <div style="color:red;font-weight:bold">
            في حال مواجهة مشاكل بالدخول مراجعة دائرة تكنولوجيا المعلومات
            <br />
            <hr />
            على جميع أعضاء هيئة التدريس استكمال البيانات قبل نهاية دوام يوم الأحد الموافق 18/10/2020
        </div>
    </div>
</div>
</asp:Content>
