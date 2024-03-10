using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LuxaDeck
{
    public class LuxaforController
    {
        public enum LuxaforColors
        {
            red,
            green,
            yellow,
            blue,
            white,
            cyan,
            magenta
        }

        private readonly HttpClient httpClient_;

        public LuxaforController()
        {
            httpClient_ = new HttpClient();
        }

        public async Task SetColorAsync(string userId, LuxaforColors color)
        {
            var url = "https://api.luxafor.com/webhook/v1/actions/solid_color";
            var requestBody = new
            {
                userId = userId,
                actionFields = new { color = color.ToString() }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient_.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                Console.WriteLine($"Failed to set color: {response.StatusCode}");
            }
        }

        public async Task SetColorAsync(string userId, string hexColorCode)
        {
            var url = "https://api.luxafor.com/webhook/v1/actions/solid_color";
            var requestBody = new
            {
                userId = userId,
                actionFields = new { color = "custom", custom_color = hexColorCode }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient_.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                Console.WriteLine($"Failed to set color: {response.StatusCode}");
            }
        }
    }
}
