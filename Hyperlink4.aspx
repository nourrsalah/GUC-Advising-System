<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink4.aspx.cs" Inherits="WebApplication1.Hyperlink4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="startDateTextBox">Start Date:</label>
            <asp:TextBox ID="startDateTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="endDateTextBox">End Date:</label>
            <asp:TextBox ID="endDateTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="semesterCodeTextBox">Semester Code:</label>
            <asp:TextBox ID="semesterCodeTextBox" runat="server"></asp:TextBox>
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
