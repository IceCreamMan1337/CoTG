#nullable enable

global using System;
global using System.Numerics;
global using CoTGEnumNetwork.Enums;
global using CoTG.CoTGServer.GameObjects;
global using CoTG.CoTGServer.GameObjects.SpellNS;
global using CoTG.CoTGServer.GameObjects.SpellNS.Missile;
global using CoTG.CoTGServer.GameObjects.AttackableUnits;
global using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
global using CoTG.CoTGServer.Scripting.CSharp;

global using static CoTG.CoTGServer.Scripting.Lua.Functions;
global using static CoTG.CoTGServer.Scripting.Lua.Functions_BBB_and_CS;
global using static CoTG.CoTGServer.Scripting.CSharp.Converted.Functions_CS;
//global using CharScript = CoTG.CoTGServer.Scripting.CSharp.Converted.CCharScript;
//global using ItemScript = CoTG.CoTGServer.Scripting.CSharp.Converted.CItemScript;
//global using SpellScript = CoTG.CoTGServer.Scripting.CSharp.Converted.CSpellScript;
//global using BuffScript = CoTG.CoTGServer.Scripting.CSharp.Converted.CBuffScript;
//global using TalentScript = CoTG.CoTGServer.Scripting.CSharp.Converted.CTalentScript;

using CoTG.CoTGServer.Scripting.CSharp.Converted;

public class CharScript : CCharScript
{
    private AllCharVars? _charVars;
    public AllCharVars charVars => _charVars ?? (_charVars = owner.GetComponent<AllCharVars>());
    private AllAvatarVars? _avatarVars;
    public AllAvatarVars avatarVars => _avatarVars ?? (_avatarVars = owner.GetComponent<AllAvatarVars>());
}
public class ItemScript : CItemScript
{
    private AllCharVars? _charVars;
    public AllCharVars charVars => _charVars ?? (_charVars = owner.GetComponent<AllCharVars>());
    private AllAvatarVars? _avatarVars;
    public AllAvatarVars avatarVars => _avatarVars ?? (_avatarVars = owner.GetComponent<AllAvatarVars>());
}
public class SpellScript : CSpellScript
{
    private AllCharVars? _charVars;
    public AllCharVars charVars => _charVars ?? (_charVars = owner.GetComponent<AllCharVars>());
    private AllAvatarVars? _avatarVars;
    public AllAvatarVars avatarVars => _avatarVars ?? (_avatarVars = owner.GetComponent<AllAvatarVars>());
}
public class BuffScript : CBuffScript
{
    private AllCharVars? _charVars;
    public AllCharVars charVars => _charVars ?? (_charVars = owner.GetComponent<AllCharVars>());
    private AllAvatarVars? _avatarVars;
    public AllAvatarVars avatarVars => _avatarVars ?? (_avatarVars = owner.GetComponent<AllAvatarVars>());
}
public class TalentScript : CTalentScript
{
    private AllCharVars? _charVars;
    public AllCharVars charVars => _charVars ?? (_charVars = owner.GetComponent<AllCharVars>());
    private AllAvatarVars? _avatarVars;
    public AllAvatarVars avatarVars => _avatarVars ?? (_avatarVars = owner.GetComponent<AllAvatarVars>());
}