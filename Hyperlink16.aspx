<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink16.aspx.cs" Inherits="WebApplication1.Hyperlink16" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <label for="txtStudentID">Student ID:</label>
            <asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
