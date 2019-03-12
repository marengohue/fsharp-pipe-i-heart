namespace Crawl

module Logger =
    type Logger = Logger of (string -> string -> unit)
