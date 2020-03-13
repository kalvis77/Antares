#I __SOURCE_DIRECTORY__
#r "netstandard"
#r @"..\..\..\wwa\WorldWideAstronomy\WWA.Core\bin\Debug\netcoreapp3.0\WWA.Core.dll"

open System
open System.IO
open WorldWideAstronomy

let pvb = Array2D.zeroCreate 2 3
let pvh = Array2D.zeroCreate 2 3

let writeBC (jd1: float) (jd2: float) =
    let fpath = Path.Combine(__SOURCE_DIRECTORY__, "../../data/bcalt.dat")
    let w = new BinaryWriter(File.Open(fpath, FileMode.Create, FileAccess.Write, FileShare.Read))
    w.BaseStream.Seek(0L, SeekOrigin.Begin) |> ignore
    w.Write(jd1)
    w.Write(jd2)
    let e = [ jd1 .. 1.0 .. jd2 ]

    let wa (w: BinaryWriter) (a: int32 array) =
        a |> Array.iter (fun item -> w.Write(item))
        ()

    e
    |> List.iter (fun j ->
        WWA.wwaEpv00 (j, 0.0, pvh, pvb) |> ignore
        // let sb = Array.map2 (fun x y -> x - y) pvb.[0, *] pvh.[0, *]
        let sb = Array.map2 (-) pvb.[0, *] pvh.[0, *]

        let pbi = Array.map (fun x -> int32 (x * 1e9)) pvb.[0, *]
        let vbi = Array.map (fun x -> int32 (x * 1e9)) pvb.[1, *]
        let sbi = Array.map (fun x -> int32 (x * 1e9)) sb
        wa w pbi
        wa w vbi
        wa w sbi)
    w.Flush()
    w.Close()
    ()

// Jan 1, 2020, 2458849.500000
// Jan 1, 2030, 2462502.500000

writeBC 2458849.500000 2462502.500000
