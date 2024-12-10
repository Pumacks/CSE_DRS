using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagementSample.Models.Items
{
    public abstract class Weapon : Item
    {

        #region attributes, fields and properties
        private float weaponDamage; // Der Schaden, den jeder Schwerthieb oder jeder Pfeilschuss verursacht.
        public float WeaponDamage
        {
            get
            {
                return this.weaponDamage;
            }
            set
            {
                this.weaponDamage = value;
            }
        }
        private float attackSpeed; // Weapon Attack speed in milliseconds between attacks (1000 means 1 attack per second, 200 means 5 attacks per second, etc.). Wieviele Schwerthiebe oder Pfeilschüsse pro Zeiteinheit erfolgen können.
        public float AttackSpeed
        {
            get
            {
                return this.attackSpeed;
            }
            set
            {
                this.attackSpeed = value;
            }
        }
        private float weaponRange; // Weapon Range in Pixels. Wie weit der Schwerthieb reicht, oder wie weit Pfeile fliegen können.
        public float WeaponRange
        {
            get
            {
                return this.weaponRange;
            }
            set
            {
                this.weaponRange = value;
            }
        }
        private float lastAttackGameTimeInMilliseconds;
        public float LastAttackGameTimeInMilliseconds
        {
            get
            {
                return this.lastAttackGameTimeInMilliseconds;
            }
            set
            {
                this.lastAttackGameTimeInMilliseconds = value;
            }
        }
        private float weaponRotationFloatValue; //Is this needed? I doubt so.
        public float WeaponRotationFloatValue
        {
            get
            {
                return this.weaponRotationFloatValue;
            }
            set
            {
                this.weaponRotationFloatValue = value;
            }
        }
        // Testzwecke:
        private List<Enemy> enemies;
        public List<Enemy> Enemies
        {
            get
            {
                return this.enemies;
            }
            set
            {
                this.enemies = value;
            }
        }
        private  List<Projectile> projectiles;
        public  List<Projectile> Projectiles
        {
            get
            {
                return this.projectiles;
            }
            set
            {
                this.projectiles = value;
            }
        }
        /*
        Die Überlegung ist an dieser Stelle, wie kompliziert wir die Waffenlogik machen wollen.
        Um eine Art Balance zu haben sollten Nahkampf und Fernkampf sich ausgleichende Vor-, und Nachteile haben. Gleichzeitig soll keines davon den sicheren Tod bedeuten (Man sollte vielleicht mit dem Schwert angreifend außer Reichweite bleiben können).
        Eine Möglichkeit wäre es, dafür zu sorgen, dass das Schwert eine Art "Area of Effect" hat, während Pfeile eher für "Single-Target-Combat" geeignet sind.

        Soll es verschiedene Munition geben, oder einfach eine feste Munition, die an die jeweilige Fernkampfwaffe gebunden ist? Munition als endlicher Item-Stack, oder einfach v"unendlich Pfeile"? Haltbarkeit?

        Der momentane Plan ist zunächst die Implementierung eines relativ einfachen Systems, in welchem jede Waffe einen Schaden, eine Angriffsgeschwindigkeit und eine Reichweite hat. Dies trifft sowohl für Nahkampf als auch für Fernkampf zu.
        */
        #endregion

        public Weapon (String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange) : base (itemName, itemTexture, itemOwner) {
            this.weaponDamage = weaponDamage;
            this.attackSpeed = attackSpeed;
            this.weaponRange = weaponRange;
            this.lastAttackGameTimeInMilliseconds = 0;
        }

        // public float calculateWeaponRotation()
        // {
        //     Vector2 start = ItemOwner.Position;
        //     Vector2 end = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            
        //         float deltaX = end.X - start.X;
        //         float deltaY = end.Y - start.Y;

        //         float rotation = (float)Math.PI/2+(float)Math.Atan2(deltaY, deltaX);

        //         return rotation;
        // }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        public void weaponAttack(Entity owner) {
            // Weapon Attack Timer, depending on the weapon's attack speed
            if ((owner.GameTime.TotalGameTime.TotalMilliseconds - attackSpeed >= lastAttackGameTimeInMilliseconds) || lastAttackGameTimeInMilliseconds == 0) {
                lastAttackGameTimeInMilliseconds = (float)owner.GameTime.TotalGameTime.TotalMilliseconds;
                attack(owner);
            }
        }

        public abstract void attack(Entity owner);

        public int distance(Vector2 first, Vector2 second) {
            Vector2 combined = new Vector2(first.X-second.X, first.Y-second.Y);
            int distance = (int) Math.Sqrt(combined.X*combined.X + combined.Y*combined.Y);
            if (distance >= 0) {
                return distance;
            } else {
                Console.WriteLine("Rückgabewert der Funktion distance ist negativ!");
                return -1;
            }
        }
    }
}
