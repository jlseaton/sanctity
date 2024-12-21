Lords of Chaos: Sanctity's Edge
-------------------------------
Game client for Windows requires the .NET 6 Desktop Framework, which can be installed from here:
  x64: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.6-windows-x64-installer

Project Descriptions:
---------------------
Game.Client: Game client for Windows using .NET WinForms
Game.Core: Shared models and utility classes
Game.Realm: Game realm logic for areas
Game.Server: Game server logic for the realms comprising a world server with .NET WinForms UI
Game.Test: Unit tests
Game.World: Game server logic for the realms comprising a world server with a console based UI
--------------------------------

Deprecated Projects:
--------------------
Game.Core.PCL: Contains shared models and utility classes
Game.Data: Contains data access logic for the game
Game.Realm.PCL: Game world server logic, which can be hosted in a separate server or within the client application also, ie. single player mode
Game.Server.PCL: A WinForms game engine server, currently hosted on an Azure server full-time or can be connected to on localhost during development
Sanctity: Xamarin.Forms based PCL project that contains all of the mobile domain logic and UIs
Sanctity.Droid: Android Client including local assets and native api calls
Sanctity.iOS: iOS Client including local assets and native api calls
Sanctity.UWP: Windows Univeral Client (Windows 10, etc) including local assets and native api calls
Sanctity.Windows: Windows 8.1 Client including local assets and native api calls
Sanctity.WinForms: A WinForms client, using ClickOnce to handle automatic updates
Sanctity.WinPhone: Windows Phone 8.1 Client including local assets and native api calls
--------------------------------

Build Notes:
------------

- No known issues
--------------------------------
