# -*- coding: utf-8 -*-
"""
Created on Thu Dec  3 15:17:38 2015

@author: kalvi_000
"""

#External calibration target

"""
assumptions

windows line ending
the rest of file is not used in RTS2006
first term is delay
the rest is just zeros
"""

import sys
import datetime
import struct

"""
TComtisHeader = packed record
    StrHead: array [0..47] of char;
    TC0: double;
    CRR: array[0..6] of double;
  end;
"""


fmt = '48sd7d'
CEOL = '\r\n'
c = 299792458
fileName='Calibr.inp'

cospar='7603901'+CEOL
stationId = '01884'+CEOL
epoch = datetime.datetime.now().strftime("%Y-%m-%d") + CEOL
t1= datetime.datetime.now()+datetime.timedelta(minutes=0)
t2= datetime.datetime.now()+datetime.timedelta(minutes=15)
print(t1)
print(t2)
today = datetime.date.today()
print(today)
tc0 = (t1-datetime.datetime(year=today.year, month=today.month, day=today.day)).total_seconds()
print(tc0)
if len(sys.argv) == 1:
   delay = c*100*1e-15
else:
   delay = float(sys.argv[1])*c*1e-15     

print(delay)
data = struct.pack(fmt, (cospar+stationId+epoch+t1.strftime("%H:%M:%S")+CEOL+t2.strftime("%H:%M:%S")+CEOL).encode('ascii'),tc0, delay, 0,0,0,0,0,0)
with open(fileName, 'bw+') as f:
    f.write(data)
#    f.write(cospar.encode('ascii'))
#    f.write(stationId.encode('ascii'))
#    f.write(epoch.encode('ascii'))
#    f.write((t1.strftime("%H:%M:%S")+CEOL).encode('ascii'))
#    f.write((t2.strftime("%H:%M:%S")+CEOL).encode('ascii'))
#    f.write(tc0)
    f.close()   
    