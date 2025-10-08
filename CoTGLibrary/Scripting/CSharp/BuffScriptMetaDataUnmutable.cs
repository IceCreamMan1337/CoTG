#nullable enable

using MoonSharp.Interpreter;
using CoTG.CoTGServer.Scripting.Lua;
using static CoTG.CoTGServer.Scripting.Lua.BBCacheEntry;
using CoTGEnumNetwork.Enums;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class BuffScriptMetadataUnmutable : IBBMetadata
{
    public string BuffName = "";
    public string BuffTextureName = "";
    public string MinimapIconTextureName = "";
    public string MinimapIconEnemyTextureName = "";
    public string?[]? PopupMessage;

    public string AutoBuffActivateEvent { get; set; } = "";
    public EffCreate AutoBuffActivateEffectFlags = 0;
    public string?[] AutoBuffActivateEffect = new string[0];
    public string?[] AutoBuffActivateAttachBoneName = new string[0];

    public int SpellToggleSlot { get; set; } = 0; // [1-4]

    public bool NonDispellable { get; set; } = false;
    public bool PersistsThroughDeath { get; set; } = false;
    public bool IsPetDurationBuff { get; set; } = false;
    public bool IsDeathRecapSource { get; set; } = false;

    public int OnPreDamagePriority { get; set; } = 0;
    public bool DoOnPreDamageInExpirationOrder { get; set; } = false;

    public void Parse(Table globals)
    {
        BuffName = globals.RawGet("BuffName")?.String ?? BuffName;
        BuffTextureName = globals.RawGet("BuffTextureName")?.String ?? BuffTextureName;

        MinimapIconTextureName = globals.RawGet("MinimapIconTextureName")?.String ?? MinimapIconTextureName;
        MinimapIconEnemyTextureName = globals.RawGet("MinimapIconEnemyTextureName")?.String ?? MinimapIconEnemyTextureName;

        AutoBuffActivateEvent = globals.RawGet("AutoBuffActivateEvent")?.String ?? AutoBuffActivateEvent;
        AutoBuffActivateEffect = ReadArray<string?>(globals, "AutoBuffActivateEffect", null) ?? AutoBuffActivateEffect;
        AutoBuffActivateEffectFlags = (EffCreate?)(globals.RawGet("AutoBuffActivateEffectFlags")?.Number) ?? AutoBuffActivateEffectFlags;
        AutoBuffActivateAttachBoneName = ReadArray<string?>(globals, "AutoBuffActivateAttachBoneName", null) ?? AutoBuffActivateAttachBoneName;
        SpellToggleSlot = (int)(globals.RawGet("SpellToggleSlot")?.Number ?? SpellToggleSlot);

        bool? Invert(bool? src) => (src == null) ? null : !src;
        NonDispellable = globals.RawGet("NonDispellable")?.Boolean ??
                        Invert(globals.RawGet("Nondispellable")?.Boolean) ??
                        NonDispellable;

        PersistsThroughDeath = globals.RawGet("PersistsThroughDeath")?.Boolean ??
                               globals.RawGet("PermeatesThroughDeath")?.Boolean ??
                               PersistsThroughDeath;

        IsPetDurationBuff = globals.RawGet("IsPetDurationBuff")?.Boolean ?? IsPetDurationBuff;

        PopupMessage = ReadArray<string?>(globals, "PopupMessage", "")!;
        IsDeathRecapSource = globals.RawGet("IsDeathRecapSource")?.Boolean ?? IsDeathRecapSource;
        OnPreDamagePriority = (int)(globals.RawGet("OnPreDamagePriority")?.Number ?? OnPreDamagePriority);
        DoOnPreDamageInExpirationOrder = globals.RawGet("DoOnPreDamageInExpirationOrder")?.Boolean ?? DoOnPreDamageInExpirationOrder;
    }
}