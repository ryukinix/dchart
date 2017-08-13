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

type PriceData = {
    Date: DateTime
    Prices: float array
}

/// When /silver or /gold o served cannot GET it
/// the json output should be {errors: {message: "error-message"}}
type Error = {Message: string}

type MetalData = {
    Errors: Error
    Id: int
    SourceName: string
    SourceCode: string
    Code: string
    Name: string
    UrlizeName: string
    DisplayUrl: string
    Description: string
    UpdateAt: DateTime
    Frequency: string
    FromDate: DateTime
    ToDate: DateTime
    ColumnNames: string array
    Premium: bool
    Data: PriceData array
    Type: string
}


type Series<'a> = {
    key: string
    values: 'a array
}

type Currency = int

/// index of columns currencies
let USD:Currency = 0
let GBG:Currency = 1
let EUR:Currency = 2