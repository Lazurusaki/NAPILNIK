using UnityEngine;

public class NAPILNIK_01
{
    class Weapon
    {
        private int _damage;
        private int _bullets;

        public Weapon(int damage, int bullets)
        {
            _damage = Mathf.Max(0,damage);
            _bullets = Mathf.Max(0, bullets);
        }

        public void Fire(Player player)
        {
            if (player == null)
            {
                return;
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
            if (damage <= 0)
            {
                return;
            }

            _health = Mathf.Max(0, _health - damage);
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public Bot(Weapon weapon) 
        {
            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            if (player == null && _weapon == null)
            {
                return;
            }

            _weapon.Fire(player);
        }
    }
}
