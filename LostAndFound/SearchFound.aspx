<%@ Page Title="" Language="C#" MasterPageFile="~/HomeLayout.Master" AutoEventWireup="true" CodeBehind="SearchFound.aspx.cs" Inherits="LostAndFound.SearchFound" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <style>
        .users
{
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
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

.inner
{
    color:black;
        font-weight: 500;
}

.profile-image
{
    border-radius: 50%;
    max-width: 300px;
    width: 100%;
    height: 300px;
}
#ddlplace,#ddlprod,#ddldate
{
    margin-left:50px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align:center">Search Items</h1>
        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            Product: <asp:DropDownList ID="ddlprod" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                    </asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlprod" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            Place:<asp:DropDownList ID="ddlplace" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                    </asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlplace" />
        </Triggers>
    </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            Date:<asp:DropDownList ID="ddldate" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                    </asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddldate" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="submit" runat="server" Text="Submit" class="btn btn-success" OnClick="submit_Click"/>
                <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4"> 
    <asp:Repeater ID="userRepeater" runat="server">
    <ItemTemplate>
        <div class="col text-center mx-auto">
            <div class="card">
               <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("f_img")) %>' class="mx-auto p-3 profile-image" alt="" />
                <div class="card-body">
                    <p class="fs-5 fw-bold name">Product Name:  <span class="fw-light inner"><%# Eval("fname") %></span></p>
                    <p class=" fs-5 fw-bold name">Description:   <p class="fs-5 fw-light inner"><%# Eval("fdesc") %></p></p>

                    <p class=" fs-5 fw-bold name">Found at: <span class="fw-light inner"><%# Eval("ffoundAt") %></span></p>
                    
                    <!-- Claim button -->
                    <asp:Button runat="server" CssClass="btn btn-success float-start" Text="Claim" onClick="claim_func" CommandArgument='<%# Eval("fid") %>' />
                    <!-- Communicate button -->
                    <asp:Button runat="server" CssClass="btn btn-danger float-end" Text="Contact" onClick="contact_func" CommandArgument='<%# Eval("fid") %>'/>
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
