# Commands

First, create a command that implements the **ICommand** interface:

```C#
public class DoSomething : ICommand
{
}
```

Next, create the handler that implements the ICommandHandler<ICommand> interface:

```C#
public class DoSomethingHandler : ICommandHandler<DoSomething>
{
    private readonly IMyService _myService;

    public DoSomethingHandler(IMyService myService)
    {
        _myService = myService;
    }

    public async Task Handle(DoSomething command)
    {
        await _myService.MyMethodAsync();
    }
}
```

And finally, send the command using the command sender:

```C#
var command = new DoSomething();

await _commandSender.Send(command);
```

### Other Pages

---

- [Installation](Installation)
- [Configuration](Configuration)
- [Queries](Queries)
- [Release Notes](Release-Notes)
