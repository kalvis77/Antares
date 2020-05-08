#I __SOURCE_DIRECTORY__
#r @"netstandard"
open System.Net
open System.Net.NetworkInformation
open System.Text

// Simple ping implementation example
let p = new Ping()
let opt = PingOptions()
opt.DontFragment <- true


let str = new string('a', 30)
let buffer = Encoding.ASCII.GetBytes(str)
let timeout = 120
let reply = p.Send ("www.lu.lv", timeout, buffer,  opt)
match  reply.Status with
| IPStatus.Success ->
    printfn "Address: %s" (reply.Address.ToString())
    printfn "RoundTrip time: %A" reply.RoundtripTime
    printfn "Time to live: %A" reply.Options.Ttl
    printfn "Don't fragment: %A" reply.Options.DontFragment
    printfn "Buffer size: %A" reply.Buffer.Length
| _ -> printfn "Error" |> ignore