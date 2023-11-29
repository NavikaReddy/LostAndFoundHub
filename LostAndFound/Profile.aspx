<%@ Page Title="" Language="C#" MasterPageFile="~/HomeLayout.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LostAndFound.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
            .footer {
  position: fixed;
  left: 0;
  bottom: 0;
  width: 100%;
  background-color: #901d26;
  color: white;
  text-align: center;
}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align:center">Profile</h1>
    <table style="width:100%; height:60px; text-align:center; border-style:solid">
        <tr class="navbar">
            <td><a href="ResetPassword.aspx" style="color: #901d26; text-decoration: none; padding: 15px;">Change Password</a></td>
            <td><a href="FoundPosts.aspx" style="color: #901d26; text-decoration: none; padding: 15px;">Found Posts</a></td>
            <td><a href="LostPosts.aspx" style="color: #901d26; text-decoration: none; padding: 15px;">Lost Posts</a></td>
            <td><a href="Chat.aspx" style="color: #901d26; text-decoration: none; padding: 15px;">Chat</a></td>            
        </tr>
    </table>
</asp:Content>
