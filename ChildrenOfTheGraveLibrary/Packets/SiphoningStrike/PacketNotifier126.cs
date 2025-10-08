#nullable enable

using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.NetInfo;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using LeaguePackets.Common;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.Navigation;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Inventory;
using LENet;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using static ChildrenOfTheGraveEnumNetwork.Content.HashFunctions;
using ChangeSlotSpellDataType = ChildrenOfTheGraveEnumNetwork.Enums.ChangeSlotSpellDataType;
using Channel = ChildrenOfTheGraveEnumNetwork.Packets.Enums.Channel;
using Color = ChildrenOfTheGraveEnumNetwork.Content.Color;
using DeathData = ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.DeathData;

//siphoning strike
using SiphoningStrike.Game.Common;
using SiphoningStrike.Game.Events;
using SiphoningStrike.Game;
using Talent = SiphoningStrike.Game.Common.Talent;
using BasePacket = MirrorImage.BasePacket;
using ChatPacket = SiphoningStrike.ChatPacket;
using KeyCheckPacket = SiphoningStrike.KeyCheckPacket;
//




namespace PacketDefinitions126
{
    /// <summary>
    /// Class containing all function related packets (except handshake) which are sent by the server to game clients.
    /// </summary>
    public class PacketNotifier126
    {
        private readonly PacketHandlerManager126 _packetHandlerManager;
        private readonly NavigationGrid _navGrid;
        private Dictionary<int, List<MovementDataNormal>> _heldMovementData = new();
        private Dictionary<int, List<ReplicationData>> _heldReplicationData = new();
        public Dictionary<int, Dictionary<int, List<ReplicationData>>> _replicationAcknowledgments = new();
        /// <summary>
        /// Instantiation which preps PacketNotifier for packet sending.
        /// </summary>
        /// <param name="packetHandlerManager"></param>
        /// <param name="navGrid"></param>
        public PacketNotifier126(PacketHandlerManager126 packetHandlerManager, NavigationGrid navGrid)
        {
            _packetHandlerManager = packetHandlerManager;
            _navGrid = navGrid;
        }

        private static void Log(object packet)
        {
            if (Game.Config.EnableLogPKT)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(packet, Newtonsoft.Json.Formatting.Indented));
            }
        }

        public void NotifyS2C_SetFadeOut_Push(GameObject o, float value, float time, int userId)
        {
            time /= 1000f;

            var packet = new S2C_SetFadeOut_Push()
            {
                SenderNetID = o.NetId,
                FadeTime = time,
                FadeTargetValue = value
            };
            _packetHandlerManager.BroadcastPacketVision(o, packet, Channel.CHL_S2C);
        }

        public void NotifyS2C_SetFadeOut_Pop(GameObject o, int buffid, float time, int userId)
        {
            time /= 1000f;

            var packet = new S2C_SetFadeOut_Pop()
            {
                SenderNetID = o.NetId,
                StackID = (short)buffid, //???

            };
            _packetHandlerManager.BroadcastPacketVision(o, packet, Channel.CHL_S2C);
        }

        public void NotifyAddRegion(Region region, int userId, TeamId team)
        {
            BasePacket pkt;
            if (region.CollisionUnit is not null)
            {
                pkt = new S2C_AddUnitPerceptionBubble()
                {
                    BubbleID = region.NetId, //Check
                    ClientNetID = (uint)region.OwnerClientID,
                    Flags = 0, //TODO: CHECK
                    Radius = region.VisionRadius,
                    PerceptionBubbleType = region.Type,
                    TimeToLive = region.Lifetime,
                    UnitNetID = region.CollisionUnit.NetId,
                    SenderNetID = region.NetId //Check
                };
            }
            else
            {
                pkt = new S2C_AddPosPerceptionBubble()
                {
                    BubbleID = region.NetId,
                    ClientNetID = (uint)region.OwnerClientID,
                    Flags = 0, //TODO: CHECK
                    Radius = region.VisionRadius,
                    PerceptionBubbleType = region.Type,
                    TimeToLive = region.Lifetime,
                    Position = region.Position3D,
                    SenderNetID = region.NetId //Check
                };
            }

            _packetHandlerManager.SendPacket(userId, pkt, Channel.CHL_S2C);
        }


        private void SendSpawnPacket(int userId, AttackableUnit u, BasePacket packet, bool wrapIntoVision)
        {
            Debug.Assert(userId >= 0);
            if (wrapIntoVision)
            {
                NotifyEnterTeamVision(u, userId, packet);

            }
            else
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
        }

        public void NotifyCreateNeutral(NeutralMinion monster, float time, int userId, TeamId team, bool doVision)
        {
            var packet = new S2C_CreateNeutral
            {
                SenderNetID = 0,
                UniqueName = monster.Name,
                Name = monster.Name,
                SkinName = monster.Model,
                FaceDirectionPosition = monster.Direction,
                DamageBonus = monster.DamageBonus,
                HealthBonus = monster.HealthBonus,
                GroupPosition = monster.Camp.CampPosition,
                Position = monster.GetPosition3D(),
                //Seems to be the time it is supposed to spawn, not the time when it spawned, check this later
                BehaviorTree = monster.AIScript.AIScriptMetaData.BehaviorTree == 1,
                GroupNumber = monster.Camp.CampIndex,
                RoamState = (int)monster.RoamState,
                TeamID = (uint)monster.Team,
                MinimapIcon = monster.Camp.CampIcon,
                UnitNetID = monster.NetId,
                UnitNetNodeID = (byte)NetNodeID.Spawned
            };
            //problem with neutral minion , seem desync from the client 

            // Console.WriteLine("AIScript = " + monster.AIScript);
            SendSpawnPacket(userId, monster, packet, true); //true

            //todo : check when create spawn camp need create EnterVisibilityClient for it 
            // ConstructEnterVisibilityClientPacket(monster);
            // _packetHandlerManager.BroadcastPacketVision(monster, packet, Channel.CHL_S2C);
        }

        public void NotifyCreateTurret(LaneTurret turret, int userId, bool doVision)
        {
            if (Game.Config.VersionOfClient == "1.0.0.126" || Game.Config.VersionOfClient == "1.0.0.131")
            {
                var packet = new S2C_CreateTurret
                {
                    SenderNetID = turret.NetId,
                    UniteNetID = turret.NetId,
                    UnitNetNodeID = (byte)NetNodeID.Spawned,
                    Name = turret.Name
                };
                SendSpawnPacket(userId, turret, packet, true);
            }
            else if (Game.Config.VersionOfClient == "1.0.0.132")
            {
                var packet = new TechmaturgicalRepairBot.Game.S2C_CreateTurret
                {
                    SenderNetID = turret.NetId,
                    UniteNetID = turret.NetId,
                    UnitNetNodeID = (byte)NetNodeID.Spawned,
                    Name = turret.Name,
                    IsTargetable = true,
                    IsTargetableToTeamSpellFlags = (uint)StatusFlags.Targetable
                };

                SendSpawnPacket(userId, turret, packet, true);
            }
            else
            {
                var packet = new S2C_CreateTurret
                {
                    SenderNetID = turret.NetId,
                    UniteNetID = turret.NetId,
                    UnitNetNodeID = (byte)NetNodeID.Spawned,
                    Name = turret.Name,

                };
                SendSpawnPacket(userId, turret, packet, true);
            }

        }

        // TODO: implement option for multiple particles and groups of particles instead of hardcoding one
        public void NotifyFXCreateGroup(Particle particle, string particleName, int userId = -1)
        {
            var startPos = particle.BindObject?.Position ?? particle.StartPosition;
            var endPos = particle.TargetObject?.Position ?? particle.EndPosition;
            var ownerPos = particle.Caster?.Position ?? startPos;
            var newname = particle.Name + "bin";
            var fxData1 = new FXCreateGroupItem
            {
                //.126 offset
                TargetNetID = particle.TargetObject?.NetId ?? 0,

                //this is not correct just test flash
                //TargetNetID = particle.BindObject?.NetId ?? 0,

                NetAssignedNetID = particle.NetId,

                BindNetID = particle.BindObject?.NetId ?? 0,

                PositionX = (short)((startPos.X - _navGrid.OffsetX) / 2),
                PositionY = _navGrid.GetHeightAtLocation(startPos),
                PositionZ = (short)((startPos.Y - _navGrid.OffsetZ) / 2),

                TargetPositionX = (short)((endPos.X - _navGrid.OffsetX) / 2),
                TargetPositionY = _navGrid.GetHeightAtLocation(endPos),
                TargetPositionZ = (short)((endPos.Y - _navGrid.OffsetZ) / 2),

                OwnerPositionX = (short)((ownerPos.X - _navGrid.OffsetX) / 2),
                OwnerPositionY = _navGrid.GetHeightAtLocation(ownerPos),
                OwnerPositionZ = (short)((ownerPos.Y - _navGrid.OffsetZ) / 2),





                //lsb method 

                /*NetAssignedNetID = particle.NetId,
                 BindNetID = particle.BindObject?.NetId ?? 0,

                 PositionX = (short)((startPos.X - _navGrid.MapWidth / 2) / 2),
                 PositionY = _navGrid.GetHeightAtLocation(startPos),
                 PositionZ = (short)((startPos.Y - _navGrid.MapHeight / 2) / 2),

                 TargetPositionX = (short)((endPos.X - _navGrid.MapWidth / 2) / 2),
                 TargetPositionY = _navGrid.GetHeightAtLocation(endPos),
                 TargetPositionZ = (short)((endPos.Y - _navGrid.MapHeight / 2) / 2),

                 OwnerPositionX = (short)((ownerPos.X - _navGrid.MapWidth / 2) / 2),
                 OwnerPositionY = _navGrid.GetHeightAtLocation(ownerPos),
                 OwnerPositionZ = (short)((ownerPos.Y - _navGrid.MapHeight / 2) / 2),
 */
                OrientationVector = particle.Direction,//Vector3.Zero,
                TimeSpent = particle.TimeAlive + 0.01f
            };
            if (Game.Config.EnableLogPKT)
            {
                Console.WriteLine(particle.Name + "++++++" + particleName);

            }
            var PackageHash = (particle.Caster as AttackableUnit)?.GetObjHash() ?? 0;
            S2C_FX_Create_Group fxPacket = new()
            {
                Entries = new List<FXCreateGroupEntry>(1)
                {
                    new() {
                       // PackageHash = (particle.Caster as AttackableUnit)?.GetObjHash() ?? 0,
                        // EffectNameHash = particle.PackageHash,
                       // EffectNameHash = (uint)HashStringSDBMInc(ref PackageHash,particle.Name),
                       // EffectNameHash = (uint)HashString(particle.Name),//.Substring(0, particle.Name.Length - 5)), //particleName),
                        EffectNameHash = HashString10(particle.Name.Substring(0, particle.Name.Length - 5)), //particleName),
                        //EffectNameHash = (uint)HashString(newname),
                        Flags = (ushort)particle.Flags,
                        //TargetBoneNameHash = (uint)HashString(particle.TargetBoneName),
                        //BoneNameHash = (uint)HashString(particle.BoneName),
                        TargetBoneNameHash = HashString(particle.TargetBoneName),
                        BoneNameHash = HashString(particle.BoneName),
                        FXCreateData = new List<FXCreateGroupItem>(1)//1)
                        {
                            fxData1
                        }
                    }
                }
            };
            // _packetHandlerManager.BroadcastPacketVision(particle, fxPacket, Channel.CHL_S2C);
            //uncomment this if you want an crash 


            //todo : 
            /*   if (particle.BindObject != null)
               {
                   if(particle.BindObject is AttackableUnit)
                   {
                       if ((particle.BindObject as AttackableUnit).Status.HasFlag(StatusFlags.ForceRenderParticles)) {
                           _packetHandlerManager.BroadcastPacket(fxPacket, Channel.CHL_S2C);
                           Console.WriteLine("ntmntmntm");
                       }
                   }
               }*/

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, fxPacket, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.BroadcastPacketVision(particle, fxPacket, Channel.CHL_S2C);
            }


        }

        public void NotifyLaneMinionSpawned(LaneMinion m, int userId, bool doVision)
        {
            var packet = new S2C_Barrack_SpawnUnit
            {
                SenderNetID = m.BarrackSpawn.NetId,
                UnitNetID = m.NetId,
                UnitNetNodeID = 0x40,
                WaveCount = (byte)m.BarrackSpawn.WaveCount,
                MinionType = (byte)m.MinionSpawnType,
                DamageBonus = (short)m.DamageBonus,
                HealthBonus = (short)m.HealthBonus
            };
            // _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            SendSpawnPacket(userId, m, packet, true);

        }

        public void NotifyMarkOrSweepForSoftReconnect(int userId, bool sweep)
        {
            /*
            var packet = new S2C_MarkOrSweepForSoftReconnect();
            packet.Unknown1 = sweep;
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            */
        }

        public void NotifyMinionSpawned(Minion minion, int userId, TeamId team, bool doVision)
        {


            if (minion.ispet)
            {
                var packet = new S2C_CHAR_SpawnPet
                {
                    UnitNetID = minion.NetId, //netNodeID

                    UnitNetNodeID = (byte)NetNodeID.Spawned, //netObjID

                    Position = new Vector3(minion.Position.X, 100.0f, minion.Position.Y),

                    HealthBonus = minion.HealthBonus,
                    //TODO: The clone can be spawned from CharScript
                    CastSpellLevelPlusOne = 4,// pet.SourceSpell?.Level + 1  ?? 1,

                    CopyInventory = false,

                    DamageBonus = minion.DamageBonus,

                    SkinID = minion.SkinID,

                    Duration = minion.Buffs.GetRemainingDuration(minion.Buffs.All().FirstOrDefault().Name),

                    CloneNetID = minion.Owner?.NetId ?? 0,


                    Name = minion.Name,
                    Skin = minion.Model,

                    BuffName = minion.Buffs.All().FirstOrDefault().Name,

                    AIscript = minion.AIScriptName,


                    SenderNetID = minion.Owner?.NetId ?? 0,
                    ClearFocusTarget = false, //TODO



                    ShowMinimapIcon = false



                };

                //  _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);

                SendSpawnPacket(userId, minion, packet, doVision);

            }
            else
            {
                var spawnPacket = new S2C_SpawnMinion
                {
                    SenderNetID = 0,//minion.NetId,
                    UnitNetID = minion.NetId,
                    CloneNetID = (minion as Pet)?.ClonedUnit?.NetId ?? 0, //TODO: Proper pet implementation
                    UnitNetNodeID = (byte)NetNodeID.Spawned,
                    Position = minion.GetPosition3D(),
                    SkinID = (uint)minion.SkinID,
                    TeamID = (ushort)minion.Team,
                    IgnoreCollision = minion.Stats.ActionState.HasFlag(ActionState.IS_GHOSTED),
                    IsWard = minion.IsWard,

                    VisibilitySize = 0.0f,
                    Name = minion.Name,
                    SkinName = minion.Model,
                    UseBehaviorTreeAI = false //TODO
                };
                // _packetHandlerManager.BroadcastPacket(spawnPacket, Channel.CHL_S2C);
                SendSpawnPacket(userId, minion, spawnPacket, true);
            }


        }

        private SiphoningStrike.Game.Common.CastInfo ConvertCastInfo(ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.CastInfo ci)
        {
            if (ci.TargetPosition.X > Game.Map.NavigationGrid.TranslationMaxGridPosition.X || ci.TargetPosition.Y > Game.Map.NavigationGrid.TranslationMaxGridPosition.Y)
            {
                if (ci.Spell.TargetingType != TargetingType.DragDirection)
                {
                    ci.TargetPosition = ci.TargetPositionEnd;
                }

            }
            return new SiphoningStrike.Game.Common.CastInfo
            {
                SpellHash = HashString(ci.Spell.Name),
                SpellNetID = ci.NetId,

                SpellLevel = (byte)ci.SpellLevel > 0 ? (byte)(ci.SpellLevel - 1) : (byte)ci.SpellLevel,
                AttackSpeedModifier = ci.AttackSpeedModifier,
                CasterNetID = ci.Caster.NetId,

                MissileNetID = ci.Missile?.NetId ?? 0,
                TargetPosition = ci.TargetPosition,
                TargetPositionEnd = ci.TargetPositionEnd,

                TargetsInfo = ci.Targets.Select(
                t => new CastTargetInfo()
                {
                    UnitNetID = t.Unit?.NetId ?? 0,
                    Position = t.Unit?.Position3D ?? Vector3.One,
                    HitResult = (byte)t.HitResult
                }
            ).ToList(),


                DesignerCastTime = ci.DesignerCastTime,  //seem not correct 
                ExtraCastTime = ci.ExtraCastTime, //seem not correct 
                DesignerTotalTime = ci.DesignerTotalTime, //seem not correct 

                Cooldown = ci.Cooldown,
                StartCastTime = ci.StartCastTime,

                IsAutoAttack = ci.IsAutoAttack,
                IsSecondAutoAttack = ci.IsSecondAutoAttack,
                IsForceCastingOrChannel = ci.IsForceCastingOrChannel,
                IsOverrideCastPosition = ci.IsOverrideCastPosition,

                SpellSlot = ci.Spell.IsSummonerSpell ? (byte)(ci.Spell.Slot - 20) : (byte)ci.Spell.Slot,
                ManaCost = ci.ManaCost,


                CastLaunchPosition = ci.SpellCastLaunchPosition,
            };
        }

        public void NotifyMissileReplication(SpellMissile m, int userId = -1)
        {
            // Calculate missile direction based on trajectory, not caster facing
            var trajectoryDirection = (m.EndPoint - m.StartPoint).Normalized();

            var misPacket = new S2C_MissileReplication
            {
                SenderNetID = m.CastInfo.Caster.NetId,
                Position = m.GetPosition3D(),
                CasterPosition = m.CastInfo.Caster.GetPosition3D(),
                Direction = new Vector3(trajectoryDirection.X, 0, trajectoryDirection.Z),
                Velocity = new Vector3(0, 0, 0),
                StartPoint = m.CastInfo.Spell.Data.LockConeToPlayer ? m.CastInfo.Caster.GetPosition3D() : m.StartPoint,
                EndPoint = m.EndPoint.ToVector2().ToVector3(m.GetPosition3D().Y),
                UnitPosition = m.CastInfo.Caster.GetPosition3D(), //TODO: Verify
                Speed = m.Speed, //TODO: Send angular or radial speed for circle missiles?

                LifePercentage = (m.Lifetime > 0) ? m.TimeSinceCreation / m.Lifetime : 0,

                Bounced = (byte)((m as SpellLineMissile)?.Bounced ?? false ? 1 : 0),

                CastInfo = ConvertCastInfo(m.CastInfo)
            };
            //todo : check if is only linemissile and what case 
            if (m.Type == MissileType.Arc)
            {
                misPacket.CastInfo.SpellLevel = 0;
            }

            misPacket.Velocity = new Vector3(misPacket.EndPoint.X - misPacket.Position.X, 0, misPacket.EndPoint.Z - misPacket.Position.Z);

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, misPacket, Channel.CHL_S2C);

            }
            else
            {
                _packetHandlerManager.BroadcastPacketVision(m, misPacket, Channel.CHL_S2C);
            }
            //throw new InvalidOperationException("for trace");
        }

        public void NotifySpawnLevelProp(LevelProp levelProp, int userId, TeamId team)
        {
            var packet = new S2C_SpawnLevelProp
            {

                UniNetID = levelProp.NetId,
                UnitNetNodeID = levelProp.NetNodeID,
                Position = levelProp.Position.ToVector3(levelProp.Height),
                Facing = levelProp.Direction,
                PositionOffset = levelProp.PositionOffset,
                TeamID = 300,//(ushort)team,
                SkillLevel = levelProp.SkillLevel,
                Rank = levelProp.Rank,
                Type = (byte)(levelProp.Type - 1),
                Name = levelProp.Name,
                PropName = levelProp.Model,
                SenderNetID = 0 //Check
            };
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        public void NotifySpawnPet(Pet pet, int userId, TeamId team, bool doVision)
        {
            var packet = new S2C_CHAR_SpawnPet
            {
                UnitNetID = pet.NetId, //netNodeID

                UnitNetNodeID = (byte)NetNodeID.Spawned, //netObjID

                Position = new Vector3(pet.Position.X, 100.0f, pet.Position.Y),

                HealthBonus = pet.HealthBonus,
                //TODO: The clone can be spawned from CharScript
                CastSpellLevelPlusOne = 4,// pet.SourceSpell?.Level + 1  ?? 1,

                CopyInventory = pet.CloneInventory,

                DamageBonus = pet.DamageBonus,

                SkinID = pet.SkinID,

                Duration = pet.LifeTime,

                CloneNetID = pet.ClonedUnit?.NetId ?? 0,


                Name = pet.Name,
                Skin = pet.Model,

                BuffName = pet.CloneBuffName,

                AIscript = pet.AIScriptName,


                SenderNetID = pet.SourceSpell?.Caster?.NetId ?? 0,
                ClearFocusTarget = false, //TODO



                ShowMinimapIcon = pet.ShowMinimapIconIfClone



            };

            //   _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);

            SendSpawnPacket(userId, pet, packet, doVision);

            //hack actually this packet is totally fucked up on 126 


            // NotifyMinionSpawned(pet, userId, team, doVision);
        }





        /// <summary>
        /// Sends a packet to all players with vision of the specified attacker detailing that they have targeted the specified target.
        /// </summary>
        /// <param name="attacker">AI that is targeting an AttackableUnit.</param>
        /// <param name="target">AttackableUnit that is being targeted by the attacker.</param>
        public void NotifyAI_TargetS2C(ObjAIBase attacker, AttackableUnit? target)
        {
            var targetPacket = new S2C_AI_Target
            {
                SenderNetID = attacker.NetId,
                TargetNetID = 0
            };

            if (target != null)
            {
                targetPacket.TargetNetID = target.NetId;
            }

            // TODO: Verify if we need to account for other cases.
            if (attacker is BaseTurret)
            {
                _packetHandlerManager.BroadcastPacket(targetPacket, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.BroadcastPacketVision(attacker, targetPacket, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players that a specific unit has changed it's AIState, functionality unknown (if even needed), found in Lua AIScripts as NetSetState
        /// </summary>
        /// <param name="owner">Target unit</param>
        /// <param name="state">New AI state</param>
        public void NotifyS2C_AIState(ObjAIBase owner, AIState state)
        {
            if (owner.Team == TeamId.TEAM_NEUTRAL)
            {
                var targetPacket = new S2C_AI_State
                {
                    SenderNetID = owner.NetId,
                    AIState = (uint)state
                };


                // _packetHandlerManager.BroadcastPacket(targetPacket, Channel.CHL_S2C);
            }




        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified attacker detailing that they have targeted the specified champion.
        /// </summary>
        /// <param name="attacker">AI that is targeting a champion.</param>
        /// <param name="target">Champion that is being targeted by the attacker.</param>
        public void NotifyAI_TargetHeroS2C(ObjAIBase attacker, AttackableUnit? target)
        {
            var targetPacket = new S2C_AI_TargetHero
            {
                SenderNetID = attacker.NetId,
                TargetNetID = 0
            };

            if (target != null)
            {
                targetPacket.TargetNetID = target.NetId;
            }

            _packetHandlerManager.BroadcastPacketVision(attacker, targetPacket, Channel.CHL_S2C);

        }

        /// <summary>
        /// Sends a packet to the specified user or all users informing them of the given client's summoner data such as runes, summoner spells, masteries (or talents as named internally), etc.
        /// </summary>
        /// <param name="client">Info about the player's summoner data.</param>
        /// <param name="userId">User to send the packet to. Set to -1 to broadcast.</param>
        public void NotifyAvatarInfo(ClientInfo client, int userId = -1)
        {
            var avatar = new S2C_AvatarInfo_Server(); //Broadcast
            avatar.SenderNetID = client.Champion.NetId;
            var skills = new uint[] {
                HashString(client.SummonerSkills[0]),
                HashString(client.SummonerSkills[1])
            };

            avatar.SummonerIDs[0] = skills[0];
            avatar.SummonerIDs[1] = skills[1];
            for (int i = 1; i <= client.Champion.RuneList.Runes.Count; ++i)
            {
                int runeValue = 0;
                client.Champion.RuneList.Runes.TryGetValue(i, out runeValue);
                avatar.ItemIDs[i - 1] = (uint)runeValue;
            }

            for (int i = 0; i < client.Champion.TalentInventory.Talents.Count; i++)
            {
                var talent = client.Champion.TalentInventory.Talents.ElementAt(i).Value;
                avatar.Talents[i] = new Talent
                {
                    //todo : this have an problem , return always null 

                    Hash = HashString(talent.Id),
                    Level = talent.Rank
                };
            }

            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(avatar, Channel.CHL_S2C);
                return;
            }

            _packetHandlerManager.BroadcastPacket(avatar, Channel.CHL_S2C);

            //hack 
            var teamenemy = TeamId.TEAM_ORDER;

            if (client.Champion.Team is TeamId.TEAM_ORDER)
            {
                teamenemy = TeamId.TEAM_CHAOS;
            }


            NotifyLeaveVisibilityClient(client.Champion, teamenemy);

        }

        private BasicAttackData CreateBasicAttackData(ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            return new BasicAttackData
            {
                TargetNetID = castInfo.Target?.Unit.NetId ?? 0,

                // Based on DesignerCastTime. Always negative. Value range from replays: [-0.14, 0].
                ExtraTime = 0, //TODO: Verify, maybe related to CastInfo.ExtraCastTime?

                MissileNextID = castInfo.Missile?.NetId ?? 0,
                AttackSlot = (byte)castInfo.Spell.Slot
            };
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified  unit is starting their next auto attack.
        /// </summary>
        public void NotifyBasic_Attack(ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            var attacker = castInfo.Caster;
            var basicAttackPacket = new S2C_Basic_Attack
            {
                SenderNetID = attacker.NetId,
                BasicAttackData = CreateBasicAttackData(castInfo)
            };
            _packetHandlerManager.BroadcastPacketVision(attacker, basicAttackPacket, Channel.CHL_S2C);




        }

        /// <summary>
        /// Sends a packet to all players that the specified attacker is starting their first auto attack.
        /// </summary>
        public void NotifyBasic_Attack_Pos(ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.CastInfo castInfo)
        {
            var attacker = castInfo.Caster;
            var basicAttackPacket = new S2C_Basic_Attack_Pos
            {
                SenderNetID = attacker.NetId,
                BasicAttackData = CreateBasicAttackData(castInfo),
                Position = attacker.Position // TODO: Verify
            };
            _packetHandlerManager.BroadcastPacketVision(attacker, basicAttackPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_ForceCreateMissile(SpellMissile m)
        {
            //TODO
            /*
            var packet = new S2C_ForceCreateMissile()
            {
                SenderNetID = m.CastInfo.Caster.NetId,
                MissileNetID = m.NetId
            };
            _packetHandlerManager.BroadcastPacketVision(m, packet, Channel.CHL_S2C);
            */
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified building has died.
        /// </summary>
        /// <param name="deathData"></param>
        public void NotifyBuilding_Die(DeathData? deathData)
        {
            if (deathData is null)
            {
                return;
            }

            S2C_Building_Die buildingDie = new()
            {
                SenderNetID = deathData.Unit.NetId,
                AttackerNetID = deathData.Killer.NetId
            };

            if (deathData?.Unit is ObjAIBase obj)
            {
                AssistMarker lastAssist = obj.EnemyAssistMarkers.First();
                if (lastAssist is not null)
                {
                    buildingDie.AttackerNetID = lastAssist.Source.NetId; //Check
                }
            }

            _packetHandlerManager.BroadcastPacket(buildingDie, Channel.CHL_S2C);
        }
        /// <summary>
        /// Sends a packet to the player attempting to buy an item that their purchase was successful.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="gameObject">GameObject of type ObjAIBase that can buy items.</param>
        /// <param name="itemInstance">Item instance housing all information about the item that has been bought.</param>
        public void NotifyBuyItem(ObjAIBase gameObject, Item itemInstance)
        {
            if (itemInstance is null)
            {
                return;
            }
            var buyItemPacket = new S2C_BuyItemAns
            {
                SenderNetID = gameObject.NetId,
                ItemID = (uint)itemInstance.ItemData.Id,
                Slot = gameObject.ItemInventory.GetItemSlot(itemInstance),
                ItemsInSlot = (byte)itemInstance.StackCount,
                UseOnBought = false //TODO
            };

            _packetHandlerManager.BroadcastPacketVision(gameObject, buyItemPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the player letting them know how many times they can undo
        /// </summary>
        /// <param name="gameObject">User to send the packet to.</param>
        /// <param name="stackSize">How many undo actions they can make.</param>
        public void NotifyS2C_SetUndoEnabled(ObjAIBase gameObject, int stackSize)
        {
            //DOESNT EXIST ON .131
            /*var s2CUndoEnabled = new S2C_SetUndoEnabled
            {
                SenderNetID = (uint)gameObject.NetId,
                UndoStackSize = (byte)stackSize,
            };
            _packetHandlerManager.BroadcastPacketVision(gameObject, s2CUndoEnabled, Channel.CHL_S2C);*/
        }

        /// <summary>
        /// Sends a packet to the specified user detailing that the specified owner unit's spell in the specified slot has been changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="owner">Unit that owns the spell being changed.</param>
        /// <param name="slot">Slot of the spell being changed.</param>
        /// <param name="changeType">Type of change being made.</param>
        /// <param name="isSummonerSpell">Whether or not the spell being changed is a summoner spell.</param>
        /// <param name="targetingType">New targeting type to set.</param>
        /// <param name="newName">New internal name of a spell to set.</param>
        /// <param name="newRange">New cast range for the spell to set.</param>
        /// <param name="newMaxCastRange">New max cast range for the spell to set.</param>
        /// <param name="newDisplayRange">New max display range for the spell to set.</param>
        /// <param name="newIconIndex">New index of an icon for the spell to set.</param>
        /// <param name="offsetTargets">New target netids for the spell to set.</param>
        public void NotifyChangeSlotSpellData(int userId, ObjAIBase owner, int slot, ChangeSlotSpellDataType changeType, bool isSummonerSpell = false, TargetingType targetingType = TargetingType.Invalid, string newName = "", float newRange = 0, float newMaxCastRange = 0, float newDisplayRange = 0, int newIconIndex = 0x0, List<uint> offsetTargets = null)
        {
            switch (changeType)
            {
                case ChangeSlotSpellDataType.TargetingType:
                    {
                        if (targetingType != TargetingType.Invalid)
                        {
                            var spellData = new S2C_ChangeSlotSpellType()
                            {
                                Slot = (byte)slot,
                                IsSummonerSpell = isSummonerSpell,
                                TargetingType = (byte)targetingType,
                                SenderNetID = owner.NetId
                            };
                            _packetHandlerManager.SendPacket(userId, spellData, Channel.CHL_S2C);
                        }
                        break;
                    }
                case ChangeSlotSpellDataType.SpellName:
                    {
                        if (Game.Config.EnableLogPKT)
                        {
                            Console.WriteLine("JE SUIS LU ChangeSlotSpellDataType.SpellName:");


                        }


                        var spellData = new S2C_ChangeSlotSpellName()
                        {
                            Slot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            SpellName = newName,
                            SenderNetID = owner.NetId
                        };


                        //   this one can crash client
                        // actually when an item is used , we change name after removed them bad ideas 
                        //so we will ignore slot of item 

                        //why slot 42 ?? 
                        if (slot != 4 && slot != 5 && slot != 6 && slot != 7 && slot != 8 && slot != 9 && slot < 42)
                        {
                            _packetHandlerManager.SendPacket(userId, spellData, Channel.CHL_S2C);
                        }



                        break;
                    }

                case ChangeSlotSpellDataType.IconIndex:
                    {
                        var spellData = new S2C_ChangeSlotSpellIcon()
                        {
                            Slot = (byte)slot,
                            IsSummonerSpell = isSummonerSpell,
                            IconIndex = (byte)newIconIndex,
                            SenderNetID = owner.NetId
                        };
                        _packetHandlerManager.SendPacket(userId, spellData, Channel.CHL_S2C);
                        break;
                    }
                case ChangeSlotSpellDataType.OffsetTarget:
                    {
                        if (offsetTargets != null)
                        {
                            foreach (var target in offsetTargets)
                            {
                                var spellData = new S2C_ChangeSlotSpellOffsetTarget()
                                {
                                    Slot = (byte)slot,
                                    IsSummonerSpell = isSummonerSpell,
                                    TargetNetID = target,
                                    SenderNetID = owner.NetId
                                };
                                _packetHandlerManager.SendPacket(userId, spellData, Channel.CHL_S2C);
                            }

                        }
                        break;
                    }
            }


        }

        /// <summary>
        /// Sends a packet to all players with vision of a specified ObjAIBase explaining that their specified spell's cooldown has been set.
        /// </summary>
        /// <param name="u">ObjAIBase who owns the spell going on cooldown.</param>
        /// <param name="slotId">Slot of the spell.</param>
        /// <param name="currentCd">Amount of time the spell has already been on cooldown (if applicable).</param>
        /// <param name="totalCd">Maximum amount of time the spell's cooldown can be.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        public void NotifyCHAR_SetCooldown(ObjAIBase u, int slotId, float currentCd, float totalCd, int userId = -1, bool issummonerspell = false)
        {
            var cdPacket = new S2C_CHAR_SetCooldown
            {
                SenderNetID = u.NetId,
                Slot = (byte)slotId,
                IsSummonerSpell = false, // TODO: Unhardcode
                Cooldown = currentCd,
            };
            if (u is Champion && (slotId == 14 || slotId == 15))
            {
                cdPacket.Slot = (byte)(slotId - 14);
                cdPacket.IsSummonerSpell = true; // TODO: Verify functionality
            }
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacketVision(u, cdPacket, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, cdPacket, Channel.CHL_S2C);
            }
        }
        /*
        /// <summary>
        /// Sends a packet to the specified user that the mana or cooldown values has change
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="unit">Unit to update the cost</param>
        /// <param name="spell">Spell to update the cost</param>
        /// <param name="costType">1 To Cooldown - 2 To Mana</param>
        public void NotifyS2C_UnitSetSpellPARCost(ObjAIBase unit, Spell spell, int costType, int userId = -1)
        {
            float cooldownDiff = spell.CurrentCooldown - spell.Cooldown;
            float manaCost = spell.ManaCostBase;
            //float manaCost = spell.CastInfo.ManaCosts[Math.Min(spell.Level, spell.CastInfo.ManaCosts.Length - 1)];
            float manaDiff = (spell.ManaCost - manaCost) / manaCost;
            var packet = new S2C_UnitSetSpellPARCost()
            {
                SenderNetID = unit.NetId,
                SpellSlot = spell.Slot,
                CostType = (byte)costType,
                Amount = (costType == 1) ? cooldownDiff : manaDiff
            };

            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }
        */
        /// <summary>
        /// Sends a packet to the specified user that highlights the specified GameObject.
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="unit">GameObject to highlght.</param>
        public void NotifyCreateUnitHighlight(int userId, GameObject unit)
        {
            var highlightPacket = new S2C_CreateUnitHighlight
            {
                SenderNetID = unit.NetId,
                UnitNetID = unit.NetId
            };

            _packetHandlerManager.SendPacket(userId, highlightPacket, Channel.CHL_S2C);
        }

        public void NotifyDampenerSwitchStates(Inhibitor inhibitor)
        {
            var inhibState = new S2C_DampenerSwitch
            {
                SenderNetID = inhibitor.NetId,
                State = inhibitor.State == DampenerState.RespawningState, //CHECK FFS
                Duration = (ushort)inhibitor.RespawnTime
            };
            _packetHandlerManager.BroadcastPacket(inhibState, Channel.CHL_S2C);
        }

        public void NotifyDeath(DeathData deathData)
        {
            switch (deathData.Unit)
            {
                case Champion ch:
                    NotifyNPC_Hero_Die(deathData);
                    break;
                case Minion minion:
                    if (minion is Pet || minion is LaneMinion)
                    {
                        NotifyS2C_NPC_Die_MapView(deathData);
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case ObjBuilding building:
                    NotifyBuilding_Die(deathData);
                    break;
                default:
                    NotifyNPC_Die_Broadcast(deathData);
                    break;
            }
        }

        /// <summary>
        /// Sends a packet to the specified user which is intended for debugging.
        /// </summary>
        /// <param name="userId">ID of the user to send the packet to.</param>
        /// <param name="data">Array of bytes representing the packet's data.</param>
        public void NotifyDebugPacket(int userId, byte[] data)
        {
            _packetHandlerManager.SendPacket(userId, data, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the destruction of (usually) an auto attack missile.
        /// </summary>
        /// <param name="p">Projectile that is being destroyed.</param>
        public void NotifyDestroyClientMissile(SpellMissile p)
        {
            var misPacket = new S2C_DestroyClientMissile
            {
                SenderNetID = p.NetId
            };
            _packetHandlerManager.BroadcastPacket(misPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to either all players with vision of a target, or the specified player.
        /// The packet displays the specified message of the specified type as floating text over a target.
        /// </summary>
        /// <param name="target">Target to display on.</param>
        /// <param name="message">Message to display.</param>
        /// <param name="textType">Type of text to display. Refer to FloatTextType</param>
        /// <param name="userId">User to send to. 0 = sends to all in vision.</param>
        /// <param name="param">Optional parameters for the text. Untested, function unknown.</param>
        public void NotifyDisplayFloatingText(FloatingTextData floatTextData, TeamId team = 0, int userId = -1)
        {
            var textPacket = new S2C_DisplayFloatingText
            {
                TargetNetID = floatTextData.Target.NetId,
                FloatingTextType = (byte)floatTextData.FloatTextType,
                Param = floatTextData.Param,
                Message = floatTextData.Message,
                SenderNetID = 0 //Check
            };

            if (userId < 0)
            {
                if (team != TeamId.TEAM_UNKNOWN)
                {
                    _packetHandlerManager.BroadcastPacketTeam(team, textPacket, Channel.CHL_S2C);
                }
                else
                {
                    _packetHandlerManager.BroadcastPacketVision(floatTextData.Target, textPacket, Channel.CHL_S2C);
                }
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, textPacket, Channel.CHL_S2C);
            }
        }
        /// <summary>
        /// Sends a basic heartbeat packet to either the given player or all players.
        /// </summary>
        public void NotifyKeyCheck(int clientID, long playerId, long _EncryptedPlayerID, bool broadcast = false)
        {
            var keyCheck = new KeyCheckPacket
            {
                ClientID = (uint)clientID,
                PlayerID = (ulong)playerId,
                EncryptedPlayerID = (ulong)_EncryptedPlayerID

            };

            if (broadcast)
            {
                _packetHandlerManager.BroadcastPacket(keyCheck, Channel.CHL_HANDSHAKE);
            }
            else
            {
                _packetHandlerManager.SendPacket(clientID, keyCheck, Channel.CHL_HANDSHAKE);
            }
        }


        /// <summary>
        /// Sends a basic heartbeat packet to either the given player or all players.
        /// </summary>
        public void NotifyWorldSendGameNumber()
        {
            var WSGN = new S2C_World_SendGameNumber
            {
                GameID = 1

            };

            _packetHandlerManager.BroadcastPacket(WSGN, Channel.CHL_S2C);


        }



        S2C_OnEnterVisiblityClient ConstructEnterVisibilityClientPacket(GameObject o, List<BasePacket>? packets = null)
        {
            var itemDataList = new List<S2C_OnEnterVisiblityClient.ItemData>();
            var shields = new ShieldValues();

            var charStackDataList = new List<CharacterStackData>();
            var charStackData = new CharacterStackData
            {
                SkinID = 0,
                OverrideSpells = false,
                ModelOnly = false,
                ReplaceCharacterPackage = false,
                ID = 0
            };

            var buffCountList = new List<KeyValuePair<byte, int>>();

            if (o is AttackableUnit a)
            {
                foreach (var shield in a.Shields)
                {
                    if (shield.Magical && shield.Physical)
                    {
                        shields.MagicalAndPhysical += shield.Amount;
                    }
                    else if (shield.Magical)
                    {
                        shields.Magical += shield.Amount;
                    }
                    else if (shield.Physical)
                    {
                        shields.Phyisical += shield.Amount;
                    }
                }
                if (shields.Magical + shields.Phyisical + shields.MagicalAndPhysical <= 0)
                {
                    shields = null;
                }
                charStackData.SkinName = a.Model;

                if (a is ObjAIBase obj)
                {
                    charStackData.SkinID = (uint)obj.SkinID;

                    if (obj.ItemInventory != null)
                    {
                        foreach (var item in obj.ItemInventory.GetItems(true, true))
                        {
                            var itemData = item.ItemData;
                            if (itemData is null)
                            {
                                continue;
                            }
                            itemDataList.Add(new S2C_OnEnterVisiblityClient.ItemData
                            {
                                ItemID = (uint)itemData.Id,
                                ItemsInSlot = (byte)item.StackCount,
                                Slot = obj.ItemInventory.GetItemSlot(item),
                                //Unhardcode this when spell ammo gets introduced
                                SpellCharges = 0

                            });
                        }
                    }
                }
            }

            /*
                  TODO:
                  buffCountList = new List<KeyValuePair<byte, int>>();
                  foreach (var buff in a.Buffs.All())
                  {
                      buffCountList.Add(new KeyValuePair<byte, int>(buff.Slot, buff.TotalStackCount));
                  }
                  */



            // TODO: if (a.IsDashing), requires SpeedParams, add it to AttackableUnit so it can be accessed outside of initialization


            //TODO: Looks like it's not being used
            charStackDataList.Add(charStackData);

            MovementData md;
            if (o is AttackableUnit u && u.Waypoints.Count > 1)
            {
                md = PacketExtensions126.CreateMovementDataWithSpeedIfPossible(u, _navGrid, useTeleportID: true);
            }
            else if (o is Pet p)
            {
                md = PacketExtensions126.CreateMovementDataWithSpeedIfPossible(p, _navGrid, useTeleportID: false);
            }
            else
            {
                md = PacketExtensions126.CreateMovementDataStop(o);
            }

            var enterVis = new S2C_OnEnterVisiblityClient
            {
                SenderNetID = o.NetId,
                Items = itemDataList,
                LookAtType = (byte)LookAtType.Direction,//0,//(byte)LookAtType.Direction, //?
                LookAtPosition = o.Direction, //new Vector3(0, 0, 0),
                                              // TODO: Verify
                MovementData = md
            };


            if (o is LaneMinion u2)
            {
                if (u2.Waypoints.Count > 1)
                {
                    enterVis = new S2C_OnEnterVisiblityClient
                    {
                        SenderNetID = u2.NetId,
                        Items = itemDataList,
                        LookAtType = (byte)LookAtType.Direction,//0,//(byte)LookAtType.Direction, //?
                        LookAtPosition = u2.Waypoints[1].ToVector3(100.0f), //new Vector3(0, 0, 0),
                                                                            // TODO: Verify
                        MovementData = md
                    };
                }

            }

            if (o is Pet p2)
            {
                enterVis = new S2C_OnEnterVisiblityClient
                {
                    SenderNetID = p2.NetId,
                    Items = itemDataList,
                    LookAtType = (byte)LookAtType.Direction,//0,//(byte)LookAtType.Direction, //?
                    LookAtPosition = (o is Pet) ? new Vector3(0, 0, 0) : o.Direction, //new Vector3(0, 0, 0),
                                                                                      // TODO: Verify
                    MovementData = md
                };
            }


            if (packets != null)
            {
                enterVis.Packets = packets;
            }

            return enterVis;
        }

        /// <summary>
        /// Sends a packet to either all players with vision of the specified object or the specified user. The packet details the data surrounding the specified GameObject that is required by players when a GameObject enters vision such as items, shields, skin, and movements.
        /// </summary>
        /// <param name="o">GameObject entering vision.</param>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="isChampion">Whether or not the GameObject entering vision is a Champion.</param>
        /// <param name="ignoreVision">Optionally ignore vision checks when sending this packet.</param>
        /// <param name="packets">Takes in a list of packets to send alongside this vision packet.</param>
        /// TODO: Incomplete implementation.
        public void NotifyEnterVisibilityClient(GameObject o, int userId = -1, bool ignoreVision = false, List<BasePacket> packets = null)
        {

            var enterVis = ConstructEnterVisibilityClientPacket(o, packets);

            var pktflag = GetPacketFlagsForFrequency();

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, enterVis, Channel.CHL_S2C, pktflag);
            }
            else
            {
                if (ignoreVision)
                {
                    _packetHandlerManager.BroadcastPacket(enterVis, Channel.CHL_S2C, pktflag);
                }
                else
                {
                    _packetHandlerManager.BroadcastPacketVision(o, enterVis, Channel.CHL_S2C, pktflag);
                }
            }

        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified unit detailing that the unit has begun facing the specified direction.
        /// </summary>
        /// <param name="obj">GameObject that is changing their orientation.</param>
        /// <param name="direction">3D direction the unit will face.</param>
        /// <param name="isInstant">Whether or not the unit should instantly turn to the direction.</param>
        /// <param name="turnTime">The amount of time (seconds) the turn should take.</param>
        public void NotifyFaceDirection(GameObject obj, Vector3 direction, bool isInstant = true, float turnTime = 0.0833f)
        {
            var facePacket = new S2C_FaceDirection()
            {
                SenderNetID = obj.NetId,
                Direction = direction,
            };

            _packetHandlerManager.BroadcastPacket(facePacket, Channel.CHL_S2C);
        }

        public void NotifyFXEnterTeamVisibility(Particle particle, TeamId team, int userId)
        {
            var fxVisPacket = new S2C_FX_OnEnterTeamVisiblity
            {
                SenderNetID = particle.NetId,
                UnitNetID = particle.NetId,
            };

            fxVisPacket.VisiblityTeam = 0;
            //TODO: Provide support for more than 2 teams
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                fxVisPacket.VisiblityTeam = 1;
            }

            _packetHandlerManager.SendPacket(userId, fxVisPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified particle has been destroyed.
        /// </summary>
        /// <param name="particle">Particle that is being destroyed.</param>
        /// TODO: Change to only broadcast to players who have vision of the particle (maybe?).
        public void NotifyFXKill(Particle particle)
        {
            var fxKill = new S2C_FX_Kill
            {
                SenderNetID = 0,
                UnknownNetID = particle.NetId
            };
            _packetHandlerManager.BroadcastPacket(fxKill, Channel.CHL_S2C);
        }

        public void NotifyFXLeaveTeamVisibility(Particle particle, TeamId team, int userId = -1)
        {
            var fxVisPacket = new S2C_FX_OnLeaveTeamVisiblity
            {
                SenderNetID = particle.NetId,
                UnitNetID = particle.NetId
            };

            fxVisPacket.VisiblityTeam = 0;
            //TODO: Provide support for more than 2 teams
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                fxVisPacket.VisiblityTeam = 1;
            }

            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacketTeam(team, fxVisPacket, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, fxVisPacket, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the game has started. Sent when all players have finished loading.
        /// </summary>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players.</param>
        public void NotifyGameStart(int userId = -1)
        {
            var start = new S2C_StartGame
            {
                TournamentPauseEnabled = false
            };/*
            if (userId < 0)
            {*/
            _packetHandlerManager.BroadcastPacket(start, Channel.CHL_S2C);
            /*    }
                else
                {
                    _packetHandlerManager.SendPacket(userId, start, Channel.CHL_S2C);
                }  */


        }

        /// <summary>
        /// Sends a packet to all players detailing the state (DEAD/ALIVE) of the specified inhibitor.
        /// </summary>
        /// <param name="inhibitor">Inhibitor to check.</param>
        /// <param name="killer">Killer of the inhibitor (if applicable).</param>
        /// <param name="assists">Assists of the killer (if applicable).</param>
        public void NotifyInhibitorState(Inhibitor inhibitor, DeathData? deathData = null)
        {
            //INVESTIGATE
            switch (inhibitor.State)
            {
                case DampenerState.RegenerationState:
                    var annoucementDeath = new OnDampenerDie
                    {
                        //All mentions i found were 0, investigate further if we'd want to unhardcode this
                        GoldGiven = 0.0f,
                        OtherNetID = deathData.Killer.NetId,
                        AssistCount = 0
                    };
                    if (deathData is not null)
                    {
                        //annoucementDeath.OtherNetID = deathData.Killer.NetId;
                    }
                    NotifyS2C_OnEventWorld(annoucementDeath, inhibitor);
                    NotifyBuilding_Die(deathData);

                    break;
                case DampenerState.RespawningState:
                    var annoucementRespawn = new OnDampenerRespawn
                    {
                        OtherNetID = inhibitor.NetId
                    };
                    NotifyS2C_OnEventWorld(annoucementRespawn, inhibitor);
                    break;
            }
            NotifyDampenerSwitchStates(inhibitor);
        }

        /// <summary>
        /// Sends a packet to either the specified player or team detailing that the specified GameObject has left vision.
        /// </summary>
        /// <param name="o">GameObject that left vision.</param>
        /// <param name="team">TeamId to send the packet to; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="userId">User to send the packet to.</param>
        /// TODO: Verify where this should be used.
        public void NotifyLeaveLocalVisibilityClient(GameObject o, TeamId team, int userId = -1)
        {
            var leaveLocalVis = new S2C_OnLeaveLocalVisiblityClient
            {
                SenderNetID = o.NetId
            };

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, leaveLocalVis, Channel.CHL_S2C);
                return;
            }

            _packetHandlerManager.BroadcastPacketTeam(team, leaveLocalVis, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to either the specified user or team detailing that the specified GameObject has left vision.
        /// </summary>
        /// <param name="o">GameObject that left vision.</param>
        /// <param name="team">TeamId to send the packet to; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="userId">User to send the packet to (if applicable).</param>
        /// TODO: Verify where this should be used.
        public void NotifyLeaveVisibilityClient(GameObject o, TeamId team, int userId = -1)
        {
            var leaveVis = new S2C_OnLeaveVisiblityClient
            {
                SenderNetID = o.NetId
            };

            if (userId >= 0)
            {
                _packetHandlerManager.SendPacket(userId, leaveVis, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.BroadcastPacketTeam(team, leaveVis, Channel.CHL_S2C);
            }

            NotifyLeaveLocalVisibilityClient(o, team, userId);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the order and size of both teams on the loading screen.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="players">Client info of all players in the loading screen.</param>
        public void NotifyLoadScreenInfo(int userId, TeamId team, List<ClientInfo> players)
        {
            uint orderSizeCurrent = 0;
            uint chaosSizeCurrent = 0;

            var teamRoster = new SiphoningStrike.LoadScreen.TeamRosterUpdate
            {
                TeamSizeOrder = 6,
                TeamSizeChaos = 6
            };

            foreach (var player in players)
            {
                if (player.Team is TeamId.TEAM_ORDER)
                {
                    teamRoster.OrderPlayerIDs[orderSizeCurrent] = (ulong)player.PlayerId;
                    orderSizeCurrent++;
                }
                // TODO: Verify if it is ok to allow neutral
                else
                {
                    teamRoster.ChaosPlayerIDs[chaosSizeCurrent] = (ulong)player.PlayerId;
                    chaosSizeCurrent++;
                }
            }

            teamRoster.CurrentTeamSizeOrder = orderSizeCurrent;
            teamRoster.CurrentTeamSizeChaos = chaosSizeCurrent;

            _packetHandlerManager.SendPacket(userId, teamRoster, Channel.CHL_LOADING_SCREEN);
        }


        public void NotifyS2C_CameraBehavior(Champion target, Vector3 position)
        {
            var packet = new S2C_CameraBehavior
            {
                SenderNetID = target.NetId,
                Position = position
            };

            _packetHandlerManager.SendPacket(target.ClientId, packet, Channel.CHL_S2C);
        }
        /// <summary>
        /// Sends a packet to all players that updates the specified unit's model.
        /// </summary>
        /// <param name="obj">AttackableUnit to update.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="skinID">Unit's skin ID after changing model.</param>
        /// <param name="modelOnly">Wether or not it's only the model that it's being changed(?). I don't really know what's this for</param>
        /// <param name="overrideSpells">Wether or not the user's spells should be overriden, i assume it would be used for things like Nidalee or Elise.</param>
        /// <param name="replaceCharacterPackage">Unknown.</param>
        public void NotifyS2C_ChangeCharacterData(ObjAIBase obj, string skinName, bool modelOnly = true, bool overrideSpells = false, bool replaceCharacterPackage = false, int countchangemodel = 0)
        {
            var newCharData = new S2C_ChangeCharacterData
            {
                SenderNetID = obj.NetId,
                SkinName = skinName,
                UseSpells = overrideSpells,
                StackID = (uint)countchangemodel, //this count how much time the person has used this packet 
            };

            _packetHandlerManager.BroadcastPacketVision(obj, newCharData, Channel.CHL_S2C);
        }


        public void NotifyS2C_PopCharacterData(ObjAIBase obj, int countchangemodel = 0)
        {
            var newCharData = new S2C_PopCharacterData
            {
                SenderNetID = obj.NetId,
                // SkinName = skinName,
                //   UseSpells = overrideSpells,
                PopID = (uint)countchangemodel, //this count how much time the person has used this packet 
            };

            _packetHandlerManager.BroadcastPacketVision(obj, newCharData, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players who have vision of the specified buff's target detailing that the buff has been added to the target.
        /// </summary>
        public void NotifyNPC_BuffAdd2(Buff b, int stacks)
        {
            var addPacket = new S2C_NPC_BuffAdd2
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                ButtType = (byte)b.BuffType,
                Count = (byte)stacks,
                IsHidden = b.IsHidden,
                BuffNameHash = HashString(b.Name),
                CasterNetID = b.OriginSpell?.Caster?.NetId ?? 0,
                RunningTime = b.TimeElapsed,
                Duration = b.Duration,
            };
            if (b.SourceUnit != null)
            {
                addPacket.CasterNetID = b.SourceUnit.NetId;
            }
            _packetHandlerManager.BroadcastPacket(addPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players who have vision of the target of the specified buff detailing that the buff was removed from its target.
        /// </summary>
        /// <param name="b">Buff that was removed.</param>
        public void NotifyNPC_BuffRemove2(Buff b)
        {
            NotifyNPC_BuffRemove2(b.TargetUnit, b.Slot, b.Name);
        }
        public void NotifyNPC_BuffRemove2(AttackableUnit owner, int slot, string name)
        {
            var removePacket = new S2C_NPC_BuffRemove2
            {
                SenderNetID = owner.NetId, //TODO: Verify if this should change depending on the removal source
                BuffSlot = (byte)slot,
                BuffNameHash = HashString(name)
            };
            _packetHandlerManager.BroadcastPacket(removePacket, Channel.CHL_S2C, PacketFlags.UNSEQUENCED);// CHL_S2C
        }

        /// <summary>
        /// Sends a packet to all players with vision of the target of the specified buff detailing that the buff previously in the same slot was replaced by the newly specified buff.
        /// </summary>
        /// <param name="b">Buff that will replace the old buff in the same slot.</param>
        public void NotifyNPC_BuffReplace(Buff b)
        {
            var replacePacket = new S2C_NPC_BuffReplace
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                RunningTime = b.TimeElapsed,
                Duration = b.Duration,
                CasterNetID = b.SourceUnit?.NetId ?? 0
            };
            _packetHandlerManager.BroadcastPacketVision(b.TargetUnit, replacePacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the target of the specified buff detailing an update to the number of buffs in the specified buff's slot
        /// </summary>
        /// <param name="b">Buff who's count is being updated.</param>
        /// <param name="duration">Total time the buff should last.</param>
        /// <param name="runningTime">Time since the buff's creation.</param>
        public void NotifyNPC_BuffUpdateCount(Buff b, int stacks)
        {
            var updatePacket = new S2C_NPC_BuffUpdateCount
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                Count = (byte)stacks,
                Duration = b.Duration,
                RunningTime = b.TimeElapsed,
                CasterNetID = 0
            };
            if (b.SourceUnit != null)
            {
                updatePacket.CasterNetID = b.SourceUnit.NetId;
            }
            _packetHandlerManager.BroadcastPacketVision(b.TargetUnit, updatePacket, Channel.CHL_S2C);
        }


        /// <summary>
        /// Sends a packet to all players with vision of the specified target detailing an update to the number of buffs in each of the buff slots occupied by the specified group of buffs.
        /// </summary>
        /// <param name="target">Unit who's buffs will be updated.</param>
        /// <param name="buffs">Group of buffs to update.</param>
        /// <param name="duration">Total time the buff should last.</param>
        /// <param name="runningTime">Time since the buff's creation.</param>
        public void NotifyNPC_BuffUpdateCountGroup(AttackableUnit target, List<Buff> buffs, float duration, float runningTime)
        {
            var updateGroupPacket = new S2C_NPC_BuffUpdateCountGroup
            {
                Duration = duration,
                RunningTime = runningTime
            };
            var entries = new List<BuffUpdateCountGroupEntry>();
            for (int i = 0; i < buffs.Count; i++)
            {
                var entry = new BuffUpdateCountGroupEntry
                {
                    UnitNetID = buffs[i].TargetUnit.NetId,
                    CasterNetID = 0,
                    BuffSlot = (byte)buffs[i].Slot,
                    Count = (byte)buffs[i].StackCount
                };

                if (buffs[i].OriginSpell != null)
                {
                    entry.CasterNetID = buffs[i].OriginSpell.Caster.NetId;
                }
                entries.Add(entry);
            }
            updateGroupPacket.Entries = entries;

            _packetHandlerManager.BroadcastPacketVision(target, updateGroupPacket, Channel.CHL_S2C);
        }


        /// <summary>
        /// Sends a packet to all players with vision of the target of the specified buff detailing an update to the stack counter of the specified buff.
        /// </summary>
        /// <param name="b">Buff who's stacks will be updated.</param>
        public void NotifyNPC_BuffUpdateNumCounter(Buff b)
        {
            var updateNumPacket = new S2C_NPC_BuffUpdateCount
            {
                SenderNetID = b.TargetUnit.NetId,
                BuffSlot = (byte)b.Slot,
                Count = (byte)b.StackCount//todo: checkthis
            };
            _packetHandlerManager.BroadcastPacketVision(b.TargetUnit, updateNumPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the specified debug object's radius has changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="sender">NetId of the GameObject that is responsible for this packet being sent.</param>
        /// <param name="objID">ID of the Debug Object.</param>
        /// <param name="newRadius">New radius of the Debug Object.</param>
        public void NotifyModifyDebugCircleRadius(int userId, uint sender, int objID, float newRadius)
        {
            var debugPacket = new S2C_ModifyDebugCircleRadius
            {
                SenderNetID = sender,
                CircleID = (uint)objID,
                Radius = newRadius
            };
            _packetHandlerManager.SendPacket(userId, debugPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the specified Debug Object's color has changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="sender">NetId of the GameObject responsible for this packet being sent.</param>
        /// <param name="objID">ID of the Debug Object.</param>
        /// <param name="r">Red hex value of the Debug Object.</param>
        /// <param name="g">Green hex value of the Debug Object.</param>
        /// <param name="b">Blue hex value of the Debug Object.</param>
        public void NotifyModifyDebugObjectColor(int userId, uint sender, int objID, byte r, byte g, byte b)
        {
            var debugObjPacket = new S2C_ModifyDebugCircleColor
            {
                SenderNetID = sender,
                ObjectID = (uint)objID
            };
            var color = new MirrorImage.Game.Common.Color();
            color.Red = r;
            color.Green = g;
            color.Blue = b;
            debugObjPacket.Color = color;
            _packetHandlerManager.SendPacket(userId, debugObjPacket, Channel.CHL_S2C);
        }



        /// <summary>
        /// Sends a packet to all players with vision of the owner of the specified shield detailing it's values.
        /// </summary>
        /// <param name="unit">Target Unit</param>
        /// <param name="physical">Physical shield value modified</param>
        /// <param name="magical">Magical shield value modified</param>
        /// <param name="amount">Shield value</param>
        /// <param name="stopShieldFade">True = Time Fade; False = Damage Fade</param>
        public void NotifyModifyShield(AttackableUnit unit, bool physical, bool magical, float amount, bool stopShieldFade = false)
        {
            var answer = new S2C_ModifyShield()
            {
                SenderNetID = unit.NetId,
                Physical = physical,
                Magical = magical,
                Ammount = amount,
                StopShieldFade = stopShieldFade
            };
            _packetHandlerManager.BroadcastPacket(answer, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the owner of the specified spell detailing that a spell has been cast.
        /// </summary>
        /// <param name="s">Spell being cast.</param>
        public void NotifyNPC_CastSpellAns(ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.CastInfo ci)
        {
            //throw new InvalidOperationException("for trace");
            var packet = new S2C_NPC_CastSpellAns
            {
                SenderNetID = ci.Caster.NetId,
                CasterPositionSyncID = Environment.TickCount / 1000, //TODO:
                CastInfo = ConvertCastInfo(ci)
            };
            // _packetHandlerManager.BroadcastPacketVision(ci.Caster, packet, Channel.CHL_S2C);
            //chronoshift does like that , bad method ? 
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified unit has been killed by the specified killer.
        /// </summary>
        /// <param name="data">Data of the death.</param>
        public void NotifyNPC_Die_Broadcast(DeathData data)
        {
            var dieMapView = new S2C_NPC_Die
            {
                SenderNetID = data.Unit.NetId,
                DeathData = new SiphoningStrike.Game.Common.DeathData
                {
                    BecomeZombie = data.BecomeZombie,
                    SpellSourceType = (byte)data.DamageSource,
                    KillerNetID = data.Killer.NetId,
                    DamageType = (byte)data.DamageType,
                    DeathDuration = data.DeathDuration
                }
            };
            _packetHandlerManager.BroadcastPacket(dieMapView, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that a champion has died and calls a death timer update packet.
        /// </summary>
        /// <param name="champion">Champion that died.</param>
        /// <param name="killer">Unit that killed the Champion.</param>
        /// <param name="goldFromKill">Amount of gold the killer received.</param>
        public void NotifyNPC_Hero_Die(DeathData deathData)
        {
            var champ = deathData.Unit as Champion;
            uint killerNetID = deathData.Killer.NetId;

            NotifyNPC_Die_EventHistory(champ, deathData, killerNetID);
            NotifyS2C_UpdateDeathTimer(champ);

            var cd = new S2C_NPC_Hero_Die
            {
                SenderNetID = deathData.Unit.NetId,
                DeathData = new SiphoningStrike.Game.Common.DeathData
                {
                    KillerNetID = killerNetID,
                    SpellSourceType = (byte)deathData.DieType,
                    DamageType = (byte)deathData.DamageType,
                    BecomeZombie = deathData.BecomeZombie,
                    DeathDuration = champ.Stats.RespawnTimer / 1000f
                }
            };
            _packetHandlerManager.BroadcastPacket(cd, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that a champion has been forced to die.
        /// </summary>
        /// <param name="champion">Champion that died.</param>
        /// <param name="killer">Unit that killed the Champion.</param>
        /// <param name="goldFromKill">Amount of gold the killer received.</param>
        public void NotifyNPC_ForceDead(DeathData lastDeathData)
        {

            NotifyNPC_Die_EventHistory(lastDeathData.Unit as Champion, lastDeathData, 0);


            //league sends both packets
            var forceDead = new S2C_NPC_ForceDead
            {
                SenderNetID = lastDeathData.Unit.NetId,
            };
            var heroDie = new S2C_NPC_Hero_Die
            {
                SenderNetID = lastDeathData.Unit.NetId,
                DeathData = new SiphoningStrike.Game.Common.DeathData
                {
                    KillerNetID = lastDeathData.Killer.NetId,
                    SpellSourceType = (byte)lastDeathData.DamageSource,
                    DamageType = (byte)lastDeathData.DamageType,
                    BecomeZombie = lastDeathData.BecomeZombie,
                    DeathDuration = lastDeathData.Unit.Stats.RespawnTimer / 1000f
                }
            };
            _packetHandlerManager.BroadcastPacket(heroDie, Channel.CHL_S2C);
            _packetHandlerManager.BroadcastPacket(forceDead, Channel.CHL_S2C);
        }

        public void NotifyNPC_Die_EventHistory(Champion ch, DeathData deathData, uint killerNetID = 0)
        {
            if (ch != null)
            {


                /* var history = new S2C_NPC_Die_EventHistory()
                  {
                      SenderNetID = ch.NetId,
                      KillerNetID = killerNetID,
                      //TimeWindow = ch.EventHistory.FirstOrDefault()?.TimeStamp, //Check if first or last
                      //KillerEventSourceType = ch.EventHistory.FirstOrDefault()?.EventSourceType,
                      Events = new()
                      {
                          //TODO
                      }
                  };
                  _packetHandlerManager.SendPacket(ch.ClientId, history, Channel.CHL_S2C);*/


                //test of 20/04/24
                /*  var history = new S2C_NPC_Die_EventHistory();

                   history.SenderNetID = ch.NetId;
                   history.KillerNetID = killerNetID;
                   history.timetogetdied = 10.0f; //is default for moment 
                   history.Damage = 250.0f; //same 
                   history.ParentScriptNameHash = 105475752; //is summonerflashkek 
                   history.typeofdamage = (byte)deathData.DamageType;
                   history.TimeStamp = Game.Time.DeltaTimeSeconds;


       */

                /*  history.DurationOpenedWindows = 25; //is the second time you take damage for get killed 
                  history.Othernetid = ch.NetId; // suppose is self or assist 
                  history.Entriescounthack = 51; //50 + 1 ( 32 bit + number of people ) 
                  history.eventid = 11; //11 for spell ,38 for buff  ()
                  history.PhysicalDamage = 1000;
                  history.MagicalDamage = 300;
                  history.TrueDamage = 20;
                  history.ParentScriptNameHash = 104222500;
                  history.ParentCasterNetID = killerNetID; */

                // history.Duration = 0;
                /* if (ch.EventHistory.Count > 0)
                 {
                     float firstTimestamp = ch.EventHistory[0].Timestamp;
                     float lastTimestamp = ch.EventHistory[ch.EventHistory.Count - 1].Timestamp;
                     // history.Duration = lastTimestamp - firstTimestamp;
                 }*/
                //history.EventSourceType = 0; //TODO: Confirm that it is always zero no 
                // history.Entries = ch.EventHistory;
                //At first view chronoshift never used this packet  






                //last test 29/09/2024
                var history = new S2C_NPC_Die_EventHistory();
                history.SenderNetID = ch.NetId;
                history.KillerNetID = killerNetID;
                history.TimeWindow = ch.GetRespawnTime();
                history.Events = ch.EventHistory;
                if (ch.EventHistory.Count == 0)
                {
                    //this is an hack in case event history is empty 
                    // Console.WriteLine("event history etait VIDE NTM ");
                    history.KillerNetID = ch.NetId;
                    var e = ch.CreateEventForHistory<OnDamageGiven>(ch, ch.Spells.F);
                    e.ParentCasterNetID = ch.NetId;
                    e.OtherNetID = ch.NetId;
                    e.ScriptNameHash = 0;
                    e.ParentScriptNameHash = 0xFFFFFFFF;
                    e.EventSource = (uint)EventSource.BASICATTACK; // ?
                    e.ParentTeam = (uint)ch.Team;
                    e.SourceSpellLevel = 0;
                    e.ParentSourceType = 0;

                    var fakeevent = new EventHistoryEntry()
                    {
                        Timestamp = Game.Time.GameTime / 1000f,
                        Count = 1,

                        Source = ch.NetId,
                        Event = e

                    };

                    ch.EventHistory.Add(fakeevent);
                    history.Events = ch.EventHistory;

                }
                _packetHandlerManager.SendPacket(ch.ClientId, history, Channel.CHL_S2C);

            }
        }
        /// <summary>
        /// Sends a packet to all players with vision of the specified AttackableUnit detailing that the attacker has abrubtly stopped their attack (can be a spell or auto attack, although internally AAs are also spells).
        /// </summary>
        /// <param name="attacker">AttackableUnit that stopped their auto attack.</param>
        /// <param name="isSummonerSpell">Whether or not the spell is a summoner spell.</param>
        /// <param name="keepAnimating">Whether or not to continue the auto attack animation after the abrupt stop.</param>
        /// <param name="destroyMissile">Whether or not to destroy the missile which may have been created before stopping (client-side removal).</param>
        /// <param name="overrideVisibility">Whether or not stopping this auto attack overrides visibility checks.</param>
        /// <param name="forceClient">Whether or not this packet should be forcibly applied, regardless of if an auto attack is being performed client-side.</param>
        /// <param name="missileNetID">NetId of the missile that may have been spawned by the spell.</param>
        /// TODO: Find a better way to implement these parameters


        ///todo : too much spamed on actuial circonstence , in replay only used sometimes 
        public void NotifyNPC_InstantStop_Attack(AttackableUnit attacker, bool isSummonerSpell,
            bool keepAnimating = false,
            bool destroyMissile = true,
            bool overrideVisibility = true,
            bool forceClient = false,
            uint missileNetID = 0)
        {
            var stopAttack = new S2C_NPC_InstantStop_Attack
            {
                SenderNetID = attacker.NetId,
                KeepAnimating = keepAnimating,
                DestroyMissile = destroyMissile,
                ForceSpellCast = overrideVisibility,
                AvatarSpell = isSummonerSpell,
                //ForceStop = attacker.
                //TODO
                //ForceStop
            };
            //BroadcastPacketVision
            _packetHandlerManager.BroadcastPacket(stopAttack, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified Champion has leveled up.
        /// </summary>
        /// <param name="c">Champion which leveled up.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        public void NotifyNPC_LevelUp(ObjAIBase obj)
        {
            byte level, trainingPoints = 0;
            switch (obj)
            {
                case Champion c:
                    level = (byte)c.Experience.Level;
                    trainingPoints = c.Experience.SpellTrainingPoints.TrainingPoints;
                    break;
                case Minion m:
                    level = (byte)m.MinionLevel;
                    break;
                default:
                    return;
            }

            var levelUp = new S2C_NPC_LevelUp()
            {
                SenderNetID = obj.NetId,
                Level = level,
                // TODO: Typo >>>:(
                AveliablePoints = trainingPoints
            };

            _packetHandlerManager.BroadcastPacketVision(obj, levelUp, Channel.CHL_S2C);

            //NetworkResult __thiscall NetVisibilityObject::SendVariableSizeInternal = broadcast? 
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that they have leveled up the specified skill.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="netId">NetId of the GameObject whos skill is being leveled up.</param>
        /// <param name="slot">Slot of the skill being leveled up.</param>
        /// <param name="level">Current level of the skill.</param>
        /// <param name="points">Number of skill points available after the skill has been leveled up.</param>
        public void NotifyNPC_UpgradeSpellAns(int userId, uint netId, int slot, int level, int points)
        {
            var upgradeSpellPacket = new S2C_NPC_UpgradeSpellAns
            {
                SenderNetID = netId,
                Slot = (byte)slot,
                SpellLevel = (byte)level,
                SkillPoints = (byte)points
            };

            _packetHandlerManager.SendPacket(userId, upgradeSpellPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all users with vision of the given caster detailing that the given spell has been set to auto cast (as well as the spell in the critSlot) for the given caster.
        /// </summary>
        /// <param name="caster">Unit responsible for the autocasting.</param>
        /// <param name="spell">Spell to auto cast.</param>
        /// // TODO: Verify critSlot functionality
        /// <param name="critSlot">Optional spell slot to cast when a crit is going to occur.</param>
        public void NotifyNPC_SetAutocast(ObjAIBase caster, Spell spell, int critSlot = 0)
        {
            var autoCast = new S2C_NPC_SetAutocast
            {
                SenderNetID = caster.NetId,
                Slot = (byte)spell.Slot,
            };

            _packetHandlerManager.BroadcastPacketVision(caster, autoCast, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified unit detailing that the specified unit's stats have been updated.
        /// </summary>
        /// <param name="u">Unit who's stats have been updated.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="partial">Whether or not the packet should only include stats marked as changed.</param>
        /// TODO: Replace with LeaguePackets and preferably move all uses of this function to a central EventHandler class (if one is fully implemented).
        public void NotifyOnReplication(AttackableUnit u, int userId = -1, bool partial = true)
        {
            if (u.Replication != null)
            {
                var us = new S2C_OnReplication()
                {
                    SyncID = Environment.TickCount,
                    // TODO: Support multi-unit replication creation (perhaps via a separate function which takes in a list of units).
                    ReplicationData = new List<ReplicationData>(1){
                        u.Replication.GetData(false) //partial
                    }
                };

                var channel = Channel.CHL_S2C;



                /*  if (userId < 0)
                {
                    _packetHandlerManager.BroadcastPacketVision(u, us, channel, PacketFlags.UNSEQUENCED);
                }
                else
                {
                    _packetHandlerManager.SendPacket(userId, us, channel, PacketFlags.UNSEQUENCED);
                }
             */
                if (u is Champion)
                {
                    var pktflag = PacketFlags.RELIABLE;
                    if (Game.Time.CurrentHz < 60.0f)
                    {
                        pktflag = PacketFlags.UNSEQUENCED;
                    }
                    _packetHandlerManager.BroadcastPacket(us, channel, pktflag);
                }


            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the game has paused.
        /// </summary>
        /// <param name="seconds">Amount of time till the pause ends.</param>
        /// <param name="showWindow">Whether or not to show a pause window.</param>
        public void NotifyPausePacket(ClientInfo player, int seconds, bool isTournament)
        {
            //??
            var pg = new BID_PausePacket
            {
                //Check if SenderNetID should be the person that requested the pause or just 0
                // SenderNetID = player.Champion.NetId,
                //IsTournament = isTournament,
                //PauseTimeRemaining = seconds
                ClientID = (uint)player.ClientId,
                TournamentPause = isTournament,
                PauseTimeRemaining = seconds

            };
            //I Assumed that, since the packet requires idividual client IDs, that it also sends the packets individually, by useing the SendPacket Channel, double check if that's valid.
            _packetHandlerManager.SendPacket(player.ClientId, pg, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the specified client's loading screen progress.
        /// </summary>
        /// <param name="request">Info of the target client given via the client who requested loading screen progress.</param>
        /// <param name="clientInfo">Client info of the client who's progress is being requested.</param>
        public void NotifyPingLoadInfo(ClientInfo client, C2S_Ping_Load_Info packet)
        {
            var response = new S2C_Ping_Load_Info
            {
                ConnectionInfo = new ConnectionInfo
                {
                    ClientID = packet.ConnectionInfo.ClientID,
                    Ping = packet.ConnectionInfo.Ping,
                    PlayerID = (ulong)client.PlayerId,
                    ETA = packet.ConnectionInfo.ETA,
                    Ready = packet.ConnectionInfo.Ready,
                    Percentage = packet.ConnectionInfo.Percentage,
                    Count = packet.ConnectionInfo.Count
                }
            };

            //var response = new SiphoningStrike.Game.S2C_Ping_Load_Info
            //{
            //    ConnectionInfo = new SiphoningStrike.Game.Common.ConnectionInfo
            //    {
            //        ClientID = request.ClientID,
            //        PlayerID = (ulong)client.PlayerId,
            //        Percentage = request.Percentage,
            //        ETA = request.ETA,
            //        Count = request.Count,
            //        Ping = request.Ping,
            //        Ready = request.Ready
            //    },
            //};
            //Logging->writeLine("loaded: %f, ping: %f, %f", loadInfo->loaded, loadInfo->ping, loadInfo->f3);
            _packetHandlerManager.BroadcastPacket(response, Channel.CHL_LOW_PRIORITY, PacketFlags.UNSEQUENCED);
        }


        public void NotifyPingLoadInfo(ClientInfo client, CrystalSlash.Game.C2S_Ping_Load_Info packet)
        {
            var response = new S2C_Ping_Load_Info
            {
                ConnectionInfo = new ConnectionInfo
                {
                    ClientID = packet.ConnectionInfo.ClientID,
                    Ping = packet.ConnectionInfo.Ping,
                    PlayerID = (ulong)client.PlayerId,
                    ETA = packet.ConnectionInfo.ETA,
                    Ready = packet.ConnectionInfo.Ready,
                    Percentage = packet.ConnectionInfo.Percentage,
                    Count = packet.ConnectionInfo.Count
                }
            };

            //var response = new SiphoningStrike.Game.S2C_Ping_Load_Info
            //{
            //    ConnectionInfo = new SiphoningStrike.Game.Common.ConnectionInfo
            //    {
            //        ClientID = request.ClientID,
            //        PlayerID = (ulong)client.PlayerId,
            //        Percentage = request.Percentage,
            //        ETA = request.ETA,
            //        Count = request.Count,
            //        Ping = request.Ping,
            //        Ready = request.Ready
            //    },
            //};
            //Logging->writeLine("loaded: %f, ping: %f, %f", loadInfo->loaded, loadInfo->ping, loadInfo->f3);
            _packetHandlerManager.BroadcastPacket(response, Channel.CHL_LOW_PRIORITY);
        }

        /// <summary>
        /// Sends a packet to all players that a champion has respawned.
        /// </summary>
        /// <param name="c">Champion that respawned.</param>
        public void NotifyHeroReincarnateAlive(Champion c, float parToRestore)
        {
            var cr = new S2C_HeroReincarnateAlive
            {
                SenderNetID = c.NetId,
                Position = c.Position3D,
            };
            _packetHandlerManager.BroadcastPacket(cr, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified AI detailing that item in the specified slot was removed (or the number of stacks of the item in that slot changed).
        /// </summary>
        /// <param name="ai">AI with the items.</param>
        /// <param name="slot">Slot of the item that was removed.</param>
        /// <param name="remaining">Number of stacks of the item left (0 if not applicable).</param>
        public void NotifyRemoveItem(ObjAIBase ai, int slot, int remaining)
        {
            var ria = new S2C_RemoveItemAns()
            {
                SenderNetID = ai.NetId,
                Slot = (byte)slot,
                ItemsInSlot = (byte)remaining,
            };
            _packetHandlerManager.BroadcastPacketVision(ai, ria, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified region was removed.
        /// </summary>
        /// <param name="region">Region to remove.</param>
        public void NotifyRemoveRegion(Region region)
        {
            var removeRegion = new S2C_RemovePerceptionBubble()
            {
                BubbleID = region.NetId
            };

            _packetHandlerManager.BroadcastPacket(removeRegion, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the highlight of the specified GameObject was removed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="unit">GameObject that had the highlight.</param>
        public void NotifyRemoveUnitHighlight(int userId, GameObject unit)
        {
            var highlightPacket = new S2C_RemoveUnitHighlight
            {
                SenderNetID = unit.NetId,
                UnitNetID = unit.NetId
            };
            _packetHandlerManager.SendPacket(userId, highlightPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing skin and player name information of the specified player on the loading screen.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="player">Player information to send.</param>
        public void NotifyRequestRename(int userId, ClientInfo player)
        {
            var loadName = new SiphoningStrike.LoadScreen.RequestRename
            {
                PlayerID = (ulong)player.PlayerId,
                Name = player.Name + "\u0000",
                // Most packets show a large default value (in place of what you would expect to be 0)
                // Seems to be randomized per-game and used for every RequestRename packet during that game.
                // So, using this SkinNo may be incorrect.
                SkinID = (uint)player.SkinNo,
            };
            _packetHandlerManager.BroadcastPacket(loadName, Channel.CHL_LOADING_SCREEN);
        }

        /// <summary>
        /// Sends a packet to the specified user detailing skin information of the specified player on the loading screen.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="player">Player information to send.</param>
        public void NotifyRequestReskin(int userId, ClientInfo player)
        {
            var loadChampion = new SiphoningStrike.LoadScreen.RequestReskin
            {
                PlayerID = (ulong)player.PlayerId,
                SkinID = (uint)player.SkinNo,
                Name = player.Champion.Model + "\u0000"
            };
            _packetHandlerManager.BroadcastPacket(loadChampion, Channel.CHL_LOADING_SCREEN);
        }

        /// <summary>
        /// Sends a packet to player detailing that the game has been unpaused.
        /// </summary>
        /// <param name="unpauser">Unit that unpaused the game.</param>
        /// <param name="showWindow">Whether or not to show a window before unpausing (delay).</param>
        public void NotifyResumePacket(Champion unpauser, ClientInfo player, bool isDelayed)
        {

            var resume = new BID_ResumePacket
            {
                SenderNetID = 0,
                Delayed = isDelayed,
                ClientID = (uint)player.ClientId
            };
            if (unpauser != null)
            {
                resume.SenderNetID = unpauser.NetId;
            }

            _packetHandlerManager.SendPacket(player.ClientId, resume, Channel.CHL_S2C);

        }

        public void NotifyS2C_ActivateMinionCamp(NeutralMinionCamp monsterCamp, int userId = -1)
        {
            // seem not used in cs replay 

            var packet = new S2C_Neutral_Camp_Empty
            {
                PlayerNetID = (uint)userId, //clientid ? 

                CampIndex = (byte)monsterCamp.CampIndex,
                State = false,
                SenderNetID = 0,
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }

        }

        public void NotifyS2C_AmmoUpdate(Spell spell)
        {

            //doesn't exist in .126 ammo method 
            //was buff method in past 
            /*
            if (spell.Caster is Champion ch)
            {
                var packet = new S2C_AmmoUpdate
                {
                    IsSummonerSpell = spell.SpellName.StartsWith("Summoner"),
                    SpellSlot = spell.Slot,
                    CurrentAmmo = spell.CurrentAmmo,
                    // TODO: Implement this. Example spell which uses it is Syndra R.
                    MaxAmmo = -1,
                    SenderNetID = spell.Caster.NetId
                };

                if (spell.CurrentAmmo < spell.SpellData.MaxAmmo)
                {
                    packet.AmmoRecharge = spell.CurrentAmmoCooldown;
                    packet.AmmoRechargeTotalTime = spell.AmmoCooldown;
                }

                _packetHandlerManager.SendPacket(ch.ClientId, packet, Channel.CHL_S2C);
            }
            */
        }

        /// <summary>
        /// Sends a packet to the specified user or all users detailing that the hero designated to the given clientInfo has been created.
        /// </summary>
        /// <param name="clientInfo">Information about the client which had their hero created.</param>
        /// <param name="userId">User to send the packet to. Set to -1 to broadcast.</param>
        public void NotifyS2C_CreateHero(ClientInfo clientInfo, int userId, TeamId team, bool doVision)
        {

            var champion = clientInfo.Champion;

            if (Game.Config.VersionOfClient == "1.0.0.126" || Game.Config.VersionOfClient == "1.0.0.131")
            {

                var heroPacket = new S2C_CreateHero() // Broadcast
                {
                    UnitNetID = champion.NetId,
                    ClientID = (uint)clientInfo.ClientId,
                    NetNodeID = 0x40,
                    // For bots (0 = Beginner, 1 = Intermediate)
                    SkillLevel = 0,
                    TeamIsOrder = champion.Team is TeamId.TEAM_ORDER,
                    IsBot = false,//champion.IsBot,// is set to true , this will justrename bot to bot_champion_xxx 
                    BotRank = 0,
                    SpawnPositionIndex = 0,
                    SkinID = (uint)champion.SkinID,
                    Name = clientInfo.Name,
                    Skin = champion.Model,

                };
                SendSpawnPacket(userId, champion, heroPacket, true);
            }
            else if (Game.Config.VersionOfClient == "1.0.0.132")
            {

                var heroPacket = new TechmaturgicalRepairBot.Game.S2C_CreateHero() // Broadcast
                {
                    UnitNetID = champion.NetId,
                    ClientID = (uint)clientInfo.ClientId,
                    NetNodeID = 0x40,
                    // For bots (0 = Beginner, 1 = Intermediate)
                    SkillLevel = 0,
                    TeamIsOrder = champion.Team is TeamId.TEAM_ORDER,
                    IsBot = champion.IsBot,
                    BotRank = 0,
                    SpawnPositionIndex = 0,
                    SkinID = (uint)champion.SkinID,
                    Name = clientInfo.Name,
                    Skin = champion.Model,

                };
                SendSpawnPacket(userId, champion, heroPacket, true);
            }
            else
            {

                var heroPacket = new S2C_CreateHero() // Broadcast
                {
                    UnitNetID = champion.NetId,
                    ClientID = (uint)clientInfo.ClientId,
                    NetNodeID = 0x40,
                    // For bots (0 = Beginner, 1 = Intermediate)
                    SkillLevel = 0,
                    TeamIsOrder = champion.Team is TeamId.TEAM_ORDER,
                    IsBot = champion.IsBot,
                    BotRank = 0,
                    SpawnPositionIndex = 0,
                    SkinID = (uint)champion.SkinID,
                    Name = clientInfo.Name,
                    Skin = champion.Model,

                };
                SendSpawnPacket(userId, champion, heroPacket, true);
            }





            //test
            clientInfo.Champion.SetStatus(StatusFlags.CanMove, true);

        }

        public void NotifyS2C_CreateMinionCamp(NeutralMinionCamp monsterCamp, int userId, TeamId team)
        {

            var packet = new S2C_Neutral_Camp_Empty
            {
                PlayerNetID = (uint)userId,
                CampIndex = (byte)monsterCamp.CampIndex,
                State = false,
                SenderNetID = 0,
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }

        }

        /// <summary>
        /// Disables the U.I when the game ends
        /// </summary>
        /// <param name="player"></param>
        public void NotifyS2C_DisableHUDForEndOfGame()
        {

            _packetHandlerManager.BroadcastPacket(new S2C_DisableHUDForEndOfGame(), Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends packets to all players notifying the result of a match (Victory or defeat)
        /// </summary>
        /// <param name="losingTeam">The Team that lost the match</param>
        /// <param name="time">The offset for the result to actually be displayed</param>
        public void NotifyS2C_EndGame(TeamId winningTeam)
        {
            //TODO: Provide support for more than 2 teams
            var gameEndPacket = new S2C_EndGame
            {
                IsTeamOrderWin = winningTeam == TeamId.TEAM_ORDER
            };
            _packetHandlerManager.BroadcastPacket(gameEndPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_HandleCapturePointUpdate(int capturePointIndex, uint otherNetId, int PARType, int attackTeam, CapturePointUpdateCommand capturePointUpdateCommand)
        {
            //TODO: Provide support for more than 2 teams
            var packet = new S2C_HandleCapturePointUpdate
            {
                CpIndex = (byte)capturePointIndex,
                OtherNetID = otherNetId,
                ParType = (byte)PARType,
                AttackTeam = (byte)attackTeam,
                Command = (byte)capturePointUpdateCommand
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, PacketFlags.RELIABLE);
        }

        /// <summary>
        /// Notifies the game about a score
        /// </summary>
        /// <param name="team"></param>
        /// <param name="score"></param>
        public void NotifyS2C_HandleGameScore(TeamId team, int score)
        {
            //TODO: Provide support for more than 2 teams
            var packet = new S2C_HandleGameScore
            {
                TeamID = (uint)team,
                Score = score
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, PacketFlags.RELIABLE);
        }

        /// <summary>
        /// Sends a side bar tip to the specified player (ex: quest tips).
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="title">Title of the tip.</param>
        /// <param name="text">Description text of the tip.</param>
        /// <param name="imagePath">Path to an image that will be embedded in the tip.</param>
        /// <param name="tipCommand">Action suggestion(? unconfirmed).</param>
        /// <param name="playerNetId">NetID to send the packet to.</param>
        /// <param name="targetNetId">NetID of the target referenced by the tip.</param>
        /// TODO: tipCommand should be a lib/core enum that gets translated into a league version specific packet enum as it may change over time.
        public void NotifyS2C_HandleTipUpdate(int userId, string title, string text, string imagePath, int tipCommand, uint playerNetId, uint tipID)
        {
            var packet = new S2C_HandleTipUpdate
            {
                SenderNetID = playerNetId,
                TipCommand = (byte)tipCommand,
                TipImagePath = imagePath,
                TipName = text,
                TipOther = title,
                TipID = tipID
            };
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }


        /// <summary>
        /// Sends a side bar tip to the specified player (ex: quest tips).
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="_Objective">_Objective.</param>
        /// <param name="_Tooltip">_Tooltip.</param>
        /// <param name="_Reward">_Reward</param>
        /// <param name="_QuestType">_QuestType.</param>
        /// <param name="Command">Command</param>
        /// <param name="HandleRollovers">HandleRollovers.</param>
        /// <param name="playerNetId">NetID to send the packet to.</param>
        /// <param name="QuestID">NetID of the target referenced by the tip.</param>
        /// TODO: tipCommand should be a lib/core enum that gets translated into a league version specific packet enum as it may change over time.
        public void NotifyS2C_HandleQuestUpdate(Quest _quest, byte _Command)
        {
            var packet = new S2C_HandleQuestUpdate
            {

                Objective = _quest.Objective,
                // Tooltip = _Tooltip,
                //  Reward = _Reward,
                QuestType = (byte)_quest.Type,
                Command = _Command,
                HandleRollovers = _quest.HandleRollovers,
                QuestID = (uint)_quest.QuestID

            };
            if (_Command == 2)
            {
                packet.Tooltip = _quest.Tooltip;
                packet.Reward = _quest.Reward;
            }
            if (_Command == 1)
            {
                packet.Tooltip = _quest.Tooltip;
            }
            _packetHandlerManager.BroadcastPacketTeam(_quest.Team, packet, Channel.CHL_S2C); //(userId, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the stats (CS, kills, deaths, etc) of the player who owns the specified Champion.
        /// </summary>
        /// <param name="champion">Champion owned by the player.</param>
        public void NotifyS2C_HeroStats(Champion champion)
        {
            //TODO: Find out what exactly this does and when/how it is sent

            var response = new S2C_HeroStats { Data = champion.ChampionStatistics.GetBytes() };
            _packetHandlerManager.BroadcastPacket(response, Channel.CHL_S2C);
        }

        public void NotifyS2C_IncrementPlayerScore(ScoreData scoreData)
        {
            var packet = new S2C_IncrementPlayerScore
            {
                PlayerNetID = scoreData.Owner.NetId,
                TotalPointValue = scoreData.Owner.ChampionStats.Score,
                PointValue = scoreData.Points,
                IsCallout = scoreData.DoCallOut,
                ScoreCategory = (byte)scoreData.ScoreCategory,
                ScoreEvent = (byte)scoreData.ScoreEvent
            };

            _packetHandlerManager.BroadcastPacketVision(scoreData.Owner, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified client's team detailing a map ping.
        /// </summary>
        /// <param name="client">Info of the client that initiated the ping.</param>
        /// <param name="pos">2D top-down position of the ping.</param>
        /// <param name="targetNetId">Target of the ping (if applicable).</param>
        /// <param name="type">Type of ping; COMMAND/ATTACK/DANGER/MISSING/ONMYWAY/FALLBACK/REQUESTHELP. *NOTE*: Not all ping types are supported yet.</param>
        public void NotifyS2C_MapPing(Vector2 pos, Pings type, uint targetNetId = 0, ClientInfo client = null)
        {
            var response = new S2C_MapPing
            {
                // TODO: Verify if this is correct. Usually 0.

                TargetNetID = targetNetId,
                PingCategory = (byte)type,
                Position = new Vector3(pos.X, 60, pos.Y),
                //Unhardcode these bools later
                PlayAudio = true,
                ShowChat = true,
                PingThrottled = false,
            };

            if (targetNetId != 0)
            {
                response.TargetNetID = targetNetId;
            }

            if (client != null)
            {
                response.SenderNetID = client.Champion.NetId;
                response.SourceNetID = client.Champion.NetId;
                _packetHandlerManager.BroadcastPacketTeam(client.Team, response, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.BroadcastPacket(response, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to the specified player which forces their camera to move to a specified point given certain parameters.
        /// </summary>
        /// <param name="player">Player who'll it's camera moved</param>
        /// <param name="startPosition">The starting position of the camera (Not yet known how to get it's values)</param>
        /// <param name="endPosition">End point to where the camera will move</param>
        /// <param name="travelTime">The time the camera will have to travel the given distance</param>
        /// <param name="startFromCurretPosition">Wheter or not it starts from current position</param>
        /// <param name="unlockCamera">Whether or not the camera is unlocked</param>
        public void NotifyS2C_MoveCameraToPoint(ClientInfo player, Vector3 startPosition, Vector3 endPosition, float travelTime = 0, bool startFromCurretPosition = true, bool unlockCamera = false)
        {
            var cam = new S2C_MoveCameraToPoint
            {
                SenderNetID = player.Champion.NetId,
                StartFromCurrentPosition = startFromCurretPosition,
                TravelTime = travelTime,
                TargetPosition = endPosition
            };
            if (startPosition != Vector3.Zero)
            {
                cam.StartPosition = startPosition;
            }

            _packetHandlerManager.SendPacket(player.ClientId, cam, Channel.CHL_S2C);
        }
        public void NotifyS2C_Neutral_Camp_Empty(NeutralMinionCamp neutralCamp, ObjAIBase? killer = null, int userId = -1)
        {
            //seem not used in cs 
            var packet = new S2C_Neutral_Camp_Empty
            {
                PlayerNetID = killer?.NetId ?? 0,
                CampIndex = (byte)neutralCamp.CampIndex,
                State = false,
                SenderNetID = 0,
            };
            if (userId < 0)
            {
                _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified unit has been killed by the specified killer.
        /// </summary>
        /// <param name="data">Data of the death.</param>
        public void NotifyS2C_NPC_Die_MapView(DeathData data)
        {
            NotifyNPC_Die_EventHistory(data.Unit as Champion, data);


            var dieMapView = new S2C_NPC_Hero_Die
            {
                SenderNetID = data.Unit.NetId,
                DeathData = new SiphoningStrike.Game.Common.DeathData
                {
                    BecomeZombie = data.BecomeZombie,
                    //DieType = (byte)data.DieType,
                    DamageType = (byte)data.DamageType,
                    SpellSourceType = (byte)data.DamageSource,
                    DeathDuration = data.DeathDuration
                }
            };

            if (data.Killer != null)
            {
                dieMapView.DeathData.KillerNetID = data.Killer.NetId;
            }

            _packetHandlerManager.BroadcastPacket(dieMapView, Channel.CHL_S2C);



        }

        public void NotifyOnEnterTeamVisibility(GameObject o, TeamId team, int userId)
        {
            var enterTeamVis = new S2C_OnEnterTeamVisiblity
            {
                SenderNetID = o.NetId,
                //TODO: Provide support for more than 2 teams
                VisiblityTeam = 0
            };
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                enterTeamVis.VisiblityTeam = 1;
            }

            _packetHandlerManager.SendPacket(userId, enterTeamVis, Channel.CHL_S2C);
        }



        public void NotifyOnEvent(IEvent gameEvent, AttackableUnit sender = null)
        {
            var packet = new S2C_OnEvent
            {
                Event = gameEvent
            };

            if (sender != null)
            {
                packet.SenderNetID = sender.NetId;
            }

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }
        public void NotifyOnEventDie(DeathData deathdata, AttackableUnit sender = null)
        {
            var gameevent = new OnChampionDie();


            var packet = new S2C_OnEvent
            {
                Event = gameevent
            };

            if (sender != null)
            {
                packet.SenderNetID = sender.NetId;
            }

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }


        /// <summary>
        /// Sends a packet to all players that announces a specified message (ex: "Minions have spawned.")
        /// </summary>
        /// <param name="eventId">Id of the event to happen.</param>
        /// <param name="sourceNetID">Not yet know it's use.</param>
        public void NotifyS2C_OnEventWorld(IEvent mapEvent, AttackableUnit? source = null)
        {
            if (mapEvent == null)
            {
                return;
            }

            var packet = new S2C_OnEventWorld
            {
                EventWorld = new EventWorld
                {
                    Event = mapEvent,
                    Source = source?.NetId ?? 0
                }
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }


        /// <summary>
        /// Sends a packet to either all players with vision of the specified GameObject or a specified user.
        /// The packet contains details of which team lost visibility of the GameObject and should only be used after it is first initialized into vision (NotifyEnterVisibility).
        /// </summary>
        /// <param name="o">GameObject going out of vision.</param>
        /// <param name="userId">User to send the packet to.</param>
        public void NotifyOnLeaveTeamVisibility(GameObject o, TeamId team, int userId = -1)
        {
            var leaveTeamVis = new S2C_OnLeaveTeamVisiblity
            {
                SenderNetID = o.NetId,
                //TODO: Provide support for more than 2 teams
                VisiblityTeam = 0
            };
            if (team == TeamId.TEAM_CHAOS || team == TeamId.TEAM_NEUTRAL)
            {
                leaveTeamVis.VisiblityTeam = 1;
            }

            if (userId < 0)
            {
                // TODO: Verify if we should use BroadcastPacketTeam instead.
                _packetHandlerManager.BroadcastPacket(leaveTeamVis, Channel.CHL_S2C);
            }
            else
            {
                _packetHandlerManager.SendPacket(userId, leaveTeamVis, Channel.CHL_S2C);
            }
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified object's current animations have been paused/unpaused.
        /// </summary>
        /// <param name="obj">GameObject that is playing the animation.</param>
        /// <param name="pause">Whether or not to pause/unpause animations.</param>
        public void NotifyS2C_PauseAnimation(GameObject obj, bool pause)
        {
            var animPacket = new S2C_PauseAnimation
            {
                SenderNetID = obj.NetId,
                Pause = pause
            };

            _packetHandlerManager.BroadcastPacket(animPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified object detailing that it is playing the specified animation.
        /// </summary>
        /// <param name="obj">GameObject that is playing the animation.</param>
        /// <param name="animation">Internal name of the animation to play.</param>
        /// TODO: Implement AnimationFlags enum for this and fill it in.
        /// <param name="flags">Animation flags. Refer to AnimationFlags enum.</param>
        /// <param name="timeScale">How fast the animation should play. Default 1x speed.</param>
        /// <param name="startTime">Time in the animation to start at.</param>
        /// TODO: Verify if this description is correct, if not, correct it.
        /// <param name="speedScale">How much the speed of the GameObject should affect the animation.</param>
        public void NotifyS2C_PlayAnimation(GameObject obj, string animation, AnimationFlags flags = 0, float timeScale = 1.0f, float startTime = 0.0f, float speedScale = 1.0f)
        {
            var packet = new S2C_PlayAnimation
            {
                SenderNetID = obj.NetId,
                Flags = (byte)flags,
                ScaleTime = timeScale,
                AnimationName = animation
            };
            _packetHandlerManager.BroadcastPacketVision(obj, packet, Channel.CHL_S2C);
        }

        public void NotifyS2C_UnlockAnimation(GameObject obj, string name)
        {
            /*
            var packet = new S2C_UnlockAnimation()
            {
                AnimationName = name
            };
            //TODO: Handle animation like fades
            _packetHandlerManager.BroadcastPacketVision(obj, packet, Channel.CHL_S2C);
            */
        }

        /// <summary>
        /// Sends a packet to all players detailing an emotion that is being performed by the unit that owns the specified netId.
        /// </summary>
        /// <param name="type">Type of emotion being performed; DANCE/TAUNT/LAUGH/JOKE/UNK.</param>
        /// <param name="netId">NetID of the unit performing the emotion.</param>
        public void NotifyS2C_PlayEmote(Emotions type, uint netId)
        {
            // convert type
            EmoteID targetType;
            switch (type)
            {
                case Emotions.DANCE:
                    targetType = EmoteID.Dance;
                    break;
                case Emotions.TAUNT:
                    targetType = EmoteID.Taunt;
                    break;
                case Emotions.LAUGH:
                    targetType = EmoteID.Laugh;
                    break;
                case Emotions.JOKE:
                    targetType = EmoteID.Joke;
                    break;
                case Emotions.UNK:
                    targetType = (EmoteID)type;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var packet = new S2C_PlayEmote
            {
                SenderNetID = netId,
                EmoteID = (byte)targetType
            };
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        public void NotifyS2C_PlaySound(string soundName, AttackableUnit soundOwner)
        {
            //UNKNOWN
            /*
            var packet = new S2C_PlaySound
            {
                SoundName = soundName,
                OwnerNetID = soundOwner.NetId
            };

            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
            */
        }

        /// <summary>
        /// Sends a packet to the specified player which is meant as a response to the players query about the status of the game.
        /// </summary>
        /// <param name="userId">User to send the packet to; player that sent the query.</param>
        public void NotifyS2C_QueryStatusAns(int userId)
        {
            var response = new SiphoningStrike.Game.S2C_QueryStatusAns
            {
                IsOK = true
            };
            _packetHandlerManager.SendPacket(userId, response, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified unit detailing that its animation states have changed to the specified animation pairs.
        /// Replaces the unit's normal animation behaviors with the given animation pairs. Structure of the animationPairs is expected to follow the same structure from before the replacement.
        /// </summary>
        /// <param name="u">AttackableUnit to change.</param>
        /// <param name="animationPairs">Dictionary of animations to set.</param>
        public void NotifyS2C_SetAnimStates(AttackableUnit u, Dictionary<string, string> animationPairs)
        {
            var setAnimPacket = new S2C_SetAnimStates
            {
                SenderNetID = u.NetId,
                AnimationOverrides = animationPairs
            };

            _packetHandlerManager.BroadcastPacket(setAnimPacket, Channel.CHL_S2C);
        }

        public void NotifyS2C_SetGreyscaleEnabledWhenDead(ClientInfo client, bool enabled)
        {
            var packet = new S2C_SetGreyscaleEnabledWhenDead
            {
                Enabled = enabled,
                SenderNetID = client.Champion.NetId
            };

            _packetHandlerManager.SendPacket(client.ClientId, packet, Channel.CHL_S2C);

        }

        /// <summary>
        /// Sends a packet to the specified user detailing that the spell in the given slot has had its spelldata changed to the spelldata of the given spell name.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="netId">NetId of the unit that owns the spell being changed.</param>
        /// <param name="spellName">Internal name of the spell to grab spell data from (to set).</param>
        /// <param name="slot">Slot of the spell being changed.</param>
        public void NotifyS2C_SetSpellData(int userId, uint netId, string spellName, int slot)
        {
            if (slot < 4)//just an test for see if it crash
            {
                var spellDataPacket = new S2C_SetSpellData
                {
                    SenderNetID = netId,
                    UnitNetID = netId,
                    SpellNameHash = HashString(spellName),
                    SpellSlot = (byte)slot
                };

                _packetHandlerManager.SendPacket(userId, spellDataPacket, Channel.CHL_S2C);
            }

        }
        public void NotifyS2C_UnitSetSpellPARCost(ObjAIBase unit, Spell spell, int costType, int userId = -1)
        {
            /*  float cooldownDiff = spell.CurrentCooldown - spell.GetCooldown();
              float manaCost =
                  spell.CastInfo.ManaCosts[Math.Min(spell.CastInfo.SpellLevel, spell.CastInfo.ManaCosts.Length - 1)];
              float manaDiff = (spell.CastInfo.ManaCost - manaCost) / manaCost;
              var packet = new S2C_SetSpellData()
              {
                  SenderNetID = unit.NetId,
                  SpellSlot = spell.CastInfo.SpellSlot,
                  //  CostType = (byte)costType,
                  //  Amount = costType == 1 ? cooldownDiff : manaDiff
              };

              _packetHandlerManager.SendPacket(userId, packet.GetBytes(), Channel.CHL_S2C);
            */
        }
        /// <summary>
        /// Sends a packet to the specified player detailing that the level of the spell in the given slot has changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="netId">NetId of the unit that owns the spell being changed.</param>
        /// <param name="slot">Slot of the spell being changed.</param>
        /// <param name="level">New level of the spell to set.</param>
        public void NotifyS2C_SetSpellLevel(int userId, uint netId, int slot, int level)
        {

            /*    var spellLevelPacket = new S2C_LevelUpSpell
                {

                    SenderNetID = netId,
                    SpellSlot = (byte)slot,
                  //  SpellLevel = level
                };

                _packetHandlerManager.SendPacket(userId, spellLevelPacket, Channel.CHL_S2C);
                */
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the game has started the spawning GameObjects that occurs at the start of the game.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        public void NotifyS2C_StartSpawn(int userId, TeamId team, List<ClientInfo> players)
        {
            var start = new SiphoningStrike.Game.S2C_StartSpawn
            {
                BotCountOrder = 0,
                BotCountChaos = 0
            };

            foreach (var player in players)
            {
                if (player.Champion.IsBot)
                {
                    if (team is TeamId.TEAM_ORDER)
                    {
                        start.BotCountOrder++;
                    }
                    else
                    {
                        start.BotCountChaos++;
                    }
                }
            }

            _packetHandlerManager.SendPacket(userId, start, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified object has stopped playing an animation.
        /// </summary>
        /// <param name="obj">GameObject that is playing the animation.</param>
        /// <param name="animation">Internal name of the animation to stop.</param>
        /// <param name="stopAll">Whether or not to stop all animations. Only works if animation is empty/null.</param>
        /// <param name="fade">Whether or not the animation should fade before stopping.</param>
        /// <param name="ignoreLock">Whether or not locked animations should still be stopped.</param>
        public void NotifyS2C_StopAnimation(GameObject obj, string animation, bool stopAll = false, bool fade = false, bool ignoreLock = true)
        {
            var animPacket = new S2C_StopAnimation
            {
                SenderNetID = obj.NetId,
                Fade = fade,
                IgnoreLock = ignoreLock,
                StopAll = stopAll
            };

            _packetHandlerManager.BroadcastPacket(animPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing spell tooltip parameters that the game does not inform automatically.
        /// </summary>
        /// <param name="data">The list of changed tool tip values.</param>
        public void NotifyS2C_ToolTipVars(List<ToolTipData> data)
        {

            var copydata = data;

            List<TooltipVars> variables = new();


            foreach (var tip in copydata)
            {
                var vars = new TooltipVars()
                {
                    OwnerNetID = tip.NetID,
                    SlotIndex = (byte)tip.Slot
                };

                for (var x = 0; x < 3; x++)
                {
                    //Unk
                    //vars.HideFromEnemy[x] = tip.Values[x].Hide;
                    vars.Values[x] = tip.Values[x].Value;
                }

                variables.Add(vars);

            }

            var answer = new S2C_ToolTipVars
            {
                TooltipVarsList = variables
            };

            _packetHandlerManager.BroadcastPacket(answer, Channel.CHL_S2C, PacketFlags.NONE);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified attacker that it is looking at (targeting) the specified attacked unit with the given AttackType.
        /// </summary>
        /// <param name="attacker">Unit that is attacking.</param>
        /// <param name="target">Unit that is being attacked.</param>
        public void NotifyS2C_UnitSetLookAt(AttackableUnit attacker, LookAtType lookAtType, AttackableUnit? target, Vector3 targetPosition = default)
        {
            if (attacker is LaneMinion)
            {

            }
            else
            {
                var packet = new S2C_FaceDirection
                {
                    SenderNetID = attacker.NetId,
                    //  LookAtType = (byte)lookAtType,
                    //test
                    Direction = target?.Position3D ?? targetPosition  //targetPosition,
                                                                      //  TargetNetID = target?.NetId ?? 0
                };
                _packetHandlerManager.BroadcastPacketVision(attacker, packet, Channel.CHL_S2C);

            }

        }

        public void NotifyS2C_UpdateAscended(ObjAIBase ascendant = null)
        {
            //DOESNT EXIST IN .131
            /*
            var packet = new S2C_UpdateAscended();
            if (ascendant != null)
            {
                packet.AscendedNetID = ascendant.NetId;
            }
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, PacketFlags.NONE);
            */
        }

        /// <summary>
        /// Sends a packet to all players detailing the attack speed cap overrides for this game.
        /// </summary>
        /// <param name="overrideMax">Whether or not to override the maximum attack speed cap.</param>
        /// <param name="maxAttackSpeedOverride">Value to override the maximum attack speed cap.</param>
        /// <param name="overrideMin">Whether or not to override the minimum attack speed cap.</param>
        /// <param name="minAttackSpeedOverride">Value to override the minimum attack speed cap.</param>
        public void NotifyS2C_UpdateAttackSpeedCapOverrides(bool overrideMax, float maxAttackSpeedOverride, bool overrideMin, float minAttackSpeedOverride, AttackableUnit unit = null)
        {
            //DOESNT EXIST IN .131
            /*
            var overridePacket = new S2C_UpdateAttackSpeedCapOverrides
            {
                DoOverrideMax = overrideMax,
                DoOverrideMin = overrideMin,
                MaxAttackSpeedOverride = maxAttackSpeedOverride,
                MinAttackSpeedOverride = minAttackSpeedOverride
            };
            if (unit != null)
            {
                overridePacket.SenderNetID = unit.NetId;
            }
            _packetHandlerManager.BroadcastPacket(overridePacket, Channel.CHL_S2C);
            */
        }

        /// <summary>
        /// Sends a packet to all players with vision of the given bounce missile that it has updated (unit/position).
        /// </summary>
        /// <param name="p">Missile that has been updated.</param>
        public void NotifyS2C_UpdateBounceMissile(SpellMissile p)
        {
            //UNKNOWN
            /*
            var packet = new S2C_UpdateBounceMissile()
            {
                SenderNetID = p.NetId,
                TargetNetID = p.TargetUnit!.NetId,
                CasterPosition = p.Position3D
            };
            _packetHandlerManager.BroadcastPacketVision(p, packet, Channel.CHL_S2C);
            */
        }

        public void NotifyS2C_LineMissileHitList(SpellLineMissile p, IEnumerable<AttackableUnit> units)
        {
            var packet = new S2C_LineMissileHitList()
            {
                SenderNetID = p.NetId,
                TargetNetIDs = units.Select(u => u.NetId).ToList()
            };
            _packetHandlerManager.BroadcastPacketVision(p, packet, Channel.CHL_S2C);
        }

        public void NotifyS2C_ChainMissileSync(SpellChainMissile p)
        {
            var packet = new S2C_ChainMissileSync()
            {
                OwnerNetworkID = p.SpellOrigin.Caster.NetId,
                SenderNetID = p.NetId,
                TargetCount = 1,
            };
            packet.TargetNetIDs[0] = p.TargetUnit!.NetId;
            _packetHandlerManager.BroadcastPacketVision(p, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players updating a champion's death timer.
        /// </summary>
        /// <param name="champion">Champion that died.</param>
        public void NotifyS2C_UpdateDeathTimer(Champion champion)
        {
            //UNKNOWN
            /*
            var cdt = new S2C_UpdateDeathTimer { SenderNetID = champion.NetId, DeathDuration = champion.Stats.RespawnTimer / 1000f };
            _packetHandlerManager.BroadcastPacket(cdt, Channel.CHL_S2C);
            */
        }

        /// <summary>
        /// Sends a packet to the specified user detailing that the specified spell's toggle state has been updated.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="s">Spell being updated.</param>
        public void NotifyS2C_UpdateSpellToggle(int userId, Spell s)
        {
            //UNKNOWN
            /*
            var spellTogglePacket = new S2C_ToggleInputLockingFlag
            {
                SenderNetID = s.Caster.NetId,
                InputLockingFlags = s.Slot
            };

            _packetHandlerManager.SendPacket(userId, spellTogglePacket, Channel.CHL_S2C);
            */
        }

        public void NotifySystemMessage(ClientInfo sender, ChatType chatType, string message)
        {
            var packet = new S2C_DisplayLocalizedTutorialChatText
            {
                Message = message,
            };

            switch (chatType)
            {
                case ChatType.All:
                    _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
                    break;
                case ChatType.Team:
                    _packetHandlerManager.BroadcastPacketTeam(sender.Team, packet, Channel.CHL_S2C);
                    break;
                case ChatType.Private:
                    _packetHandlerManager.SendPacket((int)sender.PlayerId, packet, Channel.CHL_S2C);
                    break;
            }
        }

        public void NotifyChatPacket(ClientInfo sender, ChatType chatType, string message)
        {
            var packet = new ChatPacket
            {
                ClientID = (uint)sender.ClientId,
                ChatType = (uint)chatType,
                Message = message,
            };
            switch (chatType)
            {
                case ChatType.All:
                    _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_COMMUNICATION);
                    break;
                case ChatType.Team:
                    _packetHandlerManager.BroadcastPacketTeam(sender.Team, packet, Channel.CHL_COMMUNICATION);
                    break;
                case ChatType.Private:
                    _packetHandlerManager.SendPacket((int)sender.PlayerId, packet, Channel.CHL_COMMUNICATION);
                    break;
            }
        }

        public void NotifyS2C_UnitSetMinimapIcon(int userId, AttackableUnit unit, bool changeIcon, bool changeBorder)
        {
            var packet = new S2C_UnitSetMinimapIcon
            {
                //used for dominion tower 
                UnitNetID = unit.NetId,
                IconName = unit.IconInfo.BorderCategory
            };
            _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that the specified unit's team has been set.
        /// </summary>
        /// <param name="unit">AttackableUnit who's team has been set.</param>
        public void NotifySetTeam(AttackableUnit unit)
        {
            //TODO: Provide support for more than 2 teams
            var p = new S2C_UnitChangeTeam
            {
                SenderNetID = 0,
                UnitNetID = unit.NetId,
                TeamID = (uint)unit.Team // TODO: Verify if TeamID is actually supposed to be a uint
            };
            _packetHandlerManager.BroadcastPacket(p, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the spawning (of champions & buildings) that occurs at the start of the game has ended.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        public void NotifySpawnEnd(int userId)
        {
            var endSpawnPacket = new SiphoningStrike.Game.S2C_EndSpawn();
            _packetHandlerManager.SendPacket(userId, endSpawnPacket, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players with vision of the specified Champion detailing that the Champion's items have been swapped.
        /// </summary>
        /// <param name="c">Champion who swapped their items.</param>
        /// <param name="sourceSlot">Slot the item was previously in.</param>
        /// <param name="destinationSlot">Slot the item was swapped to.</param>
        public void NotifySwapItemAns(Champion c, int sourceSlot, int destinationSlot)
        {
            //TODO: reorganize in alphabetic order
            var swapItem = new S2C_SwapItemAns
            {
                SenderNetID = c.NetId,
                Source = (byte)sourceSlot,
                Destination = (byte)destinationSlot
            };
            _packetHandlerManager.BroadcastPacketVision(c, swapItem, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the amount of time since the game started (in seconds). Used to initialize the user's in-game timer.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="time">Time since the game started (in milliseconds).</param>
        public void NotifySyncMissionStartTimeS2C(int userId, float time)
        {
            var sync = new S2C_SyncMissionStartTime()
            {
                StartTime = time / 1000.0f
            };

            _packetHandlerManager.SendPacket(userId, sync, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing the amount of time since the game started (in seconds).
        /// </summary>
        /// <param name="gameTime">Time since the game started (in milliseconds).</param>
        public void NotifySynchSimTimeS2C(float gameTime)
        {
            var sync = new S2C_SynchSimTime()
            {
                SynchTime = gameTime / 1000.0f
            };

            _packetHandlerManager.BroadcastPacket(sync, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the amount of time since the game started (in seconds).
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="time">Time since the game started (in milliseconds).</param>
        public void NotifySynchSimTimeS2C(int userId, float time)
        {
            var sync = new S2C_SynchSimTime()
            {
                SynchTime = time / 1000.0f
            };

            _packetHandlerManager.SendPacket(userId, sync, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing the results of server's the version and game info check for the specified player.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="players">List of ClientInfo of all players set to connect to the game.</param>
        /// <param name="version">Version of the player being checked.</param>
        /// <param name="gameMode">String of the internal name of the gamemode being played.</param>
        /// <param name="mapId">ID of the map being played.</param>
        public void NotifySynchVersion(int userId, TeamId team, List<ClientInfo> players, string version, string gameMode, GameFeatures gameFeatures, int mapId, string[] mutators)
        {
            if (Game.Config.VersionOfClient == "1.0.0.126" || Game.Config.VersionOfClient == "1.0.0.131")
            {
                var syncVersion = new S2C_SynchVersion
                {
                    // TODO: Unhardcode all booleans below
                    IsVersionOK = true,
                    // Logs match to file.
                    // WriteToClientFile = false,
                    // Whether or not this game is considered a match.
                    // MatchedGame = true,
                    // Unknown
                    // DradisInit = false,

                    MapToLoad = mapId,
                    VersionString = Game.Config.VersionOfClient,
                    MapMode = gameMode,
                    // TODO: Unhardcode all below
                    //PlatformID = "NA1",
                    //  MutatorsNum = 0,
                    //  OrderRankedTeamName = "",
                    //  OrderRankedTeamTag = "",
                    // ChaosRankedTeamName = "",
                    // ChaosRankedTeamTag = "",
                    // site.com
                    //  MetricsServerWebAddress = "",
                    // /messages
                    //   MetricsServerWebPath = "",
                    // 80
                    //  MetricsServerPort = 0,
                    // site.com
                    //  DradisProdAddress = "",
                    // /messages
                    //  DradisProdResource = "",
                    // 80
                    //  DradisProdPort = 0,
                    // test-lb-#.us-west-#.elb.someaws.com
                    //  DradisTestAddress = "",
                    // /messages
                    //   DradisTestResource = "",
                    // 80
                    //    DradisTestPort = 0,
                    // TODO: Create a new TipConfig class and use it here (basically, unhardcode this).
                    /*    TipConfig = new TipConfig
                        {
                            TipID = 0,
                            ColorID = 0,
                            DurationID = 0,
                            Flags = 3
                        },
                        GameFeatures = (ulong)gameFeatures,*/
                };

                for (int i = 0; i < players.Count; i++)
                {
                    // Protection contre le dépassement d'index - limite à 10 joueurs maximum
                    if (i >= syncVersion.PlayerInfo.Length)
                    {
                        Console.WriteLine($"[DEBUG] Ignoring player {i} (PlayerInfo array full, max {syncVersion.PlayerInfo.Length} players)");
                        break; // Sortir de la boucle si on dépasse la taille du tableau
                    }

                    var player = players[i];
                    var info = new PlayerLoadInfo
                    {
                        PlayerID = (ulong)player.PlayerId,
                        // TODO: Change to players[i].Item2.SummonerLevel
                        SummonorLevel = 30,
                        SummonorSpell1 = HashString(player.SummonerSkills[0]),
                        SummonorSpell2 = HashString(player.SummonerSkills[1]),
                        // TODO
                        IsBot = false,
                        TeamId = (uint)player.Team,
                        BotName = "",
                        BotSkinName = "",
                        // EloRanking = player.Rank,
                        // BotSkinID = 0,
                        BotDifficulty = 0,
                        ProfileIconId = player.Icon,
                        // TODO: Unhardcode these two.
                        // AllyBadgeID = 0,
                        // EnemyBadgeID = 0
                    };

                    if (player.Champion.IsBot)
                    {
                        info.IsBot = true;
                        //TODO: Fix the display of summoner spells
                        info.BotName = player.Champion.Model + "\u0000";
                        info.BotSkinName = player.Champion.Model + "\u0000";
                        // info.BotSkinID = player.Champion.SkinID;
                        info.BotDifficulty = 1; //todo unhardcode them with difficultflag
                        info.ProfileIconId = 1;
                    }

                    syncVersion.PlayerInfo[i] = info;
                }

                byte mutatorCount = 0;
                /*  for (byte i = 0; i < mutators.Length && i < 8; i++)
                  {
                      syncVersion.Mutators[mutators[i] is null ? mutatorCount : mutatorCount++] = mutators[i];
                  }
                  syncVersion.MutatorsNum = mutatorCount;
                */
                // TODO: syncVersion.Mutators

                // TODO: syncVersion.DisabledItems

                // TODO: syncVersion.EnabledDradisMessages

                _packetHandlerManager.SendPacket(userId, syncVersion, Channel.CHL_S2C);
            }
            else if (Game.Config.VersionOfClient == "1.0.0.132")
            {
                /*  var syncVersion = new S2C_SynchVersion_131
                  {
                      // TODO: Unhardcode all booleans below
                      IsVersionOK = true,
                      // Logs match to file.
                      // WriteToClientFile = false,
                      // Whether or not this game is considered a match.
                      // MatchedGame = true,
                      // Unknown
                      // DradisInit = false,

                      MapToLoad = mapId,
                      VersionString = "1.0.0.131",
                      MapMode = gameMode,
                      // TODO: Unhardcode all below
                      //PlatformID = "NA1",
                      //  MutatorsNum = 0,
                      //  OrderRankedTeamName = "",
                      //  OrderRankedTeamTag = "",
                      // ChaosRankedTeamName = "",
                      // ChaosRankedTeamTag = "",
                      // site.com
                      //  MetricsServerWebAddress = "",
                      // /messages
                      //   MetricsServerWebPath = "",
                      // 80
                      //  MetricsServerPort = 0,
                      // site.com
                      //  DradisProdAddress = "",
                      // /messages
                      //  DradisProdResource = "",
                      // 80
                      //  DradisProdPort = 0,
                      // test-lb-#.us-west-#.elb.someaws.com
                      //  DradisTestAddress = "",
                      // /messages
                      //   DradisTestResource = "",
                      // 80
                      //    DradisTestPort = 0,
                      // TODO: Create a new TipConfig class and use it here (basically, unhardcode this).
                          TipConfig = new TipConfig
                          {
                              TipID = 0,
                              ColorID = 0,
                              DurationID = 0,
                              Flags = 3
                          },
                          GameFeatures = (ulong)gameFeatures,
                  };

                  for (int i = 0; i < players.Count; i++)
                  {
                      var player = players[i];
                      var info = new PlayerLoadInfo_131
                      {
                          PlayerID = (ulong)player.PlayerId,
                          // TODO: Change to players[i].Item2.SummonerLevel
                          SummonorLevel = 30,
                          SummonorSpell1 = (uint)HashString(player.SummonerSkills[0]),
                          SummonorSpell2 = (uint)HashString(player.SummonerSkills[1]),
                          // TODO
                          IsBot = false,
                          TeamId = (uint)player.Team,
                          BotName = "",
                          BotSkinName = "",
                          // EloRanking = player.Rank,
                          // BotSkinID = 0,
                          BotDifficulty = 0,
                          ProfileIconId = player.Icon,
                          // TODO: Unhardcode these two.
                          // AllyBadgeID = 0,
                          // EnemyBadgeID = 0
                      };

                      if (player.Champion.IsBot)
                      {
                          info.IsBot = true;
                          //TODO: Fix the display of summoner spells
                          info.BotName = player.Champion.Model + "\u0000";
                          info.BotSkinName = player.Champion.Model + "\u0000";
                          // info.BotSkinID = player.Champion.SkinID;
                          info.BotDifficulty = 1; //todo unhardcode them with difficultflag
                          info.ProfileIconId = 1;
                      }

                      syncVersion.PlayerInfo_131[i] = info;
                  }

                  byte mutatorCount = 0;
                    for (byte i = 0; i < mutators.Length && i < 8; i++)
                    {
                        syncVersion.Mutators[mutators[i] is null ? mutatorCount : mutatorCount++] = mutators[i];
                    }
                    syncVersion.MutatorsNum = mutatorCount;

                  // TODO: syncVersion.Mutators

                  // TODO: syncVersion.DisabledItems

                  // TODO: syncVersion.EnabledDradisMessages

                  _packetHandlerManager.SendPacket(userId, syncVersion, Channel.CHL_S2C); */
            }

            else
            {
                var syncVersion = new S2C_SynchVersion
                {
                    // TODO: Unhardcode all booleans below
                    IsVersionOK = true,
                    // Logs match to file.
                    // WriteToClientFile = false,
                    // Whether or not this game is considered a match.
                    // MatchedGame = true,
                    // Unknown
                    // DradisInit = false,

                    MapToLoad = mapId,
                    VersionString = "1.0.0.126",
                    MapMode = gameMode,
                    // TODO: Unhardcode all below
                    //PlatformID = "NA1",
                    //  MutatorsNum = 0,
                    //  OrderRankedTeamName = "",
                    //  OrderRankedTeamTag = "",
                    // ChaosRankedTeamName = "",
                    // ChaosRankedTeamTag = "",
                    // site.com
                    //  MetricsServerWebAddress = "",
                    // /messages
                    //   MetricsServerWebPath = "",
                    // 80
                    //  MetricsServerPort = 0,
                    // site.com
                    //  DradisProdAddress = "",
                    // /messages
                    //  DradisProdResource = "",
                    // 80
                    //  DradisProdPort = 0,
                    // test-lb-#.us-west-#.elb.someaws.com
                    //  DradisTestAddress = "",
                    // /messages
                    //   DradisTestResource = "",
                    // 80
                    //    DradisTestPort = 0,
                    // TODO: Create a new TipConfig class and use it here (basically, unhardcode this).
                    /*    TipConfig = new TipConfig
                        {
                            TipID = 0,
                            ColorID = 0,
                            DurationID = 0,
                            Flags = 3
                        },
                        GameFeatures = (ulong)gameFeatures,*/
                };

                for (int i = 0; i < players.Count; i++)
                {
                    // Protection contre le dépassement d'index - limite à 10 joueurs maximum
                    if (i >= syncVersion.PlayerInfo.Length)
                    {
                        Console.WriteLine($"[DEBUG] Ignoring player {i} (PlayerInfo array full, max {syncVersion.PlayerInfo.Length} players)");
                        break; // Sortir de la boucle si on dépasse la taille du tableau
                    }

                    var player = players[i];
                    var info = new PlayerLoadInfo
                    {
                        PlayerID = (ulong)player.PlayerId,
                        // TODO: Change to players[i].Item2.SummonerLevel
                        SummonorLevel = 30,
                        SummonorSpell1 = HashString(player.SummonerSkills[0]),
                        SummonorSpell2 = HashString(player.SummonerSkills[1]),
                        // TODO
                        IsBot = false,
                        TeamId = (uint)player.Team,
                        BotName = "",
                        BotSkinName = "",
                        // EloRanking = player.Rank,
                        // BotSkinID = 0,
                        BotDifficulty = 0,
                        ProfileIconId = player.Icon,
                        // TODO: Unhardcode these two.
                        // AllyBadgeID = 0,
                        // EnemyBadgeID = 0
                    };

                    if (player.Champion.IsBot)
                    {
                        info.IsBot = true;
                        //TODO: Fix the display of summoner spells
                        info.BotName = player.Champion.Model + "\u0000";
                        info.BotSkinName = player.Champion.Model + "\u0000";
                        // info.BotSkinID = player.Champion.SkinID;
                        info.BotDifficulty = 1; //todo unhardcode them with difficultflag
                        info.ProfileIconId = 1;
                    }

                    syncVersion.PlayerInfo[i] = info;
                }

                byte mutatorCount = 0;
                /*  for (byte i = 0; i < mutators.Length && i < 8; i++)
                  {
                      syncVersion.Mutators[mutators[i] is null ? mutatorCount : mutatorCount++] = mutators[i];
                  }
                  syncVersion.MutatorsNum = mutatorCount;
                */
                // TODO: syncVersion.Mutators

                // TODO: syncVersion.DisabledItems

                // TODO: syncVersion.EnabledDradisMessages


                _packetHandlerManager.SendPacket(userId, syncVersion, Channel.CHL_S2C);

            }
        }




        /// <summary>
        /// Sends a packet to the specified player detailing the status (results) of a surrender vote that was called for and ended.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="surrendererTeam">TeamId that called for the surrender vote; BLUE/PURPLE/NEUTRAL.</param>
        /// <param name="reason">SurrenderReason of why the vote ended.</param>
        /// <param name="yesVotes">Number of votes for the surrender.</param>
        /// <param name="noVotes">Number of votes against the surrender.</param>
        public void NotifyTeamSurrenderStatus(int userId, TeamId userTeam, TeamId surrendererTeam, SurrenderReason reason, int yesVotes, int noVotes)
        {
            var surrenderStatus = new S2C_TeamSurrenderStatus()
            {
                Reason = (uint)reason,
                ForVote = (byte)yesVotes,
                AgainstVote = (byte)noVotes,
                TeamID = (uint)surrendererTeam,
            };
            _packetHandlerManager.SendPacket(userId, surrenderStatus, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players on the same team as the Champion that made the surrender vote detailing what vote was made.
        /// </summary>
        /// <param name="starter">Champion that made the surrender vote.</param>
        /// <param name="open">Whether or not to automatically open the surrender voting menu.</param>
        /// <param name="votedYes">Whether or not voting for the surrender is still available.</param>
        /// <param name="yesVotes">Number of players currently for the surrender.</param>
        /// <param name="noVotes">Number of players currently against the surrender.</param>
        /// <param name="maxVotes">Maximum number of votes possible.</param>
        /// <param name="timeOut">Time until voting becomes unavailable.</param>
        public void NotifyTeamSurrenderVote(Champion starter, bool open, bool votedYes, int yesVotes, int noVotes, int maxVotes, float timeOut)
        {
            var surrender = new S2C_TeamSurrenderVote()
            {
                PlayerNetID = starter.NetId,
                OpenVoteMenu = open,
                VoteYes = votedYes,
                ForVote = (byte)yesVotes,
                AgainstVote = (byte)noVotes,
                NumPlayers = (byte)maxVotes,
                TeamID = (uint)starter.Team,
                TimeOut = timeOut,
            };
            _packetHandlerManager.BroadcastPacketTeam(starter.Team, surrender, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players detailing that their screen's tint is shifting to the specified color.
        /// </summary>
        /// <param name="team">TeamID to apply the tint to.</param>
        /// <param name="enable">Whether or not to fade in the tint.</param>
        /// <param name="speed">Amount of time that should pass before tint is fully applied.</param>
        /// <param name="color">Color of the tint.</param>
        public void NotifyTint(TeamId team, bool enable, float speed, float maxWeight, Color color)
        {
            var c = new MirrorImage.Game.Common.Color
            {
                Blue = color.B,
                Green = color.G,
                Red = color.R,
                Alpha = color.A
            };
            //TODO: Provide support for more than 2 teams
            var tint = new S2C_ColorRemapFX
            {
                IsFadingIn = enable,
                FadeTime = speed,
                TeamID = (uint)team,
                Color = c,
                MaxWeight = maxWeight
            };
            _packetHandlerManager.BroadcastPacket(tint, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to a specific player detailing that their screen's tint is shifting to the specified color.
        /// </summary>
        /// <param name="champ">champion to apply the tint to.</param>
        /// <param name="enable">Whether or not to fade in the tint.</param>
        /// <param name="speed">Amount of time that should pass before tint is fully applied.</param>
        /// <param name="color">Color of the tint.</param>
        public void NotifyTintPlayer(Champion champ, bool enable, float speed, Color color)
        {
            var c = new MirrorImage.Game.Common.Color
            {
                Blue = color.B,
                Green = color.G,
                Red = color.R,
                Alpha = color.A
            };

            var tint = new S2C_ColorRemapFX
            {
                IsFadingIn = enable,
                FadeTime = speed,
                TeamID = (uint)champ.Team.FromTeamId(),
                Color = c,
                MaxWeight = c.Alpha / 255.0f
            };
            _packetHandlerManager.SendPacket(champ.ClientId, tint, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that the specified Champion has gained the specified amount of experience.
        /// </summary>
        /// <param name="champion">Champion that gained the experience.</param>
        /// <param name="experience">Amount of experience gained.</param>
        /// TODO: Verify if sending to all players is correct.
        public void NotifyUnitAddEXP(Champion champion, float experience)
        {
            var xp = new S2C_UnitAddEXP
            {
                TargetNetID = champion.NetId,
                ExpAmmount = experience
            };
            // TODO: Verify if we should change to BroadcastPacketVision
            _packetHandlerManager.BroadcastPacket(xp, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to all players that the specified Champion has killed a specified player and received a specified amount of gold.
        /// </summary>
        /// <param name="c">Champion that killed a unit.</param>
        /// <param name="died">AttackableUnit that died to the Champion.</param>
        /// <param name="gold">Amount of gold the Champion gained for the kill.</param>
        /// TODO: Only use BroadcastPacket when the unit that died is a Champion.
        public void NotifyUnitAddGold(Champion target, GameObject source, float gold)
        {
            // TODO: Verify if this handles self-gold properly.
            var ag = new S2C_UnitAddGold
            {
                SenderNetID = source.NetId,
                TargetNetID = target.NetId,
                SourceNetID = source.NetId,
                GoldAmmount = gold
            };
            _packetHandlerManager.SendPacket(target.ClientId, ag, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to optionally all players (given isGlobal), a specified user that is the source of damage, or a specified user that is receiving the damage. The packet details an instance of damage being applied to a unit by another unit.
        /// </summary>
        /// <param name="isGlobal">Whether or not the packet should be sent to all players.</param>
        /// <param name="sourceId">ID of the user who dealt the damage that should receive the packet.</param>
        /// <param name="targetId">ID of the user who is taking the damage that should receive the packet.</param>
        public void NotifyUnitApplyDamage(DamageData damageData, bool isGlobal = true)
        {
            var damagePacket = new S2C_UnitApplyDamage
            {
                DamageResultType = (byte)damageData.DamageResultType,
                //DamageType = (byte)damageData.DamageType,
                TargetNetID = damageData.Target.NetId,
                SourceNetID = damageData.Attacker.NetId,
                Damage = MathF.Ceiling(damageData.Damage),
                //Sender isn't always the unit itself, sometimes missiles
                SenderNetID = damageData.Target.NetId,
                HasAttackSound = false //TODO
            };

            if (isGlobal)
            {
                _packetHandlerManager.BroadcastPacket(damagePacket, Channel.CHL_S2C);
            }
            else
            {
                // todo: check if damage dealt by disconnected players cause anything bad
                if (damageData.Attacker is Champion attackerChamp)
                {
                    _packetHandlerManager.SendPacket(attackerChamp.ClientId, damagePacket, Channel.CHL_S2C);
                }
                else if (damageData.Attacker is Pet pet && pet.Owner is Champion ch)
                {
                    _packetHandlerManager.SendPacket(ch.ClientId, damagePacket, Channel.CHL_S2C);
                }

                if (damageData.Target is Champion targetChamp)
                {
                    _packetHandlerManager.SendPacket(targetChamp.ClientId, damagePacket, Channel.CHL_S2C);
                }
            }
        }

        public void NotifyUpdateLevelPropS2C(UpdateLevelPropData propData)
        {
            var packet = new S2C_UpdateLevelProp
            {
                UpdateLevelPropData = propData
            };
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a notification that the object has entered the team's scope and fully synchronizes its state.
        /// </summary>
        public void NotifyEnterTeamVision(AttackableUnit u, int userId = -1, BasePacket? spawnPacket = null)
        {

            if (spawnPacket is not S2C_OnEnterVisiblityClient visibilityPacket)
            {
                List<BasePacket>? packets = null;
                if (spawnPacket != null)
                {
                    packets = new List<BasePacket>(1) { spawnPacket };
                }
                visibilityPacket = ConstructEnterVisibilityClientPacket(u, packets);
            }

            S2C_OnEnterLocalVisiblityClient healthbarPacket = new()
            {
                SenderNetID = u.NetId,
                MaxHealth = u.Stats.HealthPoints.Total,
                Health = u.Stats.CurrentHealth,
            };

            if (spawnPacket is not null)
            {
                _packetHandlerManager.SendPacket(userId, spawnPacket, Channel.CHL_S2C);
            }
            _packetHandlerManager.SendPacket(userId, visibilityPacket, Channel.CHL_S2C);
            _packetHandlerManager.SendPacket(userId, healthbarPacket, Channel.CHL_S2C);





            if (u.Replication is not null)
            {
                //TODO: try to include it to packets too?
                //TODO: hold until replication?
                S2C_OnReplication us = new()
                {
                    SyncID = Environment.TickCount,
                    ReplicationData = new List<ReplicationData>(1)
                    {
                        u.Replication.GetData(false)
                    }
                };
                if (u.Replication.Values != null)
                {
                    var pktflag = GetPacketFlagsForFrequency();
                    _packetHandlerManager.SendPacket(userId, us, Channel.CHL_S2C, pktflag);


                }

            }
        }

        private PacketFlags GetPacketFlagsForFrequency()
        {
            // Si la fréquence est trop élevée, utiliser UNSEQUENCED pour éviter les retards
            // qui causent l'effet de "téléportation" des minions
            if (Game.Time.CurrentHz > 60.0f)
            {
                return PacketFlags.UNSEQUENCED;
            }

            return PacketFlags.RELIABLE;
        }

        /// <summary>
        /// Creates a package and puts it in the queue that will be emptied with the NotifyWaypointGroup call.
        /// </summary>
        /// <param name="u">AttackableUnit that is moving.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="useTeleportID">Whether or not to teleport the unit to its current position in its path.</param>
        public void HoldMovementDataUntilWaypointGroupNotification(AttackableUnit u, int userId, bool useTeleportID = false)
        {
            var data = PacketExtensions126.CreateMovementDataNormal(u, _navGrid, useTeleportID);

            List<MovementDataNormal> list = null;
            if (!_heldMovementData.TryGetValue(userId, out list))
            {
                _heldMovementData[userId] = list = new List<MovementDataNormal>();
            }
            if (data != null)
                list.Add(data);
        }

        /// <summary>
        /// Sends all packets queued by HoldMovementDataUntilWaypointGroupNotification and clears queue.
        /// </summary>
        public void NotifyWaypointGroup()
        {
            var pktflag = GetPacketFlagsForFrequency();

            foreach (var kv in _heldMovementData)
            {
                int userId = kv.Key;
                var list = kv.Value;

                if (list.Count > 0)
                {

                    var packet = new S2C_WaypointGroup
                    {
                        SyncID = Environment.TickCount,
                        Movements = list
                    };
                    // _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
                    _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C, pktflag);

                    list.Clear();
                }
            }
        }

        /// <summary>
        /// Creates a package and puts it in the queue that will be emptied with the NotifyOnReplication call.
        /// </summary>
        /// <param name="u">Unit who's stats have been updated.</param>
        /// <param name="userId">UserId to send the packet to. If not specified or zero, the packet is broadcasted to all players that have vision of the specified unit.</param>
        /// <param name="partial">Whether or not the packet should only include stats marked as changed.</param>
        public void HoldReplicationDataUntilOnReplicationNotification(AttackableUnit u, int userId, bool partial = true)
        {
            var data = u.Replication.GetData(partial);

            List<ReplicationData> list = null;
            if (!_heldReplicationData.TryGetValue(userId, out list))
            {
                _heldReplicationData[userId] = list = new List<ReplicationData>();
            }
            list.Add(data);
        }

        /// <summary>
        /// Sends all packets queued by HoldReplicationDataUntilOnReplicationNotification and clears queue.
        /// </summary>
        public void NotifyOnReplication()
        {
            foreach (var kv in _heldReplicationData)
            {
                int userId = kv.Key;
                var list = kv.Value;

                if (list.Count > 0)
                {
                    var packet = new S2C_OnReplication()
                    {
                        SyncID = Environment.TickCount,
                        ReplicationData = list
                    };

                    // save the packet in list 

                    _replicationAcknowledgments[userId] = new Dictionary<int, List<ReplicationData>>();
                    _replicationAcknowledgments[userId][Environment.TickCount] = list;


                    /*if(packet.ReplicationData.First().Values != null)
                    {
                        _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_S2C, PacketFlags.UNSEQUENCED);
                    }*/
                    var pktflag = GetPacketFlagsForFrequency();
                    _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C, pktflag);

                    list.Clear();
                }
            }


            CleanupOldAcknowledgments();





        }
        //this need to be tested properly 
        public void ResendUnacknowledgedPackets(int syncID)
        {
            foreach (var playerId in _replicationAcknowledgments.Keys)
            {

                if (_replicationAcknowledgments[playerId].ContainsKey(syncID))
                {
                    var packet = new S2C_OnReplication()
                    {
                        SyncID = syncID,
                        ReplicationData = _replicationAcknowledgments[playerId][syncID]
                    };

                    var pktflag = GetPacketFlagsForFrequency();
                    _packetHandlerManager.SendPacket(playerId, packet, Channel.CHL_S2C, pktflag);
                    if (Game.Config.EnableLogPKT)
                    {
                        Console.WriteLine($"resend the pckt {syncID} at {playerId}");
                    }
                }
            }
        }

        private const int TimeoutThreshold = 5000; // Timeout pour les confirmations en millisecondes

        private void CleanupOldAcknowledgments()
        {
            var now = Environment.TickCount;

            foreach (var userId in _replicationAcknowledgments.Keys.ToList())
            {
                foreach (var syncID in _replicationAcknowledgments[userId].Keys.ToList())
                {
                    if (now - syncID > TimeoutThreshold)
                    {
                        // if an player doesn't have receive the packet , resend 
                        ResendUnacknowledgedPackets(syncID);
                        if (Game.Config.EnableLogPKT)
                        {
                            Console.WriteLine($"Timeout : SyncID {syncID} for {userId}, resend.");
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Sends a packet to all players that have vision of the specified unit.
        /// The packet details a group of waypoints with speed parameters which determine what kind of movement will be done to reach the waypoints, or optionally a GameObject.
        /// Functionally referred to as a dash in-game.
        /// </summary>
        /// <param name="u">Unit that is dashing.</param>
        /// TODO: Implement ForceMovement class which houses these parameters, then have that as the only parameter to this function (and other Dash-based functions).
        public void NotifyWaypointGroupWithSpeed(AttackableUnit u)
        {
            if (u is not BaseTurret && u is not LaneTurret && u is not ObjBuilding)
            {
                // TODO: Implement Dash class and house a List of these with waypoints.
                var md = PacketExtensions126.CreateMovementDataWithSpeed(u, _navGrid);

                var speedWpGroup = new S2C_WaypointGroupWithSpeed
                {
                    SyncID = Environment.TickCount,
                    // TOOD: Implement support for multiple speed-based movements (functionally known as dashes).
                    Movements = new List<MovementDataWithSpeed> { md }
                };
                var pktflag = GetPacketFlagsForFrequency();
                _packetHandlerManager.BroadcastPacketVision(u, speedWpGroup, Channel.CHL_LOW_PRIORITY, pktflag);

            }
        }
        public void NotifyWaypointListWithSpeed
     (
         AttackableUnit u,
         float dashSpeed,
         float leapGravity = 0,
         bool keepFacingLastDirection = false,
         GameObject target = null,
         float followTargetMaxDistance = 0,
         float backDistance = 0,
         float travelTime = 0
     )
        {
            // TODO: Implement ForceMovement class/interface and house a List of these with waypoints.
            var speeds = new SpeedParams
            {
                PathSpeedOverride = dashSpeed,
                ParabolicGravity = leapGravity,
                // TODO: Implement as parameter (ex: Aatrox Q).
                ParabolicStartPoint = u.Position,
                Facing = keepFacingLastDirection,
                FollowNetID = 0,
                FollowDistance = followTargetMaxDistance,
                FollowBackDistance = backDistance,
                FollowTravelTime = travelTime
            };

            if (target != null)
            {
                speeds.FollowNetID = target.NetId;
            }

            var speedWpGroup = new S2C_WaypointListHeroWithSpeed
            {
                SenderNetID = u.NetId,
                SyncID = Environment.TickCount,
                // TOOD: Implement support for multiple speed-based movements (functionally known as dashes).
                SpeedParams = speeds,
                Waypoints = u.Waypoints
            };
            var pktflag = GetPacketFlagsForFrequency();
            _packetHandlerManager.BroadcastPacketVision(u, speedWpGroup, Channel.CHL_LOW_PRIORITY, pktflag);
        }
        public void NotifyWaypointList(GameObject obj, List<Vector2> waypoints)
        {
            var wpList = new S2C_WaypointList
            {
                SenderNetID = obj.NetId,
                SyncID = Environment.TickCount,
                Waypoints = waypoints
            };
            var pktflag = GetPacketFlagsForFrequency();
            _packetHandlerManager.BroadcastPacketVision(obj, wpList, Channel.CHL_LOW_PRIORITY, pktflag);
        }
        /*  public void NotifyWaypointGroup(AttackableUnit u, int userId = 0, bool useTeleportID = false)
          {
              var move = PacketExtensions.CreateMovementDataNormal(u, _navGrid, useTeleportID);

              // TODO: Implement support for multiple movements.
              var packet = new S2C_WaypointGroup
              {
                  SyncID = Environment.TickCount,
                  Movements = new List<MovementDataNormal>() { move }
              };

              if (userId <= 0)
              {
                  _packetHandlerManager.BroadcastPacketVision(u, packet, Channel.CHL_LOW_PRIORITY);
              }
              else
              {
                  _packetHandlerManager.SendPacket(userId, packet, Channel.CHL_LOW_PRIORITY);
              }
          } */
        /// <summary>
        /// Sends a packet to the specified player detailing that their request to view something with their camera has been acknowledged.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="request">ViewRequest housing information about the camera's view.</param>
        /// TODO: Verify if this is the correct implementation.
        public void NotifyWorld_SendCamera_Server_Acknowledgement(ClientInfo client, SiphoningStrike.Game.C2S_World_SendCamera_Server request)
        {
            var answer = new S2C_World_SendCamera_Server_Ack
            {
                //TODO: Check these values
                SenderNetID = client.Champion.NetId,
                SyncID = request.SyncID,
            };
            _packetHandlerManager.SendPacket(client.ClientId, answer, Channel.CHL_S2C, PacketFlags.NONE);
        }
        /// <summary>
        /// Sends a packet that an unit's health bar has been hidden/shown
        /// Upon testing, I found that only the 'show' matters or we are not sending it properly.
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="show">To show the health bar or not</param>
        /// <param name="observerTeamId">Which team should see the changes</param>
        /// <param name="changeHealthBarType">Whether to change the health bar type</param>
        /// <param name="healthBarType">The type of health bar to show/hide</param>
        public void NotifyS2C_ShowHealthBar(AttackableUnit unit, bool show, TeamId observerTeamId = TeamId.TEAM_UNKNOWN, bool changeHealthBarType = false, HealthBarType healthBarType = HealthBarType.Invalid)
        {
            var showHealthBar = new S2C_ShowHealthBar
            {
                Show = show,
                SenderNetID = unit.NetId,
            };
            _packetHandlerManager.BroadcastPacketVision(unit, showHealthBar, Channel.CHL_S2C);
        }

        /// <summary>
        /// Sends a packet to the the unit that an item has ben successfully used
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="slot">The slot of the iem that has been used</param>
        /// <param name="itemsInSlot">How many stacks of the item remain</param>
        /// <param name="spellCharges">How many spell charges of the item are left</param>
        public void NotifyUseItemAns(ObjAIBase unit, byte slot, int itemsInSlot, int spellCharges = 0)
        {
            var useItemAns = new S2C_UseItemAns
            {
                SenderNetID = unit.NetId,
                Slot = slot,
                ItemsInSlot = (byte)itemsInSlot,
                SpellCharges = (byte)spellCharges,
            };
            _packetHandlerManager.BroadcastPacketVision(unit, useItemAns, Channel.CHL_S2C);
        }

        /// <summary>
        /// todo:
        /// </summary>
        /// <param name="unit">Target unit</param>
        public void NotifySetCircularMovementRestriction(ObjAIBase unit, Vector3 center, float radius, bool restrictcam = false)
        {
            var CircularMovementRestriction = new S2C_SetCircularMovementRestriction
            {
                SenderNetID = unit.NetId,
                Center = center,
                RestrictCamera = restrictcam,
                Radius = radius
            };
            _packetHandlerManager.BroadcastPacketVision(unit, CircularMovementRestriction, Channel.CHL_S2C);

        }
        /// <summary>
        /// todo:
        /// </summary>
        /// <param name="unit">Target unit</param>
        public void NotifyAttachFlexParticle(ObjAIBase unit, byte _ParticleFlexID, byte _CpIndex, uint _ParticleAttachType)
        {
            var AttachFlexParticle = new S2C_AttachFlexParticle
            {
                SenderNetID = unit.NetId,
                FlexID = _ParticleFlexID,
                CpIndex = _CpIndex,
                AttachType = (byte)_ParticleAttachType
            };

            _packetHandlerManager.BroadcastPacket(AttachFlexParticle, Channel.CHL_S2C, PacketFlags.RELIABLE);
        }
        /// <summary>
        /// todo:
        /// </summary>
        /// <param name="unit">Target unit</param>
        public void S2C_InteractiveMusicCommand(ObjAIBase unit, byte _MusicCommand, uint _MusicEventAudioEventID, uint _MusicParamAudioEventID)
        {
            /*    var InteractiveMusicCommand = S2C_MusicCueCommand
                {
                    SenderNetID = unit.NetId,
                    MusicCommand = _MusicCommand,
                    MusicEventAudioEventID = _MusicEventAudioEventID,
                    MusicParamAudioEventID = _MusicParamAudioEventID
                };

                _packetHandlerManager.BroadcastPacket(InteractiveMusicCommand, Channel.CHL_S2C, PacketFlags.NONE); */
        }
        /// <summary>
        /// todo:
        /// </summary>
        /// <param name="unit">Target unit</param>
        public void Notify_WriteNavFlags(Vector2 position, float radius, NavigationGridCellFlags flags)
        {
            var pktWriteNavFlags = new S2C_WriteNavFlags
            {
                NavFlagCricles = new List<NavFlagCricle>(),
                SyncID = Environment.TickCount,

            };
            pktWriteNavFlags.NavFlagCricles.Add(
                new NavFlagCricle()
                {
                    Position = position,
                    Radius = radius,
                    Flags = (uint)flags,
                }
                );

            _packetHandlerManager.BroadcastPacket(pktWriteNavFlags, Channel.CHL_S2C, PacketFlags.NONE);
        }

        /// <summary>
        /// Sends a packet to the specified player detailing that the specified Debug Object's color has changed.
        /// </summary>
        /// <param name="userId">User to send the packet to.</param>
        /// <param name="sender">NetId of the GameObject responsible for this packet being sent.</param>
        /// <param name="objID">ID of the Debug Object.</param>
        /// <param name="r">Red hex value of the Debug Object.</param>
        /// <param name="g">Green hex value of the Debug Object.</param>
        /// <param name="b">Blue hex value of the Debug Object.</param>
        public void NotifyS2C_ChangePARColorOverride(Champion champ, byte r, byte g, byte b, byte a, byte fr, byte fg, byte fb, byte fa, int objID = 0)
        {
            var PARColorOverridepkt = new S2C_ChangePARColorOverride
            {
                SenderNetID = champ.NetId,
                UnitNetID = (uint)objID // seem always 0 in replay at first view ? 
            };

            PARColorOverridepkt.Enabled = true; //seem never false 

            var barcolor = new MirrorImage.Game.Common.Color();
            barcolor.Red = r;
            barcolor.Green = g;
            barcolor.Blue = b;
            barcolor.Alpha = a;

            var fadecolor = new MirrorImage.Game.Common.Color();
            fadecolor.Red = fr;
            fadecolor.Green = fg;
            fadecolor.Blue = fb;
            fadecolor.Alpha = fa;

            PARColorOverridepkt.BarColor = barcolor;
            PARColorOverridepkt.FadeColor = fadecolor;



            _packetHandlerManager.SendPacket(champ.ClientId, PARColorOverridepkt, Channel.CHL_S2C);
        }


        public void NotifySetFrequencyS2C(float number)
        {
            var packet = new S2C_SetFrequency
            {
                NewFrequency = number
            };
            _packetHandlerManager.BroadcastPacket(packet, Channel.CHL_S2C);
        }



    }
}
