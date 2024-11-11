c<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="Budget.aspx.cs" Inherits="Farmacy_App.Budget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <link href="assets/css/custom.css" rel="stylesheet" />
    <link href="assets/css/style1.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">
        <section class="section dashboard">
            <div class="row">
                <div class="pagetitle">
                    <h1 style="color: #0043a6;">Monthly Costs</h1>
                    <nav>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Dashboard.aspx">Manager</a></li>
                            <li class="breadcrumb-item">Budget</li>
                        </ol>
                    </nav>
                </div>

                <div class="panel-body">
                    <div class="card">
                        <div class="card-body">
                            <div class="panel-heading">
                                <span>The budget for this month</span>
                            </div>
                            <table class="table table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Category</th>
                                        <th>Quantity</th>
                                        <th>Unit Cost (R)</th>
                                        <th>Total Cost (R)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><a href="HayStack.aspx">Haystack</a></td>
                                        <td>500 kg</td>
                                        <td>5.00</td>
                                        <td>8,747.00</td>
                                    </tr>
                                    <tr>
                                        <td>Sanitation</td>
                                        <td>3</td>
                                        <td>200.00</td>
                                        <td>600.00</td>
                                    </tr>
                                    <tr>
                                        <td>Medication</td>
                                        <td>10</td>
                                        <td>100.00</td>
                                        <td>1,000.00</td>
                                    </tr>
                                    <tr>
                                        <td>Vet Services</td>
                                        <td>3</td>
                                        <td>1,500.00</td>
                                        <td>4,500.00</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
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
</asp:Content>
    