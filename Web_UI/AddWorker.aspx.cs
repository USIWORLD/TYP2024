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
    public partial class AddWorker : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void AddWorker_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new
                {
                    Id = Id.Text,
                    Name = name.Text,
                    Surname = surname.Text,
                    Email = email.Text,
                    Password = Secrecy.HashPassword(password.Text),
                    Usertype = 2,

                };

                var response = await RegisterUserAsync(user);

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    err.InnerText = "Enter Valid Details";
                }
            }
            catch (Exception ex)
            {
                err.InnerText = $"An error occurred: {ex.Message}";
            }
        }

        private async Task<HttpResponseMessage> RegisterUserAsync(object user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("http://localhost:7041/api/Farmacy/register", content);
        }
    
    }
}