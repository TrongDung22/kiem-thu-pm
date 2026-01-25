using System;
using System.Collections.Generic;
using System.Linq;

namespace Bai3
{
    // LỚP XỬ LÝ
    public class Radix
    {
        private int number;

        public Radix(int number)
        {
            if (number < 0)
                throw new ArgumentException("Incorrect Value");
            this.number = number;
        }

        public string ConvertDecimalToAnother(int radix)
        {
            if (radix < 2 || radix > 16)
                throw new ArgumentException("Invalid Radix");

            int n = this.number;
            if (n == 0) return "0";

            List<string> result = new List<string>();
            while (n > 0)
            {
                int value = n % radix;
                if (value < 10)
                    result.Add(value.ToString());
                else
                {
                    // Chuyển 10-15 thành A-F
                    switch (value)
                    {
                        case 10: result.Add("A"); break;
                        case 11: result.Add("B"); break;
                        case 12: result.Add("C"); break;
                        case 13: result.Add("D"); break;
                        case 14: result.Add("E"); break;
                        case 15: result.Add("F"); break;
                    }
                }
                n /= radix;
            }
            result.Reverse(); // Đảo ngược lại mới đúng thứ tự
            return String.Join("", result.ToArray());
        }
    }

    class Program
    {
        // HÀM HỖ TRỢ TEST
        static void TestCase(string testID, string actual, string expected)
        {
            if (actual == expected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] {testID} | KQ: {actual}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] {testID} | Ra: {actual} - Mong: {expected}");
            }
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- BAT DAU TEST BAI 3: DOI CO SO ---\n");

            // TC10: 10 đổi sang nhị phân -> 1010
            try
            {
                Radix r1 = new Radix(10);
                TestCase("TC10_He2", r1.ConvertDecimalToAnother(2), "1010");
            }
            catch (Exception ex) { TestCase("TC10_He2", ex.Message, "1010"); }

            // TC11: 255 đổi sang Hex -> FF
            try
            {
                Radix r2 = new Radix(255);
                TestCase("TC11_He16_Max", r2.ConvertDecimalToAnother(16), "FF");
            }
            catch (Exception ex) { TestCase("TC11_He16_Max", ex.Message, "FF"); }

            // TC12: 10 đổi sang Hex -> A
            try
            {
                Radix r3 = new Radix(10);
                TestCase("TC12_He16_Chu", r3.ConvertDecimalToAnother(16), "A");
            }
            catch (Exception ex) { TestCase("TC12_He16_Chu", ex.Message, "A"); }

            // TC13: Test lỗi nhập số âm
            try
            {
                Radix rErr1 = new Radix(-5);
                TestCase("TC13_LoiSoAm", "Khong loi", "Incorrect Value");
            }
            catch (ArgumentException ex)
            {
                TestCase("TC13_LoiSoAm", ex.Message, "Incorrect Value");
            }

            // TC14: Test lỗi cơ số sai (Base 1)
            try
            {
                Radix rErr2 = new Radix(10);
                rErr2.ConvertDecimalToAnother(1); // Sai quy định
                TestCase("TC14_LoiCoSo", "Khong loi", "Invalid Radix");
            }
            catch (ArgumentException ex)
            {
                TestCase("TC14_LoiCoSo", ex.Message, "Invalid Radix");
            }

            Console.WriteLine("\n--- XONG BAI 3 ---");
            Console.ReadKey();
        }
    }
}