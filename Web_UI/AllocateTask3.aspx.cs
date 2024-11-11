using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacy_App
{
    public partial class AllocateTask3 : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        private List<int> selectAnimalId = new List<int>();
        protected async void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadTasks();
                LoadWorkers();
                await PopulateAnimalsAsync();
                ScriptManager.RegisterStartupScript(this, GetType(), "initPagination", "initPagination();", true);
                animalDict.Clear();
                await GetMessage();
                date.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                first = false;


            }
            else
            {
                selectAnimalId = GetAnimalIDs().Distinct().ToList(); // Remove duplicates
                await PopulateAnimalsAsync();
                ddlKraal_SelectedIndexChanged(sender, e);




            }
        }

        protected async Task GetMessage()
        {
            try
            {
                // Retrieve the existing animal data
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();


                string responseBody = await response.Content.ReadAsStringAsync();
                var animal = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                var unknown = animal.Where(a => a.HealthStatus == "Unknown").ToList();


                if (unknown.Count > 0)
                {
                    string fmdIds = unknown.Count > 0 ? string.Join(", ", unknown.Select(a => a.Name)) : "None";

                    string alertMessage = $"Allocate health check task for cows:{fmdIds}.";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CustomAlert", $"showCustomAlert('{alertMessage.Replace("'", "\\'").Replace("\n", "\\n")}');", true);
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
        public async void LoadTasks()
        {
            var response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetTasks");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<task>>(responseBody);

            ddlId.DataSource = tasks;
            ddlId.DataTextField = "Task_Name";
            ddlId.DataValueField = "Id";
            ddlId.DataBind();
        }

        public async void LoadWorkers()
        {
            var response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetWorkers");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<Worker>>(responseBody);

            ddlname.DataSource = tasks;
            ddlname.DataTextField = "Name";
            ddlname.DataValueField = "Id";
            ddlname.DataBind();
        }
        private Dictionary<int, string> animalDict = new Dictionary<int, string>();

        private async Task PopulateAnimalsAsync()
        {
            try
            {
                CreateAnimalTable();

                // Fetch animals
                HttpResponseMessage animalResponse = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                animalResponse.EnsureSuccessStatusCode();

                string animalResponseBody = await animalResponse.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(animalResponseBody);

                // Fetch allocated tasks (once)
                HttpResponseMessage allocatedTasksResponse = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAllocatedTasks");
                allocatedTasksResponse.EnsureSuccessStatusCode();

                string allocatedTasksResponseBody = await allocatedTasksResponse.Content.ReadAsStringAsync();
                var allocatedTasks = JsonConvert.DeserializeObject<List<TaskAllo>>(allocatedTasksResponseBody);

                foreach (var animal in animals)
                {
                    animal.varience = animal.Cost - animal.Actual_Cost;

                    if (animal.HealthStatus == "Unknown")
                    {
                        var hasHealthCheckTask = allocatedTasks.Any(t => t.AnimalId.Contains(animal.Id.ToString()) && t.Task_Id == 6);
                        animal.Recommend = hasHealthCheckTask ? "No task recommended" : "Health Check";
                    }
                    else
                    {
                        animal.Recommend = "No task recommended";
                    }

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));
                    newRow.Cells.Add(CreateCell(animal.HealthStatus.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Recommend.ToString()));

                    TableCell cell = new TableCell();
                    CheckBox list = new CheckBox
                    {
                        ID = "list_" + animal.Id,
                        Checked = selectAnimalId.Contains(animal.Id),
                        AutoPostBack = false
                    };

                    list.CheckedChanged += CheckBox_CheckedChanged;
                    cell.Controls.Add(list);
                    newRow.Cells.Add(cell);
                    ATable.Rows.Add(newRow);

                    // Check if the animal already exists in the dictionary before adding
                    if (!animalDict.ContainsKey(animal.Id))
                    {
                        animalDict.Add(animal.Id, animal.Name);
                    }
                }
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



        private void CreateAnimalTable()
        {
            ATable.Rows.Clear(); // Clear existing rows
            ATable.Attributes.Add("class", "table table-striped table-bordered");

            // Define table headers
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(CreateCell("Name")); // true for header cell
            headerRow.Cells.Add(CreateCell("Health Status"));
            headerRow.Cells.Add(CreateCell("Recommendation"));
            headerRow.Cells.Add(CreateCell("Select")); // For checkbox

            ATable.Rows.Add(headerRow);
        }

        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }

        protected void ddlId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlId.SelectedItem.Text == "Feed Animals")
            {
                feed.Visible = true;
                lblfeed.Visible = true;
                lblKraal.Visible = false;
                ddlKraal.Visible = false;
                lblsanitize.Visible = false;
                sanitize.Visible = false;
            }
            else if (ddlId.SelectedItem.Text == "Sanitize kraals")
            {
                lblKraal.Visible = true;
                ddlKraal.Visible = true;
                lblsanitize.Visible = true;
                sanitize.Visible = true;
            }
            else
            {
                feed.Visible = false;
                lblfeed.Visible = false;
                lblKraal.Visible = false;
                ddlKraal.Visible = false;
                lblsanitize.Visible = false;
                sanitize.Visible = false;
            }
        }

        protected async void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            selectAnimalId = GetAnimalIDs();
            await UpdateTotal();

            lblsanitize.Visible = false;
            sanitize.Visible = false;

        }

        protected async Task UpdateTotal()
        {
            await Calc();

        }

        protected async void AllocateTask_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;

            selectAnimalId = GetAnimalIDs();
            var selectedString = string.Join(" ", selectAnimalId);

            var taskId = ddlId.SelectedValue;
            var userId = ddlname.SelectedValue;
            var status = "Not Started";
            var duedate = time;


            int feedvalue = 0;
            string selectedKraal = ddlKraal.SelectedItem?.Text ?? "No Kraal Selected";
            string taskName = ddlId.SelectedItem.Text;



            if (ddlId.SelectedItem.Text == "Feed Animals")
            {
                await Calc();


                if (int.TryParse(feed.Text, out int parsedFeedValue))
                {

                    feedvalue = parsedFeedValue;
                }
            }

            var allo = new
            {
                Task_Id = taskId,
                User_Id = userId,
                Status = status,
                Duedate = duedate,
                AnimalId = selectedString,
                Feed = feedvalue,
            };

            var response = await AllocateAsync(allo);
            var responsebody = await response.Content.ReadAsStringAsync();

            List<string> animalNames = selectAnimalId.Select(id => animalDict[id]).ToList();
            string selectedAnimalNames = string.Join(", ", animalNames);

            if (response.IsSuccessStatusCode)
            {
                if (ddlId.SelectedItem.Text == "Sanitize kraals")
                {
                    err.InnerText = $"{taskName} task has been allocated for kraal {selectedKraal},you can track the progress on the mobile app.";
                }
                else
                {
                    err.InnerText = $"{taskName} task has been allocated for {selectedAnimalNames},you can track the progress on the mobile app.";
                }

                if (taskName == "Health Check")
                {
                    // Update recommendations for the selected animals
                    await UpdateAnimalRecommendationsAfterAllocation(selectAnimalId);
                }

            }
            else
            {

            }

        }
        private async Task UpdateAnimalRecommendationsAfterAllocation(List<int> animalIds)
        {
            try
            {
                // Fetch the current list of animals
                HttpResponseMessage animalResponse = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                animalResponse.EnsureSuccessStatusCode();

                string animalResponseBody = await animalResponse.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(animalResponseBody);

                // Fetch allocated tasks
                HttpResponseMessage allocatedTasksResponse = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAllocatedTasks");
                allocatedTasksResponse.EnsureSuccessStatusCode();

                string allocatedTasksResponseBody = await allocatedTasksResponse.Content.ReadAsStringAsync();
                var allocatedTasks = JsonConvert.DeserializeObject<List<TaskAllo>>(allocatedTasksResponseBody);

                // Update recommendations for the selected animals
                foreach (var animalId in animalIds)
                {
                    var animal = animals.FirstOrDefault(a => a.Id == animalId);
                    if (animal != null && animal.HealthStatus == "Unknown")
                    {
                        var hasHealthCheckTask = allocatedTasks.Any(t => t.AnimalId.Contains(animalId.ToString()) && t.Task_Id == 6);
                        Console.WriteLine($"Animal: {animal.Name}, Has Health Check Task: {hasHealthCheckTask}");
                        animal.Recommend = hasHealthCheckTask ? "No task recommended" : "Health Check recommended";
                        Console.WriteLine($"Animal: {animal.Name}, Recommendation: {animal.Recommend}");
                    }
                }

                // Optionally, refresh the animal table to reflect changes in the UI
                RefreshAnimalTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async void RefreshAnimalTable()
        {
            animalDict.Clear();
            ATable.Rows.Clear();
            await PopulateAnimalsAsync();
        }

        public string Kraal(int kraal)
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

        private List<int> GetAnimalIDs()
        {
            List<int> list = new List<int>();


            foreach (TableRow row in ATable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        if (control is CheckBox checkBox && checkBox.Checked)
                        {
                            //string checkBoxId = checkBox.ID; 
                            string animalIdString = checkBox.ID.Replace("list_", "");

                            if (int.TryParse(animalIdString, out int animalId))
                            {
                                list.Add(animalId);
                            }

                        }
                    }
                }
            }

            return list;
        }


        private async Task<HttpResponseMessage> AllocateAsync(object user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("http://localhost:7041/api/Farmacy/AllocateTask", content);
        }

        protected async void ddlKraal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedKraalId = int.Parse(ddlKraal.SelectedValue);
            await DisplaySanitizedSizeAsync(selectedKraalId);
        }


        private async Task DisplaySanitizedSizeAsync(int kraalId)
        {
            try
            {
                // Fetch kraals data
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetKraals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var kraals = JsonConvert.DeserializeObject<List<KraalS>>(responseBody);

                // Find the selected kraal
                var selectedKraal = kraals.FirstOrDefault(k => k.Id == kraalId);
                if (selectedKraal != null)
                {
                    // Always set the sanitizer value to sanitize field, ignoring SizeSanitized or SizeUnsanitized
                    sanitize.Text = selectedKraal.Sanitizer.ToString();

                    lblsanitize.Visible = true;
                    sanitize.Visible = true;
                }
                else
                {
                    sanitize.Text = "No data available";
                    lblsanitize.Visible = true;
                    sanitize.Visible = true;
                }
            }
            catch (HttpRequestException ex)
            {
                sanitize.Text = $"Error: {ex.Message}";
                lblsanitize.Visible = true;
                sanitize.Visible = true;
            }
        }



        private async Task Calc()
        {
            HttpResponseMessage responseA = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
            responseA.EnsureSuccessStatusCode();

            string responseBody = await responseA.Content.ReadAsStringAsync();
            var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

            var selectedAnimals = animals.Where(animal => selectAnimalId.Contains(animal.Id)).ToList();
            decimal? total = selectedAnimals.Sum(animal => animal.HayNeeded);
            feed.Text = total?.ToString();
        }

        public class task
        {

            public int Id { get; set; }


            public string Task_Name { get; set; }


            public string Description { get; set; }


            public string Category { get; set; }

        }

        public class KraalS
        {

            public int Id { get; set; }


            public string Name { get; set; }


            public int Size { get; set; }


            public int Sanitizer { get; set; }


            public int SizeSanitized { get; set; }


            public int SizeUnsanitized { get; set; }
        }

        public class Worker
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
            public int UserType { get; set; }
        }

        public class TaskAllo
        {
            public int Id { get; set; }
            public int Task_Id { get; set; }
            public string AnimalId { get; set; }
            public string User_Id { get; set; }
            public string Status { get; set; }
            public DateTime Duedate { get; set; }
        }

        public class Animal
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Species { get; set; }
            public string Gender { get; set; }
            public string Breed { get; set; }
            public string Colour { get; set; }
            public string Recommend { get; set; }

            public decimal A_Weight { get; set; }
            public decimal Height { get; set; }
            public string HealthStatus { get; set; }
            public DateTime DateAcquired { get; set; }
            public int Age { get; set; }
            public int Kraal { get; set; }
            public string AppetiteStatus { get; set; }
            public decimal New_Weight { get; set; }
            public decimal HayNeeded => A_Weight * 0.3M;
            public decimal UpdateHayNeeded => New_Weight * 0.3M - HayNeeded;
            public decimal Cost => HayNeeded * 10.0M;

            public decimal Actual_Cost;
            public decimal varience;
        }



    }
}