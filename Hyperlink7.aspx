<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink7.aspx.cs" Inherits="WebApplication1.Hyperlink7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="studentIdTextBox">Student Id:</label>
            <asp:TextBox ID="studentIdTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="advisorIdTextBox">Advisor Id:</label>
            <asp:TextBox ID="advisorIdTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
        </div>
        <div>
            <asp:Button ID="btnRedirect" runat="server" Text="Go to All Functionalities As an Admin" OnClick="btnRedirect_Click" />
        </div>
    </form>
</body>
</html>
