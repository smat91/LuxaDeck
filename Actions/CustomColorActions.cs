using BarRaider.SdTools;
using SkiaSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LuxaDeck.Actions.Helper;

namespace LuxaDeck
{
    [PluginActionId("com.smat91.luxadeck.customcolors.keyaction")]
    public class CustomColorActions : KeypadBase
    {
        #region Private Members

        private PluginSettings settings_;
        private readonly LuxaforController luxaforController_;
        private readonly ColorActionsCommon colorActionsCommon_;
        
        #endregion

        public CustomColorActions(ISDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            // Initialize settings from payload or create default
            settings_ = payload.Settings?.ToObject<PluginSettings>() ?? PluginSettings.CreateDefaultSettings();

            if (settings_.UserId == null && settings_.SelectedColor == null)
            {
                settings_ = PluginSettings.CreateDefaultSettings();
            }
            
            luxaforController_ = new LuxaforController();
            colorActionsCommon_ = new ColorActionsCommon();
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override async void KeyPressed(KeyPayload payload)
        {
            await colorActionsCommon_.HandleKeyPressed(luxaforController_, settings_);
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override async void OnTick()
        {
            await colorActionsCommon_.UpdateKeyImageWithColor(Connection , settings_.SelectedColor);
        }

        public override async void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Tools.AutoPopulateSettings(settings_, payload.Settings);
            await SaveSettings();
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        #region Private Methods

        private async Task SaveSettings()
        {
            await Connection.SetSettingsAsync(JObject.FromObject(settings_));
        }

        #endregion
    }
}