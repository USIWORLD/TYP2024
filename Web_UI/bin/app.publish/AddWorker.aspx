<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="AddWorker.aspx.cs" Inherits="Farmacy_App.AddWorker" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <meta charset="utf-8" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0" />
 <title>Farmacy Management System</title>
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

  <div class="container">
    <h2 style="color: #0043a6">Worker Management</h2>
              <div class="panel panel-default" style="color: #1c1a1a" >
                   <div class="panel-heading" style="color: #0043a6" >
                         Add a new worker
                       </div>
        <div class="panel-body">

         <label style="font-weight:normal;color: #0043a6;">Please fill in the form below to add a new worker:</label>

         <div class="row">

     <div class="col-md-5 col-sm-12 col-xs-12">

         <div class="form-group">
             <label style="font-weight:normal;">Worker ID</label>
             <asp:TextBox class="form-control" ID="Id" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label style="font-weight:normal;">Name</label>
             <asp:TextBox class="form-control" ID="name" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label style="font-weight:normal;">Surname</label>
             <asp:TextBox class="form-control" ID="surname" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label style="font-weight:normal;">Email</label>
             <asp:TextBox class="form-control" ID="email" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label style="font-weight:normal;">Password</label>
             <asp:TextBox class="form-control" ID="password" runat="server"></asp:TextBox>
         </div>




         <asp:Button ID="newID" class="btn btn-primary" runat="server" Text="Submit" OnClick="AddWorker_Click" />
         <p ID="err" runat="server"></p>
         <asp:Literal ID="litMessage" runat="server" />

         <asp:Label ID="added" runat="server" Text=""></asp:Label>

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
