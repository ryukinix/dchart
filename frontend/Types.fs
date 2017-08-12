/// This module define all the types to handle the data of charting on
/// frontend interface
///
/// [Thu 10 Aug 2017 07:44:34 AM -03]
/// TODO: use JsonProvider on new style of Type Providers of .NET Core
/// I'm having trouble to integrate TypeProviders
/// and using .NET Core 1.1 (target of Giraffe project) or
/// .NET standard 1.6 (Fable project)
module Frontend.Types

open System

type DatePrice = { 
    x: DateTime
    y: float
}

type MetalData = {
    Name: string
    Currencies: string []
    UpdateAt: System.DateTime
    mutable data: DatePrice [] 
}

type DateScatterValue = {
    x: DateTime
    y: float
    size: float
}

type Series<'a> = {
    key: string
    values: 'a array
}
