# State of the server

It is in a playable state, but not perfect, we will list some issues we found here, free for you to fix it: 

Champion issues will be listed in their own file (`Champion_list.md`)

- The first summoners spell can be casted if the champion is silenced/stun.
- Lua MapScript can sometimes bug, and Inhibitor or Nexus can become untargetable for clients but not for minion.
- Dominion central buff can be capture cross the map.
- Boots stack.
- Reconnexion works, but you lost all abilities, you need re-level up.
- Alistar can push entities outside the map.
- Graybuff on Old TT would give negative Move Speed, so was deactivated.
- Server can sometimes silently crash at the start of a game, this one has been really hard to find and debug.
- The pathfinding can cause lag, if too many minion can't find a good path.
- General Sync Issues
