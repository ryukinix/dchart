namespace Frontend.App

open System
open Fable.Core
open Fable.Import
open Fable.Core.JsInterop
open Frontend.Charting
open Frontend.Types

module ChartingTest =

    let random = Random()

    let randomValues(): DatePrice [] =
        [|1 .. 10|] |> Array.map (fun i ->
        {
            x = DateTime(2014, 1, -i)
            y = float (random.Next() / 100000)
        })

    let drawChart() =
        let series = [|
                {
                    key = "Silver"
                    values = randomValues()
                };
                {
                    key = "Gold"
                    values = randomValues()
                }
            |]

        Charting.drawLineChart series "#chart" "Date" "Price"

    drawChart()
