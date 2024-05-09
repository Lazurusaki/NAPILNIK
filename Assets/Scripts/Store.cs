using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Store : MonoBehaviour
{
    public interface IWarehouse
    {
        public bool CheckAvailability(Good good, int count);
        void Remove(Good good, int count);
    }

    public class Shop
    {
        private readonly Warehouse _warehouse;

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

    public class Warehouse : IWarehouse
    {
        private readonly Dictionary<Good, int> _goods;

        public Dictionary<Good, int> Goods { get; private set; }

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

        public bool CheckAvailability(Good good, int count)
        {
            if (!_goods.ContainsKey(good) || _goods[good] < count)
            {
                return false;
            }

            return true;
        }
    }

    public class Cart
    {
        private readonly Dictionary<Good, int> _goods;

        public Cart(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException("Warehouse can't be null");
            }

            _goods = new Dictionary<Good, int>();
        }

        public void Add(IWarehouse warehouse, Good good, int count)
        {
            if (good == null)
            {
                throw new ArgumentNullException("Good can't be null");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("Count can't be less than 1");
            }

            if (!warehouse.CheckAvailability(good, count))
            {
                throw new ArgumentException("Warehouse don't contain good");
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

        public void Order(IWarehouse warehouse)
        {
            if (_goods.Count > 0)
            {
                foreach (var good in _goods)
                {
                    warehouse.Remove(good.Key, good.Value);
                }
            }
        }
    }

    public class Good
    {
        private string _name;

        public string Name { get; private set; }

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

