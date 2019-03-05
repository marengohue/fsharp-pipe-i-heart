module Http

    open System.Net.Http

    let fetchUsingClient (client:HttpClient) (url: string) =
        client.GetStringAsync url
            |> Async.AwaitTask
            |> Async.RunSynchronously

