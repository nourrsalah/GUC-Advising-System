<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink11.aspx.cs" Inherits="WebApplication1.Hyperlink11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="courseIdTextBox">Course ID:</label>
            <asp:TextBox ID="courseIdTextBox" runat="server"></asp:TextBox>
        </div>
         <div>
            <asp:Button ID="btnDeleteCourse" runat="server" Text="Delete Course" OnClick="SubmitButton_Click" />
        </div>
    </form>
</body>
</html>
