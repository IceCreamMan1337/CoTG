using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.Scripting.CSharp;

namespace CoTG.CoTGServer.GameObjects
{
    public class AISquadList
    {
        private static int nextAISquadListID = 1;
        private readonly object lockObject = new();

        public string Squadname { get; set; }
        public int AISquadListID { get; private set; }
        public AISquadClass _aisquad { get; set; }


        public string icon { get; set; }

        public AISquadList(string _icon = "", string _Squadname = "", AISquadClass __aisquad = null)
        {
            lock (lockObject)
            {
                AISquadListID = nextAISquadListID++;
            }
            icon = _icon;
            Squadname = _Squadname;
            _aisquad = __aisquad;
        }


    }

    public class AISquadListManager
    {
        private ConcurrentDictionary<int, AISquadList> aisquadLists = new();




        public void AddAISquadList(AISquadList aisquadList)
        {
            aisquadLists.TryAdd(aisquadList.AISquadListID, aisquadList);

        }

        public AISquadList GetAISquadListByID(int aisquadListID)
        {
            if (aisquadLists.TryGetValue(aisquadListID, out AISquadList aisquadList))
            {
                return aisquadList;
            }
            else
            {
                // Gérez le cas où l'AISquadListID n'existe pas
                return null;
            }
        }

        public AISquadList GetAISquadListBySquadname(string squadname)
        {

            foreach (var aisquadList in aisquadLists.Values)
            {
                if (aisquadList.Squadname == squadname)
                {
                    return aisquadList;
                }
            }

            // Gérez le cas où le squadname n'a pas été trouvé
            return null;
        }
        public AISquadList GetAISquadListByiconcamp(string icon)
        {

            foreach (var aisquadList in aisquadLists.Values)
            {
                if (aisquadList.icon == icon)
                {
                    return aisquadList;
                }
            }

            // Gérez le cas où le squadname n'a pas été trouvé
            return null;
        }

        public AISquadList GetAISquadListByobjAIsquadClass(AISquadClass _AISquadClass)
        {

            foreach (var aisquadList in aisquadLists.Values)
            {

                if (aisquadList._aisquad == _AISquadClass)
                {
                    return aisquadList;
                }
            }

            // Gérez le cas où le squadname n'a pas été trouvé
            return null;
        }
        public AISquadList GetAISquadListByMinion(AttackableUnit minion)
        {
            foreach (var aisquadList in aisquadLists.Values)
            {
                if (aisquadList._aisquad.Members != null && aisquadList._aisquad.Members.Contains(minion))
                {
                    return aisquadList;
                }
            }

            // Gérez le cas où le minion n'a pas été trouvé dans une AISquadList
            return null;
        }

        public AISquadList GetAISquadListByMission(AIMissionClass AIMission)
        {
            foreach (var aisquadList in aisquadLists.Values)
            {
                if (aisquadList._aisquad.AssignedMission != null && aisquadList._aisquad.AssignedMission == AIMission)
                {
                    return aisquadList;
                }
            }

            // Gérez le cas où le minion n'a pas été trouvé dans une AISquadList
            return null;
        }

        public AIMissionClass RetrieveAIMission(AIMissionTopicType AIMission, AttackableUnit TargetUnit, Vector3 TargetLocation, int LaneID)
        {
            foreach (var aisquadList in aisquadLists.Values)
            {
                if (aisquadList._aisquad.AssignedMission != null)
                {
                    var themission = aisquadList._aisquad.AssignedMission;
                    if (themission.Topic == AIMission && themission.TargetUnit == TargetUnit && themission.TargetLocation == TargetLocation && themission.LaneID == LaneID)
                    {
                        return themission;
                    }

                }
            }

            // Gérez le cas où le minion n'a pas été trouvé dans une AISquadList
            return null;
        }



        /// <summary>
        /// Retourne tous les squads gérés par ce manager
        /// </summary>
        /// <returns>Collection de tous les AISquadList</returns>
        public IEnumerable<AISquadList> GetAllSquads()
        {
            return aisquadLists.Values;
        }

        /// <summary>
        /// Supprime un squad du manager
        /// </summary>
        /// <param name="aisquadListID">ID du squad à supprimer</param>
        /// <returns>True si le squad a été supprimé, false sinon</returns>
        public bool RemoveAISquadList(int aisquadListID)
        {
            return aisquadLists.TryRemove(aisquadListID, out _);
        }

        /// <summary>
        /// Supprime un squad du manager par son nom
        /// </summary>
        /// <param name="squadname">Nom du squad à supprimer</param>
        /// <returns>True si le squad a été supprimé, false sinon</returns>
        public bool RemoveAISquadListByName(string squadname)
        {
            var squad = GetAISquadListBySquadname(squadname);
            if (squad != null)
            {
                return RemoveAISquadList(squad.AISquadListID);
            }
            return false;
        }

        /// <summary>
        /// Retourne le nombre total de squads gérés
        /// </summary>
        /// <returns>Nombre de squads</returns>
        public int GetSquadCount()
        {
            return aisquadLists.Count;
        }
    }


    public class AISquadClass
    {
        public AISquad SquadId { get; set; }
        public int MaxMemberNum { get; set; }
        public float CreatedTime { get; set; }
        public List<AttackableUnit> Members { get; set; }

        public string Squadname { get; set; }

        public TeamId _teamId { get; set; }

        // ✅ Système d'exécution périodique pour les BehaviourTree
        private GameScriptTimer _squadBehaviourTreeTimer;
        private float _lastSquadExecutionTime = 0f;
        private float _accumulatedSquadTime = 0f; // Temps accumulé avec DeltaTime
        private const float SQUAD_EXECUTION_INTERVAL = 5.0f; // 5 secondes (maintenant en secondes)
        private bool _squadExecutionEnabled = false;

        public AIMissionClass AssignedMission { get; set; }

        public AISquadClass(string squadstring = "", int maxMemberNum = 0, TeamId team = TeamId.TEAM_UNKNOWN)
        {
            Members = new List<AttackableUnit>();
            MaxMemberNum = maxMemberNum;
            CreatedTime = Game.Time.GameTime;
            Squadname = squadstring;
            _teamId = team;

            // ✅ Activer automatiquement l'exécution des behaviour trees
            _squadExecutionEnabled = true;
        }


        public void AddUnittoSquad(AttackableUnit unit)
        {
            Members.Add(unit);
        }

        public void AddUnitListtoSquad(IEnumerable<AttackableUnit> Listunit)
        {
            Members.AddRange(Listunit);
        }

        public void RemoveUnittoSquad(AttackableUnit unit)
        {
            Members.Remove(unit);
        }


        public string GetSquadName()
        {
            if (AssignedMission != null)
            {
                return $"{AssignedMission.Topic} Squad";
            }
            return "Empty Squad";
        }
        public bool Isonealive()
        {
            try
            {
                if (Members == null)
                {
                    return false;
                }

                foreach (var entity in Members)
                {
                    if (entity == null)
                    {
                        continue; // Ignorer les entités null
                    }


                    bool isAlive = false;

                    isAlive = !entity.Stats.IsDead;


                    if (isAlive)
                    {
                        return true;
                    }
                }


                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public string GetSquadDescription()
        {
            return $"Squad {SquadId} - {GetSquadName()} - Members: {Members?.Count ?? 0}/{MaxMemberNum}";
        }

        /// <summary>
        /// Initialise le timer d'exécution périodique pour les BehaviourTree du squad
        /// </summary>
        private void InitializeSquadBehaviourTreeTimer()
        {
            try
            {
                // ✅ Créer un timer qui se répète toutes les 5 secondes
                _squadBehaviourTreeTimer = new GameScriptTimer(
                    SQUAD_EXECUTION_INTERVAL / 1000f, // Convertir en secondes
                    ExecuteSquadBehaviourTrees,
                    false, // Ne pas exécuter immédiatement
                    true   // Répéter indéfiniment
                );

                // ✅ Ajouter le timer à la liste des timers du jeu
                Game.AddGameScriptTimer(_squadBehaviourTreeTimer);

                _squadExecutionEnabled = true;

            }
            catch (Exception e)
            {
                _squadExecutionEnabled = false;
            }
        }

        /// <summary>
        /// Exécute les BehaviourTree de tous les membres du squad
        /// </summary>
        public void ExecuteSquadBehaviourTrees()
        {
            try
            {
                if (!_squadExecutionEnabled || Members == null || Members.Count == 0)
                {
                    return;
                }

                // Accumuler le temps avec ScaledDeltaTime (temps accéléré pour la simulation)
                _accumulatedSquadTime += Game.Time.ScaledDeltaTime / 1000f; // Convertir en secondes

                // Vérifier si c'est le moment d'exécuter les behaviour trees
                if (_accumulatedSquadTime - _lastSquadExecutionTime < SQUAD_EXECUTION_INTERVAL)
                {
                    return;
                }

                _lastSquadExecutionTime = _accumulatedSquadTime;

                // Exécuter les behaviour trees de tous les membres
                foreach (var member in Members)
                {
                    if (member is Champion champion && !champion.Stats.IsDead)
                    {
                        ExecuteMemberBehaviourTree(champion);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Exécute le BehaviourTree d'un membre spécifique du squad
        /// </summary>
        private void ExecuteMemberBehaviourTree(Champion champion)
        {
            try
            {
                if (champion == null || champion.Stats.IsDead)
                {
                    return;
                }

                // ✅ Utiliser le BehaviourTree de la mission (géré par AIMissionClass)
                var squadMissionBehaviorTree = AssignedMission?.BehaviourMissionAI;

                if (squadMissionBehaviorTree != null)
                {
                    // ✅ Vérifier si c'est un script de mission (qui a une méthode Execute())
                    if (squadMissionBehaviorTree.GetType().GetMethod("Execute") != null)
                    {
                        // C'est un script de mission, appeler Execute() directement
                        try
                        {
                            var missionScript = squadMissionBehaviorTree as dynamic;
                            bool result = missionScript.Execute();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {

                        try
                        {
                            squadMissionBehaviorTree.Update();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Active ou désactive l'exécution périodique des BehaviourTree du squad
        /// </summary>
        public void SetSquadExecutionEnabled(bool enabled)
        {
            _squadExecutionEnabled = enabled;
        }

        /// <summary>
        /// Nettoie les ressources du squad (appelé lors de la suppression)
        /// </summary>
        public void Cleanup()
        {
            try
            {
                if (_squadBehaviourTreeTimer != null)
                {
                    _squadBehaviourTreeTimer.EndTimerWithoutCallback();
                    _squadBehaviourTreeTimer = null;
                }

                _squadExecutionEnabled = false;

            }
            catch (Exception e)
            {
            }
        }
    }

    public class AIMissionClass
    {
        public AIMissionTopicType Topic { get; set; }
        public AttackableUnit TargetUnit { get; set; }
        public Vector3 TargetLocation { get; set; }
        public int LaneID { get; set; }
        public LogicResultType Status { get; set; }
        public int MissionID { get; set; }
        public AImission_bt BehaviourMissionAI { get; set; }
        private static int nextMissionID = 1;

        public AIMissionClass(AIMissionTopicType missionType, AttackableUnit targetUnit, Vector3 position, int lane)
        {
            Topic = missionType;
            TargetUnit = targetUnit;
            TargetLocation = position;
            LaneID = lane;
            Status = LogicResultType.RUNNING; // todo 
            MissionID = nextMissionID++;

            // ✅ Initialiser automatiquement le BehaviourTree pour cette mission

        }

        public string GetMissionName()
        {
            return Topic.ToString();
        }





    }
}




