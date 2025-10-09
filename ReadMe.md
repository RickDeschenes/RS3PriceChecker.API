# RS3PriceChecker

RS3PriceChecker is a .NET 6 solution for retrieving and displaying RuneScape 3 Grand Exchange item prices. It provides a RESTful API and supporting services for accessing item details and price information.

## Features

- Fetch current prices for RuneScape 3 items from the Grand Exchange.
- Retrieve detailed information for specific items.
- Modular architecture with separate service and repository layers.
- Extensible API for integration with other applications.

## Project Structure

- `RS3PriceChecker.API`: ASP.NET Core Web API exposing endpoints for price and item queries.
- `RS3PriceChecker.Services`: Business logic for interacting with the Grand Exchange and item details.
- `RS3PriceChecker.Repository`: Data access layer for item information.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022 or later

### Build and Run

1. Clone the repository:
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Build the solution.
5. Run the API project (`RS3PriceChecker.API`).

### API Usage

- **Get Item Price**
Returns the current price for the specified item.

- **Get Item Details**

Returns detailed information about the specified item.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.

## Contact

For questions or support, open an issue on [GitHub](https://github.com/RickDeschenes/RS3PriceChecker.API).
