using System;
using System.Numerics;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.Content.Navigation.aimesh;

namespace CoTG.CoTGServer.Content.Navigation
{
    public class Actor_Common
    {
        public Actor_Common_vtbl __vftable; // VFT
                                            // public Actor_Pimpl Pimpl;
        public Vector3 sideVec;
        public Vector3 newDestination;
        public Vector3 newPosition;
        public Vector3 miscVector;
        /* 
         public Loki_Functor<bool, Loki_Typelist<ulong, Loki_NullType>, Loki_SingleThreaded> fnPathOwnerVisible;
         public Loki_Functor<void, Loki_Typelist<r3dPoint3D, Loki_Typelist<float, Loki_Typelist<ulong, Loki_NullType>>>> fnSendStopS2C;
         public Loki_Functor<void, Loki_Typelist<r3dPoint3D, Loki_Typelist<float, Loki_Typelist<ulong, Loki_NullType>>>> fnSendStopC2S;
         public Loki_Functor<void, Loki_Typelist<string, Loki_NullType>, Loki_SingleThreaded> fnSendDebugMessage;
         public Loki_Functor<Actor_Common_OnCollisionEnum, Loki_Typelist<Actor_Common, Loki_Typelist<bool, Loki_NullType>>, Loki_SingleThreaded> m_OnCollision;
         public Loki_Functor<void, Loki_NullType, Loki_SingleThreaded> m_OnCollisionFree;
         public Loki_Functor<void, Loki_Typelist<bool, Loki_NullType>, Loki_SingleThreaded> m_OnOverideMoveStop;
         public Loki_Functor<void, Loki_NullType, Loki_SingleThreaded> m_OnActorStop;
        */
        public AIMesh m_Parent;
        //    public FrameTimer m_StuckTimer;
        //    public FrameTimer m_RepathTimer;
        //   public FrameTimer m_RepathOnDuplicate;
        //   public FrameTimer m_TimeBetweenPathUpdates;
        public bool m_ForceRefreshPath;
        public int m_RepathedCount;
        public NavigationGridCell m_CurrentGridCell;
        public NavigationGridLocator m_CurrentGridCellLocator;
        public Vector3 m_ServerSentPosition;
        public Vector3 m_Movement;
        public float m_MaxSpeed;
        public bool m_PathActive;
        //   public NavigationPath m_Path;
        public Vector3 m_GotoPosition;
        public Vector3 m_ClickedLocation;
        public Vector3 m_ParabolicStartPoint;
        public Vector3 m_LastTrackMovement;
        //  public GameTimer m_TrackStartTimer;
        public bool m_TrackPastUnit;
        public bool m_SuppressNearCheck;
        public bool m_FixedTravelPathCalculated;
        public float m_LastTimeDelta;
        public byte mLastTeleport;
        public bool m_ForceNextPositionToSnap;
        public float m_MaxCollisionAvoidanceRatio;
        public Actor_Common m_NextActorInGridCell;
        //     public BlockedMemory blockedMemory_;
        public bool mGhosted;
        public bool mGhostProof;
        public int mGettingOutOfCollisionGhosted;
        public bool mAvoidanceOn;
        public Vector3 mPositionWhenAddedAvoidance;
        public float mRadius;
        public uint m_ParentID;
        public string m_Name;
        public bool m_UseSlowerButMoreAccurateSearch;
        public Vector3 m_Position;
        public Vector3 m_PrevPosition;
        public Vector3 m_ObjPosition;
    }
    public struct Actor_Common_vtbl
    {
        public Action<Actor_Common, bool, AIManagerOwnerI> OnVisibilityStateChange;
        public Action<Actor_Common> OnCreate;
    }
    public struct AIManagerOwnerI
    {
        public AIManagerOwnerI_vtbl __vftable; // VFT
    }

    public struct AIManagerOwnerI_vtbl
    {
        public Action<AIManagerOwnerI> ClearTarget;
        public Action<AIManagerOwnerI> StartingMove;
        public Func<AIManagerOwnerI, Vector3> GetPosition;
        public Action<AIManagerOwnerI, Vector3> SetPosition;
        public Action<AIManagerOwnerI> HandleParabolicMovement;
        public Action<AIManagerOwnerI, GameObject> SetEnemyID;
        public Action<AIManagerOwnerI, GameObject> SetGoingToLastPositionEnemyID;
        public Func<AIManagerOwnerI, bool> IsHero;
        public Action<AIManagerOwnerI, ChannelingStopCondition, ChannelingStopSource, bool, bool> ChannelingStop;
        public Action<AIManagerOwnerI, uint> ProcessEvent;
        //  public Func<AIManagerOwnerI, CallForHelp> GetCallForHelp;
        public Action<AIManagerOwnerI, GameObject> FollowUnit;
        public Action<AIManagerOwnerI> ClearPredictionPath;
    }

}
