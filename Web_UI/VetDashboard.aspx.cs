using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacy_App
{
    public partial class VetDashboard : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;
        protected async void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                await GetMessage();

            }


            if (first)
            {
                await PopulateWorkerAsync();
                await PopulateAnimalsAsync();
                await PopulateStatusAsync();
                await PopulateMedAsync();
                await PopulateFMDAnimalsAsync();
                await PopulateVisitAsync();

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


                string responseBody = await response.Content.ReadAsStringAsync();
                var animal = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                var highRiskAnimals = animal.Where(a => a.HealthStatus == "HighRisk").ToList();


                if (highRiskAnimals.Count > 0)
                {
                    string highRiskIds = highRiskAnimals.Count > 0 ? string.Join(", ", highRiskAnimals.Select(a => a.Name)) : "None";

                    string alertMessage = $"High-risk animals detected: {highRiskIds}.";

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

        private async Task PopulateAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);
                List<Animal> list = new List<Animal>();
                int count = animals.Count();
                // lblanimals.Text = count.ToString();
                foreach(var animal in animals)
                {
                    if(animal.HealthStatus.Equals("FMD Confirmed"))
                    {
                        list.Add(animal);
                    }
                }
                int countFMD = list.Count();
                lblFMD.Text = countFMD.ToString();
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
        private async Task PopulateWorkerAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetWorkers");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var workers = JsonConvert.DeserializeObject<List<Worker>>(responseBody);

                int count = workers.Count();
                //lblworker.Text = count.ToString();

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

        private async Task PopulateMedAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetMedications");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var meds = JsonConvert.DeserializeObject<List<Medication>>(responseBody);

                int count = meds.Count();
                lblMed.Text = count.ToString();

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

        private async Task PopulateVisitAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetVisits");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var visits = JsonConvert.DeserializeObject<List<VetVisits>>(responseBody);

                VisitContent.Controls.Clear();

                foreach (var visit in visits)
                {

                    Button btnVisit = new Button
                    {
                        Text = visit.VisitDate.Date.ToString("yyyy/MM/dd"),
                        CommandArgument = visit.VisitDate.Date.ToString("yyyy/MM/dd"),
                        
                        Width = 100,

                    };
                    btnVisit.Style.Add("border", "none");
                    btnVisit.Style.Add("background", "none");
                    btnVisit.Style.Add("color", "blue");
                    btnVisit.Click += btnVisit_Command_Click;

                    VisitContent.Controls.Add(btnVisit);
                    VisitContent.Controls.Add(new LiteralControl("<br />"));

                }

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

        protected void btnVisit_Command_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnVisit = (Button)sender;
                string visitDate = btnVisit.CommandArgument;
                Response.Redirect($"VetReports.aspx?id={visitDate}", false);


            }
            catch (Exception ex)
            {

                err.InnerText = $"Error: {ex.Message}";

            }
        }

        private async Task PopulateStatusAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetCounts");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<HealthCounts>(responseBody);

                lblH.Text = animals.HealthyCount.ToString();
                lblEx.Text = animals.ExposedCount.ToString();
                lblHR.Text = animals.HighRiskCount.ToString();

                HealthyCows.Text = animals.HealthyCount.ToString();
                HighRiskCows.Text = animals.HighRiskCount.ToString();
                ExposedCows.Text = animals.ExposedCount.ToString();


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

        private async Task PopulateFMDAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetFMDAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                int count = animals.Count();
                //lblFMD.Text = count.ToString();
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

        public class HealthCounts
        {

            public int key { get; set; }
            public int HealthyCount { get; set; }


            public int ExposedCount { get; set; }

            public int HighRiskCount { get; set; }
        }

        public class Worker
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Email { get; set; }
        }

        public class Medication
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Dosage { get; set; }

            public string Frequency { get; set; }

            public string Category { get; set; }

            public string Type { get; set; }

            public decimal Cost { get; set; }

            public int Inventory { get; set; }

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
        }

        public class taskInfo
        {

            public int Id { get; set; }


            public string Task_Name { get; set; }


            public string Description { get; set; }


            public string Category { get; set; }

        }

        public class VetVisits
        {
            public int Visitkey { get; set; }

            public DateTime VisitDate { get; set; }

            public string AnimalID { get; set; }

            public string Availability { get; set; }

            public decimal Cost { get; set; }

        }
    }
}