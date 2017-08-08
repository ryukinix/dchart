namespace Backend.Models

open FSharp.Data

[<CLIMutableAttribute>]
type Message =
    {
        Text : string
    }

// not working yet... need correct references
// type Metal = JsonProvider<"../Template.json">
