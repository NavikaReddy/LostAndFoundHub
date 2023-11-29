<%@ Page Title="" Language="C#" MasterPageFile="~/HomeLayout.Master" AutoEventWireup="true" CodeBehind="LostPosts.aspx.cs" Inherits="LostAndFound.LostPosts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .users
{
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}
        .inner
{
    color:black;
        font-weight: 500;
}


.card
{
    background-image: url('https://images.unsplash.com/photo-1541140134513-85a161dc4a00?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8Z3JleSUyMGJhY2tncm91bmR8ZW58MHx8MHx8&w=1000&q=80');
}

.name{
    color:#047c82;
    font-weight: 500;
    text-transform: capitalize;
}

.profile-image
{
    border-radius: 50%;
    max-width: 300px;
    width: 100%;
    height: 300px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h1 style="text-align:center">Lost Posts</h1>
    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4">
    <asp:Repeater ID="userRepeater" runat="server">
    <ItemTemplate>
        <div class="col text-center mx-auto">
            <div class="card">
               <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("l_img")) %>' class="mx-auto p-3 profile-image" alt="" />
                <div class="card-body">
                    <p class="fs-5 fw-bold name">Product Name:  <span class="fw-light inner"><%# Eval("lname") %></span></p>
                    <p class=" fs-5 fw-bold name">Description:   <p class="fs-5 fw-light inner"><%# Eval("ldesc") %></p></p>
                    <p class=" fs-5 fw-bold name">Lost at: <span class="fw-light inner"><%# Eval("llostAt") %></span></p>
                    <!-- Edit button -->
                    <!--<asp:Button runat="server" CssClass="btn btn-warning float-start" Text="Edit" CommandArgument='<%# Eval("lid") %>' />-->
                    <!-- Delete button -->
                    <asp:Button runat="server" CssClass="btn btn-danger float-end" Text="Delete" OnClick="btndel_Click" CommandArgument='<%# Eval("lid") %>'/>
                </div>
            </div>
        </div>
            </ItemTemplate>
</asp:Repeater>
        <div id="noContainer" runat="server" class="col text-center mx-auto" visible="false">
            <img id="no" runat="server" style="height: 500px; width: 500px;"/>
        </div>
   </div>
</asp:Content>
