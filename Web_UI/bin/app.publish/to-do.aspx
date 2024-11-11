<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="to-do.aspx.cs" Inherits="Farmacy_App.to_do" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
        <section class="section dashboard">
            <div class="row">
                <div class="pagetitle">
                    <h1 style="color: #0043a6;">To Do List</h1>
                    <nav>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Dashboard.aspx">Manager</a></li>
                            <li class="breadcrumb-item active">To Do List</li>
                        </ol>
                    </nav>
                </div>

                <div class="panel-body">
                <div class="table-responsive">
                    <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover" style="width: auto; table-layout: auto;">
                        <asp:TableHeaderRow Style="font-weight: normal;">
                            <asp:TableHeaderCell Style="font-weight: normal;">Date</asp:TableHeaderCell>
                            <asp:TableHeaderCell Style="font-weight: normal;"> Title</asp:TableHeaderCell>
                            <asp:TableHeaderCell Style="font-weight: normal;"> Note</asp:TableHeaderCell>  
                        </asp:TableHeaderRow>
                    </asp:Table>
                    <p id="P1" runat="server" visible="true"></p>
                </div>
                        <p id="err" runat="server" visible="true"></p>
                </div>
                    
            </div>

        </section>

    </main>

    <!-- Vendor JS Files -->
    <script src="assets/vendor/apexcharts/apexcharts.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/chart.js/chart.umd.js"></script>
    <script src="assets/vendor/echarts/echarts.min.js"></script>
    <script src="assets/vendor/quill/quill.js"></script>
    <script src="assets/vendor/simple-datatables/simple-datatables.js"></script>
    <script src="assets/vendor/tinymce/tinymce.min.js"></script>
    <script src="assets/vendor/php-email-form/validate.js"></script>

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>
</asp:Content>
