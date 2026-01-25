using System;
using System.Collections.Generic;
using System.Linq;

namespace Bai2
{
    // LỚP XỬ LÝ 
    class Polynomial
    {
        private int n;
        private List<int> a;

        public Polynomial(int n, List<int> a)
        {
            // Logic kiểm tra lỗi theo đề bài
            if (n < 0 || a.Count != n + 1)
                throw new ArgumentException("Invalid Data");
            this.n = n;
            this.a = a;
        }

        public int Cal(double x)
        {
            int result = 0;
            for (int i = 0; i <= this.n; i++)
            {
                // Công thức: a[i] * x^i
                result += (int)(a[i] * Math.Pow(x, i));
            }
            return result;
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
            Console.WriteLine("--- BAT DAU TEST BAI 2: DA THUC ---\n");

            // TC06: Bậc 1 (n=1, a={2, 3}, x=2) -> 2 + 3*2 = 8
            try
            {
                Polynomial p1 = new Polynomial(1, new List<int> { 2, 3 });
                TestCase("TC06_Bac1", p1.Cal(2).ToString(), "8");
            }
            catch (Exception ex) { TestCase("TC06_Bac1", ex.Message, "8"); }

            // TC07: Bậc 2 (n=2, a={1, 2, 1}, x=3) -> 1 + 2*3 + 1*3^2 = 1+6+9 = 16
            try
            {
                Polynomial p2 = new Polynomial(2, new List<int> { 1, 2, 1 });
                TestCase("TC07_Bac2", p2.Cal(3).ToString(), "16");
            }
            catch (Exception ex) { TestCase("TC07_Bac2", ex.Message, "16"); }

            // TC08: Test lỗi Bậc âm (n=-1) -> Mong đợi lỗi "Invalid Data"
            try
            {
                Polynomial pError1 = new Polynomial(-1, new List<int> { 1 });
                // Nếu không lỗi là sai
                TestCase("TC08_LoiBacAm", "Khong loi", "Invalid Data");
            }
            catch (ArgumentException ex)
            {
                // Nếu bắt được lỗi đúng thông báo thì Pass
                TestCase("TC08_LoiBacAm", ex.Message, "Invalid Data");
            }

            // TC09: Test lỗi thiếu hệ số (n=2 mà chỉ đưa 2 số) -> Mong đợi lỗi "Invalid Data"
            try
            {
                Polynomial pError2 = new Polynomial(2, new List<int> { 1, 2 });
                TestCase("TC09_ThieuHeSo", "Khong loi", "Invalid Data");
            }
            catch (ArgumentException ex)
            {
                TestCase("TC09_ThieuHeSo", ex.Message, "Invalid Data");
            }

            Console.WriteLine("\n--- XONG BAI 2 ---");
            Console.ReadKey();
        }
    }
}