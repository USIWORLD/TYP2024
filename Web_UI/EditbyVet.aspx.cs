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

namespace Farmacy_App
{
    public partial class EditbyVet : System.Web.UI.Page
    {
        HttpClient client;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string animalIdString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateAnimals(animalId)));
                }

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

        private async Task UpdateAnimals(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"GetAnimal/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                /*var animal = JsonConvert.DeserializeObject<Animal>(responseBody);

                /(animal != null)
                {
                    id = animal.Id;
                    ddlGender.SelectedValue = animal.Gender;
                    ddlSACowBreed.SelectedValue = animal.Breed;
                    ddlCowColor.SelectedValue = animal.Colour;
                    weight.Text = animal.A_Weight.ToString();
                    height.Text = animal.Height.ToString();
                    date.Text = animal.DateAcquired.ToString("yyyy/MM/dd");
                    Age.Text = animal.Age.ToString();
                    ddlKraal.SelectedValue = animal.Kraal.ToString();
                }*/

                string[] animal = responseBody.Split(' ');

                if (animal.Length >= 11)
                {
                    int Id;
                    if (!int.TryParse(animal[0], out Id))
                    {
                        throw new FormatException("Invalid id format");
                    }

                    string species = animal[1];
                    string gender = animal[2];
                    string breed = animal[3];
                    string colour = animal[4];

                    decimal Weight;
                    if (!Decimal.TryParse(animal[5], out Weight))
                    {
                        throw new FormatException("Invalid weight format");
                    }

                    decimal Height;
                    if (!Decimal.TryParse(animal[6], out Height))
                    {
                        throw new FormatException("Invalid height format");
                    }

                    string healthstatus = animal[7];
                   

                    DateTime dateAcquired;

                    if (!DateTime.TryParseExact(animal[8], "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out dateAcquired))
                    {
                        throw new FormatException("Invalid date format");
                    }

                    int age;

                    if (!int.TryParse(animal[10], out age))
                    {
                        throw new FormatException("Invalid age format");
                    }

                    int kraal;

                    if (!int.TryParse(animal[11], out kraal))
                    {
                        throw new FormatException("Invalid age format");
                    }

                    ddlHealthStatus.SelectedValue = healthstatus;
                  



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
                HttpResponseMessage response = await client.GetAsync($"GetAnimal/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();


                string[] animal = responseBody.Split(' ');

                if (animal.Length >= 10)
                {
                    int Id;
                    if (!int.TryParse(animal[0], out Id))
                    {
                        throw new FormatException("Invalid id format");
                    }

                    string species = animal[1];
                    string gender = animal[2];
                    string breed = animal[3];
                    string colour = animal[4];

                    decimal Weight;
                    if (!Decimal.TryParse(animal[5], out Weight))
                    {
                        throw new FormatException("Invalid weight format");
                    }

                    decimal Height;
                    if (!Decimal.TryParse(animal[6], out Height))
                    {
                        throw new FormatException("Invalid height format");
                    }

                    string healthstatus = animal[7];


                    DateTime dateAcquired;

                    if (!DateTime.TryParseExact(animal[8], "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out dateAcquired))
                    {
                        throw new FormatException("Invalid date format");
                    }

                    int age;

                    if (!int.TryParse(animal[10], out age))
                    {
                        throw new FormatException("Invalid age format");
                    }

                    int kraal;

                    if (!int.TryParse(animal[11], out kraal))
                    {
                        throw new FormatException("Invalid age format");
                    }


                    var Animals = new
                    {
                        Id = id,
                        Species = species,
                        Gender = gender,
                        Breed = breed,
                        Colour = colour,
                        A_Weight = Weight,
                        Height = Height,
                        HealthStatus = ddlHealthStatus.SelectedValue,
                        DateAcquired = dateAcquired,
                        Age = age,
                        Kraal = kraal,

                    };



                    var json = JsonConvert.SerializeObject(Animals);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    HttpResponseMessage updateresponse = await client.PutAsync($"EditAnimal/{id}", content);


                    if (updateresponse.IsSuccessStatusCode)
                    {
                        string UresponseBody = await updateresponse.Content.ReadAsStringAsync();
                        var animals = JsonConvert.DeserializeObject<Animal>(UresponseBody);
                        error.InnerText = "Animal Edited Sucessesfully";
                        Response.Redirect("medicationAdmin.aspx");
                    }
                    else
                    {
                        error.InnerText = "Error while editing the animal data.";
                    }
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


            public string Species { get; set; }


            public string Gender { get; set; }



            public string Breed { get; set; }


            public string Colour { get; set; }


            public decimal A_Weight { get; set; }


            public decimal Height { get; set; }


            public string HealthStatus { get; set; }


            public string AppetiteStatus { get; set; }


            public DateTime DateAcquired { get; set; }

            public int Age { get; set; }

            public int Kraal { get; set; }
        }


      
    }
}