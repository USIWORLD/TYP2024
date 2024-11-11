<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="KraalSanitizer.aspx.cs" Inherits="Farmacy_App.KraalSanitizer" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- BOOTSTRAP STYLES-->
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <!-- FONTAWESOME STYLES-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <!-- CUSTOM STYLES-->
    <link href="assets/css/custom.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
    <!-- GOOGLE FONTS-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 style="color: #0043a6;">Allocate Sanitizer to Kraals</h2>

        <div class="panel panel-default" style="color: #1c1a1a">
            <div class="panel-heading" style="color: #0043a6">
                Allocate Sanitizer
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label style="font-weight:normal;">Select Kraal</label>
                            <asp:DropDownList ID="kraalDropdown" runat="server" CssClass="form-control"  OnSelectedIndexChanged="KraalDropdown_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label style="font-weight:normal;">Amount of Sanitizer to Allocate</label>
                            <asp:TextBox class="form-control" ID="sanitizerAmount" runat="server"></asp:TextBox>
                        </div>

                        <asp:Button ID="AllocateSanitizerButton" runat="server" Text="Allocate Sanitizer" OnClick="AllocateSanitizerButton_Click" class="btn btn-primary" />
                        <asp:Label ID="allocationResult" runat="server" ForeColor="Green"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

     <div class="panel panel-default" style="color: #1c1a1a">
    <div class="panel-heading" style="color: #0043a6">
        Kraal List
    </div>
    <div class="panel-body">
        <div class="table-responsive">
            <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Style="font-weight: normal;">Kraal Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell Style="font-weight: normal;">Kraal Size</asp:TableHeaderCell>
                    <asp:TableHeaderCell Style="font-weight: normal;">Sanitizer Allocated</asp:TableHeaderCell>
                    <asp:TableHeaderCell Style="font-weight: normal;">Size Sanitized</asp:TableHeaderCell>
                    <asp:TableHeaderCell Style="font-weight: normal;">Size Unsanitized</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                </asp:Table>
        </div></div>
    </div>
        </div>
    <!-- JQUERY SCRIPTS -->
    <script src="assets/js/jquery-1.10.2.js"></script>
    <!-- BOOTSTRAP SCRIPTS -->
    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>