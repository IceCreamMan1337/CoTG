using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveLibrary.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

public class Pet : Minion
{
    private float _returnRadius;

    /// <summary>
    /// Entity that the pet is cloning (Ex. Who Mordekaiser's ghost is)
    /// </summary>
    public ObjAIBase ClonedUnit { get; private set; }
    /// <summary>
    /// Buff Assigned to this Pet at Spawn
    /// </summary>
    public string CloneBuffName { get; set; }
    /// <summary>
    /// Spell that created this Pet
    /// </summary>
    public Spell SourceSpell { get; set; }
    /// <summary>
    /// Duration of CloneBuff
    /// </summary>
    public float LifeTime { get; set; }
    public bool CloneInventory { get; set; }
    public bool DoFade { get; set; }
    public bool ShowMinimapIconIfClone { get; set; }
    public bool DisallowPlayerControl { get; set; }
    public bool IsClone { get; set; }
    //hack
    public override bool HasSkins => false;

    public Pet(
        Champion owner,
        Spell spell,
        Vector2 position,
        string name,
        string model,
        string buffName,
        float lifeTime,
        Stats stats = null,
        bool cloneInventory = true,
        bool showMinimapIfClone = true,
        bool disallowPlayerControl = false,
        bool doFade = false,
        bool isClone = true,
        string AIScript = "Pet.lua"
        ) : base(owner, position, model, name, owner.Team, owner.SkinID, stats: stats, AIScript: AIScript)
    {
        _returnRadius = GlobalData.ObjAIBaseVariables.DefaultPetReturnRadius;

        SourceSpell = spell;
        LifeTime = lifeTime;
        CloneBuffName = buffName;
        CloneInventory = cloneInventory;
        ShowMinimapIconIfClone = showMinimapIfClone;
        DisallowPlayerControl = disallowPlayerControl;
        DoFade = doFade;
        IsClone = isClone;

        GoldOwner = new GoldOwner(owner);
        Owner.SetPet(this);




        //Game.ObjectManager.AddObject(this);
    }

    public Pet(
        Champion owner,
        Spell spell,
        ObjAIBase cloned,
        Vector2 position,
        string buffName,
        float lifeTime,
        Stats stats = null,
        bool cloneInventory = true,
        bool showMinimapIfClone = true,
        bool disallowPlayerControl = false,
        bool doFade = false,
        string AIScript = "Pet.lua"
        ) : base(owner, cloned.Position, cloned.Model, cloned.Name, owner.Team, cloned.SkinID, stats: null, AIScript: AIScript)
    {
        Stats = new Stats(cloned.CharData);


        Stats.HealthPoints.BaseValue = (float)cloned.Stats.HealthPoints.Total;
        Stats.CurrentHealth = (float)cloned.Stats.CurrentHealth;
        Stats.CurrentMana = (float)cloned.Stats.ManaPoints.Total;


        Team = owner.Team;

        Stats.IsDead = false;
        Stats.IsZombie = false;

        //_returnRadius = GlobalData.ObjAIBaseVariables.DefaultPetReturnRadius;

        if (position == Vector2.Zero)
        {
            Position = cloned.Position;
        }
        else
        {
            Position = position;
        }

        SourceSpell = spell;
        LifeTime = lifeTime;
        ClonedUnit = cloned;
        CloneBuffName = buffName;
        CloneInventory = cloneInventory;
        ShowMinimapIconIfClone = showMinimapIfClone;
        DisallowPlayerControl = disallowPlayerControl;
        DoFade = doFade;
        IsClone = true;

        GoldOwner = new GoldOwner(owner);
        Owner.SetPet(this);


        //Game.ObjectManager.AddObject(this);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        SpawnPetNotify(this, userId, team, doVision);
        //this is surely bad , in case of shaco 
        if (!this.HasBuff("YorickRARevive"))
        {
            var h = new HealData(this, 100000.0f);
            TakeHeal(h);
        }



    }

    internal override void OnAdded()
    {
        base.OnAdded();
        if (CloneInventory && ClonedUnit != null)
        {
            foreach (var item in ClonedUnit.ItemInventory.GetItems())
            {
                ItemInventory.AddItem(item.ItemData);
            }
        }
        Buffs.Add(CloneBuffName, LifeTime, 1, SourceSpell, this, Owner);




    }

    protected override void OnReachedDestination()
    {
        SetAIState(AIState.AI_PET_IDLE);
        base.OnReachedDestination();
    }

    public float GetReturnRadius()
    {
        return _returnRadius;
    }

    public void SetReturnRadius(float radius)
    {
        _returnRadius = radius;
    }
}
