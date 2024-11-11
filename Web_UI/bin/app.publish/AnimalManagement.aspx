<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="AnimalManagement.aspx.cs" Inherits="Farmacy_App.AnimalManagement" Async="true" %>

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
     <script src = "https://unpkg.com/sweetalert/dist/sweetalert.min.js" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <main id="main" class="main">
        <section class="section dashboard" id="header">
            <div class="row">
                <div class="col-lg-10">
                    <div class="row">
                        <div class="col-xxl-4 col-md-12">
                            <h2 style="color: #0043a6">Animal Management</h2>
                            <div class="panel panel-default">   
                                     <div class="panel-body">
                                         <div style="display: flex; align-items: center; justify-content: flex-start; padding: 10px 0; gap: 15px; border-bottom: 1px solid #ddd; margin-bottom: 15px;">
                                                                                 
                                            <!-- Health Status Filter -->
                                            <asp:DropDownList ID="ddlHealthStatus" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlHealthStatus_SelectedIndexChanged"
                                                style="border-radius: 4px; padding: 8px 12px; background-color: #ffffff; border: 1px solid #ced4da; font-size: 14px; width: 150px;">
                                                <asp:ListItem Value="" Text="Health Status" />
                                                <asp:ListItem Value="Healthy" Text="Healthy" />
                                                <asp:ListItem Value="Exposed" Text="Exposed" />
                                                <asp:ListItem Value="HighRisk" Text="HighRisk" />
                                            </asp:DropDownList>

                                            <!-- Gender Filter -->
                                            <asp:DropDownList ID="ddlGender" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged"
                                                style="border-radius: 4px; padding: 8px 12px; background-color: #ffffff; border: 1px solid #ced4da; font-size: 14px; width: 150px;">
                                                <asp:ListItem Value="" Text="Gender" />
                                                <asp:ListItem Value="Male" Text="Male" />
                                                <asp:ListItem Value="Female" Text="Female" />
                                            </asp:DropDownList>

                                            <!-- Show All Button -->
                                            <asp:Button ID="btnShowAll" runat="server" Text="Show All"
                                                Style="border-radius: 4px; padding: 8px 20px; background-color: #007bff; color: white; border: none; font-size: 14px; cursor: pointer; transition: background-color 0.3s ease;"
                                                onmouseover="this.style.backgroundColor='#0056b3';"
                                                onmouseout="this.style.backgroundColor='#007bff';"  OnClick="btnShowAll_Click" />

                                              <asp:Button class='bi bi-clipboard-plus' ID="btnAddAnimal" runat="server" 
                                                OnClick="btnAddAnimal_Click" Text="Add" 
                                                Style="background-color: #28a745; margin-left: auto; width: 80px;padding: 8px 12px;color:white; border-radius: 4px; border: none; font-size: 14px; transition: background-color 0.3s ease;"
                                                onmouseover="this.style.backgroundColor='#218838';" 
                                                onmouseout="this.style.backgroundColor='#28a745';" />
                                        </div>

                                    <div class="table-responsive">
                                        <!--Filtering doesn't work here-->
                                        <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                            <asp:TableHeaderRow Style="font-weight: normal;" runat="server">
                                                <asp:TableHeaderCell Style="font-weight: normal;">Name</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;"> Gender</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;"> Breed</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Colour</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Weight</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">HealthStatus</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Date</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Age</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Kraal</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                        
                                    <div class="pagination" id="pagination" style="display: flex; justify-content: center; margin-top: 20px;">
                                              <!-- Buttons will be added dynamically -->
                                  </div>
                              <p id="err" runat="server" ></p>
                                </div>
                        </div>
                    </div>
                </div>
 
            </div>
        </div>
               <div class="col-lg-2" style="margin-top: 90px;">
    <div style="display: flex; justify-content: center; align-items: center; border-radius: 5px;"> 
        <a href="AllocateTask3.aspx" style="display: block;width:250px;" class="btn btn-primary">Allocate Task</a>
    </div>
</div>
                </div>
                        
                 
               
</section>
        
<script>
    const rowsPerPage = 4; // Show 5 rows per page
    let currentPage = 1;
    const table = document.getElementById("<%= aTable.ClientID %>");
    const rows = table.getElementsByTagName("tr");
    const totalRows = rows.length - 1; // Exclude header row
    const totalPages = Math.ceil(totalRows / rowsPerPage);

    function showPage(page) {
        currentPage = page;

        // Hide all data rows (excluding header)
        for (let i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
            rows[i].style.display = 'none'; // Hide all data rows
        }

        // Determine the start and end indices based on the current page
        const start = (page - 1) * rowsPerPage + 1; // Start from 1 to skip header row
        const end = start + rowsPerPage - 1; // Determine end index

        // Show the rows for the current page
        for (let i = start; i <= end; i++) {
            if (i < rows.length) { // Ensure we don't go out of bounds
                rows[i].style.display = ''; // Show the row
            }
        }

        updatePagination(); // Update pagination buttons
    }

    // Function to update pagination buttons
    function updatePagination() {
        const pagination = document.getElementById("pagination");
        pagination.innerHTML = ""; // Clear existing buttons

        for (let i = 1; i <= totalPages; i++) {
            const button = document.createElement("button");
            button.textContent = i;
            button.className = "pagination-button";
            button.style.margin = "5px";
            button.style.padding = "5px 10px";
            button.style.border = "1px solid #007bff";
            button.style.backgroundColor = (i === currentPage) ? "#007bff" : "white";
            button.style.color = (i === currentPage) ? "white" : "#007bff";
            button.style.borderRadius = "5px";
            button.style.cursor = "pointer";

            button.onclick = function () {
                showPage(i);
            };
            pagination.appendChild(button);
        }
    }

    // Initialize pagination on page load
    window.onload = function () {
        showPage(currentPage);
    };
    document.addEventListener('DOMContentLoaded', function () {
        // "Show All" button click handler
        document.getElementById('<%= btnShowAll.ClientID %>').addEventListener('click', function () {
            // Show all rows (no pagination)
            for (let i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
                rows[i].style.display = ''; // Show all data rows
            }

            // Hide pagination controls
            var paginationControls = document.querySelectorAll('.pagination');
            paginationControls.forEach(function (control) {
                control.style.display = 'none'; // Hide pagination controls
            });
        });
    });

</script>


         <script>
            function filterTable(attribute, value) {
                var rows = document.querySelectorAll('#<%= aTable.ClientID %> tr');
                rows.forEach(function (row, index) {
                    if (index === 0) return; // Skip header row

                    if (attribute === 'all') {
                        row.classList.remove('hidden');

                    }
                    
                        var matchesFilter = true;

                        if (attribute === 'health-status') {
                            var healthStatusCell = row.getAttribute('data-health-status');
                            if (healthStatusCell !== value) {
                                matchesFilter = false;
                            }
                        }

                        if (attribute === 'gender') {
                            var genderCell = row.getAttribute('data-gender');
                            if (genderCell !== value) {
                                matchesFilter = false;
                            }
                        }

                        if (attribute === 'breed') {
                            var breedCell = row.cells[3];
                            if (breedCell.getAttribute('data-breed') !== value) {
                                matchesFilter = false;
                            }
                        }

                        if (attribute === 'kraal') {
                            var kraalCell = row.cells[10];
                            if (kraalCell.getAttribute('data-kraal') !== value) {
                                matchesFilter = false;
                            }
                        }

                        if (matchesFilter) {
                            row.classList.remove('hidden');
                        } else {
                            row.classList.add('hidden');
                        }
                    
                });
             }

             document.getElementById('<%= ddlHealthStatus.ClientID %>').addEventListener('change', function () {
        var value = this.value;
        filterTable('health-status', value);
    });

             document.getElementById('<%= ddlGender.ClientID %>').addEventListener('change', function () {
                 var value = this.value;
                 filterTable('gender', value);
             });
             </script>


<script>
    document.addEventListener('DOMContentLoaded', function () {
        // "Show More" button click handler
        document.getElementById('<%= btnShowAll.ClientID %>').addEventListener('click', function() {
            // Show all rows (no filters applied)
            var allRows = document.querySelectorAll('#<%= aTable.ClientID %> tr');
            allRows.forEach(function (row, index) {
                if (index > 0) { // Skip header row
                    row.style.display = ''; // Show the row
                }
            });

            // Hide "Show All" button
            this.style.display = 'none'; // Hide the button

            // Hide pagination controls
            var paginationControls = document.querySelectorAll('.pagination');
            paginationControls.forEach(function (control) {
                control.style.display = 'none'; // Hide pagination controls
            });
        });
    });
</script>


        
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

                                    <script>
                                        function showCustomAlert(message) {
                                            Swal.fire({
                                                title: 'Alert',
                                                text: message,
                                                width: '350px',
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
        font-size: 14px;  /* Adjust the size as needed */
    }
</style>
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
    
</asp:Content>
