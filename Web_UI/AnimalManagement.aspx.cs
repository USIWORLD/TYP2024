using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using static Farmacy_App.weeklyreport;

namespace Farmacy_App
{
    public partial class AnimalManagement : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                string address = "http://localhost:7041/api/Farmacy/GetAnimals";
                await PopulateAnimalsAsync(address);
                await GetMessage();
                first = false;
            }
        }

        protected async Task GetMessage()
        {
            try
            {
                // Retrieve the existing animal data
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                // Read the response body
                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                // Filter the animals with "Unknown" HealthStatus
                var unknownAnimals = animals.Where(a => a.HealthStatus == "Unknown").ToList();

                // If there are unknown animals, show alert
                if (unknownAnimals.Count > 0)
                {
                    // Join all unknown animal names in a comma-separated list
                    string animalNames = string.Join(", ", unknownAnimals.Select(a => a.Name));

                    // Format the alert message with the animal names
                    string alertMessage = $"Recently added cows: {animalNames} needs a health check, allocate a task.";

                    // Trigger the custom alert JavaScript function
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CustomAlert",
                        $"showCustomAlert('{alertMessage.Replace("'", "\\'").Replace("\n", "\\n")}');", true);
                }
            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Request Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
        }




        public string Kraals(int kraal)
        {
            if (kraal == 0)
            {
                return "Arrival";

            }
            else if (kraal == 1)
            {
                return "One";

            }
            else if (kraal == 2)
            {
                return "Two";

            }
            else if (kraal == 3)
            {
                return "Three";
            }
            else if (kraal == 4)
            {
                return "FMD Confirmed";
            }
            else if (kraal == 5)
            {
                return "Quarantine";
            }
            else
            {
                return "Unassigned";
            }
        }





        private async Task PopulateAnimalsAsync(string address)
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync(address);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);


                foreach (var animal in animals)
                {
                    if (animal.New_Weight > 0)
                    {
                        animal.A_Weight = animal.New_Weight;
                    }

                    TableRow row = CreateRows(animal);
                    aTable.Rows.Add(row);
                }

                // Hide rows based on current page
                Button btnShowMore = new Button
                {
                    Text = "Show More",
                    CssClass = "btn btn-primary",
                };



            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }

        }

      


        private TableRow CreateRows(Animal animal)
            {
                //ASP Table
                TableRow newRow = new TableRow();
                newRow.Cells.Add(CreateCell(animal.Name.ToString()));
                newRow.Cells.Add(CreateCell(animal.Gender.ToString()));
                newRow.Cells.Add(CreateCell(animal.Breed.ToString()));
                newRow.Cells.Add(CreateCell(animal.Colour.ToString()));
                newRow.Cells.Add(CreateCell(animal.A_Weight.ToString("0")));
                newRow.Cells.Add(CreateCell(animal.HealthStatus.ToString()));
                newRow.Cells.Add(CreateCell(animal.DateAcquired.ToString("yyyy/MM/dd")));
                newRow.Cells.Add(CreateCell(animal.Age.ToString()));
                newRow.Cells.Add(CreateCell(Kraals(animal.Kraal)));

                newRow.Attributes["data-health-status"] = animal.HealthStatus;
                newRow.Attributes["data-gender"] = animal.Gender;
                newRow.Attributes["data-breed"] = animal.Breed;
                newRow.Attributes["data-kraal"] = animal.Kraal.ToString();
                TableCell cell = new TableCell();

                Panel panel = new Panel();
                panel.CssClass = "button-panel";
                panel.Attributes["style"] = "margin-bottom:5px;";
            
                Button btnEdit = new Button
                {
                    Text = "Edit",
                    CssClass = "btn btn-primary",
                    CommandArgument = animal.Id.ToString(),
                    Width = 100,
                };

                btnEdit.Click += btnEdit_Command_Click;

                LiteralControl control = new LiteralControl("&nbsp;");

                //cell.Controls.Add(control);

                //Delete button

                Button btnDelete = new Button
                {
                    Text = "Delete",
                    CssClass = "btn btn-danger",
                    CommandArgument = animal.Id.ToString(),
                    Width = 100,
                };

                btnDelete.Click += btnDelete_Command_Click;

                cell.Controls.Add(btnEdit);
                cell.Controls.Add(panel);
                cell.Controls.Add(btnDelete);
                newRow.Cells.Add(cell);

                aTable.Rows.Add(newRow);

                return newRow;
            }
        

            private TableCell CreateCell(string text)
            {
                return new TableCell { Text = text };
            }


        protected void btnEdit_Command_Click(object sender, EventArgs e)
            {
                try
                {
                    Button btnEdit = (Button)sender;
                    int id = Convert.ToInt32(btnEdit.CommandArgument);
                    Response.Redirect($"EditAnimal.aspx?id={id}", false);
                }
                catch (Exception ex)
                {
                    err.InnerText = $"Error: {ex.Message}";
                } 
            }
        


        protected void btnDelete_Command_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnDelete = (Button)sender;
                int id = Convert.ToInt16(btnDelete.CommandArgument);

                foreach (TableRow row in aTable.Rows)
                {
                    if (row.Cells.Count > 0 && row.Cells[0].Text == id.ToString())
                    {
                        aTable.Rows.Remove(row);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
        }



        public class Animal
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Species { get; set; }
            public string Gender { get; set; }
            public string Breed { get; set; }
            public string Colour { get; set; }
            public decimal A_Weight { get; set; }
            public decimal Height { get; set; }
            public string HealthStatus { get; set; }
            public DateTime DateAcquired { get; set; }
            public int Age { get; set; }
            public int Kraal { get; set; }
            public string AppetiteStatus { get; set; }
            public decimal New_Weight { get; set; }
        }

        protected void btnAddAnimal_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAnimal.aspx", false);
        }

        protected async void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch all animals from your API
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                // Clear existing rows in the table
                aTable.Rows.Clear(); // Clear previous rows, but keep header intact
                TableHeaderRow headerRow = new TableHeaderRow(); // Create a new header row
                                                                 // Add header cells (assuming you already have the header set in your .aspx)
                aTable.Rows.Add(headerRow); // Add header back to the table

                // Add all animals to the table
                foreach (var animal in animals)
                {
                    if (animal.New_Weight > 0)
                    {
                        animal.A_Weight = animal.New_Weight;
                    }

                    TableRow row = CreateRows(animal); // Create the row for the animal
                    aTable.Rows.Add(row); // Add the row to the table
                }

                // Hide pagination controls
                ClientScript.RegisterStartupScript(this.GetType(), "HidePagination", "document.getElementById('pagination').style.display = 'none';", true);

                // Show all rows (this assumes you have the showAll function defined in your script)
                ClientScript.RegisterStartupScript(this.GetType(), "ShowAllRows", "showAll();", true);
            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
        }


        // Function to check if the row already exists in the table
        private bool RowExists(TableRow newRow)
        {
            foreach (TableRow existingRow in aTable.Rows)
            {
                // Compare based on unique identifiers, e.g., Animal Tag or other unique property
                if (existingRow.Cells[0].Text == newRow.Cells[0].Text) // Assuming the first cell contains the unique identifier
                {
                    return true; // Row already exists
                }
            }
            return false; // Row does not exist
        }





        protected async void ddlHealthStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlHealthStatus.SelectedValue;

            switch (selectedValue)
            {       
                case "Healthy":
                    await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetHealthyAnimals");
                    break;
                case "Exposed":
                    await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetExposedAnimals");
                    break;
                case "HighRisk":
                    await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetHighRiskAnimals");
                    break;
            }
        }

        protected async void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlGender.SelectedValue == "Male")
            {
                await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetMaleAnimals");

            }else if(ddlGender.SelectedValue == "Female")
            {
                await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetFemaleAnimals");

            }
        }



        private async Task filterKraal(int kraal)
        {
            await PopulateAnimalsAsync($"GetKAnimal?kraal={kraal}");

        }

        protected async void btnKraal1_Click(object sender, EventArgs e)
        {
            await filterKraal(1);
        }
        protected async void btnKraal2_Click(object sender, EventArgs e)
        {
            await filterKraal(2);
        }
        protected async void btnKraal3_Click(object sender, EventArgs e)
        {
            await filterKraal(3);
        }
        protected async void btnKraal4_Click(object sender, EventArgs e)
        {
            await filterKraal(4);
        }
        protected async void btnKraal5_Click(object sender, EventArgs e)
        {
            await filterKraal(5);
        }
    }

}