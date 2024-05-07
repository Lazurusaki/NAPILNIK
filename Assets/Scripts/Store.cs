using System;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{    
    class Shop
    {
        private Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException("Warehouse can't be null");
            }

            _warehouse = warehouse;
        }

        public Cart Cart()
        {
            return new Cart(_warehouse);
        }        
    }

    class Warehouse
    {
        private Dictionary<Good, int> _goods;

        public readonly Dictionary<Good, int> Goods;

        public Warehouse()
        {
            _goods = new Dictionary<Good, int>();
            Goods = _goods;
        }

        public void Delive(Good good, int count)
        {
            if (good == null)
            {
                throw new ArgumentNullException("Good can't be null");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("Count can't be less than 1");
            }

            if (_goods.ContainsKey(good))
            {
                _goods[good] += count;
            }
            else
            {
                _goods.Add(good, count);
            }
        }

        public void Remove(Good good, int count)
        {
            if (good == null)
            {
                throw new ArgumentNullException("Good can't be null");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("Count can't be less than 1");
            }

            if (!_goods.ContainsKey(good))
            {
                throw new ArgumentException("Warehouse don't contain good");        
            }
            
            if ( count > _goods[good])
            {
                throw new ArgumentOutOfRangeException("Removing good count can't be greater than warehouse contains");       
            }

            if (_goods[good] == count)
            {
                _goods.Remove(good);
            }
            else
            {
                 _goods[good] -= count;
            }           
        }
    }

    class Cart
    {
        private Dictionary<Good, int> _goods;
        private Warehouse _warehouse;

        public Cart(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException("Warehouse can't be null");
            }

            _goods = new Dictionary<Good, int>();
            _warehouse = warehouse;
        }

        public void Add(Good good, int count)
        {
            if (good == null)
            {
                throw new ArgumentNullException("Good can't be null");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("Count can't be less than 1");
            }

            if (!_warehouse.Goods.ContainsKey(good))
            {
                throw new ArgumentNullException("Good is not available");
            }

            if (_warehouse.Goods[good] < count)
            {
                throw new ArgumentOutOfRangeException("Not enough goods");
            }

            if (_goods.ContainsKey(good))
            {
                _goods[good] += count;
            }
            else
            {
                _goods.Add(good, count);
            }

            _warehouse.Remove(good, count);
        }

        public void Order()
        {
            if (_goods.Count > 0)
            {
                foreach (var good in _goods)
                {
                    _warehouse.Remove(good.Key, good.Value);
                }
            }
        }
    }

    class Good
    {
        private string _name;

        public readonly string Name;

        public Good(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be null or empty");
            }

            _name = name;
            Name = _name;
        }
    }
}