using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.GameObjects.SpellNS;

public partial class Spell
{
    private int ClientId => (Caster as Champion)?.ClientId ?? -1;

    private int _iconIndex = 0;


    public int IconIndex
    {
        get => _iconIndex;
        set
        {
            if (_iconIndex != value)
            {
                _iconIndex = value;
                if (ClientId != -1)
                {
                    ChangeSlotSpellDataNotifier
                    (
                        ClientId, Caster, Slot,
                        ChangeSlotSpellDataType.IconIndex, newIconIndex: value
                    );
                }
            }
        }
    }

    public bool Sealed { get; set; }

    private bool _enabled = false;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled == value)
            {
                return;
            }

            _enabled = value;
            if (IsVisibleSpell)
            {
                //Why are storing the enabled state in two places? (Here and in Stats)
                //???
                if (IsSummonerSpell)
                {
                    Caster.Stats.SetSummonerSpellEnabled((byte)Slot, _enabled);
                    return;
                }
                Caster.Stats.SetSpellEnabled((byte)Slot, _enabled);
            }
        }
    }

    private TargetingType _targetingType { get; set; }
    public TargetingType TargetingType
    {
        get => _targetingType;
        set
        {
            if (_targetingType != value)
            {
                _targetingType = value;
                if (ClientId != -1)
                {
                    ChangeSlotSpellDataNotifier
                    (
                        ClientId, Caster, Slot,
                        ChangeSlotSpellDataType.TargetingType, targetingType: value
                    );
                }
            }
        }
    }

    public float CastRange => GetCastRange(Level); //TODO: Networking
    public float GetCastRange(int level) => GetByLevel(Data.CastRange, level);
}