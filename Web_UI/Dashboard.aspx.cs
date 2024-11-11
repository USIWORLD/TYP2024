using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace Farmacy_App
{
    public partial class Dashboard1 : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                await GetMessage();

            }

            if (first)
            {
                await PopulateWorkerAsync();
                await PopulateAnimalsAsync();
                await PopulateTasksAsync();
                await PopulateStatusAsync();
                first = false;
            }

        }


        protected async Task GetMessage()
        {
            try
            {
                // Retrieve the existing animal data
                HttpResponseMessage response = await client.GetAsync("GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animal = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                var highRiskAnimals = animal.Where(a => a.HealthStatus == "HighRisk").ToList();
                var fmdAnimals = animal.Where(a => a.HealthStatus == "FMD Confirmed").ToList();

                if (highRiskAnimals.Count > 0 || fmdAnimals.Count > 0)
                {
                    string highRiskIds = highRiskAnimals.Count > 0 ? string.Join(", ", highRiskAnimals.Select(a => a.Name)) : "None";
                    string fmdIds = fmdAnimals.Count > 0 ? string.Join(", ", fmdAnimals.Select(a => a.Name)) : "None";

                    string alertMessage = $"High-risk animals detected with Tags: {highRiskIds}\nFMD confirmed in animals with Tags: {fmdIds}.";

                    // Display the alert message in SweetAlert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CustomAlert", $"showCustomAlert('{alertMessage.Replace("'", "\\'").Replace("\n", "\\n")}');", true);

                    // Send email in background
                    SendEmailInBackground(alertMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Request Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error: {ex.Message}";
            }
        }


        private void SendEmailInBackground(string alertMessage)
        {
            Task.Run(() => SendEmailAsync(alertMessage));
        }

        private async Task SendEmailAsync(string alertMessage)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("wiseup2riseupdk@gmail.com"),
                    Subject = "Animal Health Alert",
                    Body = alertMessage
                };

                mail.To.Add("mabasolebo2330@gmail.com");

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("wiseup2riseupdk@gmail.com", "CodeXplorers");
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the email sending error appropriately.
            }
        }



        private async Task PopulateTasksAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetTasks");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<taskInfo>>(responseBody);

                int count = tasks.Count();
                lbltask.Text = count.ToString();
            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
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

                int count = animals.Count();
                lblanimals.Text = count.ToString();
            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
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
                lblworker.Text = count.ToString();

            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
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
                lblFMd.Text = animals.fmd.ToString();

                HealthyCows.Text = animals.HealthyCount.ToString();
                HighRiskCows.Text = animals.HighRiskCount.ToString();
                ExposedCows.Text = animals.ExposedCount.ToString();
                FMDCows.Text = animals.fmd.ToString();

            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
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
        }

        public class HealthCounts
        {

            public int key { get; set; }
            public int HealthyCount { get; set; }


            public int ExposedCount { get; set; }

            public int HighRiskCount { get; set; }

            public int fmd { get; set; }
        }

        public class Worker
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Email { get; set; }
        }



        public class taskInfo
        {

            public int Id { get; set; }


            public string Task_Name { get; set; }


            public string Description { get; set; }


            public string Category { get; set; }

        }
    }

}