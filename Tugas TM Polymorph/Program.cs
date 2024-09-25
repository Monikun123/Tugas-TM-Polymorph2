// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;

using System;

public class Program
{
    static void Main()
    {
        // Hero Selection
        Basic a1 = SelectHero();
        Opponent o1 = new Opponent();
        String[] actions = { "Attacking", "Blocking" };

        Opponent[] opps = new Opponent[10];
        for (int i = 0; i < 10; i++)
        {
            opps[i] = new Opponent();
        }

        var rand = new Random();

        // Main battle loop
        while (a1.getHP() > 0 && o1.getHP() > 0)
        {
            Console.WriteLine("Hero HP: " + a1.getHP());
            Console.WriteLine("Opponent HP: " + o1.getHP());
            Console.WriteLine("Choose action: 0) Attack 1) Block");
            int hero_action = Convert.ToInt32(Console.ReadLine());
            int opp_action = rand.Next(0, 2);

            Console.WriteLine("Hero " + actions[hero_action]);
            Console.WriteLine("Opponent " + actions[opp_action]);

            int hero_dmg = hero_action == 0 ? a1.attacking() : a1.blocking();
            int opp_dmg = opp_action == 0 ? o1.attacking() : o1.blocking();

            // Apply damage
            a1.calculating_damage(opp_dmg);
            o1.calculating_damage(hero_dmg);

            if (o1.getHP() <= 0)
            {
                Console.WriteLine("Opponent defeated!");
                break;
            }

            if (a1.getHP() <= 0)
            {
                Console.WriteLine("Hero has been defeated!");
                break;
            }
        }
    }

    // Method to allow the player to select a hero class
    static Basic SelectHero()
    {
        Console.WriteLine("Choose your hero class:");
        Console.WriteLine("1) Warrior");
        Console.WriteLine("2) Mage");
        Console.WriteLine("3) Archer");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                return new Warrior(20);
            case 2:
                return new Mage(20);
            case 3:
                return new Archer(20);
            default:
                Console.WriteLine("Invalid choice. Defaulting to Warrior.");
                return new Warrior(20);
        }
    }
}

public class Elemental
{
    int NONE = 0;
    int AQUA = 1;
    int FIRE = 2;
    int OWN;

    public Elemental(int _choosen_element)
    {
        OWN = _choosen_element;
    }

    public int effect(int _opp_element)
    {
        /* ELEMENTAL DICTIONARY
            * None > Aqua
            * Aqua > Fire
            * Fire > None
            */

        int diff = _opp_element - OWN;
        if (diff == 0)
        {
            return 0;
        }
        else if (diff >= 1)
        {
            return -10;
        }
        else
        {
            return 10;
        }
    }

}
public class Basic
{
    protected int ATK = 10;
    protected int DEF = 10;
    protected int SPD = 10;
    protected int EXP = 0;
    protected int HP = 100;
    protected int MP = 100;

    public virtual int attacking()
    {
        return ATK;
    }

    public virtual int blocking()
    {
        return DEF;
    }

    public void calculating_damage(int _dmg)
    {
        HP -= _dmg;
    }

    public int getHP()
    {
        return HP;
    }
}

public class Warrior : Basic
{
    int WEAPON_DMG = 50;
    int ARMOR = 20;
    Elemental ELEMENT;

    public Warrior(int _element, int _weapon_dmg = 10)
    {
        ELEMENT = new Elemental(_element);
        WEAPON_DMG = _weapon_dmg;
    }

    public Warrior(int _weapon_dmg = 10)
    {
        WEAPON_DMG = _weapon_dmg;
    }

    public override int attacking()
    {
        return ATK + WEAPON_DMG;
    }

    public int attacking(int _opp_elemental)
    {
        return ATK + WEAPON_DMG + ELEMENT.effect(_opp_elemental);
    }

    public override int blocking()
    {
        return DEF + ARMOR;
    }

}

public class Mage:Basic
{
    int Intelligence = 20;
    int Armor = 10;
    Elemental ElEMENT;

    public Mage(int _element, int intelligence = 20)
    {
        ElEMENT = new Elemental(_element);
        Intelligence = intelligence;
    }

    public Mage(int intelligence=20)
    {
        Intelligence=intelligence;
    }
    public override int attacking()
    {
        return ATK + Intelligence;
    }

    public int attacking(int _opp_elemental)
    {
        return ATK + Intelligence + ElEMENT.effect(_opp_elemental);
    }

    public override int blocking()
    {
        return DEF + Armor;
    }

}

public class Archer : Basic
{
    int AGL;
    int Armor;
    Elemental ELEMENT;
    public Archer(int _element, int agl = 20)
    {
        ELEMENT = new Elemental(_element);
        AGL = agl;
    }

    public Archer(int agl = 20)
    {
        AGL = agl;
    }
    public override int attacking()
    {
        return ATK + AGL;
    }

    public int attacking(int _opp_elemental)
    {
        return ATK + AGL + ELEMENT.effect(_opp_elemental);
    }

    public override int blocking()
    {
        return DEF + Armor;
    }
}
public class Opponent
{
    private int ATK = 10;
    private int DEF = 10;
    private int HP = 50;

    public int attacking()
    {
        return ATK;
    }

    public int blocking()
    {
        return DEF;
    }

    public void calculating_damage(int _dmg)
    {
        HP -= _dmg;
    }

    public int getHP()
    {
        return HP;
    }
}

