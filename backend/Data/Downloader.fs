// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

/// This namespace will define all the stuff about caching data and handling it.
namespace Backend.Data

/// TODO: Avoid replication, write type Asset to abstract Silver/Gold operations [DONE]


open System
open System.Net
open System.IO
open FSharp.Data
open Microsoft.FSharp.Control.CommonExtensions // get AsyncGetResponse

/// Downloader module is a simple handler to download files
module Downloader =
    let Interval = 60 * 1000 // 1 minute in milliseconds to download on update

    /// get the default temp folder on operating system
    let path = Path.GetTempPath()

    /// fetchUrl asynchronously
    let fetchUrlAsync (url: string) =
        async {
            let req = WebRequest.Create(Uri(url))
            use! resp = req.AsyncGetResponse()
            use stream = resp.GetResponseStream()
            use reader = new IO.StreamReader(stream)
            return reader.ReadToEnd().Replace("null", "-1") // this is bad¹
        }
    /// the -1 avoid future problems on parsing the data by static types
    /// on chart creation

    /// download [synchronized]
    let fetchUrl (url: string) : string =
        fetchUrlAsync url
        |> Async.RunSynchronously

    /// Save a file on temp folder and return its path (synchronized)
    /// [SIDE-EFFECTS] : save file on disk, replace null by -1
    let save (url: string ) (filename: string) =
        let filePath = Path.Combine(path, filename)
        File.WriteAllText(filePath, fetchUrl(url))

    /// Update a routine as async at each `Interval`
    let update (routine: unit -> unit) =
        async {
            while true do
                do! Async.Sleep Interval
                routine()
            }
