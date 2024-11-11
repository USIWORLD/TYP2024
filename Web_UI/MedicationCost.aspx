<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="MedicationCost.aspx.cs" Inherits="Farmacy_App.MedicationCost" Async="true" %>
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
            <h1>Medication</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="dashboard.aspx">Manager</a></li>
                  
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
                            <div id="chart-container">
                                   <canvas id="medicationChart"></canvas>
                               </div>
                            <div class="table table-responsive" style="display:none">
                                
                                <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                          <asp:TableHeaderRow Style="font-weight: normal;">
                                              <asp:TableHeaderCell Style="font-weight: normal;">Medication Name</asp:TableHeaderCell>
                                              <asp:TableHeaderCell Style="font-weight: normal;">Dosage</asp:TableHeaderCell>  
                                              <asp:TableHeaderCell Style="font-weight: normal;">Price(Rands)</asp:TableHeaderCell>
                                              <asp:TableHeaderCell Style="font-weight: normal;">Animal Tag</asp:TableHeaderCell>
                                               <asp:TableHeaderCell Style="font-weight: normal;">Quantity Allocated</asp:TableHeaderCell>

                                              <asp:TableHeaderCell Style="font-weight: normal;">Quantity Used</asp:TableHeaderCell>
                                              <asp:TableHeaderCell Style="font-weight: normal;"> Month-End Cost(Rands)</asp:TableHeaderCell>                                          
                                              <asp:TableHeaderCell Style="font-weight: normal;">Varience(Rands)</asp:TableHeaderCell>
                                            

                                                
                                          </asp:TableHeaderRow>
                                      </asp:Table>

                                            <label style="color: #0043a6; display: inline-block; margin-right: 20px;">Total Allocated:</label>
                                        <asp:TextBox runat="server" ID="total" style="display: inline-block; border-radius:4px;margin-right: 40px;"></asp:TextBox>

                                        <label style="color: #0043a6; display: inline-block; margin-right: 20px;">Total Used:</label>
                                        <asp:TextBox runat="server"  ID="cost" style="display: inline-block;border-radius:4px;"></asp:TextBox>

                                            <label style="color: #0043a6; display: inline-block; margin-right: 20px;">OverSpend:</label>
                                            <asp:TextBox runat="server"  ID="over" style="display: inline-block;border-radius:4px;"></asp:TextBox>
                                 </div>
                                      <br />
                                      <br />

                                                                </div>
    </div>
</div>

               
                                            <asp:Label runat="server" style="color: #0043a6;font-weight: 400;font-size: 1.2em;">Recommendations:</asp:Label>

                                                 <div class="card" style="width: auto; table-layout: auto;">
                                                   <div class="card-body">
     
                                                     <asp:Label runat="server" ID="recommend2" style="color: black; display: block; font-size: 1.1em; font-weight: 400; padding: 5px 0; width: auto;"></asp:Label>

                                                    <asp:Label runat="server" ID="recommend" style="color: black; display: block; font-size: 1.1em; font-weight: 400; padding: 5px 0; width: auto;"></asp:Label>
                                                         <a href="MedicationManagement.aspx" class="btn btn-primary">Edit Medication Information</a>

                                                   </div>
                                            </div>

                                               <br />

                                      
                                      <p id="err" runat="server" visible="true"></p>
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

        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script>
    window.onload = function () {
        var ctx = document.getElementById('medicationChart').getContext('2d');
        var medicationChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: chartData.labels, // Use labels from server
                datasets: [
                    {
                        label: 'Quantity Allocated',
                        data: chartData.totalSize, // Use total size data from server
                        backgroundColor: 'RGB(59, 204, 105)',
                        borderColor: 'RGB(59, 204, 105)',
                        borderWidth: 1,
                        barThickness: 40, // Adjust thickness as needed
                        categoryPercentage: 1.0, // Ensure bars are next to each other with no gap
                        barPercentage: 1.0 // Ensure bars are next to each other with no gap
                    },
                     {
                        label: 'Used Quantity',
                        data: chartData.used, // Use used quantity data from server
                         backgroundColor: 'RGB(245, 88, 99)',
                         borderColor: 'RGB(245, 88, 99)',
                        borderWidth: 1,
                        barThickness: 40, // Adjust thickness as needed
                        categoryPercentage: 1.0, // Ensure bars are next to each other with no gap
                        barPercentage: 1.0 // Ensure bars are next to each other with no gap
                    }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        stacked: false, // Ensure bars are side by side, not stacked
                        barPercentage: 1.0, // Make bars take up the full space of each category
                        categoryPercentage: 1.0, // Make sure bars are next to each other with no gap
                        title: {
                            display: true,
                            text: 'Medication',
                            color: '#08061c',
                            font: {
                                size: 15  // Set the font size
                            }
                        }
                    },
                    y: {
                        stacked: false, // Ensure bars are not stacked
                        title: {
                            display: true,
                            text: 'Quantity',
                            color: '#08061c',
                            font: {
                                size: 15  // Set the font size
                            }
                        },
                     suggestedMax: 3 

                    }
                },
                plugins: {
                    legend: {
                        display: true,
                        position: 'top'
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                var label = tooltipItem.dataset.label || '';
                                if (label) {
                                    label += ': ';
                                }
                                label += tooltipItem.raw + ' units';
                                return label;
                            }
                        }
                    }
                }
            }
        });
    };
</script>



</asp:Content>
