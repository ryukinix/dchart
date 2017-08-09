namespace Backend.Models

open FSharp.Data

/// the JSON type for Metal price data (like SILVER.json and GOLD.json)
type Metal = JsonProvider<"../AssetTemplate.json">
