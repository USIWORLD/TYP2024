<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="EditBudget.aspx.cs" Inherits="Farmacy_App.EditBudget" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <H2 style="color: #0043a6;">Budget Allocation</H2>
    <div class="panel panel-default" style="color: #1c1a1a" >
        <div class="panel-heading" style="color: #0043a6" >
           Price Allocation
        </div>
        <div class="panel-body">

 

<div class="row">

    <div class="col-md-5 col-sm-12 col-xs-12">
 

           <div class="form-group">
             <label style="font-weight:normal;">Category</label>
             <asp:DropDownList class="form-control" ID="ddlfreq" runat="server">
          
            <asp:ListItem Text="Animal Feed" Value="Animal Feed"></asp:ListItem>
            <asp:ListItem Text="Sanitation" Value="Sanitation"></asp:ListItem>
            <asp:ListItem Text="Medication" Value="Medication"></asp:ListItem>
             </asp:DropDownList>

         </div>

           <div class="form-group">
             <label style="font-weight:normal;">Budget Price</label>
             <asp:TextBox class="form-control" id="price"   runat="server" ></asp:TextBox>
         </div>

         <div class="form-group">
         <label  style="font-weight:normal;"> Month</label>
         <asp:TextBox class="form-control" id="month"   runat="server" ></asp:TextBox>

         </div>

        <asp:Button ID="newID"  runat ="server" Text="Edit" OnClick="AddAnimal_Click" class="btn btn-primary"/> 
   <asp:Label ID="error" runat="server"></asp:Label>
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
