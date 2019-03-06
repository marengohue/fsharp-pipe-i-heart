module Webpage
    open Link

    type Webpage =
        | CrawledWebpage of Link list * Link
        | UncrawledWebpage of Link
