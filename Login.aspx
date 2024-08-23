<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <h2>Login</h2>
            <div>
                <label for="username">Username:</label>
                <asp:TextBox ID="username" runat="server" placeholder="Enter your username"></asp:TextBox>
            </div>
            <div>
                <label for="password">Password:</label>
                <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </div>
            
        </div>
    </form>
</body>
</html>
