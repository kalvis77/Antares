#I __SOURCE_DIRECTORY__
#r @"netstandard"

open System
open System.IO

printfn "Reading Riga SLR input with F#!"
// Assemble input file path from project source structure
let inputFile = @"../../data/RTS2006/o20180107_1508_tandemx.txt"
let inpFile f = seq { use fileReader = new StreamReader(File.OpenRead(f))
        while not fileReader.EndOfStream do
            yield fileReader.ReadLine() }
//Simple version
//Seq.iter( printfn "%s") (inpFile inputFile)

//Get individual elements from sequence
let data = inpFile inputFile

printfn "%A" (Seq.head data)
printfn "%A" (Seq.head data)
printfn "%s" (String.replicate 10 "-")

//Explicit lambda
//Seq.iter(fun(a:string) -> printfn "%A" (a.Split([|' '|], StringSplitOptions.RemoveEmptyEntries))) (inpFile inputFile)
Seq.iter(fun(a:string) -> printfn "%A" (a.Split([|' '|], StringSplitOptions.RemoveEmptyEntries))) (data)

//Simple split example
let s = @"one two 3"
let a = s.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
printfn "%A" a
