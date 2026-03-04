using System;
using System.Linq;
using System.IO;

namespace LinqDay02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup Data
            var products = ListGenerators.ProductList;
            var customers = ListGenerators.CustomerList;
            string[] dictionaryWords = File.ReadAllLines(@"H:\Depi-Course\Seasion24-Tech(LINQ-EF)D02\LinqDay02\LinqDay02\dictionary_english.txt");

            // ==========================================
            // LINQ - RESTRICTION OPERATORS
            // ==========================================

            // 1. Find all products that are out of stock. 
            var outOfStock = products.Where(p => p.UnitsInStock == 0);

            // 2. Find all products that are in stock and cost more than 3.00 per unit. 
            var inStockMoreThan3 = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m);

            // 3. Returns digits whose name is shorter than their value. 
            string[] digitsArr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }; 
            var shortDigits = digitsArr.Where((name, index) => name.Length < index);


            // ==========================================
            // LINQ - ELEMENT OPERATORS
            // ==========================================

            // 1. Get first Product out of Stock [cite: 10]
            var firstOutOfStock = products.FirstOrDefault(p => p.UnitsInStock == 0);

            // 2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned. 
            var expensiveProduct = products.FirstOrDefault(p => p.UnitPrice > 1000m);

            // 3. Retrieve the second number greater than 5 [cite: 12]
            int[] arrElement = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 }; // [cite: 13]
            var secondNumberGt5 = arrElement.Where(n => n > 5).ElementAtOrDefault(1);


            // ==========================================
            // LINQ - AGGREGATE OPERATORS
            // ==========================================

            int[] arrAgg = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 }; // [cite: 16]

            // 1. Uses Count to get the number of odd numbers in the array 
            var oddCount = arrAgg.Count(n => n % 2 != 0);

            // 2. Return a list of customers and how many orders each has. 
            var customerOrdersCount = customers.Select(c => new { c.Name, OrderCount = c.Orders.Length });

            // 3. Return a list of categories and how many products each has 
            var categoryProductCount = products.GroupBy(p => p.Category).Select(g => new { Category = g.Key, Count = g.Count() });

            // 4. Get the total of the numbers in an array. 
            var totalArrNumbers = arrAgg.Sum();

            // 5. Get the total number of characters of all words in dictionary_english.txt 
            var totalChars = dictionaryWords.Sum(w => w.Length);

            // 6. Get the total units in stock for each product category. 
            var totalStockPerCategory = products.GroupBy(p => p.Category).Select(g => new { Category = g.Key, TotalStock = g.Sum(p => p.UnitsInStock) });

            // 7. Get the length of the shortest word in dictionary_english.txt 
            var shortestWordLen = dictionaryWords.Min(w => w.Length);

            // 8. Get the cheapest price among each category's products 
            var cheapestPricePerCategory = products.GroupBy(p => p.Category).Select(g => new { Category = g.Key, MinPrice = g.Min(p => p.UnitPrice) });

            // 9. Get the products with the cheapest price in each category (Use Let) 
            var cheapestProductsPerCategory = from p in products
                                              group p by p.Category into g
                                              let minPrice = g.Min(x => x.UnitPrice)
                                              select new { Category = g.Key, Products = g.Where(x => x.UnitPrice == minPrice) };

            // 10. Get the length of the longest word in dictionary_english.txt 
            var longestWordLen = dictionaryWords.Max(w => w.Length);

            // 11. Get the most expensive price among each category's products. 
            var maxPricePerCategory = products.GroupBy(p => p.Category).Select(g => new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) });

            // 12. Get the products with the most expensive price in each category. 
            var mostExpensiveProductsPerCategory = from p in products
                                                   group p by p.Category into g
                                                   let maxPrice = g.Max(x => x.UnitPrice)
                                                   select new { Category = g.Key, Products = g.Where(x => x.UnitPrice == maxPrice) };

            // 13. Get the average length of the words in dictionary_english.txt 
            var avgWordLen = dictionaryWords.Average(w => w.Length);

            // 14. Get the average price of each category's products. 
            var avgPricePerCategory = products.GroupBy(p => p.Category).Select(g => new { Category = g.Key, AvgPrice = g.Average(p => p.UnitPrice) });


            // ==========================================
            // LINQ - ORDERING OPERATORS
            // ==========================================

            // 1. Sort a list of products by name 
            var sortedProductsByName = products.OrderBy(p => p.ProductName);

            // 2. Uses a custom comparer to do a case-insensitive sort of the words in an array. 
            string[] wordsArr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "Clover", "cHeRry" }; // 
            var sortedWordsCaseInsensitive = wordsArr.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);

            // 3. Sort a list of products by units in stock from highest to lowest. 
            var sortedProductsByStockDesc = products.OrderByDescending(p => p.UnitsInStock);

            // 4. Sort a list of digits, first by length of their name, and then alphabetically by the name itself. 
            var sortedDigits = digitsArr.OrderBy(d => d.Length).ThenBy(d => d);

            // 5. Sort first by word length and then by a case-insensitive sort of the words in an array. 
            var sortedWordsLenThenCase = wordsArr.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);

            // 6. Sort a list of products, first by category, and then by unit price, from highest to lowest. 
            var sortedProductsCatThenPrice = products.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);

            // 7. Sort first by word length and then by a case-insensitive descending sort of the words in an array. 
            var sortedWordsLenThenCaseDesc = wordsArr.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);

            // 8. Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array. 
            var reversedDigitsWithI = digitsArr.Where(d => d.Length > 1 && d[1] == 'i').Reverse();


            // ==========================================
            // LINQ - TRANSFORMATION OPERATORS
            // ==========================================

            // 1. Return a sequence of just the names of a list of products. 
            var productNamesOnly = products.Select(p => p.ProductName);

            // 2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types). 
            string[] wordsT = { "aPPLE", "BlueBeRrY", "cHeRry" }; // 
            var upperLowerWords = wordsT.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });

            // 3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.  52]
            var productPropsRenamed = products.Select(p => new { p.ProductName, Price = p.UnitPrice });

            // 4. Determine if the value of ints in an array match their position in the array. 
            int[] arrTrans = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 }; // 
            var numberPositions = arrTrans.Select((num, index) => new { Number = num, InPlace = (num == index) });

            // 5. Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.  68]
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 }; // 
            int[] numbersB = { 1, 3, 5, 7, 8 }; // 
            var pairsA_lessThan_B = from a in numbersA
                                    from b in numbersB
                                    where a < b
                                    select new { A = a, B = b };

            // 6. Select all orders where the order total is less than 500.00. 
            var smallOrders = customers.SelectMany(c => c.Orders).Where(o => o.Total < 500.00);

            // 7. Select all orders where the order was made in 1998 or later. 
            var orders1998OrLater = customers.SelectMany(c => c.Orders).Where(o => o.OrderDate.Year >= 1998);


            // ==========================================
            // LINQ - PARTITIONING OPERATORS
            // ==========================================

            // 1. Get the first 3 orders from customers in Washington ]
            var first3WaOrders = customers.Where(c => c.Address == "WA").SelectMany(c => c.Orders).Take(3);

            // 2. Get all but the first 2 orders from customers in Washington. ]
            var skipFirst2WaOrders = customers.Where(c => c.Address == "WA").SelectMany(c => c.Orders).Skip(2);

            // 3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array. , 127]
            int[] numPart = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 }; // ]
            var takeWhilePosition = numPart.TakeWhile((n, index) => n >= index);

            // 4. Get the elements of the array starting from the first element divisible by 3. ]
            var skipWhileDivBy3 = numPart.SkipWhile(n => n % 3 != 0);

            // 5. Get the elements of the array starting from the first element less than its position. ]
            var skipWhileLessThanPos = numPart.SkipWhile((n, index) => n >= index);


            // ==========================================
            // LINQ - QUANTIFIERS
            // ==========================================

            // 1. Determine if any of the words in dictionary_english.txt contain the substring 'ei'. , 145]
            var containsEi = dictionaryWords.Any(w => w.Contains("ei"));

            // 2. Return a grouped a list of products only for categories that have at least one product that is out of stock. ]
            var groupsWithOutOfStock = products.GroupBy(p => p.Category).Where(g => g.Any(p => p.UnitsInStock == 0));

            // 3. Return a grouped a list of products only for categories that have all of their products in stock. ]
            var groupsWithAllInStock = products.GroupBy(p => p.Category).Where(g => g.All(p => p.UnitsInStock > 0));

            Console.WriteLine("All exercises completed successfully!");
        }
    }
}