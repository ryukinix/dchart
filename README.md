# Description

A simple dynamic charting project to fetch assets on Backend and render graphics
with d3 on Frontend. Use Giraffe on Backend and Fable on Frontend. Full F#
web application.

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 1.0.4 and runtime
* [node](https://nodejs.org/en/download/)
* [yarn](https://yarnpkg.com)
* [Giraffe](https://github.com/dustinmoris/Giraffe)
* [Fable](https://github.com/fable-compiler/Fable)
* [Mono](http://www.mono-project.com/download/) (for build)


## Build

You need fetch the `nodejs` dependencies with `yarn install` on the root of the
repository. So then build the Frontend and Backend, in that order. For building
Frontend and Backend a shell script util is provided: `build.sh`.

The overall steps is:

``` shell
yarn install
./build.sh
```

The build.sh will call `dotnet restore && dotnet fable yarn-run build` on the root
of Frontend. This will generate a /public/bundle.js file as target.
So then, will be called on src/Backend: `dotnet clean; dotnet restore; msbuild; dotnet run`.
This should generate the binaries, copy the `public` folder as `WebRoot` and provide
as static files.

The build binaries should be generated on `src/Backend/bin/Debug/netcoreapp1.1/`
directory.

## License
Unlicensed

## Author
Manoel Vilela
