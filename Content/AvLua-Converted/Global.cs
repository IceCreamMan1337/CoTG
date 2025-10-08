#nullable enable

global using System;
global using System.Numerics;
global using ChildrenOfTheGraveEnumNetwork.Enums;
global using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
global using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
global using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;
global using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
global using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
global using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

global using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua.Functions;
global using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua.Functions_BBB_and_CS;
global using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.Functions_CS;

global using AIScript = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.CAIScript;
//global using CharScript = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.CCharScript;
//global using ItemScript = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.CItemScript;
//global using SpellScript = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.CSpellScript;
//global using BuffScript = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.CBuffScript;
//global using TalentScript = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.CTalentScript;

using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

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