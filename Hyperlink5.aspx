<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink5.aspx.cs" Inherits="WebApplication1.Hyperlink5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <label for="major">Major:</label>
            <asp:TextBox ID="majorTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="semester">Semester:</label>
            <asp:TextBox ID="semesterTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="creditHours">Credit Hours:</label>
            <asp:TextBox ID="creditHoursTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="courseName">Course Name:</label>
            <asp:TextBox ID="courseNameTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="offeredBit">Offered Bit:</label>
            <asp:TextBox ID="isOfferedTextBox" runat="server"></asp:TextBox>
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
