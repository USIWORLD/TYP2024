using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Farmacy_App
{
    public partial class AddAnimal : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        
        protected async void AddAnimal_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new
                {
                    Name = name.Text,
                    Species = "Cow",
                    Gender = ddlGender.SelectedValue,
                    Breed = ddlSACowBreed.SelectedValue,
                    Colour = ddlCowColor.SelectedValue,
                    A_Weight = Convert.ToDecimal(weight.Text),
                    Height = 143,
                    HealthStatus = ddlHealthStatus.SelectedValue,
                    DateAcquired = date.Text,
                    Age = Convert.ToInt16(Age.Text),
                    Kraal = Convert.ToInt16(ddlKraal.SelectedValue),
                    AppetiteStatus = "Normal",
                    New_Weight = 0.00,
                };

                var response = await RegisterAnimalAsync(user);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("AnimalManagement.aspx");
                }
                else
                {
                    error.Text = $"Enter Valid Details {response}";
                }
            }
            catch (Exception ex)
            {
                error.Text = $"An error occurred: {ex.Message}";
            }
        }


        private async Task<HttpResponseMessage> RegisterAnimalAsync(object user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("http://localhost:7041/api/Farmacy/RegisterAnimal", content);
        }
    }
}