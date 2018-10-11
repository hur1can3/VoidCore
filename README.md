# VoidCore

A core library for building domain-driven business apps on Asp.Net Core with Single Page Application support.

WARNING - this project is still in the design phase as a personal project. The API is subject to change and the version numbers may fluctuate. I will remove this warning when the project reaches a stable state.

## Domain Events

Extract logic from your controller and separate cross-cutting concerns like logging and validation. All logic for an event can be put into a single file.

Events, validators and post processors can be injected since events are immutable and stateless. Validators and post processors are added through a decorator.

```csharp
public class PersonsController : Controller
{
    // For extra credit, inject GetPerson event parts on construction and let DI handle dependencies
    public IActionResult Get(string name)
    {
        var request = new GetPerson.Request(name);

        var result = new GetPerson.Handler(_data, _mapper)
            .AddRequestValidator(new GetPerson.RequestValidator())
            .AddPostProcessor(new GetPerson.Logger(_logger))
            .Handle(request);

        return _responder.Respond(result);
    }
}
```

<!-- markdownlint-disable MD033 -->
<details>
    <summary>
        Show a full real-world event example
    </summary>
<!-- markdownlint-disable MD033 -->

```csharp
public class GetPerson
{
    public class Handler : EventHandlerAbstract<Request, Response>
    {
        public Handler(PersonData data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        protected override Result<Response> HandleInternal(Request request)
        {
            var person = _data.Persons
                .ProjectTo<Response>(_mapper)
                .FirstOrDefault(l => l.Id == request.Id));

            return (person != null) ?
                Result.Ok(personDto) :
                Result.Fail<Response>("Person not found.");
        }

        private readonly PersonData _data;
        private readonly IMapper _mapper;
    }

    // Immutable request
    public class Request
    {
        public Request(int id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    // Immutable response
    public class Response
    {
        public Response(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }

    // A validator for the request
    public class RequestValidator : ValidatorAbstract<Request>
    {
        protected override void BuildRules()
        {
            CreateRule("Id is required.", "id")
                .InvalidWhen(request => string.IsNullOrWhiteSpace(request.Id));
        }
    }

    // Log it.
    public class Logger : FallibleEventLogger<Request, Response>
    {
        public Logging(ILoggingService logger) : base(logger) { }

        // Always log the incoming request
        public override void OnBoth(Request request, IFallible<Response> result)
        {
            _logger.Info($"RequestName: {request.Name}");
        }

        // Log the email of the person on success
        public override void OnSuccess(Request request, IFallible<Response> result)
        {
            _logger.Info($"Found: {result.Value.Email}");
        }

        // Logging extends FallibleEventLogger which means that failures will be automatically logged.
        // There is also PostProcessorAbstract for a clean slate of all 3 channels (both, success, fail), and IPostProcessor for a single channel (just called Process).

        private readonly ILoggingService _logger;
    }
}
```

</details>

## Validation

```csharp
class CreatePersonValidator : ValidatorAbstract<Entity>
{
    protected override void BuildRules()
    {
        CreateRule("Name is required.", "name")
            .InvalidWhen(entity => string.IsNullOrWhitespace(entity.Name));

        CreateRule("Phone is required for employees.", "phone")
            // ValidWhens are OR'd.
            // Any of the invalid conditions can invalidate the entity.
            .InvalidWhen(entity => string.IsNullOrWhitespace(entity.Phone))
            .InvalidWhen(entity => !PhoneIsValidFormat(entity.Phone))

            // ExceptWhen suppresses any violations
            // ExceptWhens are AND'd.
            // All suppression expressions have to be true to suppress
            .ExceptWhen(entity => !entity.IsEmployee)
            .ExceptWhen(entity => entity.DoesNotHavePhone)
    }
}
```

## Results for Fallible Operations

Adapted from [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions). Any method that might fail can return a Result for explicit fallibility. Results can be typed or untyped to follow the CQRS principle.

```csharp
// A fallible method returns a Result<> or Result
public Result<Person> GetPersonById(int id)
{
    var person = _data.Persons.Find(id);

    if (person == null)
    {
        Result.Fail("Person is not found.", "personIdField"));
    }

    Result.Ok(person);
}

// Call your method and handle the result.
var result = GetPersonById(id);

if (result.IsFailed)
{
    var failures = result.Failures;
}

if (result.IsSuccess)
{
  // Generic results like Result<Person> have a Person value on success.
  var person = result.Value;
}

// Combine multiple results to check for failures
IEnumerable<Result> results = CheckLotsOfThings();

var singleResult = results.Combine();

if (result.IsSuccess)
{
    return "Hooray!"
}

```

## Text Search on Object Properties

Check many properties of an object for an array of terms. It will split any search string on whitespace, or it can take an explicit array of terms.

```csharp
IQueryable<Person> people = GetPeople();
var searchString = "find all these words in any single property";

var matchedPeople = people
    .SearchStringProperties(
        searchString,
        obj => obj.FirstName,
        obj => obj.LastName,
        obj => obj.Biography
    );
```

## Standardized Responses

Make predictable presentation APIs with...

* User messages
* Validation failures with message and field name
* Data sets
* Data sets with pagination
* Downloadable files

VoidCore.AspNet includes HttpResponder to send a Result of the above through IActionResult to a web client.

## Asp.Net Core Configuration

There are many helpers to build an application with...

* Serilog multi-platform file logging.
* Active Directory group authorization via Windows authentication.
* HTTPS with redirection and HSTS headers.
* Antiforgery for SPAs.
* Exception handling (SPA and MVC) with logging.
  * Api endpoints return a JSON {message: ""} object.
  * MVC will redirect to safe error or forbidden pages in non-development.
* Routing for SPA and Web API.
* Model wrappers for data repositories with default implementation for EF Core.

## Developers

You will find everything you need to build and test this project in the Scripts folder.

There is a script to install and update global tools used to develop this project.

There are also VSCode tasks for each script.

This project is not currently released, but feel free to use it as is.

To use it you can:

1. Reference it via local ProjectReference in your csproj. It will be built with your dependent project.
2. Deploy it to a local Nuget store via [nuget.exe](https://dist.nuget.org/win-x86-commandline/latest/nuget.exe)...

```powershell
cd Scripts/
./buildAllPkg.ps1
nuget init "VoidCore.Model/out" "/path/to/your/nuget/repo/"
nuget init "VoidCore.AspNet/out" "/path/to/your/nuget/repo/"
```
