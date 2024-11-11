using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using static Farmacy_App.VetDashboard;

namespace Farmacy_App
{
    public partial class ConductReport : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        // Your Twilio Credentials
        const string accountSid = "ACef6d3d936c938dc26789fd657f227b50"; // Replace with your Account SID
        const string authToken = "d3f54805ce248e23b7cb41d4461c3153";    // Replace with your Auth Token
        const string twilioPhoneNumber = "+19032063204"; // Your Twilio phone number (formatted with country code)


        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                string animalIdString = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateAnimals(animalId)));
                }

               await PopulateAnimalsAsync();
                await PopulateVisitAsync();
                await PopulateHealthyAnimalsAsync();
                await PopulateFMDAnimalsAsync();

                first = false;
            }


        }

        private async Task UpdateAnimals(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"GetsAnimal?id={id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animal = JsonConvert.DeserializeObject<Animal>(responseBody);

                if (animal != null)
                {
                    animal.Id = id;
                    animal.Name = animal.Name;
                    lblAID.Text = animal.Name;
                    ddlGender.SelectedValue = animal.Gender;
                    ddlSACowBreed.SelectedValue = animal.Breed;
                    ddlCowColor.SelectedValue = animal.Colour;
                    weight.Text = animal.A_Weight.ToString();
                    height.Text = animal.Height.ToString();
                    date.Text = animal.DateAcquired.ToString("yyyy/MM/dd");
                    Age.Text = animal.Age.ToString();
                    ddlKraal.SelectedValue = animal.Kraal.ToString();
                    ddlHealthStatus.SelectedValue = animal.HealthStatus.ToString();
                }
            }
            catch (HttpRequestException ex)
            {
                P1.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                P1.InnerText = $"Error : {ex.Message}";
            }
        }

        protected async Task PopulateHealthyAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetHealthyAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

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
        private void SendSmsNotification(object user)
        {
            try
            {
                // Initialize Twilio clientsent
                TwilioClient.Init(accountSid, authToken);

                // Create and send SMS
                var message = MessageResource.Create(
                    body: $"FMD confirmed cows detected. From farmacy management system.",
                    from: new PhoneNumber(twilioPhoneNumber), // Your Twilio phone number
                    to: new PhoneNumber("+27725085655") // Recipient's phone number (can be your own number)
                );

                // Log the message SID for tracking
                Console.WriteLine($"Message sent with SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                err.InnerText = $"Failed to send SMS notification: {ex.Message}";
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

                foreach (var animal in animals)
                {
                    if (animal.HealthStatus.Equals("HighRisk") || animal.HealthStatus.Equals("FMD Confirmed"))
                    {
                        TableRow newRow = new TableRow();
                        newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Gender.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Breed.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Colour.ToString()));
                        newRow.Cells.Add(CreateCell(animal.A_Weight.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Height.ToString()));
                        newRow.Cells.Add(CreateCell(animal.HealthStatus.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Age.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Kraal.ToString()));


                        Button btnEdit = new Button
                        {
                            Text = "Diagnose",
                            CssClass = "btn btn-primary",
                            CommandArgument = animal.Id.ToString(),
                            Width = 100,

                        };

                        btnEdit.Click += btnEdit_Command_Click;

                        TableCell cell = new TableCell();


                        cell.Controls.Add(btnEdit);


                        newRow.Cells.Add(cell);

                    }

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


        private async Task PopulateFMDAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetFMDAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                int count = animals.Count();
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

        private async Task PopulateVisitAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetVisits");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var visits = JsonConvert.DeserializeObject<List<VetVisits>>(responseBody);

                

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

        protected void Bio_Cevit_Clicked(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Command_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnEdit = (Button)sender;
                int id = Convert.ToInt32(btnEdit.CommandArgument);
                Response.Redirect($"EditbyVet.aspx?id={id}", false);


            }
            catch (Exception ex)
            {

                err.InnerText = $"Error: {ex.Message}";

            }
        }

        protected void AddAnimal_Click(object sender, EventArgs e)
        {
            string animalIdString = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                RegisterAsyncTask(new PageAsyncTask(() => SaveData(animalId)));

            }
        }

        protected async Task SaveData(int id)
        {
            try
            {
                // Retrieve the existing animal data
                HttpResponseMessage response = await client.GetAsync($"GetsAnimal?id={id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animal = JsonConvert.DeserializeObject<Animal>(responseBody);

                // Update the animal data
                var updatedAnimal = new
                {
                    Id = id,
                    Name = animal.Name,
                    Species = animal.Species,
                    Gender = ddlGender.SelectedValue,
                    Breed = ddlSACowBreed.SelectedValue,
                    Colour = ddlCowColor.SelectedValue,
                    A_Weight = animal.A_Weight,
                    Height = Convert.ToDecimal(height.Text),
                    HealthStatus = "FMD Confirmed",
                    DateAcquired = Convert.ToDateTime(date.Text).ToString("yyyy-MM-ddTHH:mm:ss"),
                    Age = Convert.ToInt32(Age.Text),
                    Kraal = Convert.ToInt32(ddlKraal.SelectedValue),
                    appetiteStatus = animal.AppetiteStatus,
                    New_Weight = Convert.ToDecimal(weight.Text),
                };

                // Serialize the updated animal object to JSON
                var json = JsonConvert.SerializeObject(updatedAnimal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage updateresponse = await client.PutAsync($"EditAnimal/{id}", content);

                if (updateresponse.IsSuccessStatusCode)
                {
                    SendSmsNotification(updatedAnimal);

                    string UresponseBody = await updateresponse.Content.ReadAsStringAsync();
                    var animals = JsonConvert.DeserializeObject<Animal>(UresponseBody);
                    P1.InnerText = "Animal Edited Successfully, redirecting to Allocate Medication page.";
                    Response.Redirect($"AllocatefromVet.aspx?id={id}", false);
                }
                else
                {
                    P1.InnerText = "Error while editing the animal data.";
                }
            }
            catch (HttpRequestException ex)
            {
                P1.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                P1.InnerText = $"Error: {ex.Message}";
            }
        }


        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
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


            public string AppetiteStatus { get; set; }


            public string HealthStatus { get; set; }


            public DateTime DateAcquired { get; set; }

            public int Age { get; set; }

            public int Kraal { get; set; }
        }

        public class VetVisits
        {
            public int Visitkey { get; set; }

            public DateTime VisitDate { get; set; }

            public string AnimalID { get; set; }

            public string Availability { get; set; }

            public decimal Cost { get; set; }

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
    }
}