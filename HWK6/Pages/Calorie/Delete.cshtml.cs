using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CalorieRestAPI.Controllers;
using CalorieRestAPIMySQL.Interfaces;
using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Repositories;
using CalorieRestAPIMySQL.Data;
using CalorieRestAPI;


namespace CalorieWebApp.Pages.Calories
{
    /// <summary>
    /// Page model class for the Delete page
    /// </summary>
    public class DeleteModel : PageModel
    {
        public CalorieClass Calorie  = new();

        public string ErrorMessage  = "";

        public string SuccessMessage  = "";

        /// <summary>
        /// HTTP Get method to retrieve the calorie with the specified id
        /// </summary>
        /// <param name="id">The id of the calorie to delete</param>
        /// <returns>A Task representing the asynchronous operation</returns>

        public async Task OnGetAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:7248");

                var response = await client.GetAsync("Calorie/" +id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Calorie = Newtonsoft.Json.JsonConvert.DeserializeObject<CalorieClass>(content);
                }
                else
                {
                    ErrorMessage = "Error retrieving calorie";
                }
            }
        }

        /// <summary>
        /// HTTP Post method to delete the calorie with the specified id
        /// </summary>
        /// <param name="id">The id of the calorie to delete</param>
        public async void OnPostAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:7248");

                var response = await client.DeleteAsync("/Calorie/" + id);
                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Calorie successfully deleted";
                }
                else
                {
                    ErrorMessage = "Error deleting calorie";
                }
            }
        }
    }
}


