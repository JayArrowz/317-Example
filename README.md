# 317-Example for NetScape
This is a example of a 317 server using the Netscape Framework.
To see NetScape go here: https://github.com/JayArrowz/NetScape

## Configuration
Config exists inside appsettings.json. Ensure you have the correct ConnectionStrings to the postgres database

## Prerequisites
* [PostgresSQL](https://www.postgresql.org/download/)
* [Net5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

## Seeding the database

Run the command at the root of the repo:
```
dotnet tool install -g dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Cache Dir
Create the folder ```AspNetServerData\Cache``` in your users home folder and add the cache
This can be changed via the appsettings.json

## Container Modules
Currently this example uses the 317 modules. There are other revision specific modules available on Nuget to replace these ones.

```csharp
            List<Module> modules = new()
            {
                new ThreeOneSevenGameModule(),
                new MessagesModule(
                    typeof(ThreeOneSevenEncoderMessages.Types),
                    typeof(ThreeOneSevenDecoderMessages.Types)
                ),
                new ThreeOneSevenLoginModule(),
                new ThreeOneSevenUpdatingModule()
            };
            ServerHandler.RunServer<MyPlayer>("appsettings.json", BuildDbOptions, modules);
            Console.ReadLine();
```

## Changing Player Schema
A user can either use the default Player object or create their own by extending it.
A example is shown here:
```csharp
    public class MyPlayer : Player
    {
        public string ANewFieldInDb { get; set; }
    }
```

```chsarp
ServerHandler.RunServer<MyPlayer>("appsettings.json", BuildDbOptions, modules);
```

Whenever a new field has been added to the player a new migration has to be created and the database has to be updated by running the following commands:
```
dotnet ef migrations add NewMigrationName
dotnet ef database update
```

## Adding a New Message Subscriber
A example message handler can be seen here, the server automatically detects these annotations across the whole solution:
```csharp
    [MessageHandler]
    public class TestButtonHandler
    {
        [Message(typeof(ThreeOneSevenDecoderMessages.Types.ButtonMessage))]
        public void DoSomeShit(DecoderMessage<ThreeOneSevenDecoderMessages.Types.ButtonMessage> message)
        {
            Console.WriteLine("Button clicked: " + message.Message.InterfaceId);
        }
    }
```
