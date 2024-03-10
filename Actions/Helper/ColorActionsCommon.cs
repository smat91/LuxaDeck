using BarRaider.SdTools;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LuxaDeck.Actions.Helper
{
    public class ColorActionsCommon
    {
        public async Task HandleKeyPressed(LuxaforController luxaforController, PluginSettings settings)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, "Key Pressed");

            if (Enum.TryParse(settings.SelectedColor, out LuxaforController.LuxaforColors color))
            {
                // Use the selected color to set the Luxafor color
                await luxaforController.SetColorAsync(settings.UserId, color);
            }
            else if (Regex.IsMatch(settings.SelectedColor, "^#(?:[0-9a-fA-F]{3}){1,2}$"))
            {
                // If selectedColorString is a valid hex code, use it directly
                var colorCode = settings.SelectedColor.Replace("#", "");
                await luxaforController.SetColorAsync(settings.UserId, colorCode);
            }
        }

        public async Task UpdateKeyImageWithColor(ISDConnection connection, string selectedColorString)
        {
            // Load the SVG content
            string svgContent = File.ReadAllText(@"Images/flag.svg");

            // Define the color map
            Dictionary<string, string> colorMap = new Dictionary<string, string>
            {
                {"red", "#FF0000"},
                {"green", "#00FF00"},
                {"yellow", "#FFFF00"},
                {"blue", "#0000FF"},
                {"white", "#FFFFFF"},
                {"cyan", "#00FFFF"},
                {"magenta", "#FF00FF"}
            };

            // Get the hexadecimal value for the selected color
            string selectedColorHex = "#FF0000";
            if (colorMap.TryGetValue(selectedColorString, out var value))
            {
                selectedColorHex = value;
            }
            else if (Regex.IsMatch(selectedColorString, "^#(?:[0-9a-fA-F]{3}){1,2}$"))
            {
                // If selectedColorString is a valid hex code, use it directly
                selectedColorHex = selectedColorString;
            }

            // Create a regex pattern to find the flag's current fill color in the style attribute
            string flagPattern = @"(?<=id=""flag""[^>]*style=""\s*fill:)[^;]+";

            // Replace the flag's fill color in the SVG content
            svgContent = Regex.Replace(svgContent, flagPattern, selectedColorHex);

            // Convert the modified SVG to base64-encoded PNG
            var base64Image = ConvertSvgToBase64Png(svgContent);

            // Set the image on the Stream Deck key
            try
            {
                await connection.SetImageAsync(base64Image);
            }
            catch (Exception e)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, $"Failed to set image on Stream Deck: {e.Message}");
                throw;
            }
        }

        private string ConvertSvgToBase64Png(string svgContent, int width = 144, int height = 144)
        {
            var svg = new SkiaSharp.Extended.Svg.SKSvg();
            var svgData = Encoding.UTF8.GetBytes(svgContent);
            using (var stream = new MemoryStream(svgData))
            {
                var picture = svg.Load(stream);
                // Calculate scale to fit the SVG in the desired image size
                float scaleX = width / svg.Picture.CullRect.Width;
                float scaleY = height / svg.Picture.CullRect.Height;
                var matrix = SKMatrix.CreateScale(scaleX, scaleY);

                using (var image = SKImage.FromPicture(picture, new SKSizeI(width, height), matrix))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    //return Convert.ToBase64String(data.ToArray());
                    return Image.FromStream(data.AsStream()).ToBase64(true);
                }
            }
        }
    }
}
