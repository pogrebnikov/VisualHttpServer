# Visual HTTP Server

`Visual HTTP Server` is a GUI HTTP server. It provides interception of requests, processing and return response with adjusted status code and body.

![Main Window](https://raw.githubusercontent.com/pogrebnikov/VisualHttpServer/master/doc/main-window-routes.png)

## Features

- Creating routes.
- Intercepting HTTP requests.
- Logging HTTP requests.

## Goals

- Create a convenient tool for intercepting HTTP requests.
- Implement proxy server mode.
- Support multiple HTTP body formats such as JSON, XML, etc.
- Generate dynamic response using built-in programming language.

## System requirements

- [Windows](https://github.com/dotnet/core/blob/main/release-notes/8.0/supported-os.md#windows)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Compiling

1. Download the archive with the sources of the [latest release](https://github.com/pogrebnikov/VisualHttpServer/releases).
2. Unpack the archive.
3. Navigate `src` folder in command prompt.
4. Run command `dotnet build VisualHttpServer.sln`.

![Build command](https://raw.githubusercontent.com/pogrebnikov/VisualHttpServer/master/doc/build-command.png)

## Installation

`Visual HTTP Server` is a portable application and does not require installation.

## Deployment

1. Navigate `src\VisualHttpServer\bin\Debug\net8.0-windows` in explorer.

![Output folder](https://raw.githubusercontent.com/pogrebnikov/VisualHttpServer/master/doc/build-output.png)

2. Copy files to target folder.

## License

[MIT](https://raw.githubusercontent.com/pogrebnikov/VisualHttpServer/master/LICENSE.txt)
