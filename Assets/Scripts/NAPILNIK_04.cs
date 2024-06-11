using System;
using System.Security.Cryptography;
using System.Text;

namespace NAPILNIK_04
{
    public interface IPaymentSystem
    {
        public string GetPayingLink(Order order);
    }

    public interface IHasher
    {
        public string GetHash(string input);
    }

    class Program
    {
        static void Main(string[] args)
        {
            string secretKey = "UltraMegaSuperSecretSystemKey";

            int orderID = 213214;
            int orderAmount = 100000;

            Order order = new Order(orderID, orderAmount);

            System1 system1 = new System1(new MD5Hasher());
            System2 system2 = new System2(new MD5Hasher());
            System3 system3 = new System3(secretKey, new SHA1Hasher());
  
            system1.GetPayingLink(order);
            system2.GetPayingLink(order);
            system3.GetPayingLink(order);
        }
    }

    public class MD5Hasher : IHasher
    {
        public string GetHash(string input)
        {
            if (input == null) 
                throw new ArgumentNullException("Input string can't be null");

            if (input.Length < 1)
                throw new ArgumentException("Input string can't be empty");

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder stringBuiler = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuiler.Append(hashBytes[i].ToString("X2"));
                }

                return stringBuiler.ToString();
            }
        }
    }

    public class SHA1Hasher : IHasher
    {
        public string GetHash(string input)
        {
            if (input == null) 
                throw new ArgumentNullException("Input string can't be null");

            if (input.Length < 1) 
                throw new ArgumentException("Input string can't be empty");

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                StringBuilder stringBuiler = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuiler.Append(hashBytes[i].ToString("X2"));
                }

                return stringBuiler.ToString();
            }
        }
    }

    public class Order
    {
        public int ID { get; private set; }
        public int Amount { get; private set; }

        public Order(int id, int amount)
        {
            if (int.TryParse(id.ToString(), out _) == false) 
                throw new ArgumentException("Incorrect ID");

            if (amount < 1) 
                throw new ArgumentOutOfRangeException("Amount can't be less than 1");

            ID = id;
            Amount = amount;
        }
    }

    public class System1: IPaymentSystem
    {
        private IHasher _hasher;

        public System1(IHasher hasher)
        {      
            if (hasher == null)
                throw new ArgumentNullException("Hasher can't be null");

            _hasher = hasher;
        }

        public string GetPayingLink(Order Order)
        {
            return "pay.system1.ru/order?amount=" + Order.Amount + "RUB&hash=" + _hasher.GetHash(Order.ID.ToString());
        }    
    }

    public class System2 : IPaymentSystem
    {
        private IHasher _hasher;

        public System2(IHasher hasher)
        {
            if (hasher == null)
                throw new ArgumentNullException("Hasher can't be null");

            _hasher = hasher;
        }

        public  string GetPayingLink(Order Order)
        {
            return "order.system2.ru/pay?hash=" + _hasher.GetHash(Order.ID.ToString() + Order.Amount.ToString());
        }
    }

    public class System3 : IPaymentSystem
    {
        private string _key;
        private IHasher _hasher;

        public System3(string key, IHasher hasher)
        {
            if (key == null)
                throw new ArgumentNullException("Secret key can't be null");

            if (key.Length < 1)
                throw new ArgumentException("Secret key can't be empty");

            _key = key;

            if (hasher == null)
                throw new ArgumentNullException("Hasher can't be null");
 
            _hasher=hasher;
    }

        public string GetPayingLink(Order Order)
        {
            return "system3.com/pay?amount=" + Order.Amount + "&currency=RUB&hash=" + _hasher.GetHash(Order.Amount.ToString() + Order.ID.ToString() + _key);
        }
    }
}
