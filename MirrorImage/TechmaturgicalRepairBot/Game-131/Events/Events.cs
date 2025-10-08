using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;
namespace

 TechmaturgicalRepairBot.Game.Events
{
    public class OnDie : ArgsDie, IEvent
    {
        public EventID ID => EventID.OnDie;
    }
    public class OnKill : ArgsBase, IEventEmptyHistory
    {
        public EventID ID => EventID.OnKill;
    }
    public class OnChampionDie : ArgsDie, IEventEmptyHistory
    {
        public EventID ID => EventID.OnChampionDie;
    }
    public class OnChampionKill : ArgsBase, IEventEmptyHistory
    {
        public EventID ID => EventID.OnChampionKill;
    }
    public class OnChampionDoubleKill : ArgsBase, IEventEmptyHistory // 9
    {
        public EventID ID => EventID.OnChampionDoubleKill;
    }
    public class OnChampionTripleKill : ArgsBase, IEventEmptyHistory // 10
    {
        public EventID ID => EventID.OnChampionTripleKill;
    }
    public class OnChampionQuadraKill : ArgsBase, IEventEmptyHistory // 11
    {
        public EventID ID => EventID.OnChampionQuadraKill;
    }
    public class OnChampionPentaKill : ArgsBase, IEventEmptyHistory // 12
    {
        public EventID ID => EventID.OnChampionPentaKill;
    }
    public class OnChampionUnrealKill : ArgsBase, IEventEmptyHistory // 13
    {
        public EventID ID => EventID.OnChampionUnrealKill;
    }
    public class OnFirstBlood : ArgsBase, IEventEmptyHistory // 14
    {
        public EventID ID => EventID.OnFirstBlood;
    }
    public class OnDamageTaken : ArgsBase, IEvent // 15
    {
        public EventID ID => EventID.OnDamageTaken;
    }
    public class OnDamageGiven : ArgsBase, IEvent // 16
    {
        public EventID ID => EventID.OnDamageGiven;
    }
    public class OnSpellCast1 : ArgsBase, IEventEmptyHistory // 17
    {
        public EventID ID => EventID.OnSpellCast1;
    }
    public class OnSpellCast2 : ArgsBase, IEventEmptyHistory // 18
    {
        public EventID ID => EventID.OnSpellCast2;
    }
    public class OnSpellCast3 : ArgsBase, IEventEmptyHistory // 19
    {
        public EventID ID => EventID.OnSpellCast3;
    }
    public class OnSpellCast4 : ArgsBase, IEventEmptyHistory // 20
    {
        public EventID ID => EventID.OnSpellCast4;
    }
    public class OnSpellAvatarCast1 : ArgsBase, IEventEmptyHistory // 21
    {
        public EventID ID => EventID.OnSpellAvatarCast1;
    }
    public class OnSpellAvatarCast2 : ArgsBase, IEventEmptyHistory // 22
    {
        public EventID ID => EventID.OnSpellAvatarCast2;
    }
    public class OnGoldSpent : ArgsGoldSpent, IEvent // 23
    {
        public EventID ID => EventID.OnGoldSpent;
    }
    public class OnGoldEarned : ArgsGoldEarned, IEvent // 24
    {
        public EventID ID => EventID.OnGoldEarned;
    }
    public class OnItemConsumeablePurchased : ArgsItemConsumedPurchased, IEvent // 25
    {
        public EventID ID => EventID.OnItemConsumeablePurchased;
    }
    public class OnCriticalStrike : ArgsDamageCriticalStrike, IEvent // 26
    {
        public EventID ID => EventID.OnCriticalStrike;
    }
    public class OnAce : ArgsBase, IEventEmptyHistory // 27
    {
        public EventID ID => EventID.OnAce;
    }
    public class OnReincarnate : ArgsBase, IEventEmptyHistory // 28
    {
        public EventID ID => EventID.OnReincarnate;
    }
    public class OnDampenerKill : ArgsBase, IEventEmptyHistory // 30
    {
        public EventID ID => EventID.OnDampenerKill;
    }
    public class OnDampenerDie : ArgsDie, IEventEmptyHistory // 31
    {
        public EventID ID => EventID.OnDampenerDie;
    }
    public class OnDampenerRespawnSoon : ArgsBase, IEventEmptyHistory // 32
    {
        public EventID ID => EventID.OnDampenerRespawnSoon;
    }
    public class OnDampenerRespawn : ArgsBase, IEventEmptyHistory // 33
    {
        public EventID ID => EventID.OnDampenerRespawn;
    }
    public class OnTurretKill : ArgsBase, IEventEmptyHistory // 35
    {
        public EventID ID => EventID.OnTurretKill;
    }
    public class OnTurretDie : ArgsDie, IEvent // 36
    {
        public EventID ID => EventID.OnTurretDie;
    }
    public class OnMinionKill : ArgsDie, IEvent // 38
    {
        public EventID ID => EventID.OnMinionKill;
    }
    public class OnMinionDenied : ArgsBase, IEventEmptyHistory // 39
    {
        public EventID ID => EventID.OnMinionDenied;
    }
    public class OnNeutralMinionKill : ArgsDie, IEvent // 40
    {
        public EventID ID => EventID.OnNeutralMinionKill;
    }
    public class OnSuperMonsterKill : ArgsBase, IEventEmptyHistory // 41
    {
        public EventID ID => EventID.OnSuperMonsterKill;
    }
    public class OnHQKill : ArgsBase, IEventEmptyHistory // 44
    {
        public EventID ID => EventID.OnHQKill;
    }
    public class OnHQDie : ArgsBase, IEventEmptyHistory // 45
    {
        public EventID ID => EventID.OnHQDie;
    }
    public class OnHeal : ArgsHeal, IEvent // 46
    {
        public EventID ID => EventID.OnHeal;
    }
    public class OnCastHeal : ArgsHeal, IEvent // 47
    {
        public EventID ID => EventID.OnCastHeal;
    }
    public class OnBuff : ArgsBuff, IEvent // 48
    {
        public EventID ID => EventID.OnBuff;
    }
    public class OnKillingSpree : ArgsKillingSpree, IEvent // 50
    {
        public EventID ID => EventID.OnKillingSpree;
    }
    public class OnKillingSpreeSet1 : ArgsBase, IEventEmptyHistory // 51
    {
        public EventID ID => EventID.OnKillingSpreeSet1;
    }
    public class OnKillingSpreeSet2 : ArgsBase, IEventEmptyHistory // 52
    {
        public EventID ID => EventID.OnKillingSpreeSet2;
    }
    public class OnKillingSpreeSet3 : ArgsBase, IEventEmptyHistory // 53
    {
        public EventID ID => EventID.OnKillingSpreeSet3;
    }
    public class OnKillingSpreeSet4 : ArgsBase, IEventEmptyHistory // 54
    {
        public EventID ID => EventID.OnKillingSpreeSet4;
    }
    public class OnKillingSpreeSet5 : ArgsBase, IEventEmptyHistory // 55
    {
        public EventID ID => EventID.OnKillingSpreeSet5;
    }
    public class OnKillingSpreeSet6 : ArgsBase, IEventEmptyHistory // 56
    {
        public EventID ID => EventID.OnKillingSpreeSet6;
    }
    public class OnKilledUnitOnKillingSpree : ArgsBase, IEventEmptyHistory // 57 //ParamsKillingSpree
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpree;
    }
    public class OnKilledUnitOnKillingSpreeSet1 : ArgsBase, IEventEmptyHistory // 58
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpreeSet1;
    }
    public class OnKilledUnitOnKillingSpreeSet2 : ArgsBase, IEventEmptyHistory // 59
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpreeSet2;
    }
    public class OnKilledUnitOnKillingSpreeSet3 : ArgsBase, IEventEmptyHistory // 60
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpreeSet3;
    }
    public class OnKilledUnitOnKillingSpreeSet4 : ArgsBase, IEventEmptyHistory // 61
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpreeSet4;
    }
    public class OnKilledUnitOnKillingSpreeSet5 : ArgsBase, IEventEmptyHistory // 62
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpreeSet5;
    }
    public class OnKilledUnitOnKillingSpreeSet6 : ArgsBase, IEventEmptyHistory // 63
    {
        public EventID ID => EventID.OnKilledUnitOnKillingSpreeSet6;
    }
    public class OnDeathAssist : ArgsAssist, IEvent // 69
    {
        public EventID ID => EventID.OnDeathAssist;
    }
    public class OnQuit : ArgsBase, IEventEmptyHistory // 70
    {
        public EventID ID => EventID.OnQuit;
    }
    public class OnLeave : ArgsBase, IEventEmptyHistory // 71
    {
        public EventID ID => EventID.OnLeave;
    }
    public class OnReconnect : ArgsBase, IEventEmptyHistory // 72
    {
        public EventID ID => EventID.OnReconnect;
    }
    public class OnGameStart : ArgsBase, IEventEmptyHistory // 73
    {
        public EventID ID => EventID.OnGameStart;
    }
    public class OnPing : ArgsBase, IEventEmptyHistory // 78
    {
        public EventID ID => EventID.OnPing;
    }
    public class OnPingPlayer : ArgsBase, IEventEmptyHistory // 79
    {
        public EventID ID => EventID.OnPingPlayer;
    }
    public class OnPingBuilding : ArgsBase, IEventEmptyHistory // 80
    {
        public EventID ID => EventID.OnPingBuilding;
    }
    public class OnPingOther : ArgsBase, IEventEmptyHistory // 81
    {
        public EventID ID => EventID.OnPingOther;
    }
    public class OnEndGame : ArgsBase, IEventEmptyHistory // 82
    {
        public EventID ID => EventID.OnEndGame;
    }
    public class OnSpellLevelup1 : ArgsBase, IEventEmptyHistory // 83
    {
        public EventID ID => EventID.OnSpellLevelup1;
    }
    public class OnSpellLevelup2 : ArgsBase, IEventEmptyHistory // 84
    {
        public EventID ID => EventID.OnSpellLevelup2;
    }
    public class OnSpellLevelup3 : ArgsBase, IEventEmptyHistory // 85
    {
        public EventID ID => EventID.OnSpellLevelup3;
    }
    public class OnSpellLevelup4 : ArgsBase, IEventEmptyHistory // 86
    {
        public EventID ID => EventID.OnSpellLevelup4;
    }
    public class OnItemPurchased : ArgsItemConsumedPurchased, IEvent // 91
    {
        public EventID ID => EventID.OnItemPurchased;
    }
    public class OnSurrenderVoteStart : ArgsBase, IEventEmptyHistory // 99
    {
        public EventID ID => EventID.OnSurrenderVoteStart;
    }
    public class OnSurrenderVote : ArgsBase, IEventEmptyHistory // 100
    {
        public EventID ID => EventID.OnSurrenderVote;
    }
    public class OnSurrenderVoteAlready : ArgsBase, IEventEmptyHistory // 101
    {
        public EventID ID => EventID.OnSurrenderVoteAlready;
    }
    public class OnSurrenderCountDown : ArgsBase, IEventEmptyHistory
    {
        public EventID ID => EventID.OnSurrenderCountDown;
    }
    public class OnSurrenderFailedVotes : ArgsBase, IEventEmptyHistory // 102
    {
        public EventID ID => EventID.OnSurrenderFailedVotes;
    }
    public class OnSurrenderTooEarly : ArgsBase, IEventEmptyHistory // 103
    {
        public EventID ID => EventID.OnSurrenderTooEarly;
    }
    public class OnSurrenderAgreed : ArgsBase, IEventEmptyHistory // 104
    {
        public EventID ID => EventID.OnSurrenderAgreed;
    }
    public class OnSurrenderSpam : ArgsBase, IEventEmptyHistory // 105
    {
        public EventID ID => EventID.OnSurrenderSpam;
    }
    public class OnPause : ArgsBase, IEventEmptyHistory // 116
    {
        public EventID ID => EventID.OnPause;
    }
    public class OnPauseResume : ArgsBase, IEventEmptyHistory // 117
    {
        public EventID ID => EventID.OnPauseResume;
    }
    public class OnMinionsSpawn : ArgsBase, IEventEmptyHistory // 118
    {
        public EventID ID => EventID.OnMinionsSpawn;
    }
    public class OnStartGameMessage1 : ArgsGlobalMessageGeneric, IEventEmptyHistory // 119
    {
        public EventID ID => EventID.OnStartGameMessage1;
    }
    public class OnStartGameMessage2 : ArgsGlobalMessageGeneric, IEventEmptyHistory // 120
    {
        public EventID ID => EventID.OnStartGameMessage2;
    }
    public class OnAlert : ArgsAlert, IEventEmptyHistory // 124
    {
        public EventID ID => EventID.OnAlert;
    }
    public class OnScoreboardOpen : ArgsBase, IEventEmptyHistory // 125
    {
        public EventID ID => EventID.OnScoreboardOpen;
    }
    public class OnAudioEventFinished : ArgsBase, IEventEmptyHistory // 126
    {
        public EventID ID => EventID.OnAudioEventFinished;
    }
    public class OnNexusCrystalStart : ArgsBase, IEventEmptyHistory // 127
    {
        public EventID ID => EventID.OnNexusCrystalStart;
    }
    public class OnCapturePointNeutralized_A : ArgsBase, IEventEmptyHistory // 128
    {
        public EventID ID => EventID.OnCapturePointNeutralized_A;
    }
    public class OnCapturePointNeutralized_B : ArgsBase, IEventEmptyHistory // 129
    {
        public EventID ID => EventID.OnCapturePointNeutralized_B;
    }
    public class OnCapturePointNeutralized_C : ArgsBase, IEventEmptyHistory // 130
    {
        public EventID ID => EventID.OnCapturePointNeutralized_C;
    }
    public class OnCapturePointNeutralized_D : ArgsBase, IEventEmptyHistory // 131
    {
        public EventID ID => EventID.OnCapturePointNeutralized_D;
    }
    public class OnCapturePointNeutralized_E : ArgsBase, IEventEmptyHistory // 132
    {
        public EventID ID => EventID.OnCapturePointNeutralized_E;
    }
    public class OnCapturePointCaptured_A : ArgsBase, IEventEmptyHistory // 133
    {
        public EventID ID => EventID.OnCapturePointCaptured_A;
    }
    public class OnCapturePointCaptured_B : ArgsBase, IEventEmptyHistory // 134
    {
        public EventID ID => EventID.OnCapturePointCaptured_B;
    }
    public class OnCapturePointCaptured_C : ArgsBase, IEventEmptyHistory // 135
    {
        public EventID ID => EventID.OnCapturePointCaptured_C;
    }
    public class OnCapturePointCaptured_D : ArgsBase, IEventEmptyHistory // 136
    {
        public EventID ID => EventID.OnCapturePointCaptured_D;
    }
    public class OnCapturePointCaptured_E : ArgsBase, IEventEmptyHistory // 137
    {
        public EventID ID => EventID.OnCapturePointCaptured_E;
    }
    public class OnVictoryPointThreshold1 : ArgsBase, IEventEmptyHistory // 139
    {
        public EventID ID => EventID.OnVictoryPointThreshold1;
    }
    public class OnVictoryPointThreshold2 : ArgsBase, IEventEmptyHistory // 140
    {
        public EventID ID => EventID.OnVictoryPointThreshold2;
    }
    public class OnVictoryPointThreshold3 : ArgsBase, IEventEmptyHistory // 141
    {
        public EventID ID => EventID.OnVictoryPointThreshold3;
    }
    public class OnVictoryPointThreshold4 : ArgsBase, IEventEmptyHistory // 142
    {
        public EventID ID => EventID.OnVictoryPointThreshold4;
    }
}