<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Farmacy_App.Dashboard1" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script src = "https://unpkg.com/sweetalert/dist/sweetalert.min.js" ></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <div class="background">
        <main id="main" class="main">
            <asp:Label ID="lblEx" style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="lblH" style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="lblHR" style="color: #ffffff00;" runat="server"></asp:Label>
           <asp:Label ID="lblFMd" style="color: #ffffff00;" runat="server"></asp:Label>


            <asp:Label ID="HealthyCows" style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="HighRiskCows" style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="ExposedCows" style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="FMDCows" style="color: #ffffff00;" runat="server"></asp:Label>

            <div class="pagetitle">
                <h1 style="color: #ffffff;">Dashboard</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Manager</a></li>
                        <li class="breadcrumb-item active">Dashboard</li>
                    </ol>
                </nav>
            </div>

            <!-- End Page Title -->
            <section class="section dashboard">

                <div class="row">
                    <!-- Left side columns -->
                    <div class="col-lg-8">
                        <div class="row">

                            <!-- Tasks -->
                            <div class="col-xxl-6 col-md-6">
                                <div class="card info-card sales-card">

                                    <div class="filter">
                                        <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
                                        <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                                            <li class="dropdown-header text-start">
                                                <h6>Filter</h6>
                                            </li>

                                            <li><a class="dropdown-item" href="#">Today</a></li>
                                            <li><a class="dropdown-item" href="#">This Month</a></li>
                                            <li><a class="dropdown-item" href="#">This Year</a></li>
                                        </ul>
                                    </div>

                                    <div class="card-body">
                                        <h5 class="card-title">Tasks <span>| Today</span></h5>

                                        <div class="d-flex align-items-center">
                                            <div class="card-icon roundedv-circle d-flex align-items-center justify-content-center">
                                                <i class="bi bi-cart"></i>
                                            </div>
                                            <div class="ps-3">
                                                <h3 style="color: #0043a6;">
                                                    <asp:Label ID="lbltask" runat="server"></asp:Label></h3>

                                            </div>
                                        </div>
                                        <p id="error" runat="server"></p>
                                    </div>

                                </div>
                            </div>
                            <!-- End Tasks -->

                            <!-- Animals Available -->
                            <div class="col-xxl-6 col-md-6">
                                <div class="card info-card revenue-card">

                                    <div class="filter">
                                        <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
                                        <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                                            <li class="dropdown-header text-start">
                                                <h6>Filter</h6>
                                            </li>

                                            <li><a class="dropdown-item" href="#">Today</a></li>
                                            <li><a class="dropdown-item" href="#">This Month</a></li>
                                            <li><a class="dropdown-item" href="#">This Year</a></li>
                                        </ul>
                                    </div>

                                    <div class="card-body">
                                        <h5 class="card-title">Animals <span>| This Month</span></h5>

                                        <div class="d-flex align-items-center">
                                            <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                                <i class="bi bi-piggy-bank"></i>
                                            </div>
                                            <div class="ps-3">
                                                <h3 style="color: #0043a6;">
                                                    <asp:Label ID="lblanimals" runat="server"></asp:Label></h3>
                                                <span class="text-success small pt-1 fw-bold">8%</span> <span class="text-muted small pt-2 ps-1">increase</span>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-6"> </div>
                            
          <div class="col-lg-6">  
    <div class="card" >
        <div class="card-body pb-0" >
            <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
            <h5 class="card-title">Health Tracker</h5>

            <div id="trafficChart" style="min-height: 300px;" class="echart"></div>

            <script>
                document.addEventListener("DOMContentLoaded", () => {
                    const chart = echarts.init(document.querySelector("#trafficChart"));

                    const option = {
                        tooltip: {
                            trigger: 'item'
                        },
                        legend: {
                            top: '5%',
                            left: 'center'
                        },
                        series: [{
                            name: 'Access From',
                            type: 'pie',
                            radius: ['40%', '70%'],
                            avoidLabelOverlap: false,
                            label: {
                                show: false,
                                position: 'center'
                            },
                            emphasis: {
                                label: {
                                    show: true,
                                    fontSize: '18',
                                    fontWeight: 'bold'
                                }
                            },
                            labelLine: {
                                show: false
                            },
                            data: [
                                {
                                    value:  <%= lblEx.Text %>,
                                    name: 'Exposed'
                                },
                                {
                                    value: <%= lblH.Text %>,
                                    name: 'Healthy'
                                }, {
                                    value:  <%= lblHR.Text %>,
                                    name: 'HighRisk'
                                },
                                {
                                    value:  <%= lblFMd.Text %>,
                                    name: 'FMD'
                                }

                            ]
                        }]
                    };

                    chart.setOption(option)

                    // Event listener for clicks on chart segments
                    chart.on('click', function (params) {
                        if (params.name === 'HighRisk') {
                            window.location.href = 'HighRisk.aspx';
                        }
                    })
                })
            </script>

            
        </div>

                                  </div>
                            </div>
                            <!-- End Reports -->
                            <div class="col-lg-6"> </div>

                        </div>
                    </div>
                    <!-- End Left side columns -->

                    <!-- Right side columns -->
                    <div class="col-lg-4">
                        
                        <!-- Recent Activity -->
                        <div class="card info-card sales-card">
                            <div class="filter">
                                <a class="icon" href="#" data-bs-toggle="dropdown"><i class="bi bi-three-dots"></i></a>
                                <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                                    <li class="dropdown-header text-start">
                                        <h6>Filter</h6>
                                    </li>

                                    <li><a class="dropdown-item" href="#">Today</a></li>
                                    <li><a class="dropdown-item" href="#">This Month</a></li>
                                    <li><a class="dropdown-item" href="#">This Year</a></li>
                                </ul>
                            </div>

                            <div class="card-body">
                                <h5 class="card-title">Workers <span>| Today</span></h5>
                                <div class="d-flex align-items-center">
                                    <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                        <i class="bi bi-people"></i>
                                    </div>
                                    <div class="ps-3">
                                        <h3 style="color: #0043a6;">
                                            <asp:Label ID="lblworker" runat="server"></asp:Label></h3>
                                    </div>
                                </div>

                            </div>


                        </div>
                        <!-- End Recent Activity -->

                        <!-- Animal Status -->
                        <div class="card">
                            <div class="filter">
                                <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow">
                                    <li class="dropdown-header text-start">
                                        <h6>Filter</h6>
                                    </li>

                                    <li><a class="dropdown-item" href="#">Today</a></li>
                                    <li><a class="dropdown-item" href="#">This Month</a></li>
                                    <li><a class="dropdown-item" href="#">This Year</a></li>
                                </ul>
                            </div>

                            
                        </div>

                    </div>
                    <!-- End Right side columns -->
                </div>

            </section>

        </main>
        <!-- End #main -->


        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

                                   <script>
                                       function showCustomAlert(message) {
                                           Swal.fire({
                                               title: 'Alert',
                                               text: message,
                                               width: '450px',
                                               position: 'top',
                                               padding: '0.3em',  // Custom padding                                          
                                               confirmButtonText: 'Ok',
                                               confirmButtonColor: '#3085d6',  // Default SweetAlert blue color
                                               customClass: 'custom-swal',
                                           });

                                       }
    </script>
    
<style>
    .custom-swal .swal2-html-container {
        font-size: 15px;  /* Adjust the size as needed */
    }
</style>






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
        </footer>
        <!-- End Footer -->

        <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
    </div>


<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <!-- Vendor JS Files -->
    <script src="assets/vendor/apexcharts/apexcharts.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/chart.js/chart.umd.js"></script>
    <script src="assets/vendor/echarts/echarts.min.js"></script>
    <script src="assets/vendor/quill/quill.js"></script>
    <script src="assets/vendor/simple-datatables/simple-datatables.js"></script>
    <script src="assets/vendor/tinymce/tinymce.min.js"></script>
    <script src="assets/vendor/php-email-form/validate.js"></script>
    <script src = "https://unpkg.com/sweetalert/dist/sweetalert.min.js" ></script>

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

    <!-- Farm Visualizer JS File -->
    <script src="assets/js/scripts.js"></script>
</asp:Content>