# JobSityChallenge
### Assignment
The goal of this exercise is to create a simple browser-based chat application using .NET.

This application should allow several users to talk in a chatroom and also to get stock quotes
from an API using a specific command.

### Installation and execution
* Download the repo
* Open `JobSityNETChallenge.UI` directory in the termianal and run the command `npm install`
* Set `JobSityNETChallenge.Services.Api` as startup project and execute `Update-Database -Context JobSityNETChallengeContext` and `Update-Database -Context EventStoreSqlContext` to generate the database
* Run the UI Client with `npm start`
* Configure `JobSityNETChallenge.Services.Api` and `StockBot` as multiple startup sessions and start the backend

### Bonus assignments made
* Several chatrooms
* .NET Identity
* Handle Bot errors

### Demo video
* https://youtu.be/NuRCV38q83E
