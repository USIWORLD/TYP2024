<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="Questionnaire.aspx.cs" Inherits="Farmacy_App.Questionnaire"  Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <main id="main" class="main">

  <div class="pagetitle">
    <h1>Questionnaire</h1>
    
  </div><!-- End Page Title -->

  <section class="section profile">
    <div class="row">

      <div class="col-xl-12">

        <div class="card">
          <div class="card-body pt-3">
         <div class="panel panel-default">
             <div class="panel-body">
             <div class="table-responsive">
                 <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                     <asp:TableHeaderRow Style="font-weight: normal;">
                         <asp:TableHeaderCell>Q_ID</asp:TableHeaderCell>
                         <asp:TableHeaderCell >DateAcquired</asp:TableHeaderCell>
                         <asp:TableHeaderCell >Lameness</asp:TableHeaderCell>
                         <asp:TableHeaderCell >FootBlisters</asp:TableHeaderCell>
                         <asp:TableHeaderCell >MouthBlisters</asp:TableHeaderCell>
                         <asp:TableHeaderCell >Drooling</asp:TableHeaderCell>
                         <asp:TableHeaderCell >AppetiteDrop</asp:TableHeaderCell>
                         <asp:TableHeaderCell >Fever</asp:TableHeaderCell>
                         <asp:TableHeaderCell>NasalDischarge</asp:TableHeaderCell>
                         <asp:TableHeaderCell >BehavioralChange</asp:TableHeaderCell>
                         <asp:TableHeaderCell >WeightLoss</asp:TableHeaderCell>
                         <asp:TableHeaderCell >UnusualTiredness</asp:TableHeaderCell>
                         <asp:TableHeaderCell >WorkerID</asp:TableHeaderCell>
                         <asp:TableHeaderCell >CID</asp:TableHeaderCell>
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
  </section>

</main><!-- End #main -->

<!-- ======= Footer ======= -->
<footer id="footer" class="footer">
  <div class="copyright">
    &copy; Copyright <strong><span>NiceAdmin</span></strong>. All Rights Reserved
  </div>
  <div class="credits">
    <!-- All the links in the footer should remain intact. -->
    <!-- You can delete the links only if you purchased the pro version. -->
    <!-- Licensing information: https://bootstrapmade.com/license/ -->
    <!-- Purchase the pro version with working PHP/AJAX contact form: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/ -->
    Designed by <a href="https://bootstrapmade.com/">BootstrapMade</a>
  </div>
</footer><!-- End Footer -->

<a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

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
