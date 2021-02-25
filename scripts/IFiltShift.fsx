#I __SOURCE_DIRECTORY__
#r @"netstandard"


// https://www.alluxa.com/optical-filter-specs/angle-of-incidence-aoi-and-polarization/
// 532*[Math]::sqrt(1-[Math]::pow([Math]::sin(1.98*[Math]::PI/180)*1.000274/1.4607,2))

open System

let l = 532.0
let nf = 1.4607
let n0 = 1.000274

let theta wl =
    let arg = (nf / n0) * Math.Sqrt(1.0 - (wl / l) ** 2.0)
    Math.Asin(arg) * 180.0 / Math.PI

let wl a =
    l * Math.Sqrt(1. - (n0 /nf * Math.Sin(a))**2.0)    