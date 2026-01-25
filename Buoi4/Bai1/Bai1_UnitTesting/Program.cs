using System;

namespace Bai1
{
    class Program
    {
        // ==========================================
        // KHU VỰC 1: HÀM XỬ LÝ
        // ==========================================
        static double Power(double x, int n)
        {
            if (n == 0)
                return 1.0;
            else if (n > 0)
                return x * Power(x, n - 1);
            else
                // Trường hợp n < 0
                return Power(x, n + 1) / x;
        }

        // ==========================================
        // KHU VỰC 2: HÀM HỖ TRỢ TEST
        // ==========================================
        static void TestCase(string testID, double actual, double expected)
        {
            // So sánh số thực (double) cần độ lệch nhỏ (epsilon)
            if (Math.Abs(actual - expected) < 0.0001)
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

        // ==========================================
        // KHU VỰC 3: MAIN - CHẠY TEST
        // ==========================================
        static void Main(string[] args)
        {
            Console.WriteLine("--- BAT DAU TEST BAI 1: HAM MU (POWER) ---\n");

            // 1. Test trường hợp n = 0 (Theo Excel TC01)
            // 5 mũ 0 phải bằng 1
            TestCase("TC01_MuKhong", Power(5, 0), 1);

            // 2. Test trường hợp n > 0 (Theo Excel TC02)
            // 2 mũ 3 = 8
            TestCase("TC02_MuDuong", Power(2, 3), 8);

            // 3. Test số lẻ (Theo Excel TC03)
            // 1.5 mũ 2 = 2.25
            TestCase("TC03_SoThuc", Power(1.5, 2), 2.25);

            // 4. Test n < 0 (Theo Excel TC04)
            // 2 mũ -1 = 1/2 = 0.5
            TestCase("TC04_MuAm", Power(2, -1), 0.5);

            // 5. Test n < 0 sâu hơn (Theo Excel TC05)
            // 2 mũ -2 = 1/4 = 0.25
            TestCase("TC05_MuAm2", Power(2, -2), 0.25);

            Console.WriteLine("\n--- DA CHAY XONG ---");
            Console.ReadKey();
        }
    }
}