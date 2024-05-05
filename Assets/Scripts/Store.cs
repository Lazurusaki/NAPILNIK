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

        public void AddToCart(Good good, int count, Cart cart)
        {
            if (_warehouse.Goods.ContainsKey(good))
            {
                if (_warehouse.Goods[good] >= count)
                {
                    cart.Add(good, count);
                    _warehouse.Remove(good, count);
                }
                else
                {
                    print("Noot enought goods available");
                }
            }
            else
            {
                print(good.Name + " is not available");
            }
        }   
    }

    class Warehouse
    {
        private Dictionary<Good, int> _goods;

        public Dictionary<Good, int> Goods => _goods;

        public Warehouse()
        {
            _goods = new Dictionary<Good, int>();
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

            if (_goods.ContainsKey(good))
            {
                if (_goods[good] > count)
                {
                    _goods[good] -= count;
                }
                else if (_goods[good] == count)
                {
                    _goods.Remove(good);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Removing good count can't be greater than warehouse contains");
                }
            }
            else
            {
                print("Warehouse don't contain this good");
            }
        }

        public void ShowGoods()
        {
            if (_goods.Count > 0)
            {
                foreach (var good in _goods)
                {
                    print($"Name: {good.Key.Name}  Count: {good.Value}");
                }
            }
            else
            {
                print("Warehouse is empty");
            }
        }
    }

    class Cart
    {
        private Dictionary<Good, int> _goods;

        public Cart()
        {
            _goods = new Dictionary<Good, int>();
        }

        public void Add(Good good, int count)
        {
            if (good == null)
            {
                throw new ArgumentNullException("Warehouse can't be null");
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

        public void Show()
        {
            if (_goods.Count > 0)
            {
                foreach (var good in _goods)
                {
                    print($"Name: {good.Key.Name}  Count: {good.Value}");
                }
            }
            else
            {
                print("Cart is empty");
            }
        }
    }

    class Good
    {
        private string _name;

        public string Name => _name;

        public Good(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be null or empty");
            }

            _name = name;
        }
    }
}