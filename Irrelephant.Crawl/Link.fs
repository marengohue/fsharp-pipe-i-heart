namespace Crawl

module Link =
    open System

    type Link = Link of Uri

    let makeLink url = 
        match Uri.TryCreate(url, UriKind.Absolute) with 
            | true, uri -> Some (Link uri)
            | _ -> None