module Crawler
    open Webpage
    open Logger
    open Link
    open FSharp.Data

    type Crawler = {
        pages: Webpage list
        logger: Logger
    }

    let getLinkFromAnchor (anchor: HtmlNode) =
        anchor.TryGetAttribute("href")
            |> Option.map (fun a -> a.Value())
            |> Option.map makeLink
            |> Option.map Option.get

    let getAllLinks linkFilter (Link linkUrl) =
        HtmlDocument.Load(linkUrl.ToString()).Descendants("a")
            |> Seq.choose getLinkFromAnchor
            |> Seq.filter linkFilter
            |> Seq.toList

    let getAllCatalogLinks = getAllLinks (fun link ->
        let (Link linkUrl) = link in
            linkUrl.Host = "catalog.onliner.by"
    )

    let crawlSinglePage webpage =
        match webpage with
            | UncrawledWebpage pageLink -> [ CrawledWebpage (pageLink |> getAllLinks (fun x -> true), pageLink)]
            | _ -> [ webpage ]

    let crawlWebsite (crawler: Crawler) =
        let newPages = crawler.pages |> List.collect crawlSinglePage
        { crawler with pages = newPages }
