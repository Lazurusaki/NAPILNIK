using System;
using System.Security.Cryptography;
using System.Text;

namespace NAPILNIK_04
{
    public interface IPaymentSystem
    {
        public string GetPayingLink(Order order);
    }

    class Program
    {
        static void Main(string[] args)
        {
            string secretKey = "UltraMegaSuperSecretSystemKey";

            System1 system1 = new System1();
            System2 system2 = new System2();
            System3 system3 = new System3(secretKey);

            int orderID = 213214;
            int orderAmount  = 100000;

            Order order = new Order(orderID, orderAmount);

            system1.GetPayingLink(order);
            system2.GetPayingLink(order);
            system3.GetPayingLink(order);
        }
    }

    public static class HashUtility
    {
        public static string GetMd5Hash(string input)
        {
            if (input == null) throw new ArgumentNullException("Input string can't be null");
            if (input.Length < 1) throw new ArgumentException("Input string can't be empty");

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string GetSha1Hash(string input)
        {
            if (input == null) throw new ArgumentNullException("Input string can't be null");
            if (input.Length < 1) throw new ArgumentException("Input string can't be empty");

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }


    public class Order
    {
        private int _id;
        private int _amount;

        public int ID { get; private set; }
        public int Amount { get; private set; }

        public Order(int id, int amount)
        {
            if (!int.TryParse(id.ToString(), out _)) throw new ArgumentException("Incorrect ID");
            if (amount < 1) throw new ArgumentOutOfRangeException("Amount can't be less than 1");

            _id = id;
            _amount = amount;
            ID = id;
            Amount = amount;
        }
    }

    public class System1 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string link = "hash=" + HashUtility.GetMd5Hash(order.ID.ToString());
            return link;
        }
    }

    public class System2 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string link = "hash=" + HashUtility.GetMd5Hash(order.ID.ToString()) + "amount=" + order.Amount;
            return link;
        }
    }

    public class System3 : IPaymentSystem
    {
        private string _key;

        public System3(string key)
        {
            if (key.Length == 0) throw new ArgumentException("Incorrect system key");

            _key = key;
        }

        public string GetPayingLink(Order order)
        {
            string link = "hash=" + HashUtility.GetSha1Hash(order.Amount.ToString()) + order.ID +  "key=" + _key;
            return link;
        }
    }
}
