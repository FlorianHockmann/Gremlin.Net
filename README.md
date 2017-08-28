# Gremlin.Net
Gremlin.Net is a cross-platform Gremlin Server driver for .NET that is written in C#. It uses WebSockets to communicate with Gremlin Server.

[![AppVeyor](https://img.shields.io/appveyor/ci/FlorianHockmann/Gremlin-Net.svg?style=plastic)](https://ci.appveyor.com/project/FlorianHockmann/Gremlin-Net) [![Coveralls](https://img.shields.io/coveralls/FlorianHockmann/Gremlin.Net.svg?style=plastic)](https://coveralls.io/r/FlorianHockmann/Gremlin.Net)

## Current State: Gremlin.Net now part of Apache TinkerPop

An extended version of Gremlin.Net is now part of the Apache TinkerPop project. This extended version contains a .NET Gremlin language variant (GLV) that makes it much more convenient to work with Gremlin in .NET. More information about this extended version of Gremlin.Net can be found in the [reference documentation of Apache TinkerPop](http://tinkerpop.apache.org/docs/current/reference/#gremlin-DotNet). The extended versions of Gremlin.Net that are now managed by Apache TinkerPop can be easily identified as they also follow TinkerPop's versioning (3.x.y versions currently).

I only maintain this repository for the pre-GLV versions (0.x.y) in case a critical bug is found that needs to be fixed. All work on new features is happening at [Apache TinkerPop](http://tinkerpop.apache.org/).

## Getting Started

Install the [NuGet package](https://www.nuget.org/packages/Gremlin.Net/).

## Usage

```cs
var gremlinServer = new GremlinServer("localhost", 8182);
using (var gremlinClient = new GremlinClient(gremlinServer))
{
    var result =
        await gremlinClient.SubmitWithSingleResultAsync<bool>("g.V().has('name', 'gremlin').hasNext()");
}
```

## API Reference

The API reference can be found here: [https://florianhockmann.github.io/Gremlin.Net/api/Gremlin.Net.html](https://florianhockmann.github.io/Gremlin.Net/api/Gremlin.Net.html)
