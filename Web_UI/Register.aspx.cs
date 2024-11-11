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

namespace WebFarmApplication
{
    public partial class Register : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new
                {
                    Id = username.Text,
                    Name = name.Text,
                    Surname = surname.Text,
                    Email = email.Text,
                    Password = Secrecy.HashPassword(password.Text),
                    UserType = Convert.ToInt16(usertype.Text)
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