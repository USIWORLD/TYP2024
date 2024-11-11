<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="Sanitation.aspx.cs" Inherits="Farmacy_App.Sanitation" Async="true" %>
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
            <h1>Sanitation</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="dashboard.aspx">Kohrsolin</a></li>
                  
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
                                    <canvas id="sanitationChart"></canvas>
                                </div>

                            <div class="table table-responsive" style="display:none">
                              
                                <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                         <asp:TableHeaderRow Style="font-weight: normal;">
                                            <asp:TableHeaderCell Style="font-weight: normal;">Kraal</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Kraal Size(Square meters)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Cost Allocated(Rands)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Size Sanitized(Square meters)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Month-End Cost(Rands)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Size not Sanitized</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Varience(Rands)</asp:TableHeaderCell>
                                          </asp:TableHeaderRow>
                                      </asp:Table>

                                        <label style="color: #0043a6; display: inline-block; margin-right: 20px;">Total Allocated:</label>
                                        <asp:TextBox runat="server" ID="total" style="display: inline-block; border-radius:4px;margin-right: 40px;"></asp:TextBox>

                                        <label style="color: #0043a6; display: inline-block; margin-right: 20px;">Total Spent:</label>
                                        <asp:TextBox runat="server"  ID="cost" style="display: inline-block;border-radius:4px;"></asp:TextBox>
                                        <br />
                                        <br />

                                             </div>
                                             </div>
                                         </div>

                                             <asp:Label runat="server" ID="Label1" style="color: #0043a6;font-weight: 400;font-size: 1.2em;">Recommendations:</asp:Label>

                                                 <div class="card" style="width: auto; table-layout: auto;">
                                                   <div class="card-body">
      
    
                                                 <asp:Label runat="server" ID="recommend2" style="color: black; display: block; font-size: 1.1em; font-weight: 400; padding: 5px 0; width: auto;"></asp:Label>

                                                <asp:Label runat="server" ID="recommend" style="color: black; display: block; font-size: 1.1em; font-weight: 400; padding: 5px 0; width: auto;"></asp:Label>
                                                   <a href="AllocateTask3.aspx" class="btn btn-primary" >Allocate Task</a>

                                                </div>
                                                     </div>

                                                                
                                       <br />
                    
                                </div>

                              
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
        <script src="https://cdn.jsdelivr.net/npm/chart.js@3.9.1/dist/chart.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0"></script>
        
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<script>
window.onload = function () {
    var ctx = document.getElementById('sanitationChart').getContext('2d');
    var sanitationChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: chartData.labels, // Use labels from server
            datasets: [
                {
                    label: 'Full Size',
                    data: chartData.totalSize, // Use total size data from server
                    backgroundColor: 'RGB(100, 126, 209)',
                    borderColor: 'RGB(62, 163, 194)',
                    borderWidth: 1,
                    barThickness: 30, // Adjust thickness
                    categoryPercentage: 0.5, // Adjust to reduce space between bars
                    barPercentage: 1.0
                },
                {
                    label: 'Sanitized Area',
                    data: chartData.sanitized, // Use sanitized data from server
                    backgroundColor: 'green',
                    stack: 'Stack 1', // Stack this dataset
                    borderWidth: 1,
                    barThickness: 30, // Adjust thickness
                    categoryPercentage: 0.5, // Align with full size bars
                    barPercentage: 1.0
                },
            ]
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    stacked: false, // Do not stack full size bars
                    title: {
                        display: true,
                        text: 'Kraal'
                    }
                },
                y: {
                    stacked: true, // Stack the sanitized and unsanitized bars
                    title: {
                        display: true,
                        text: 'Size (Square Meters)'
                    },
                    suggestedMax: 400 // Adds extra space above the maximum value (300)
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            // Get kraal index from tooltipItem.dataIndex
                            var kraalIndex = tooltipItem.dataIndex;

                            // Get kraal name for the tooltip (optional, can be omitted if not needed)
                            var kraalName = chartData.labels[kraalIndex];

                            // Get animal names for this kraal
                            var animalNames = chartData.animals[kraalIndex];

                            // Only display the animal names in the tooltip
                            return 'Animals: ' + animalNames.join(', ');
                        }
                    }
                }
            }

                
        }
        
    });
};
</script>






</asp:Content>
