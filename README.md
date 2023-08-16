#  <img src="https://raw.githubusercontent.com/myquay/Microformats/4a2af9ec1e17704436f55693ebbcf5d0540f1593/logo.svg" alt="Microformats Logo" style="width: 30px"> Microformats .NET

**A .NET Microformats2 parser.**

This parser implements the full Microformats2 specification, including backwards compatibility with classic Microformats.

## Implementation

This parser does not restrict the property or microformat names to those explicitly mentioned in the specification. Any h-\*, p-\*, u-\*, e-\*, dt-\* will be handelled with regard to the parsing rules for that type.

## Key Features

* Compatible with both [classic microformats](http://microformats.org/wiki/Main_Page#Classic_Microformats) and [microformats2](http://microformats.org/wiki/microformats2) syntaxes.
* Targets .NET Standard 2.0 for high compatibility

## Installation

This library is distributed as a NuGet package. To install with the .NET CLI run `dotnet add package Microformats --version {version number} `

## Quick Start

### Parse a HTML string

```csharp
 var parser = new Mf2();
 var html = "...";
 var result = parser.Parse(html);
```

### Configure the parser

```csharp
var parser = new Mf2().WithOptions(o =>
{
    o.DiscoverLang = true;
    o.UpgradeClassicMicroformats = true;
    o.BaseUri = new Uri("http://example.org");
    return o;
}); ;
 var html = "...";
 var result = parser.Parse(html);
```

## Roadmap

- [ ] Format to JSON

## Contributing

Contributions are very welcome!

### Ways to contribute

* Fix an existing issue and submit a pull request
* Review open pull requests
* Report a new issue
* Make a suggestion/ contribute to a discussion

## Acknowledgments

The project logo is derived from the [microformats logo mark](http://microformats.org/wiki/spread-microformats) by [Rémi Prévost](http://microformats.org/wiki/User:Remi).

## License

MIT
