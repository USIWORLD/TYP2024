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
    public partial class EditMedication : System.Web.UI.Page
    {
        HttpClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string medIdString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(medIdString) && int.TryParse(medIdString, out int taskId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateTasks(taskId)));
                }

            }
        }

      
        protected void AddAnimal_Click(object sender, EventArgs e)
        {
            string taskIdString = Request.QueryString["id"]; 

            if (!string.IsNullOrEmpty(taskIdString) && int.TryParse(taskIdString, out int taskId))
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                RegisterAsyncTask(new PageAsyncTask(() => SaveData(taskId)));
            }
        }

        private async Task UpdateTasks(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"GetMed/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var animal = JsonConvert.DeserializeObject<Medication>(responseBody);

               if (animal != null)
               {
                    id = animal.Id;
                    name.Text = animal.Name;
                    dosage.Text = animal.Dosage;
                    ddlfreq.SelectedValue = animal.Frequency;
                    dllcategory.SelectedValue = animal.Category;
                    dlltype.SelectedValue = animal.Type;
                    txtCost.Text = animal.Cost.ToString();
                    Quantity.Text = animal.Quantity.ToString(); 
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
                var animal = new
                {
                    Id = id,
                    Name = name.Text,
                    dosage = dosage.Text,
                    Frequency = ddlfreq.SelectedValue,
                    Category = dllcategory.SelectedValue,
                    Type = dlltype.SelectedValue,
                    Cost = Convert.ToDecimal(txtCost.Text),
                    Quantity = Convert.ToInt32(Quantity.Text),
                };

                var json = JsonConvert.SerializeObject(animal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"EditMedication/{id}", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<Medication>(responseBody);
                Response.Redirect("MedicationManagement.aspx");


            }
            catch (Exception ex)
            {
                error.InnerText = $"An error occurred: {ex.Message}";
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

            public int Quantity { get; set; }

        }



    }
}