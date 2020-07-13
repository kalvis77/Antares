#I __SOURCE_DIRECTORY__
#r @"netstandard"


// External calibration target
//
// assumptions
//
// windows line ending
// the rest of file is not used in RTS2006
// first term is distance squared, but script accepts input in ns
// the rest is just zeros

open System
open System.IO
open System.Text

// TComtisHeader = packed record
//     StrHead: array [0..47] of char;
//     TC0: double;
//     CRR: array[0..6] of double;
//   end;

[<Literal>]
let CEOL = "\r\n"
let  c = 299792458.0
// todo: add path here
let fileName = Path.Combine("C:/work","calibr.inp")
let cospar = "7603901" + CEOL
let stationId = "01884" + CEOL

let sqr (x: float) = x * x

let args : string array = fsi.CommandLineArgs |> Array.tail
let d, d2 = match args.Length with
            | 1  -> 
                let dist = (args.[0] |> float ) * c * 1e-9 / 2.0
                dist, sqr dist
            | _ ->
                let dist = 100.0 * c * 1e-9 / 2.0
                dist, sqr dist
printfn "Equivalent target dist = %f  m" d            
let cf = Array.zeroCreate<float> 6 
let epoch = DateTime.Now.ToString("yyyy'-'MM'-'dd") + CEOL
printfn "Today's date %s" epoch

let ep1 = DateTime.Now.AddMinutes(0.0) //timedelta(minutes=0)
let ep2 = ep1.AddMinutes(15.0)
let t1  = ep1.ToString("HH':'mm':'ss") + CEOL 
let t2  = ep2.ToString("HH':'mm':'ss") + CEOL 
let tc0 = ep1.TimeOfDay.TotalSeconds |> floor
printfn "Elapsed seconds in day %A" tc0 
// todo: check buffer reading and output
let bw = new BinaryWriter(File.Open(fileName, FileMode.Create))
let data = Encoding.ASCII.GetBytes(cospar + stationId + epoch + t1 + t2)
bw.Write(data)
bw.Write(tc0)
bw.Write(d2)
let buffer = Array.zeroCreate<byte> 48
Buffer.BlockCopy(cf, 0, buffer, 0, 48) 
bw.Write(buffer)
bw.Close() 
