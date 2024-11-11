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
using static Farmacy_App.VetDashboard;

namespace Farmacy_App
{
    public partial class AnimalProfile : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;
        string aID;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                string animalIdString = Request.QueryString["id"];
                aID = animalIdString;
                if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateAnimals(animalId)));
                }
                await populateMeds();
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
                    AnimalName.Text = animal.Name;
                    lblAnimal.Text = animal.Id.ToString();
                    aTag.Text = animal.Id.ToString();
                    kraal.Text = animal.Kraal.ToString();
                    breed.Text = animal.Breed;
                    color.Text = animal.Colour;
                    age.Text = animal.Age.ToString();
                    healthstatus.Text = animal.HealthStatus.ToString();
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

        private async Task populateMeds()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAlloMeds");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var meds = JsonConvert.DeserializeObject<List<AllocateMed>>(responseBody);
            foreach(var med in meds)
            {
                if (med.AnimalId.Contains(aID) || aID.Contains(med.AnimalId))
                {
                    MedName.Text += med.Med_Id;
                    txtMedNotes.Value += "\n" + med.MedNotes;
                }
            }
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

        public class AllocateMed
        {

            public int Id { get; set; }
            public string Med_Id { get; set; }
            public string User_Id { get; set; }
            public string Status { get; set; }
            public DateTime Date { get; set; }
            public string AnimalId { get; set; }
            public string MedNotes { get; set; }
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
    }
}