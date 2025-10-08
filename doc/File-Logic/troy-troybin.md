Each Particle has their own troy/troybin file 

Troy files are, in essence, the same as `ini` files, and they're read the same exact way.
But contrary at the `ini`, `troy` are more difficult to understand

We will just explain what's simple

```
[ring]
p-bindtoemitter=1
```
This means the particle needs be bound to an emiter, when you see a particle in lua, you can check if it needs be bound to a Target 

```
p-mesh=icicleRing.sco
p-meshtex=icicleRing.tga
```
Here you can find the mesh+ this is the texture that will be attached to the particle 

```
p-texture=IceExplode.tga
```
The texture of the particle

```
p-life=10
```
Sometimes particle have an lifetime, this allows to client to automatically delete them in cases where the particle isn't destroyed by the server 


