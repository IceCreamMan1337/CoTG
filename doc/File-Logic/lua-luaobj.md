Luaobj / Lua , is all script for spell/mapscript used by Riot 

`Luaobj` files can be "decompiled" them with tools like unluac and, like `ini` or `troy`, `lua` can also be read by the client directly 

For Luaobj files, Riot seems to have used Luaplus 5.1 to make and run them. They essentially a "compiled" lua script 

We had multiple choices 
- Reverse Luaplus 5.1 in C# for get luaobj directly 
- Read Lua with MoonSharp 
- Convert Script lua to C# script for performance and debugging

We went with last two options
- Used MoonSharp for running LevelScripts 
- Converted Scripts to C# for performance and debugging