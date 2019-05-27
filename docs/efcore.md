# VoidCore.EfCore

[![NuGet package](https://img.shields.io/nuget/v/VoidCore.EfCore.svg?style=flat-square)](https://www.nuget.org/packages/VoidCore.EfCore/)
[![MyGet package](https://img.shields.io/myget/voidcoredev/vpre/VoidCore.EfCore.svg?label=myget&style=flat-square)](https://www.myget.org/feed/voidcoredev/package/nuget/VoidCore.EfCore)

## Installation

```powerShell
dotnet add package VoidCore.EfCore
```

## Features

VoidCore.EfCore includes helpers for using Entity Framework Core with unit of work pattern and auto change tracking history:

* Helper to execute a Raw SQL Command 
* Return IQueryable from Raw Sql
* Saves all changes made in this context to the database with distributed transaction.
* Automatically recording data changes history.
* Built on Top of Voidcore.AspNet and Voidcore.Model

## AutoHistory
### How to use AutoHistory

`AutoHistory` will recording all the data changing history in one `Table` named `AutoHistories`, this table will recording data
`UPDATE`, `DELETE` history.

1. Enable AutoHistory

```csharp
public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
    { }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // enable auto history functionality.
        modelBuilder.EnableAutoHistory();
    }
}
```

2. Ensure AutoHistory in DbContext. This must be called before bloggingContext.SaveChanges() or bloggingContext.SaveChangesAsync().

```csharp
bloggingContext.EnsureAutoHistory()
```

### Use Custom AutoHistory Entity
You can use a custom auto history entity by extending the VoidCore.EfCore.AutoHistory class.

```csharp
class CustomAutoHistory : AutoHistory
{
    public String CustomField { get; set; }
}
```

Then register it in the db context like follows:
```csharp
modelBuilder.EnableAutoHistory<CustomAutoHistory>(o => { });
```

Then provide a custom history entity creating factory when calling EnsureAutoHistory. The example shows using the
factory directly, but you should use a service here that fills out your history extended properties(The properties inherited from `AutoHistory` will be set by the framework automatically).
```csharp
db.EnsureAutoHistory(() => new CustomAutoHistory()
                    {
                        CustomField = "CustomValue"
                    });
```

## UnitOfWork
A plugin for Microsoft.EntityFrameworkCore to support repository, unit of work patterns, and multiple database with distributed transaction supported.

### How to use UnitOfWork

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddUnitOfWork<QuickStartContext>()
        .AddCustomRepository<Blog, CustomBlogRepository>();

    //multiple ef dbcontext
     services
        .AddUnitOfWork<QuickStartContext1, QuickStartContext2, QuickStartContext3>();
}

/// <summary>
/// Represents all the tables, views and functions of the database.
/// </summary>
public interface IDomainUnitOfWork 
{
    IReadOnlyRepository<Blog> Blogs { get; }

    IWritableRepository<Post> Posts { get; }
}

public class DomainUnitOfWork : IDomainUnitOfWork
{
    private readonly IEfUnitOfWork _unitOfWork;
    private ILoggingStrategy _loggingStrategy;
    private readonly IDateTimeService _now;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public DomainUnitOfWork(IEfUnitOfWork unitOfWork, ILoggingStrategy loggingStrategy, IDateTimeService now, ICurrentUserAccessor currentUserAccessor)
    {
        _unitOfWork = unitOfWork;
        _loggingStrategy = loggingStrategy;
        _now = now;
        _currentUserAccessor = currentUserAccessor;
    }
    public IReadOnlyRepository<Blog> Blogs => _unitOfWork.GetWritableRepository<Blog>(loggingStrategy: _loggingStrategy);

    public IWritableRepository<Post> Posts => _unitOfWork.GetWritableRepository<Post>(loggingStrategy: _loggingStrategy).AddSoftDeletability(_now,_currentUserAccessor).AddAuditability(_now, _currentUserAccessor);
}

private async Task<Post> GetPost(IDomainUnitOfWork uow, int byId, CancellationToken cancellationToken = default) 
{
      return await uow.Posts.Get(byId, cancellationToken)
}
```

