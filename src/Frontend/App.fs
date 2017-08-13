module Frontend.App

open System
open Fable.Core
open Fable.Import
open Fable.Core.JsInterop
open Fable.PowerPack
open Fable.PowerPack.Fetch
open Frontend.Charting
open Frontend.Types

let mutable defaultCurrency = USD

/// Util module to transform data: build series from server data
/// for charting; and filter data based on the defaultCurrency
module Transform =
    
    /// Select the currency
    let filterCurrency (curr:Currency) (price:PriceData) =
        {
            x = price.Date
            y = Array.item curr price.Prices
        }


    let metalSeries (curr:Currency) (metal:MetalData) : Series<DatePrice> =
        /// filtering currency 
        let raw = metal.Data |> Array.map (filterCurrency curr)
        /// drop null values (-1)
        let values = Array.filter (fun price -> (int) price.y >= 0) raw
        {
            key = metal.Name
            values = values
        }


/// This module handle the basic communication with the server
/// to retrieve data from Metal prices
module Client =

    /// Fetch /silver and /gold prices
    let rec fetch() = promise {
        let! silver = fetchAs<MetalData> "/silver" []
        let! gold = fetchAs<MetalData> "/gold" []
        return [|silver; gold|]
    }
    
module Main =
    let create() = promise {
        let! data = Client.fetch()
        let series = data |> Array.map (Transform.metalSeries defaultCurrency)
        Charting.drawLineChart series "#chart" "Date" "Price"
    }

    create() |> Promise.start
