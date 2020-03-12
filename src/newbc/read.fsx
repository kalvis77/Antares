#I __SOURCE_DIRECTORY__
#r @"netstandard"
#r @"..\..\..\wwa\WorldWideAstronomy\WWA.Core\bin\Debug\netcoreapp3.1\WWA.Core.dll"

open System
open System.IO 
open WorldWideAstronomy

let fpath = Path.Combine(__SOURCE_DIRECTORY__, "../../data/bc.dat")
printfn "%s" fpath

let fi = FileInfo(fpath)
let l = fi.Length
let ni = (l - 16L) / int64(9 * 4)
printfn "Coordinate file length is %d" l
printfn "Number of positions is %d" ni
let reader = new BinaryReader(File.Open(fpath, FileMode.Open, FileAccess.Read, FileShare.Read))
reader.BaseStream.Seek(0L, SeekOrigin.Begin) |> ignore
let jd1 = reader.ReadDouble()
printfn "JD is %f" jd1
let jd2 =  reader.ReadDouble()
printfn "JD is %f" jd2
let np = int(jd2 - jd1) + 1 
printfn "Number of predictions %d" np

//Execute loop between  two dates and compaare results
let e = [jd1..1.0..jd2]
printfn "Number if items %d" (e.Length)

let ind e jd = 
  match e |> List.tryFindIndex(fun x -> jd = x) with
  | Some i -> 16L + (int64 i) * 36L
  | None _ -> -1L

let i1 = ind e jd1
let i2 = ind e jd2

reader.BaseStream.Seek(i1 + 36L, SeekOrigin.Begin) |> ignore
let xb = reader.ReadInt32()
let yb = reader.ReadInt32()
let zb = reader.ReadInt32()
let vxb = reader.ReadInt32()
let vyb = reader.ReadInt32()
let vzb = reader.ReadInt32()
let sxb = reader.ReadInt32()
let syb = reader.ReadInt32()
let szb = reader.ReadInt32()



// BC file format, from barycoor.h
(* Class to provide access to barycentric coordinate file
     
         Barycentric coordinates (Earth coordinates and speed realative
         to the barycenter of the Solar system, the coordinates of the Sun
         relative to the barycenter of the Solar system) are used when
         computing apparent positions of stars and other celestial objects
     
        File format:
         - at first 2 double values which specifies Julian days of the
           begin and end of the time interval, for which the data are
           available.
         - after that integer array (9 numbers per day) of coordinates
           in units of 1e-9 astronomical units. First 3 numbers per day
           contains Earth coordinates, next 3 - Earth's speed and last 3 -
           Sun coordinates
     
         Really only first use opens the file. Next BarycentricCoordFile objects
         reuse earlier craeted object.
         Object of class BarycentricCoordFile::Error is thrown in case of an error.
*)
// read while eof, dump as integers (int32)

