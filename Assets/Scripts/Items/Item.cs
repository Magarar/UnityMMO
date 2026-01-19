using System;
using UnityEngine;

namespace Items{
    
    public enum ItemSubclass
{
    Consumable,
    Container,
    Weapon,
    Gem,
    Armor,
    Reagent,
    Projectile,
    TradeGoods,
    ItemEnhancement,
    Recipe,
    CurrencyTokenObsolete,
    Quiver,
    QuestItem,
    Key,
    PermanentObsolete,
    Miscellaneous,
    Glyph,
    BattlePet,
    WowToken,
    Profession
    
}

public enum ItemConsumableSubtype
{
    Generic,
    Potion,
    Elixir,
    Scroll,
    Food,
    ItemEnhancement,
    Bandage,
    Other
}

public enum ItemContainerSubtype
{
    Bag,
    SoulBag,
    HerbBag,
    EnchantingBag,
    EngineeringBag,
    GemBag,
    MiningBag,
    LeatherBag,
    InscriptionBag,
    TackleBag,
    CookingBag
}

public enum ItemWeaponSubtype
{
    Axe1H,
    Axe2H,
    Bows,
    Guns,
    Mace1H,
    Mace2H,
    PoleArm,
    Sword1H,
    Sword2H,
    WarGlaive,
    Staff,
    Unarmed,
    Generic,
    Dagger,
    Spear,
    Thrown,
    Obsolete3,
    CrossBow,
    Wand,
    FishingPole
    
}

public enum ItemGemSubtype
{
    Intellect,
    Agility,
    Strength,
    Vit,
    Critical,
    ExAtk,
    InAtk,
    Haste,
    Dodge
}

public enum ItemArmorSubtype
{
    Miscellaneous,
    Cloth,
    Leather,
    Mail,
    Plate,
    Cosmetic,
    Shield,
    Librams,
    Idols,
    Totems,
    Sigils,
    Relic
    
}

public enum ItemReagentSubtype
{
    Reagent,
    Keystone,
    ContextToken
}

public enum ItemProjectileSubtype
{
    Wand,
    Bolt,
    Arrow,
    Bullet,
    Thrown
}

public enum ItemTradeSkillsSubtype
{
    TradeGoods,
    Parts,
    Explosives,
    Devices,
    JewelCrafting,
    Cloth,
    Leather,
    Metal,
    Cooking,
    Herb,
    Elemental,
    Other,
    Enhancing,
    Materials,
    ItemEnhancement,
    WeaponEnhancement,
    Inscription,
    Engineering,
}

public enum ItemEnhancementSubtype
{
    Head,
    Neck,
    Shoulder,
    Cloak,
    Chest,
    Wrist,
    Hands,
    Waist,
    Legs,
    Feet,
    Finger,
    Weapon,
    TwoHandWeapon,
    Shiled,
    Misc
}

public enum ItemRecipeSubtype
{
    Book,
    LeatherWorking,
    Tailoring,
    Engineering,
    Blacksmithing,
    Cooking,
    Alchemy,
    FirstAid,
    Enhancing,
    Fishing,
    JewelCrafting,
    Inscription
}

public enum ItemMoneySubtype
{
    Money
}

public enum ItemQuiverSubtype
{
    Quiver,
    Bolt,
    AmmoPunch
}

public enum ItemQuestSubtype
{
    Quest
}

public enum ItemKeySubtype
{
    Key,
    LookPick
}

public enum ItemPermanentSubtype
{
    Permanent
}

public enum ItemMiscellaneousSubtype
{
    Junk,
    Reagent,
    CompanionPet,
    Holiday,
    Other,
    Mount,
    MountEquipment,
}

public enum ItemGlyphSubtype
{
    Warrior,
    Paladin,
    Hunter,
    Rogue,
    Priest,
    DeathKnight,
    Shaman,
    Mage,
    Warlock,
    Monk,
    Druid,
    DemonHunter
}

public enum ItemBattlePetSubtype
{
    Humanoid,
    Dragonkin,
    Flying,
    Undead,
    Critter,
    Magic,
    Elemental,
    Beast,
    Aquatic,
    Mechanical
}

public enum ItemWowTokenSubtype
{
    WowToken
}

public enum ItemProfessionSubtype
{
    Blacksmithing,
    Leatherworking,
    Alchemy,
    Herbalism,
    Mining,
    Cooking,
    Tailoring,
    Engineering,
    Enchanting,
    Fishing,
    Skinning,
    JewelCrafting,
    Inscription,
    Archaeology,
    
    
}

public enum Item_Type
{
    ItemNoneCoolDown,
    ItemUseCoolDown
}

    
    public class Item
    {
        public int itemId { get; set; } = 0;
    
        public int stack { get; set; } = 0;
    
        public string itemName {get;set;} = string.Empty;
    
        public int slot {get;set;} = 0;
    
        public int itemClass {get;set;} = 0;
    
        public int weight {get;set;} = 0;
    
        public int tier {get;set;} = 0;
    
        public int profession {get;set;} = 0;
    
        public string note {get;set;}
    
        public int quality {get;set;} = 0;
    
        public string description {get;set;}
    
        public int displayId {get;set;} = 0;
    
        public int inventoryType {get;set;} = 0;
    
        public int allowClass {get;set;} = 0;
    
        public int itemLevel {get;set;} = 0;
    
        public int requiredLevel {get;set;} = 0;
    
        public int requiredHonor {get;set;} = 0;
    
        public int requiredRank {get;set;} = 0;
    
        public int setId {get;set;} = 0;
    
        public int itemType {get;set;} = 0;
    
        public int cutLevel {get;set;} = 0;
    
        public int endurance  {get;set;} = 0;
    
        public int item_common {get;set;} = 0;
    
        public int item_subclass {get;set;} = 0;
    
        public int item_subtype  {get;set;} = 0;
    
        public int item_begin {get;set;} = 0;
    
        public int item_sold {get;set;} = 0;
    
        public int item_pass{get;set;} = 0;
    
        public int item_discarded {get;set;} = 0;
    
        public int gender_type {get;set;} = 0;
    
        public int gender_size_type {get;set;} = 0;
    
        public int item_sets_name {get;set;} = 0;
    
        public int practice {get;set;} = 0;
    
        public int practice_page {get;set;} = 0;
    
        public int increase_hp {get;set;} = 0;
    
        public int increase_mp {get;set;} = 0;
    
        public int increase_sp {get;set;} = 0;
    
        public int hp_overtime{get;set;} = 0;
    
        public int mp_overtime {get;set;} = 0;
    
        public int sp_overtime {get;set;} = 0;

        public int str {get;set;} = 0;
    
        public int intel {get;set;} = 0;
    
        public int vit {get;set;} = 0;
    
        public int agi {get;set;} = 0;
    
        public int obs {get;set;} = 0;
    
        public int str_Percentage {get;set;} = 0;
    
        public int intel_Percentage {get;set;} = 0;
    
        public int vit_Percentage {get;set;} = 0;
    
        public int agi_Percentage {get;set;} = 0;
    
        public int obs_Percentage {get;set;} = 0;
    
        public int exAtk {get;set;} = 0;
    
        public int inAtk {get;set;} = 0;
    
        public int exAtk_Percentage {get;set;} = 0;
    
        public int inAtk_Percentage {get;set;} = 0;
    
        public int delay_enemy {get;set;} = 0;
    
        public int hp_consumption_increase {get;set;} = 0;
    
        public int hp_consumption_decrease {get;set;} = 0;
    
        public int mp_consumption_increase {get;set;} = 0;
    
        public int mp_consumption_decrease {get;set;} = 0;
    
        public int sp_consumption_increase {get;set;} = 0;
    
        public int sp_consumption_decrease {get; set; } = 0;
    
        public int weapon_exAtk_lower_limit_increase {get; set;} = 0;
    
        public int weapon_exAtk_lower_limit_decrease {get; set;} = 0;
    
        public int weapon_inAtk_lower_limit_increase {get; set;} = 0;
    
        public int weapon_inAtk_lower_limit_decrease {get; set;} = 0;
    
        public int weapon_exAtk_upper_limit_increase {get; set;} = 0;
    
        public int weapon_exAtk_upper_limit_decrease {get; set;} = 0;
    
        public int weapon_inAtk_upper_limit_increase {get; set;} = 0;
    
        public int weapon_inAtk_upper_limit_decrease {get; set;} = 0;
    
        public string weapon_attack_default_target {get; set;} = string.Empty;
    
        public int weapon_attack_default_target_damage {get; set;} = 0;
    
        public int buy_price {get; set;} = 0;
    
        public int sell_price {get; set;} = 0;
        
        public int increase_hp_percentage {get; set;}
    
        public int increase_mp_percentage {get; set;}
    
        public int increase_sp_percentage {get; set;}
    
        public int restore_hp {get; set;}
    
        public int restore_mp {get; set;}
    
        public int restore_sp {get; set;}
        
        public int item_cooldown {get; set;}
        
        public int item_speed {get; set;}

        public Sprite sprite { get; set; } = null;
        
        public ItemSubclass curItemSubclass {get;set;}
    
        public ItemConsumableSubtype curitemConsumableSubclass {get;set;}
        public ItemContainerSubtype curItemContainerSubtype {get;set;}
        public ItemWeaponSubtype curItemWeaponSubtype {get;set;}
        public ItemGemSubtype curItemGemSubtype {get;set;}
        public ItemArmorSubtype curItemArmorSubtype {get;set;}
        public ItemReagentSubtype curItemReagentSubtype {get;set;}
        public ItemProjectileSubtype curItemProjectileSubtype {get;set;}
        public ItemTradeSkillsSubtype curItemTradeSkillsSubtype {get;set;}
        public ItemEnhancementSubtype curItemEnhancementSubtype {get;set;}
        public ItemRecipeSubtype curItemRecipeSubtype {get;set;}
        public ItemMoneySubtype curItemMoneySubtype {get;set;}
        public ItemQuiverSubtype curItemQuiverSubtype {get;set;}
        public ItemQuestSubtype curItemQuestSubtype {get;set;}
        public ItemKeySubtype curItemKeySubtype {get;set;}
        public ItemPermanentSubtype curItemPermanentSubtype {get;set;}
        public ItemMiscellaneousSubtype curItemMiscellaneousSubtype {get;set;}
        public ItemGlyphSubtype curItemGlyphSubtype {get;set;}
        public ItemBattlePetSubtype curItemBattlePetSubtype {get;set;}
        public ItemWowTokenSubtype curItemWowTokenSubtype {get;set;}
        public ItemProfessionSubtype curItemProfessionSubtype {get;set;}
        
        public Item_Type curItemType {get;set;}
        
        
        
        public void InitItemType(int subClass,int subType)
    {
        curItemSubclass = (ItemSubclass)subClass;
        switch (curItemSubclass)
        {
            case ItemSubclass.Consumable:
                curitemConsumableSubclass = (ItemConsumableSubtype)subType;
                break;
            case ItemSubclass.Container:
                curItemContainerSubtype = (ItemContainerSubtype)subType;
                break;
            case ItemSubclass.Weapon:
                curItemWeaponSubtype = (ItemWeaponSubtype)subType;
                break;
            case ItemSubclass.Gem:
                curItemGemSubtype = (ItemGemSubtype)subType;
                break;
            case ItemSubclass.Armor:
                curItemArmorSubtype = (ItemArmorSubtype)subType;
                break;
            case ItemSubclass.Reagent:
                curItemReagentSubtype = (ItemReagentSubtype)subType;
                break;
            case ItemSubclass.Projectile:
                curItemProjectileSubtype = (ItemProjectileSubtype)subType;
                break;
            case ItemSubclass.TradeGoods:
                curItemTradeSkillsSubtype = (ItemTradeSkillsSubtype)subType;
                break;
            case ItemSubclass.ItemEnhancement:
                curItemEnhancementSubtype = (ItemEnhancementSubtype)subType;
                break;
            case ItemSubclass.Recipe:
                curItemRecipeSubtype = (ItemRecipeSubtype)subType;
                break;
            case ItemSubclass.CurrencyTokenObsolete:
                curItemMoneySubtype = (ItemMoneySubtype)subType;
                break;
            case ItemSubclass.Quiver:
                curItemQuiverSubtype = (ItemQuiverSubtype)subType;
                break;
            case ItemSubclass.QuestItem:
                curItemQuestSubtype = (ItemQuestSubtype)subType;
                break;
            case ItemSubclass.Key:
                curItemKeySubtype = (ItemKeySubtype)subType;
                break;
            case ItemSubclass.PermanentObsolete:
                curItemPermanentSubtype = (ItemPermanentSubtype)subType;
                break;
            case ItemSubclass.Miscellaneous:
                curItemMiscellaneousSubtype = (ItemMiscellaneousSubtype)subType;
                break;
            case ItemSubclass.Glyph:
                curItemGlyphSubtype = (ItemGlyphSubtype)subType;
                break;
            case ItemSubclass.BattlePet:
                curItemBattlePetSubtype = (ItemBattlePetSubtype)subType;
                break;
            case ItemSubclass.WowToken:
                curItemWowTokenSubtype = (ItemWowTokenSubtype)subType;
                break;
            case ItemSubclass.Profession:
                curItemProfessionSubtype = (ItemProfessionSubtype)subType;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    }
    
    
}
