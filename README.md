# LuxaDeck

LuxaDeck is a plugin for the Elgato Stream Deck that enables users to control Luxafor flag devices directly from their Stream Deck interface. With LuxaDeck, you can easily set your Luxafor device's color to communicate your availability, mood, or any other status indicators you choose to your colleagues or roommates without interrupting your workflow.

## Features

- **Color Selection**: Choose from a range of predefined colors or select a custom color using a color picker.
- **User Customization**: Enter your Luxafor User ID to ensure commands are sent to your specific device.
- **Easy Integration**: Seamlessly integrates with the Stream Deck application and your Luxafor device.

## Getting Started

### Prerequisites

- Elgato Stream Deck hardware.
- Stream Deck software installed on your computer.
- Luxafor v2 software installed on your computer.
- A Luxafor Flag device.

### Installation

To install LuxaDeck, follow these steps:

1. **Checkout the latest version** of the LuxaDeck git repository.
2. **Open a Command Prompt window** as Administrator. This is necessary for the installation script to run properly.
3. **Navigate to the `Util` folder** within the LuxaDeck files. You can do this by typing `cd path\to\LuxaDeck\Util` in the Command Prompt, replacing `path\to\LuxaDeck` with the actual path where you extracted LuxaDeck.
4. **Run the installation script** by typing `Install.bat <Debug/Release> <UUID>` and pressing Enter. Replace `<Debug/Release>` with `Debug` if you are installing for development purposes or `Release` for a production environment. Replace `<UUID>` with the UUID of the plugin, for example, `com.smat91.luxadeck`.

   Example command for a release installation:
   ```
   .\install.bat Release com.smat91.luxadeck
   ```

This script will build the project, kill any running StreamDeck processes, delete old plugin files if they exist, start the StreamDeck application, and install the new plugin.

**Note:** Ensure that the `OUTPUT_DIR` and `DISTRIBUTION_TOOL` paths within the `Install.bat` script are correctly set to your environment. The default output directory is set to `C:\TEMP`, and the distribution tool path is set to `C:\Program Files\Elgato\StreamDeck\DistributionTool.exe`. Adjust these paths if necessary before running the installation script.

### Configuration

1. Drag the LuxaDeck action to one of your Stream Deck keys.
2. Click on the key to open the settings panel.
3. Enter your Luxafor User ID in the provided text field. You will find it in the General tab under Webhook in your Luxafor software.
4. Use the color picker to select a custom color or the dropdown if you are using the standard color action.

## Usage

Once configured, simply press the configured Stream Deck key to change the color of your Luxafor device. The selected color will instantly be applied to your Luxafor flag, signaling your chosen status to those around you.

## Development

To contribute to LuxaDeck or customize it further, you will need the following:

- Visual Studio 2022 or later.
- Stream Deck SDK and StreamDeck-Tools by [BarRaider](https://github.com/BarRaider/streamdeck-tools).

Clone this repository and open the solution file in Visual Studio to start development.

## License

LuxaDeck is released under the MIT License. See the LICENSE file for more details.

## Acknowledgments

- Thanks to [BarRaider](https://github.com/BarRaider/streamdeck-tools) for the StreamDeck-Tools which made developing this plugin much easier.
