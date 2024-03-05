# TicTacToe-Network

CS408 - Computer Networks Course Project

- [Introduction](#introduction)
- [Objectives](#objectives)
- [Rules](#rules)
- [Installation](#installation)
- [Usage](#usage)

![TicTacToe-Network Demo](https://github.com/akifisitan/TicTacToe-Network/blob/main/tictactoe.png)

## Introduction

- This is a project created for the CS408 Computer Networks course at Sabanci University.
- The project consists of two separate modules, the server module and the client module. A game of TicTacToe is played between connected clients.
- The server module accepts incoming requests and handles the game logic.
- The client module connects to the server and sends requests to the server for game moves or leaving the server.
- C# .NET Framework v4.5.2 and WinForms were used to build the GUI application.

## Rules

- Each player must have a unique nickname and at most 4 players can join the server at the same time.
- The first 2 players play the game while the others spectate. If one of the active players disconnects, the next waiting spectator takes up their place.
- Game scores are stored for each player, but only connected player scores are displayed.

## Objectives

- Learn the basics of socket programming.
- Learn the basics of C# and the .NET Framework.
- Learn the basics of GUI programming with WinForms.

## Installation

- Download and install the .NET Framework Runtime (v4.5.2 or newer)
- Download the [server](https://github.com/akifisitan/TicTacToe-Network/releases/download/v1.0.0/tictactoe_network_server.exe) and [client](https://github.com/akifisitan/TicTacToe-Network/releases/download/v1.0.0/tictactoe_network_client.exe) modules.

## Usage

1. Launch the server module, enter a valid port number and start the server. Note down the IP address of the server machine.
2. Launch the client module, choose a username and connect to the server by giving the IP address and port number of the server.
3. Once at least 2 players have joined, start the game via the server module.
4. Once a game is over, a new one starts after 5 seconds.
