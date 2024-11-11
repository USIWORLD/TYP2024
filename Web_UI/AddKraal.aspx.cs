using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacy_App
{
    public partial class AddKraal : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void AddAnimal_Click(object sender, EventArgs e)
        {
            try
            {
                var med = new
                {
                   Name = name.Text,
                   Size = size.Text,
                   SizeSanitized = 0,
                   SizeUnsanitized = 0,
                   Sanitizer= 150
                };

                var response = await RegisterMedAsync(med);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    error.Text = "\n" + "Szucessfully added";
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

        private async Task<HttpResponseMessage> RegisterMedAsync(object user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("http://localhost:7041/api/Farmacy/AddKraal", content);
        }
    }
}