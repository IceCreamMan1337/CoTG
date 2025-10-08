using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_NPC_IssueOrderReq : PacketHandlerBase<C2S_NPC_IssueOrderReq>
    {
        public async Task ValidateAndFixWaypoints(List<Vector2> waypoints, ObjAIBase unit)
        {
            var nav = Game.Map.NavigationGrid;

            //TODO: Find the nearest point on the path and discard everything before it
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                if (await nav.CastCircleAsync(waypoints[i], waypoints[i + 1], unit.PathfindingRadius, true))
                {
                    var ithWaypoint = waypoints[i];
                    var lastWaypoint = waypoints[waypoints.Count - 1];
                    var path = nav.GetPath(ithWaypoint, lastWaypoint, unit);
                    waypoints.RemoveRange(i, waypoints.Count - i);
                    if (path != null)
                    {
                        waypoints.AddRange(path);
                    }
                    else
                    {
                        waypoints.Add(ithWaypoint);
                    }



                    break;
                }
            }
        }
        public override bool HandlePacket(int userId, C2S_NPC_IssueOrderReq req)
        {
            if (req.MovementData.Waypoints == null)
            {
                return false;
            }

            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            if (peerInfo == null || req == null)
            {
                return true;
            }

            var champion = peerInfo.Champion;
            var nav = Game.Map.NavigationGrid;



            var target = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;
            var pet = champion.GetPet();
            List<Vector2> waypoints = null;
            List<Vector2> waypoints2 = null;
            ObjAIBase unit = champion;

            var orderType = (OrderType)req.OrderType;


            var vector2Waypoints = req.MovementData.Waypoints.Select(w => new Vector2(w.X, w.Y));

            switch (orderType)
            {
                case OrderType.PetHardMove:
                case OrderType.PetHardAttack:
                case OrderType.PetHardReturn:
                    unit = pet;
                    goto case OrderType.MoveTo;
                case OrderType.MoveTo:
                case OrderType.AttackTo:
                case OrderType.AttackMove:
                case OrderType.Use:

                    // if (unit.MovementParameters == null && req.MovementData.Waypoints != null && req.MovementData.Waypoints.Count > 0)
                    // {
                    //     waypoints = req.MovementData.Waypoints.ConvertAll(PacketExtensions.WaypointToVector2);
                    //     for (int i = 0; i < waypoints.Count; i++)
                    //     {
                    //         waypoints[i] = PacketExtensions.TranslateFromCenteredCoordinates(waypoints[i], nav);
                    //     }
                    //     ValidateAndFixWaypoints(waypoints, unit);
                    // }



                    //  if (unit.MovementParameters == null && vector2Waypoints != null && vector2Waypoints.Count() > 0)
                    //  {
                    //      waypoints = vector2Waypoints.Select(w => TranslateFromCenteredCoordinates(w)).ToList();
                    //      ValidateAndFixWaypoints(waypoints, unit);
                    //      
                    //  }
                    if (unit != null)
                    {
                        // Check if the unit can change waypoints (not casting non-cancellable spells)
                        if (!unit.CanChangeWaypoints())
                        {
                            // Reject the movement order - client will not update visually
                            return false;
                        }

                        unit.IssueOrDelayOrder(orderType, target, req.Position.ToVector2(), waypoints);
                    }

                    break;
                case OrderType.Taunt:
                case OrderType.Stop:
                    champion.IssueOrDelayOrder(orderType, null, Vector2.Zero);
                    break;
                case OrderType.PetHardStop:
                    if (pet != null)
                    {
                        pet.UpdateMoveOrder(orderType, true);
                    }
                    break;
            }

            return true;
        }
        /*
        public override bool HandlePacket(int userId, C2S_NPC_IssueOrderReq req)
        {
            if (req.MovementData.Waypoints == null)
            {
                return false;
            }

            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            if (peerInfo == null || req == null)
            {
                return true;
            }

            var champion = peerInfo.Champion;
            var nav = Game.Map.NavigationGrid;

            var target = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;
            var pet = champion.GetPet();
            List<Vector2> waypoints = null;

            ObjAIBase unit = champion;

            var orderType = (OrderType)req.OrderType;

            
            var vector2Waypoints = req.MovementData.Waypoints.Select(w => new Vector2(w.X,w.Y));

            switch (orderType)
            {
                case OrderType.PetHardMove:
                case OrderType.PetHardAttack:
                case OrderType.PetHardReturn:
                    unit = pet;
                    goto case OrderType.MoveTo;
                case OrderType.MoveTo:
                case OrderType.AttackTo:
                case OrderType.AttackMove:
                case OrderType.Use:
                    if (unit.MovementParameters == null && vector2Waypoints != null && vector2Waypoints.Count() > 0)
                    {
                        waypoints = vector2Waypoints.ToList();//.Select(w => TranslateFromCenteredCoordinates(w)).ToList();
                       // ValidateAndFixWaypoints(waypoints, unit);
                    }
                    unit.IssueOrDelayOrder(orderType, target, req.Position.ToVector2(), waypoints);
                    break;
                case OrderType.Taunt:
                case OrderType.Stop:
                    champion.IssueOrDelayOrder(orderType, null, Vector2.Zero);
                    break;
                case OrderType.PetHardStop:
                    if (pet != null)
                    {
                        pet.UpdateMoveOrder(orderType, true);
                    }
                    break;
            }

            return true;
        }*/

        private Vector2 TranslateFromCenteredCoordinates(Vector2 vector)
        {
            // For some reason, League coordinates are translated into center-based coordinates (origin at the center of the map),
            // so we have to translate them back into normal coordinates where the origin is at the bottom left of the map.
            return (2 * vector) + Game.Map.NavigationGrid.MiddleOfMap;
        }
    }
}
