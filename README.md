# VoidCore

[![License](https://img.shields.io/github/license/void-type/VoidCore.svg?style=flat-square)](https://github.com/void-type/VoidCore/blob/master/LICENSE.txt)
[![Build Status](https://img.shields.io/azure-devops/build/void-type/VoidCore/1.svg?style=flat-square)](https://dev.azure.com/void-type/VoidCore/_build/latest?definitionId=1&branchName=master)
[![Test Coverage](https://img.shields.io/azure-devops/coverage/void-type/VoidCore/1.svg?style=flat-square)](https://dev.azure.com/void-type/VoidCore/_build/latest?definitionId=1&branchName=master)

A set of core libraries for building domain-driven business applications. Includes opinionated support for Asp.Net Core applications.

[FoodStuffs](https://github.com/void-type/foodstuffs) is a comprehensive example of how applications can be built using VoidCore.

| Warning |
| --- |
| This project is still in the design phase as a personal project. The API is subject to change and the version numbers may fluctuate. I will remove this warning when the project reaches a stable state. |

## Documentation

Read about the packages available.

| Docs | Release | Pre-Release | Description |
| --- | --- | --- | --- |
| [VoidCore.AspNet](docs/aspnet.md) | [![NuGet package](https://img.shields.io/nuget/v/VoidCore.AspNet.svg?style=flat-square)](https://www.nuget.org/packages/VoidCore.AspNet/) | [![MyGet package](https://img.shields.io/myget/voidcoredev/vpre/VoidCore.AspNet.svg?label=myget&style=flat-square)](https://www.myget.org/feed/voidcoredev/package/nuget/VoidCore.AspNet) | Configure an Asp.Net Core web application with enterprise SPA conventions. |
| [VoidCore.Domain](docs/domain.md) | [![NuGet package](https://img.shields.io/nuget/v/VoidCore.Domain.svg?style=flat-square)](https://www.nuget.org/packages/VoidCore.Domain/) | [![MyGet package](https://img.shields.io/myget/voidcoredev/vpre/VoidCore.Domain.svg?label=myget&style=flat-square)](https://www.myget.org/feed/voidcoredev/package/nuget/VoidCore.Domain) | Domain-driven, functional and event-based development. |
| [VoidCore.Model](docs/model.md) | [![NuGet package](https://img.shields.io/nuget/v/VoidCore.Model.svg?style=flat-square)](https://www.nuget.org/packages/VoidCore.Model/) | [![MyGet package](https://img.shields.io/myget/voidcoredev/vpre/VoidCore.Model.svg?label=myget&style=flat-square)](https://www.myget.org/feed/voidcoredev/package/nuget/VoidCore.Model) | Services and interfaces for opinionated business applications. |
 [VoidCore.EfCore](docs/efcore.md) | [![NuGet package](https://img.shields.io/nuget/v/VoidCore.EfCore.svg?style=flat-square)](https://www.nuget.org/packages/VoidCore.EfCore/) | [![MyGet package](https://img.shields.io/myget/voidcoredev/vpre/VoidCore.EfCore.svg?label=myget&style=flat-square)](https://www.myget.org/feed/voidcoredev/package/nuget/VoidCore.Model) | EF Core Helpers for unit of work and change tracking. |
| [VoidCore.Finance](https://github.com/void-type/VoidCore.Finance/blob/master/README.md) | [![NuGet package](https://img.shields.io/nuget/v/VoidCore.Finance.svg?style=flat-square)](https://www.nuget.org/packages/VoidCore.Finance/) | [![MyGet package](https://img.shields.io/myget/voidcoredev/vpre/VoidCore.Finance.svg?label=myget&style=flat-square)](https://www.myget.org/feed/voidcoredev/package/nuget/VoidCore.Finance) | A financial library with primary focus on simple amortization and time-value of money. |

## Developers

VoidCore is built on [Azure Pipelines](https://dev.azure.com/void-type/VoidCore/_build/latest?definitionId=1&branchName=master) and released on [Nuget](https://www.nuget.org/packages?q=voidcore&prerel=false).

To work on VoidCore, you will need the [.Net Core SDK](https://dotnet.microsoft.com/download).

You will also need some global tools. To install them easily, just run the following:

```powershell
cd build/
./installAndUpdateTools.ps1
```

See the /build folder for scripts used to test and build this project.

There are [VSCode](https://code.visualstudio.com/) tasks for each script. The build task (ctrl + shift + b) performs the standard CI build.
