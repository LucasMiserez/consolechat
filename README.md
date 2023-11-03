# Console Chat
A simple console chat application made with dotnet core


## Overview

This is a simple console-based chat application created in C# with Firebase integration. The application allows users to connect to a chatroom, exchange messages in real-time, and exit the chatroom.

## Features

- Create a chatroom.
- Connect to a chatroom with a unique identifier (UID).
- Real-time message exchange within the chatroom.

## Prerequisites

Before using this application, ensure that you have the following:

- .NET Core SDK (or .NET Framework) installed on your computer.
- A Firebase project with a Realtime Database to obtain your authentication secret and database URL.

## Getting Started

1. Clone or download the application from the GitHub repository.

2. Open the project in your preferred C# development environment (e.g., Visual Studio, Visual Studio Code, Rider).

3. In the Firebase.cs file, update the Firebase configuration with your Firebase project's authentication secret and database URL:

    ``csharp
   IFirebaseConfig config = new FirebaseConfig
   {
       AuthSecret = "YOUR_FIREBASE_AUTH_SECRET",
       BasePath = "YOUR_FIREBASE_PROJECT_URL",
   };
    ``

4. Build and run the application.

5. Enter the chatroom UID when prompted.

6. Start sending and receiving messages within the chatroom.

## Usage

- To send a message, type your message and press Enter.
- To exit the chatroom, type "/exit" and press Enter.

## Contributing

Contributions to this project are welcome. Feel free to create a pull request or open an issue on the GitHub repository.

## License

This project is licensed under the [GNU General Public License version 3 (GPL-3.0)](LICENSE). This license grants you the freedom to use, modify, and distribute the application in accordance with the terms and conditions specified in the GPL-3.0.

Please review the [LICENSE](LICENSE) file for detailed information about your rights and responsibilities under this license.

