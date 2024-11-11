using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Farmacy_App
{
    public partial class EditAnimal : System.Web.UI.Page
    {
        HttpClient client;
        bool first = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && first)
            {
                string animalIdString = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateAnimals(animalId)));
                    first = false;
                }
            }
        }

        protected void EditAnimal_Click(object sender, EventArgs e)
        {
            string animalIdString = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                RegisterAsyncTask(new PageAsyncTask(() => SaveData(animalId)));
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

                if (animal.New_Weight > 0)
                {
                    animal.A_Weight = animal.New_Weight;
                }

                if (animal != null)
                {

                    animal.Id = id;
                    ddlGender.SelectedValue = animal.Gender;
                    ddlSACowBreed.SelectedValue = animal.Breed;
                    ddlCowColor.SelectedValue = animal.Colour;
                    weight.Text = animal.A_Weight.ToString("0");
                    Age.Text = animal.Age.ToString();
                    ddlKraal.SelectedValue = animal.Kraal.ToString();
                }
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

        protected async Task SaveData(int id)
        {
            try
            {
                // Retrieve the existing animal data
                HttpResponseMessage response = await client.GetAsync($"GetsAnimal?id={id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animal = JsonConvert.DeserializeObject<Animal>(responseBody);

                var updatedAnimal = new
                {
                    Id = id,
                    Name = animal.Name, // You should get this from a form control if it's editable
                    Species = animal.Species, // You should get this from a form control if it's editable
                    Gender = ddlGender.SelectedValue,
                    Breed = ddlSACowBreed.SelectedValue,
                    Colour = ddlCowColor.SelectedValue,
                    A_Weight = animal.A_Weight, // Use the updated weight
                    Height = animal.Height, // You should get this from a form control if it's editable
                    HealthStatus = animal.HealthStatus, // You should get this from a form control if it's editable
                    DateAcquired = animal.DateAcquired, // You should get this from a form control if it's editable
                    Age = Convert.ToInt32(Age.Text),
                    Kraal = Convert.ToInt32(ddlKraal.SelectedValue),
                    AppetiteStatus = animal.AppetiteStatus, // You should get this from a form control if it's editable
                    New_Weight = Convert.ToDecimal(weight.Text),
                };

                // Serialize the updated animal object to JSON
                var json = JsonConvert.SerializeObject(updatedAnimal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage updateresponse = await client.PutAsync($"EditAnimal/{id}", content);
                updateresponse.EnsureSuccessStatusCode();

                if (updateresponse.IsSuccessStatusCode)
                {
                    string UresponseBody = await updateresponse.Content.ReadAsStringAsync();
                    error.InnerText = $"Animal Edited Successfully.";
                    Response.Redirect("AnimalManagement.aspx");
                }
                else
                {
                    string errorBody = await updateresponse.Content.ReadAsStringAsync();
                    error.InnerText = $"Error while editing the animal data. Response: {errorBody}";
                }

            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error: {ex.Message}";
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
    }
}