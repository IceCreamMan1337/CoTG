# Replay Parsing

> So this part will only concern 2/3 Way to parse Replay of league , this part can be really important for see and understand how work the server structure and how the gameclient process it 

> i will not talk about other format like rofl or another , this concern newest version of league , and cannot be accurate with old method , i let some source in case you want check it 

---

## League Replay File .LRF


#### Introduction : 

League Replay was a tool that permited to players to get replays for their games, these replays were shared on some websites, like LolKing / opgg etc .. 
League Replay was cut in 2 parts: 
- the first for recording, LolRecorder
- the second for replaying it, LolReplay

LolRecorder was attached to the process League of legend.exe, when the gameclient was started, the LolRecorder was started, it tried to do a connection on the "spectator view" of the game and scrapped all PKT sent to it 

LolReplay, it tried to simulate the spectator connection with an false server and then sent all PKT in the same order and same frame as recorded

> "League Of Legends.exe" "8391" "" "" "spectator localhost Blowfishkey playerID"

#### Format of LRF file 

LRF File is a compressed Replay file.
LolRecorder does something like: 
- Start the executable, scrap playerid , blowfish , Region 
- Connect to league official spectator api 
- Start capturing PKT with tools like WinPcap, this one support enet 
- Write in an buffer and compress it with a method similat to Black Desert Online 
- End of game, finish scrapping data and start writing the entire file lrf replay file

The format of the file looked like this
- An 8-byte binary header(`int32`:Format Version | `int32`:JSON chunk length)
- A JSON chunk containing match metadata (e.g., `matchID`, date, players)
- the compressed binary chunk containing captured game action

Technically, you can just open an LRF file with a basic text editor and see the json chunk to find relevant information.
example of the json part : 

```
{"accountID":XXXXXXXX,
"assists":0,
"barracks":0,
"champion":"Tryndamere",
"damageDealt":79596,
"damageTaken":8012,
"deaths":1,
"elo":1554,
"eloChange":13,
"gold":7369,
"healed":1258,
"item1":3154,
"item2":2003,
"item3":3006,
"item4":3093,
"item5":3031,
"item6":0,
"killingSpree":4,
...
"wins":60,
"won":true}
```

#### How to parse an LRF file  

There are many software created by the community for parsing LRF files, 
- LoL-Replay-Parsing-Server
- ENetUnpack
- ReplayInspector
- PcapDecrypt 
- ETC ... 

All these one use approx same method for parse replay , i will put some example of a different project 
```cs
		// Basic header
		var _unused = reader.ReadByte();
		var _version = reader.ReadByte();
		var _compressed = reader.ReadByte();
		var _reserved = reader.ReadByte();

		// JSON data
		var _jsonLength = reader.ReadInt32();
		var _json = reader.ReadExactBytes(_jsonLength);
		var _replay = JsonConvert.DeserializeObject<Replay>(Encoding.UTF8.GetString(_json));

		// Binary data offset start position
		var _offsetStart = stream.Position;

		// Stream data
		var _stream = _replay.DataIndex.First(kvp => kvp.Key == "stream").Value;
		var _data = reader.ReadExactBytes(_stream.Size);

		if((_data[0] & 0x4C) != 0)
		{
			_data = BDODecompress.Decompress(_data);
		}
 ```

After the json, there will be all compressed binary blocks.
It will be something like this:
 
```
4F 2E C4 1F = _offsetStart

00 19 46 2A 00 00 02 C0 A6 00 40 34 45 2C 00 00 00 29 10 80 00 1C 63 83 FF 00 

00 = Not Compressed 

01 00 09 03 E4 86 03 4C 07 46 

01 = Compressed , so need apply the "black desert online decompression "
```
If not compressed, we can do just a `memcpy`.
If compressed, we have a block that contains all information.

```
            int compressedSize = (flags & 0x2) != 0 ? BitConverter.ToInt32(input, 1) : input[1];
            int decompressedSize = (flags & 0x2) != 0 ? BitConverter.ToInt32(input, 5) : input[2];
            int inputIndex = (flags & 0x2) != 0 ? 9 : 3;
            int outputIndex = 0;
```
This compression method allow to save a lot of space, when you "extract/uncompress" a replay lrf, for a little file like 2.5mb, it can grow to approximatelly the original x4/x5 size (and possibly more)

#### How Replay was used for the OpenSource project LeagueSandBox 
In league, most PKTs have the same structure, but there are few special cases, we will talk about those later.
As an example:
```

    03 18 00 00 00 C2 F8 70 00 40 00 00 02 00 00 00 00 00 00 01 00 F8 70 00 
    
    40 62 0C 2A 
    
    
    00 48 58 FC 43

``` 
The PKTs will respect a format : 

Enet ChannelID = 1 Byte
PacketSize = 4 Bytes (`UInt32`)
PacketID = 1 Byte
SenderID = 4 Bytes (`Uint32`)
PacketDefinition = this will be in function of the packetID and packetsize 
TimeStamp 

So the previous PKT can be broken down as follows
```
03 = channelID

18 00 00 00 size ( size = 24)

C2 PacketID  ( sizecount +1 )

F8 70 00 40 SenderID   ( sizecount +4 )

all packet definition with  ( sizecount +19  )
00 00
02 
00 00 00 00 
00 00 
01 00 
F8 70 00 40 
62 0C 
2A 00

48 58 FC 43 Timestamp ( when the packet has been sended to gameclient gametime )
```

These replay files are "extracted/parsed" to a json.
on this json, we can find each PKT parsed with the help of their size, timestamp and channel
this block contains their own base64 for the packetdefinition, this allows to save space again 

```

  {
    "Time": 575704.0,  = approx 9minute 59 seconds 
    "Bytes": "VBYAAEA=", 
    "Channel": 3,
  },
  ```

LeagueSandBox team used a tool "packetdumper" to find each PacketId.
If the PacketDefinition of the packet was correct the packet could get parsed into readable data, allowing the team to examine some comportement in detail.
If the packet was not correctly parsed, some futher research was needed.

## Official Replay/Spectator File ( .rep)

#### Introduction : 
League used an specific format for the replay, for the majority of time, it was just a server opened to sending PKTs of the spectator mode.
But the client had a specific method for reading replays until s1.
If GameClient was started with a .rep file in it's start arguments, it open and read it like the spectator mode without the need of a server. 
in his log is called "Replay mode" 

```
000001.668|  196320.0000kb|    272.0000kb added| ALWAYS| C:\replay.rep
000001.680|  196332.0000kb|     12.0000kb added| ALWAYS| Started System Init
000001.680|  196332.0000kb|      0.0000kb added| ALWAYS| Replay mode
000001.696|  196512.0000kb|    180.0000kb added| ALWAYS| StartSession called
```

#### Format of REP file  
This doesn't have a specific format, the file just contains all raw PKTs

```
00 18 00 00 00 00 00 00 00 00 00 00 00 38 06 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
```
So we can parse it like the same method as json, but directly in raw.
```
00 = channelID

18 00 00 00 size ( size = 24)

00 PacketID  ( sizecount +1 )

00 00 00 00 SenderID   ( sizecount +4 )

all packet definition with  ( sizecount +19  )
00 00 00 38 06 00 00 00 00 00 00 00 00 00 00 00 00 00 00 


00 00 00 00 Timestamp ( when the packet has been sended to gameclient gametime )
```

So it's easy to write or modify a Replay file for the GameClient "manually" to see how the client react to some PKTs

### Source/Sauce

https://gaming.stackexchange.com/questions/177712/how-does-league-replays-work

https://miscellaneousstuff.github.io/project/2021/09/03/tlol-part-3-initial-ideas.html#league-of-legends-replays

https://github.com/VeranusXia/LOLReplay

https://github.com/ericp1337/LoL-Replay-Parsing-Server
