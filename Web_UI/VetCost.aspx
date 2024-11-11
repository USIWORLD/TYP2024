
<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="VetCost.aspx.cs" Inherits="Farmacy_App.VetCost" Async="true" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
                  <!-- BOOTSTRAP STYLES-->
 <link href="assets/css/bootstrap.css" rel="stylesheet" />
 <!-- Bootstrap core CSS -->
 <link rel="stylesheet" href="assets/css/bootstrap.min.css">
 <!-- FONTAWESOME STYLES-->
 <link href="assets/css/font-awesome.css" rel="stylesheet" />
 <!-- MORRIS CHART STYLES-->
 <link href="assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
 <!-- CUSTOM STYLES-->
 <link href="assets/css/custom.css" rel="stylesheet" />
 <!-- MAIN STYLE -->
 <link href="assets/css/style1.css" rel="stylesheet" />
 <link href="assets/css/style.css" rel="stylesheet" />
 <!-- GOOGLE FONTS-->
 <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
</asp:Content>

   
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <main id="main" class="main">
        <div class="pagetitle">
            <h1>Vet Services</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="dashboard.aspx">Vet</a></li>
                  
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <!-- Current Month Report -->
                    <div class="card">
                         <div class="card-body">
                            <h6 class="card-title">Current Month</h6>
                            <div class="table table-responsive">
                                    <div class="table table-responsive">
    
                                 <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                         <asp:TableHeaderRow Style="font-weight: normal;">
                                            <asp:TableHeaderCell Style="font-weight: normal;">Visit Date</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Animal Tag</asp:TableHeaderCell>
                                             <asp:TableHeaderCell Style="font-weight: normal;">Purpose</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Cost (Rands)</asp:TableHeaderCell>
                                            
                                          </asp:TableHeaderRow>
                                      </asp:Table>
                                  </div>

                       

                                   <label style="color: #0043a6; display: inline-block; margin-right: 20px;">Total Allocated:</label>
                                    <asp:TextBox runat="server" ID="total" style="display: inline-block;    border-radius:4px;margin-right: 40px;"></asp:TextBox>

                                    <label style="color: #0043a6; display: inline-block; margin-right: 20px;">Total Used:</label>
                                     <asp:TextBox runat="server"  ID="cost" style="display: inline-block;border-radius:4px;"></asp:TextBox>

                                      
                                      
                                      <p id="err" runat="server" visible="true"></p>
                            </div>
                            </div>
                        </div>
                   
                </section>
        </main>




 <!-- JQUERY SCRIPTS -->
<script src="assets/js/jquery-1.10.2.js"></script>
<!-- BOOTSTRAP SCRIPTS -->
<script src="assets/js/bootstrap.min.js"></script>
<!-- METISMENU SCRIPTS -->
<script src="assets/js/jquery.metisMenu.js"></script>
<!-- MORRIS CHART SCRIPTS -->
<script src="assets/js/morris/raphael-2.1.0.min.js"></script>
<script src="assets/js/morris/morris.js"></script>
<!-- CUSTOM SCRIPTS -->
<script src="assets/js/custom.js"></script>
<!-- OTHER SCRIPTS -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/5.0.2/js/bootstrap.min.js"></script>
<script src="https://code.jquery.com/jquery.min.js"></script>
</asp:Content>
