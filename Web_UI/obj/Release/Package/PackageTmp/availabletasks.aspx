<%@ Page Title="" Language="C#" MasterPageFile="~/WorkerMenu.Master" AutoEventWireup="true" CodeBehind="availabletasks.aspx.cs" Inherits="Farmacy_App.availabletasks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Task Manager</title>
    <!-- BOOTSTRAP STYLES -->
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <!-- FONTAWESOME STYLES -->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <!-- MORRIS CHART STYLES -->
    <link href="assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <!-- CUSTOM STYLES -->
    <link href="assets/css/custom.css" rel="stylesheet" />
    <!-- MAIN STYLE -->
    <link href="assets/css/style1.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
    <!-- GOOGLE FONTS -->
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
                            <canvas id="myChart" style="width:100%;max-width:700px"></canvas>
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

    <!-- Chart.js Library -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js"></script>

    <script>
        $(document).ready(function () {
            var xValues = ["Italy", "France", "Spain", "USA", "Argentina"];
            var yValues = [55, 49, 44, 24, 15];
            var barColors = ["red", "green", "blue", "orange", "brown"];

            new Chart("myChart", {
                type: "bar",
                data: {
                    labels: xValues,
                    datasets: [{
                        backgroundColor: barColors,
                        data: yValues
                    }]
                },
                options: {
                    legend: { display: false },
                    title: {
                        display: true,
                        text: "Task Distribution by Country"
                    }
                }
            });
        });
    </script>
</asp:Content>
