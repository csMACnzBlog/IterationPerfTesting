# Iteration Perf Testing #

Running some Perf iterations comparing 2d spacial objects for collision.

```
> dotnet run -c Release
         1 Iterations: EntityClass: Duration (ms): 0; Total Collisions: 0;   ~~~  EntityStruct: Duration (ms): 0; Total Collisions: 0;   ~~~  Dumb Spacial Hash: Duration (ms): 8; Total Collisions: 0; 
         2 Iterations: EntityClass: Duration (ms): 0; Total Collisions: 0;   ~~~  EntityStruct: Duration (ms): 0; Total Collisions: 0;   ~~~  Dumb Spacial Hash: Duration (ms): 0; Total Collisions: 0;
        10 Iterations: EntityClass: Duration (ms): 0; Total Collisions: 0;   ~~~  EntityStruct: Duration (ms): 0; Total Collisions: 0;   ~~~  Dumb Spacial Hash: Duration (ms): 0; Total Collisions: 0;
       100 Iterations: EntityClass: Duration (ms): 0; Total Collisions: 0;   ~~~  EntityStruct: Duration (ms): 0; Total Collisions: 0;   ~~~  Dumb Spacial Hash: Duration (ms): 0; Total Collisions: 0;
     1,000 Iterations: EntityClass: Duration (ms): 1; Total Collisions: 0;   ~~~  EntityStruct: Duration (ms): 1; Total Collisions: 0;   ~~~  Dumb Spacial Hash: Duration (ms): 0; Total Collisions: 0; 
    10,000 Iterations: EntityClass: Duration (ms): 154; Total Collisions: 0;   ~~~  EntityStruct: Duration (ms): 138; Total Collisions: 0;   ~~~  Dumb Spacial Hash: Duration (ms): 1; Total Collisions: 0; 
   100,000 Iterations: EntityClass: Duration (ms): 15759; Total Collisions: 66;   ~~~  EntityStruct: Duration (ms): 14278; Total Collisions: 66;   ~~~  Dumb Spacial Hash: Duration (ms): 16; Total Collisions: 66; 
 1,000,000 Iterations:   ~~~  Dumb Spacial Hash: Duration (ms): 312; Total Collisions: 6334; 
10,000,000 Iterations:   ~~~  Dumb Spacial Hash: Duration (ms): 14076; Total Collisions: 637391; 
```