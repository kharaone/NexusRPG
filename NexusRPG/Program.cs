using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            var itemFactory = new ItemFactory();
            itemFactory.CreateRandomItem();
        }
    }

    public class Entity
    {
        public Guid Id { get; set; }
    }

    public class NamedEntity : Entity
    {
        public string Name { get; set; }
    }

    public class Character : NamedEntity
    {

    }

    public class Hero : Character
    {

    }

    public class Monster : Character
    {

    }

    public class Item
    {
        public string BaseName { get; set; }
        public int RequiredPlayerLevel { get; set; }
        public int ItemLevel { get; set; }

        public Item()
        {
            RequiredPlayerLevel = 1;
            ItemLevel = 1;
        }
    }


    public class Armor : Item
    {
        public int BaseDefense { get; set; }

        public string StatDescription()
        {
            return string.Format("Defense: {0}", this.BaseDefense);
        }
    }

    public class Weapon : Item
    {
        public int BaseMinDamage { get; set; }
        public int BaseMaxDamage { get; set; }
        public double CriticalHitChance { get; set; }
        public int CriticalHitBonusDamage { get; set; }
        public string StatDescription()
        {
            return string.Format("Damage: {0} - {1}", this.BaseMinDamage, this.BaseMaxDamage);
        }
    }

    public class Consumable : Item
    {
        public int Doses { get; set; }
    }

    public class ItemFactory
    {
        public CryptoRandom Rng { get; set; }
        public ItemFactory()
        {
            Rng = new CryptoRandom();
        }
        public Weapon CreateBaseSword()
        {
            return new Weapon
            {
                BaseMinDamage = 4,
                BaseMaxDamage = 7,
                BaseName = "Sword"
            };
        }

        public Item CreateRandomItem()
        {
            Item item=new Item();

            int itemRoll = Rng.Next(0, 8) % 7; // A number 0 - 6
            switch (itemRoll)
            {
                case 0:
                    item = CreateBaseSword();
                    break;
                case 1:
                    item = CreateBaseDagger();
                    break;
                case 2:
                    item = CreateBaseClub();
                    break;
                case 3:
                    item = CreateBaseClothShirt();
                    break;
                case 4:
                    item = CreateBaseHat();
                    break;
                case 5:
                    item = CreateBaseGloves();
                    break;
                case 6:
                    item = CreateBaseBoots();
                    break;
                case 7:
                    item = CreateBasePotion();
                    break;
                case 8:
                    item = CreateBaseShield();
                    break;
            }

            item = TweakBaseStatsOfItem(item, 5);
            return item;
        }

        public Item TweakBaseStatsOfItem(Item item, int maxAmount)
        {
            int amountToAdd = Rng.Next(0, maxAmount);
    
            if (item is Armor) {
                // Add defense
                var armor = (Armor) item;
                armor.BaseDefense += amountToAdd;
            }
            else if (item is Weapon)
            {
                // Add min and max damage
                var weapon = (Weapon)item;
                weapon.BaseMinDamage += amountToAdd;
                weapon.BaseMaxDamage += amountToAdd;
            }
            else if (item is Consumable)
            {
                // Add min and max damage
                var consumable = (Consumable)item;
                consumable.Doses += (int) Math.Round((double)amountToAdd/2);
            }

            return item;

        }

            
        public Weapon CreateBaseDagger()
        {
            return new Weapon
            {
                BaseMinDamage = 3,
                BaseMaxDamage = 6,
                BaseName = "Dagger"
            };
        }

        public Weapon CreateBaseClub()
        {
            return new Weapon
            {
                BaseMinDamage = 1,
                BaseMaxDamage = 9,
                BaseName = "Club"
            };
        }

        public Armor CreateBaseClothShirt()
        {
            return new Armor
            {
                BaseDefense = 3,
                BaseName = "Cloth Shirt"
            };
        }

        public Armor CreateBaseHat()
        {
            return new Armor
            {
                BaseDefense = 1,
                BaseName = "Hat"
            };
        }

        public Armor CreateBaseGloves()
        {
            return new Armor
            {
                BaseDefense = 1,
                BaseName = "Gloves"
            };
        }

        public Armor CreateBaseBoots()
        {
            return new Armor
            {
                BaseDefense = 2,
                BaseName = "Boots"
            };
        }

        public Armor CreateBaseShield()
        {
            return new Armor
            {
                BaseDefense = 10,
                BaseName = "Shield"
            };
        }

        public Consumable CreateBasePotion()
        {
            return new Consumable
            {
                Doses = 1,
                BaseName = "Healing Potion"
            };
        }

    }
}
