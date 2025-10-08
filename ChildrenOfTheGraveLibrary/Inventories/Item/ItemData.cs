using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.FileSystem;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.StatsNS;
using System.IO;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Inventory
{
    public class ItemData : StatsModifier
    {
        // Meta
        public int Id { get; init; }
        public string Name { get; init; }

        // General
        public int MaxStacks { get; init; }
        public int Price { get; init; }
        public string ItemGroup { get; init; }
        public bool Consumed { get; init; }
        public string SpellName { get; init; }
        public float SellBackModifier { get; init; }

        // Recipes
        public int[] RecipeItem { get; init; }

        // Not from data
        public ItemRecipe Recipe { get; init; }
        public int TotalPrice => Recipe.TotalPrice;

        public bool ClearUndoHistoryOnActivate { get; init; }

        public ItemData(string itemName)
        {
            RFile? file = Cache.GetFile($"{ContentManager.ItemsPath}/{itemName}.ini");
            if (file is null)
            {
                Name = "";
                Id = -1;

                return;
            }

            string name = Path.GetFileNameWithoutExtension(file.MainFileName);
            Name = name;
            Id = int.Parse(name);

            MaxStacks = file.GetValue("Data", "MaxStack", 1);
            Price = file.GetValue("Data", "Price", 0);
            ItemGroup = file.GetValue("Data", "ItemGroup", "");
            Consumed = file.GetValue("Data", "Consumed", false);
            SpellName = file.GetValue("Data", "SpellName", "");
            SellBackModifier = file.GetValue("Data", "SellBackModifier", 0.7f);

            RecipeItem = new int[4];
            RecipeItem[0] = file.GetValue("Data", "RecipeItem1", -1);
            RecipeItem[1] = file.GetValue("Data", "RecipeItem2", -1);
            RecipeItem[2] = file.GetValue("Data", "RecipeItem3", -1);
            RecipeItem[3] = file.GetValue("Data", "RecipeItem4", -1);

            AbilityPower.FlatBonus = file.GetValue("Data", "FlatMagicDamageMod", 0f);
            AbilityPower.PercentBonus = file.GetValue("Data", "PercentMagicDamageMod", 0f);

            Armor.FlatBonus = file.GetValue("Data", "FlatArmorMod", 0f);
            Armor.PercentBonus = file.GetValue("Data", "PercentArmorMod", 0f);

            AttackDamage.FlatBonus = file.GetValue("Data", "FlatPhysicalDamageMod", 0f);
            AttackDamage.PercentBonus = file.GetValue("Data", "PercentPhysicalDamageMod", 0f);
            AttackSpeedMultiplier.PercentBonus = file.GetValue("Data", "PercentAttackSpeedMod", 0f);

            CriticalChance.PercentBonus = file.GetValue("Data", "FlatCritChanceMod", 0f);
            CriticalDamage.PercentBonus = file.GetValue("Data", "FlatCritDamageMod", 0f);
            CriticalDamage.PercentMultiplicativeBonus = file.GetValue("Data", "PercentCritDamageMod", 0f);

            HealthPoints.FlatBonus = file.GetValue("Data", "FlatHPPoolMod", 0f);
            HealthPoints.PercentBonus = file.GetValue("Data", "PercentHPPoolMod", 0f);
            HealthRegeneration.FlatBonus = file.GetValue("Data", "FlatHPRegenMod", 0f);
            HealthRegeneration.PercentBonus = file.GetValue("Data", "PercentBaseHPRegenMod", 0f);

            LifeSteal.PercentBonus = file.GetValue("Data", "PercentLifeStealMod", 0f);

            ManaPoints.FlatBonus = file.GetValue("Data", "FlatMPPoolMod", 0f);
            ManaPoints.PercentBonus = file.GetValue("Data", "PercentMPPoolMod", 0f);
            ManaRegeneration.FlatBonus = file.GetValue("Data", "FlatMPRegenMod", 0f);
            ManaRegeneration.PercentBonus = file.GetValue("Data", "PercentBaseMPRegenMod", 0f);

            MagicPenetration.FlatBonus = file.GetValue("Data", "FlatMagicPenetrationMod", 0f);
            MagicResist.FlatBonus = file.GetValue("Data", "FlatSpellBlockMod", 0f);
            MagicResist.PercentBonus = file.GetValue("Data", "PercentSpellBlockMod", 0f);

            MoveSpeed.FlatBonus = file.GetValue("Data", "FlatMovementSpeedMod", 0f);
            MoveSpeed.PercentBonus = file.GetValue("Data", "PercentMovementSpeedMod", 0f);

            ExpBonus.PercentBonus = file.GetValue("Data", "PercentEXPBonus", 0f);

            float tenacityValue = file.GetValue("Data", "PercentHardnessMod", 0f);
            Tenacity.IncPercentBonusPerm(tenacityValue);

            ClearUndoHistoryOnActivate = file.GetValue("Data", "ClearUndoHistoryOnActivate", ClearUndoHistoryOnActivate);

            Recipe = new(this);
        }
    }
}