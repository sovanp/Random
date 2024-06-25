# Automation Converter Solution

This solution contains three projects:

1. **AutomationScriptConverter**: A library that processes individual `.pAutomation` files based on specified parameters.
2. **FolderProcessor**: A library that processes multiple `.pAutomation` files in a specified folder, using parameters from a CSV file.
3. **FolderProcessorApp**: A Windows Forms application that provides a user interface to process `.pAutomation` files.

## Prerequisites

- Visual Studio 2015
- .NET Framework 4.5.2

## Setup and Run Instructions

### Step 1: Open the Solution

1. Open Visual Studio 2015.
2. Go to `File > Open > Project/Solution`.
3. Navigate to the project directory and open `AutomationConverterSolution.sln`.

### Step 2: Build the Solution

1. Right-click on the solution in the Solution Explorer.
2. Select `Build Solution`.

### Step 3: Run the Windows Form Application

1. Set `FolderProcessorApp` as the startup project by right-clicking on it in the Solution Explorer and selecting `Set as StartUp Project`.
2. Press `F5` to run the Windows Form application.

### Step 4: Use the Application

1. Use the form to browse and select the folder path, file extension, and CSV file.
2. Click `Process` to process the files according to the parameters provided in the CSV file.

### Notes

- Ensure the CSV file follows the format: `OldMethodName,NewMethodName,AutomationSetId`.
- The application will display the content of the CSV file in the DataGridView.
- The application will only add a dependency if it is not already present in the `.pAutomation` file.

If you encounter any issues, please verify the paths and ensure all dependencies are correctly referenced.
