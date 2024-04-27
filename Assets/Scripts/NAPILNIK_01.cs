using System;
using UnityEngine;

public class NAPILNIK_01
{
    class Weapon
    {
        private int _damage;
        private int _bullets;

        public Weapon(int damage, int bullets)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException("Damage can't be less than zero");
            }

            if (bullets < 0)
            {
                throw new ArgumentOutOfRangeException("Bullets can't be less than zero");
            }

            _damage = damage;
            _bullets = bullets;
        }

        public void Fire(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("Player can't be null");
            }

            if (_bullets > 0)
            {
                player.TakeDamage(_damage);
                _bullets--;
            }
        }
    }

    class Player
    {
        private int _health;

        public Player(int health)
        {
            _health = Mathf.Max(0, health);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException("Damage can't be less than zero");
            }

            _health -= damage;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public Bot(Weapon weapon) 
        {
            if (weapon == null)
            {
                throw new ArgumentNullException("Weapon can't be null");
            }

            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("Player can't be null");
            }

            _weapon.Fire(player);
        }
    }
}
