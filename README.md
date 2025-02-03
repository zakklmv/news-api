
### Background and Assumption

This service was quickly developed with some minimum necessary features for first usage. I will note a few improvements that could be made:

- **Configuration of Caching:** Move the configuration for caching data from the Hacker News API into a configuration file. Currently, this value is hardcoded to 5 minutes. I believe 5 minutes is quite short since top stories don't appear that frequently.
  
- **Logging:** Adding logging would be beneficial. Ideally, I would implement this using Decorators to keep the code as clean and readable as possible (even for analysts).
  
- **Caching Strategy:** Right now, I use in-memory caching to reduce the load on the API from which we fetch the news. This is sufficient for the time being and can be scaled to a certain extent. However, if the load significantly increases, it would be wise to switch to Redis.
  
-  **Sorting:** Currently, the sorting of news by popularity is not re-verified; it is returned in the same order as it is received from the Hacker News API. It may need to be added in the future if there are incidents.



# News.Api

This repository contains the News.Api project, an ASP.NET Core Web API solution. 

## Prerequisites

Ensure you have the following prerequisites installed on your system:

- Windows, macOS, or Linux operating system
- .NET 8 SDK

## Installation of .NET 8

Follow these steps to install .NET 8 SDK:
1. Visit the [.NET 8 Download page](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Download the installer for Windows.
3. Run the installer and follow the on-screen instructions.


## Building the Project

Once .NET 8 SDK is installed, clone this repository and build the project:

1. Open a terminal or command prompt.
2. Clone the repository.
3. Build the solution with the following command:

   ```bash
   dotnet build News.Api.sln
   ```

## Running the Project

After building the project successfully, you can run it locally:

1. Run the project using:

   ```bash
   dotnet run --project News.Api
   ```

2. Open your web browser and visit `http://localhost:5217/swagger/index.html` to access the API.
