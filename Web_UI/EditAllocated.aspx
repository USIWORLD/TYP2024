<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="EditAllocated.aspx.cs" Inherits="Farmacy_App.EditAllocated" Async="true" %>
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
      <main id="main" class="main">
      <div class="pagetitle">
          <h1>Allocate Medication</h1>
          <nav>
              <ol class="breadcrumb">
                  <li class="breadcrumb-item"><a href="dashboard.aspx">Home</a></li>
                  <li class="breadcrumb-item active">Allocate Medication</li>
              </ol>
          </nav>
      </div>
      <!-- End Page Title -->

      <section class="section">
          <div class="row">

              <div class="col-lg-6">

                  <div class="card">
                      <div class="card-body">
                          <h6 class="card-title"></h6>

                          <div class="col-md-12 col-sm-12 col-xs-12">

                              <div class="form-group">
                                  <label style="font-weight: normal;">Animal Tag</label>
                                  <asp:TextBox ID="animalTag" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                              </div>

                              <div class="form-group">
                                  <label style="font-weight: normal;">Allocated Medication</label>
                                  <asp:TextBox ID="allocatedMeds" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                              </div>

                              <div class="form-group">
                                  <label style="font-weight: normal;">Worker</label>
                                  <asp:TextBox ID="ddlname" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                              </div>

                              <div class="form-group">
                                  <label style="font-weight: normal;">Date</label>
                                  <asp:TextBox class="form-control" ID="date" runat="server"></asp:TextBox>
                              </div>

                              <h5 class="card-title">Medical Notes</h5>

                              <div class="d-flex align-items-center">
                                  <textarea id="txtMedNotes" cols="50" rows="5" runat="server"></textarea>
                                  <br />
                              </div>
                              <asp:Button ID="newID" class="btn btn-primary" runat="server" Text="Submit" OnClick="AllocateTask_Click" />
                              <p id="err" runat="server"></p>
                              <asp:Literal ID="litMessage" runat="server" />

                              <asp:Label ID="added" runat="server" Text=""></asp:Label>


                          </div>


                      </div>
                  </div>
              </div>

              <div class="col-lg-6">
                  <div class="card">
                      <div class="card-body">
             
                              <div class="panel-body">
                                  <div class="table-responsive">
                                      <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                          <asp:TableHeaderRow Style="font-weight: normal;">
                                              <asp:TableHeaderCell Style="font-weight: normal;">ID</asp:TableHeaderCell>
                                              <asp:TableHeaderCell Style="font-weight: normal;">Name</asp:TableHeaderCell>
                                               <asp:TableHeaderCell Style="font-weight: normal;">Quantity</asp:TableHeaderCell>
                                      
                                              <asp:TableHeaderCell Style="font-weight: normal;">Cost</asp:TableHeaderCell>
                                              <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>
                                          </asp:TableHeaderRow>
                                      </asp:Table>
                                      <p id="P1" runat="server" visible="true"></p>
                                  </div>
                              </div>
                             <!--<div class="col-md-12 col-sm-12 col-xs-12">
                         <div class="form-group">
                             <label style="font-weight: normal;">Select Animal</label>
                             <asp:DropDownList class="form-control" ID="DropDownList1" runat="server"></asp:DropDownList>
                         </div>-->

                               
                          </div>
                      </div>


                  </div>
              </div>

          </div>
      </section>
  </main>


  <!-- Template Main JS File -->
  <script src="assets/js/main.js"></script>


  <%-- <div class="container" >
<h2>Weekly reports</h2>
    <p> Reports</p><
<textarea class="form-control" rows="3" readonly>22/04/24 - 28/04/24.txt &#10;29/04/24 - 05/05/24.txt
</textarea>
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
<script src="assets/js/custom.js"></script>--%>
</asp:Content>
