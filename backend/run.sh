#!/bin/sh
dotnet clean; dotnet restore; msbuild; dotnet run
