#I __SOURCE_DIRECTORY__
#I @"../.paket/load/netcoreapp3.1"
#r @"netstandard"

#load "XPlot.Plotly.fsx"
#load "MathNet.Numerics.FSharp.fsx"

open XPlot.Plotly
open MathNet.Numerics.Distributions

let data = Normal.Samples(0.0, 1.5) |> Seq.take 100 |> Seq.toArray

Histogram(x = data)
|> Chart.Plot
|> Chart.Show

Histogram(y = data)
|> Chart.Plot
|> Chart.Show