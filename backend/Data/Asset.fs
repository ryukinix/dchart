namespace Backend.Data

open System.IO

type Asset =
    { Url: string
      Filename: string}

    member this.Filepath() = Path.Combine(Downloader.path, this.Filename)
    member this.Get() = File.ReadAllText(this.Filepath())
    member this.Save() = Downloader.save this.Url this.Filename
    member this.Update() = Downloader.update this.Save  // async


module Assets =
    let Silver =
        {Url = "https://www.quandl.com/api/v1/datasets/LBMA/SILVER.json"
         Filename = "SILVER.json"}
    let Gold =
        {Url = "https://www.quandl.com/api/v1/datasets/LBMA/GOLD.json"
         Filename = "GOLD.json"}
