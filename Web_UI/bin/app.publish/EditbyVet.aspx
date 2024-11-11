<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="EditbyVet.aspx.cs" Inherits="Farmacy_App.EditbyVet" Async="true" %>
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
 <!-- GOOGLE FONTS-->
 <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="container">
        <H2 style="color: #0043a6;">Animal Medication Management</H2>
        <div class="panel panel-default" style="color: #1c1a1a" >
            <div class="panel-heading" style="color: #0043a6" >
               Edit Animals
            </div>
            <div class="panel-body">

     
    
    <div class="row">

        <div class="col-md-5 col-sm-12 col-xs-12">
     
         
             <div class="form-group">
                 <label  style="font-weight:normal;">Animal Health Status</label>
                  <asp:DropDownList class="form-control" ID="ddlHealthStatus" runat="server">
                 <asp:ListItem Text="Healthy" Value="Healthy"></asp:ListItem>
                 <asp:ListItem Text="Exposed" Value="Exposed"></asp:ListItem>
                 <asp:ListItem Text="HighRisk" Value="HighRisk"></asp:ListItem>
                 <asp:ListItem Text="FMD Confirmed" Value="FMD Confirmed"></asp:ListItem>
             </asp:DropDownList>
             </div>
             </div>
         

         </div>   
            <asp:Button ID="newID"  runat ="server" Text="Submit" OnClick="AddAnimal_Click" class="btn btn-primary"/> 
            <p ID="error" runat="server"></p>
            <asp:Literal ID="litMessage" runat="server" />

            <asp:Label ID="added" runat="server" ></asp:Label>
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
