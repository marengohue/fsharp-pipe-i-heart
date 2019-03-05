module Crawler

    open System
    open Http
    open System.Net.Http

    type Link = Link of Uri

    type Logger = Logger of (string -> string -> unit)

    type Webpage =
        | CrawledWebpage of Link list * Link
        | UncrawledWebpage of Link

    type Crawler = {
        pages: Webpage list
        logger: Logger
    }

    let makeLink url = 
        match Uri.TryCreate(url, UriKind.RelativeOrAbsolute) with 
            | true, uri -> Some (Link uri)
            | _ -> None

    let getAllLinks httpClient (Link linkUrl) =
        let allWebpageContent = linkUrl
            |> fetchUsingClient httpClient
            |> extractLinks

        match makeLink "ds" with
            | Some link -> [ link ]
            | None -> [ ]

    let crawlSinglePage httpClient webpage =
        match webpage with
            | UncrawledWebpage pageLink -> [ CrawledWebpage (pageLink |> (getAllLinks httpClient), pageLink)]
            | _ -> [ webpage ]

    let crawlWebsite (crawler: Crawler) =
        use client =  new HttpClient()
        let newPages = crawler.pages |> List.collect (crawlSinglePage client)
        { crawler with pages = newPages }