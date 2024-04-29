using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace TA_Lab5
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public interface IHashFunction<T>
        {
            int ComputeHash(T key);
        }

        public abstract class HashTable<TKey, TValue>
        {
            protected const int DefaultCapacity = 64; 
            public List<KeyValuePair<TKey, TValue>>[] items;

            public HashTable()
            {
                items = new List<KeyValuePair<TKey, TValue>>[DefaultCapacity];
            }

            public HashTable(int capacity)
            {
                items = new List<KeyValuePair<TKey, TValue>>[capacity];
            }

            public abstract void Add(TKey key, TValue value);

            public abstract void Remove(int key, TValue value);

            public abstract string Search(int key);

            public abstract bool TryGetValue(TKey key, out TValue value);
            public abstract void Clear();

            public abstract int Count { get; }
            
        }

        public class MyHashTable<TKey, TValue> : HashTable<TKey, TValue>
        {
            private const double LoadFactor = 0.75;
            private int count;
            private readonly IHashFunction<TKey> hashFunction;

            public MyHashTable(IHashFunction<TKey> hashFunction) : base()
            {
                this.hashFunction = hashFunction;
            }

            public MyHashTable(int capacity, IHashFunction<TKey> hashFunction) : base(capacity)
            {
                this.hashFunction = hashFunction;
            }

            private void Resize()
            {
                if ((double)count / items.Length >= LoadFactor)
                {
                    int newCapacity = items.Length * 2;
                    var newItems = new List<KeyValuePair<TKey, TValue>>[newCapacity];

                    foreach (var item in items)
                    {
                        if (item != null)
                        {
                            foreach (var pair in item)
                            {
                                int index = ComputeHash(pair.Key);
                                if (newItems[index] == null)
                                {
                                    newItems[index] = new List<KeyValuePair<TKey, TValue>>();
                                }
                                newItems[index].Add(pair);
                            }
                        }
                    }

                    items = newItems;
                }
            }

            private int ComputeHash(TKey key)
            {
                return Math.Abs(hashFunction.ComputeHash(key) % items.Length);
            }

            public override void Add(TKey key, TValue value)
            {
                Resize();

                int index = ComputeHash(key);

                if (items[index] == null)
                {
                    items[index] = new List<KeyValuePair<TKey, TValue>>();
                }

                items[index].Add(new KeyValuePair<TKey, TValue>(key, value));

                count++;
            }

            public override void Remove(int key, TValue value)
            {
                
                int index = key;

                if (items[index] != null)
                {
                    if (items[index].Count > 1)
                    {
                        int counter = -1;
                        foreach (var pair in items[index])
                        {
                            if(pair.Value.Equals(value))
                            {
                                counter++;
                            }
                        }
                        items[index].RemoveAt(counter);
                    }
                    else
                    {
                        items[index].RemoveAt(0);
                    }

                    count--;
                }

                
            }

            public override string Search(int key)
            {
                int index = key;
                string message = "";

                if (items[index] != null)
                {
                    
                    foreach (var pair in items[index])
                    {
                        message += $"Ім'я: {pair.Key}, Середній бал: {pair.Value}\n";
                    }
                    
                }
                return message;
            }




            public override bool TryGetValue(TKey key, out TValue value)
            {
                int index = ComputeHash(key);

                if (items[index] != null)
                {
                    foreach (var pair in items[index])
                    {
                        if (pair.Key.Equals(key))
                        {
                            value = pair.Value;
                            return true;
                        }
                    }
                }

                value = default(TValue);
                return false;
            }

            public override void Clear()
            {
                Array.Clear(items, 0, items.Length);
                count = 0;
            }

            public override int Count => count;

            
        }

        public class MultiplicationHashFunction : IHashFunction<string>
        {
            public int ComputeHash(string key)
            {
                const double C = 0.6180339887; 
                const int M = 50;

                double hashValue = M * ((C * GetStringCode(key)) % 1);

                return (int)hashValue;
            }

            private static double GetStringCode(string key)
            {
                double code = 0;

                foreach (char c in key)
                {
                    code += (int)c; 
                }

                return code;
            }
        }



    }
}
