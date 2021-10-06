# Queries

First, create the model for the result and a query that implements the **IQuery<>** interface:

```C#
public class Something
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class GetSomething : IQuery<Something>
{
    public int Id { get; set; }
}
```

Next, create the handler:

```C#
public class GetSomethingQueryHandler : IQueryHandler<GetSomething, Something>
{
    private readonly MyDbContext _dbContext;

    public GetProductsHandler(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public Task<Something> Handle(GetSomething query)
    {
        return _dbContext.Somethings.FirstOrDefaultAsync(x => x.Id == query.Id);
    }
}
```

And finally, get the result using the query sender:

```C#
var query = new GetSomething { Id = 123 };
var something = await _querySender.GetResultAsync(query);
```

### Other Pages

---

- [Installation](Installation)
- [Configuration](Configuration)
- [Commands](Commands)
- [Release Notes](Release-Notes)
