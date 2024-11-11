<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="StatusAdmin.aspx.cs" Inherits="Farmacy_App.medicationAdmin" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Working version of https://dribbble.com/shots/14552329--Exploration-Task-Management-Dashboard -->
    <title>Task Manager</title>
    <!-- BOOTSTRAP STYLES-->
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
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
<div class="container" >
       <div   class="row">
     <div class="col-lg-12">
         <div class="row">
             <div class="col-xxl-8 col-md-18">
                 <h2 style="color: #0043a6">FMD High Risk & Confirmed Animals</h2>
                 <div class="panel panel-default">
                     <div class="panel-heading">
                         <span>High Risk & Confirmed Animals Today</span>
                     </div>
                     <div class="panel-body">
                         <div class="table-responsive">
                             <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                 <asp:TableHeaderRow Style="font-weight: normal;">
                                     <asp:TableHeaderCell Style="font-weight: normal;">Animal_Tag</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;"> Gender</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;"> Breed</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">Colour</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">Weight</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">Height</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">HealthStatus</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">Age</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">Kraal</asp:TableHeaderCell>
                                     <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>
                                 </asp:TableHeaderRow>
                             </asp:Table>
                             <p id="err" runat="server" visible="true"></p>
                         </div>
                     </div>
                 </div>
             </div>
         </div>
     </div> 
  
</div>
        </div>
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
</asp:Content>

