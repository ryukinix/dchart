/// This module is where is defined the logic behind the generation
/// of dynamic charting using D3
/// partially borrowed from:
/// https://github.com/hoonzis/FabledCharting
namespace Frontend.Charting

open System
open Fable.Core
open Fable.Import
open Fable.Import.Browser
open Frontend.Types

type Axis =
    abstract axisLabel: string -> Axis
    abstract tickFormat: System.Func<Object,string> -> Axis

module DateUtils =
    [<Emit("new Date($0)")>]
    let fromTicks (ticks: int): DateTime = jsNative

[<AbstractClass>]
type Chart() =
    abstract xAxis: Axis
    abstract yAxis: Axis
    abstract showLegend: bool -> Chart
    abstract showXAxis: bool -> Chart
    abstract showYAxis: bool -> Chart
    abstract color: string[] -> Chart

[<AbstractClass>]
type LineChart() = inherit Chart()
        with member __.useInteractiveGuideline (value:bool): Chart = failwith "JSOnly"

type models =
    abstract lineChart: unit -> LineChart

module nv =
    [<Erase>]
    let models: models = failwith "JS only"

module Charting =
         
    let prepareLineChart xLabel yLabel =
        let colors = D3.Scale.Globals.category10()
        let chart = nv.models.lineChart().useInteractiveGuideline(true).showLegend(true).showXAxis(true).color(colors.range())
        let timeFormat = D3.Time.Globals.format("%x")
        chart.xAxis.axisLabel(xLabel).tickFormat(fun x -> 
            let dateValue = DateUtils.fromTicks(x :?> int)
            timeFormat.Invoke(dateValue)
        ) |> ignore
        chart.yAxis.axisLabel(yLabel).tickFormat(D3.Globals.format(",.1f")) |> ignore
        chart


    let clearAndGetParentChartDiv (selector:string) =
        let element = D3.Globals.select(selector);
        element.html("") |> ignore
        element

    let drawChart (chart:Chart) (data: Object) (chartSelector: string) =
        let chartElement = clearAndGetParentChartDiv(chartSelector)
        chartElement.style("height","500px") |> ignore
        chartElement.datum(data).call(chart) |> ignore


    let drawLineChart (data: Series<DatePrice> array) (chartSelector:string) xLabel yLabel =
        let chart = prepareLineChart xLabel yLabel
        drawChart chart data chartSelector