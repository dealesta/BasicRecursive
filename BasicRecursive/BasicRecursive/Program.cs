using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicRecursive
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Original List!");

            var items = new List<Item>();

            // Populate sample data
            const int numberOfSample = 50;
            const int numberOfRoot = 1;

            var rnd = new Random();

            Enumerable.Range(1, numberOfSample).ToList().ForEach(a =>
            {
                var item = new Item()
                {
                    Id = a,
                    Text = RandomString(10),
                    ParentId = a <= numberOfRoot ? (int?)null : rnd.Next(1, a)
                };
                items.Add(item);
            });

            Console.WriteLine($"{"Id",3} : {"Text",15} : {"ParentId",3}");
            Console.WriteLine("----------------------------------------");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,3} : {item.Text,15} : {item.ParentId,3}");
            }

            var result = new List<Item>();
            foreach (var item in items.Where(a => a.ParentId == null))
            {
                result.Add(BuildTree(item, items));
            }
            Console.WriteLine("========================");
            Console.WriteLine("Recursive List!");
            Console.WriteLine($"{"Id",3} : {"Text",15} : {"ParentId",3}");
            Console.WriteLine("----------------------------------------");
            int count = 0;
            PrintTree(result);

            Console.ReadLine();
        }

        //To create recursive object
        static Item BuildTree(Item root, IList<Item> items)
        {

            var children = new List<Item>();

            if (items.Any(a => a.ParentId == root.Id))
            {
                foreach (var item in items.Where(a => a.ParentId == root.Id))
                {
                    children.Add(BuildTree(item, items));
                }
            }

            root.Children = children;

            return root;
        }
        //To Printout recursive object
        static void PrintTree(IList<Item> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,3} : {item.Text,15} : {item.ParentId,3}");

                if (item.Children.Count() > 0)
                {
                    PrintTree(item.Children);
                }

            }
        }

        //Create random string source: https://stackoverflow.com/a/1344242
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    //Sample object
    class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public IList<Item> Children { get; set; }
    }
}
