using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxaDeck.Actions.Helper
{
    public class PluginSettings
    {
        public static PluginSettings CreateDefaultSettings()
        {
            return new PluginSettings
            {
                UserId = string.Empty, // Default user ID if not set
                SelectedColor = LuxaforController.LuxaforColors.red.ToString() // Default color if not set
            };
        }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string SelectedColor { get; set; }
    }
}
