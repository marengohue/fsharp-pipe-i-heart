open Crawler
open Webpage
open Logger
open Link

let logFn = Logger (printfn "[GET] %s: %s")

let makeCrawler (loggerFn: Logger) startUrl =
    match (makeLink startUrl) with
        | Some link -> Some { pages=[UncrawledWebpage link]; logger=loggerFn }
        | _ -> None


let doCrawl startUrl =
    match (makeCrawler logFn startUrl) with
        | Some crawler ->
            crawlWebsite crawler |> ignore
            0
        | _ ->
            failwith "Unable to build crawler out of input start uri."

[<EntryPoint>]
let main argv =
    match argv with 
        | [| startUrl |]
        | [| startUrl; _ |] ->
            doCrawl startUrl
        | _ -> failwith "Please provide a startUrl argument."