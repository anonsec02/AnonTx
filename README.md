# AnonTx

AnonTx is a standalone Bitcoin privacy desktop application designed for maximum anonymity. It allows users to generate Bitcoin addresses and broadcast signed transactions exclusively over the Tor network.

## Features

- **Anonymous Address Generation**: Generate new Bitcoin addresses with QR codes.
- **Secure Broadcasting**: Broadcast pre-signed transactions over Tor to ensure your IP address remains hidden.
- **Tor Integration**: All network traffic is routed through the Tor network by default.
- **Open Source**: Transparency is key to privacy. View the full source code on GitHub.

## Technical Specifications

- **Platform**: Windows (Standalone Desktop Application)
- **Framework**: .NET 6 / Avalonia UI (Fluent Design)
- **Privacy**: Integrated Tor onion routing.

## Development

AnonTx is developed by `anonsec02`.

### Building from Source

1. Install .NET 6 SDK.
2. Clone the repository: `git clone https://github.com/anonsec02/AnonTx.git`
3. Navigate to the project directory: `cd AnonTx`
4. Build the project: `dotnet build`
5. Run the application: `dotnet run --project WalletAnonTx.Fluent.Desktop`

## License

This project is licensed under the MIT License - see the LICENSE file for details.
