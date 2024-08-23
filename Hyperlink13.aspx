<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink13.aspx.cs" Inherits="WebApplication1.Hyperlink13" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="typeTextBox">Type:</label>
            <asp:TextBox ID="typeTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="dateTimeTextBox">Date:</label>
            <asp:TextBox ID="dateTimeTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="courseIDTextBox">Course ID:</label>
            <asp:TextBox ID="courseIDTextBox" runat="server"></asp:TextBox>
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
