Here we talk about "modding map" in league of legend 

Tools necessary : 

- Wooxy 
- A 3D editor like blender 
- lol-Ngrideditor (Converter bmp to aimesh_ngrid)
- An editor for DDS/ BMP like Paint.NET 
- A wgeo from 4.20 or more (example new summonerrift)
- WGEOtoNVR , is an converter wgeo to nvr  

# WGEO/NVR Creation

There isn't a proper editor for `NVR`, pretty sure with a plugin in maya or blender it could save time, but for the moment we are most in proof of concept 

Take an 3d model compatible with `.obj` format (make sure the texture is in the ".dds" format)
In Blender : If it's an entire map , make sure each object has his own dedicated texture (rock1/rock2 etc ... ) 
Then select all models from the map and convert the faces to triangle(league doesn't support square faces)
After to export to `obj`, I recommend exporting each 3dmodel in obj with different name and their texture associated (simpler for re-import them in league format)

When you have your entire map or 3d model exported correctly.
You can use wooxy (be aware wooxy is detected has virus by windows defender, but it is fine). We will share an decompilled codesource of wooxy in case you want build it manually.\

In wooxy , you can open the Creation Studio / Map Editor,
and then open an wgeo to edit, its folder of texture, and then this will start the minimalist editor.
In the editor, you can delete the old model, and then import the `obj`.
Wooxy will take the same position than in blender (so you can edit directly the position/size in blender) and do small adjustement in wooxy.\
When the map is correctly created you can export/save the `wgeo`

Now for S1 , you need convert the map to the NVR format, as back in season 1, `wgeo` wasn't supported
for this one use the WGEOtoNVR converter 

you need an room-reference.nvr , your wgeo and it will create an room.nvr with your map 

# Aimesh_ngrid Creation

This one will allow you to do the grid of the map 

So you can take existing an bmp visionPathing.bmp (or export one from the aimesh_ngrid)
Edit them in an editor like paint.net (you can take example of summoner rift aimesh)

White = Nothing\
Grey = Wall\
Green = Bush\ 

The height can be edited with the help of this tool too, just need take in account border of each pixel rather than the pixel itself 

(some other flag has appeared post season 3 like blue = passthrought the wall  etc ..)

After you just need execute LolNgridConverter-BMP

# Lua editing

Our Server use a custom lua LevelScript (an hybrid lua between s4 / s1 ), due to some issues the converter has on converting the Season 1 script.

But , in case you want does revert map / custom map 

You need create a Lua for LevelScript Server (Content/Gameclient/LEVELS/MapX/Scripts/LevelScript.lua)
and create an mapscript adapted to your version ( for example mapscript lua from s1 are different than s4 )

In them you can control what happens in the map. Turret invulnerability, monsters, etc...

# sco/scb

we recommend using the original SCO file of the thing you want edit.

Name= Turret_T1_C_01
CentralPoint= 1251.6300 98.2300 8801.9800
Verts= 4
1211.3881 98.2300 8761.7383
1291.8719 98.2300 8761.7383
1211.3881 98.2300 8842.2218
1291.8719 98.2300 8842.2218

We also recommend to use a python script to do the calculation between the original position and the new position and add the difference on all vertice 
It is necessary for the game accept your turret position 