using UnityEngine;

public class NAPILNIK_01
{
    class Weapon
    {
        private int _damage;
        private int _bullets;

        public void Fire(Player player)
        {
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
        private int _minHealth = 0;

        public void TakeDamage(int damage)
        {
            _health = Mathf.Max(_minHealth, _health - damage);
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            _weapon.Fire(player);
        }
    }
}
