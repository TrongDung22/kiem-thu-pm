using System;

namespace Bai4
{
    // Lớp ĐIỂM (Theo đề bài trang 25)
    class Diem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Diem(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    // Lớp HÌNH CHỮ NHẬT
    class HinhChuNhat
    {
        public Diem TopLeft { get; set; }
        public Diem BottomRight { get; set; }

        public HinhChuNhat(Diem topLeft, Diem botRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = botRight;
        }

        // 1. Tính diện tích
        public int TinhDienTich()
        {
            // Chiều dài = trị tuyệt đối hiệu 2 X
            int width = Math.Abs(BottomRight.X - TopLeft.X);
            // Chiều rộng = trị tuyệt đối hiệu 2 Y
            int height = Math.Abs(TopLeft.Y - BottomRight.Y);
            return width * height;
        }

        // 2. Kiểm tra giao nhau
        public bool KiemTraGiaoNhau(HinhChuNhat other)
        {
            // Logic: Hai hình KHÔNG giao nhau nếu 1 cái nằm hoàn toàn bên trái/phải/trên/dưới cái kia.
            // Nếu không rơi vào các trường hợp đó -> Là có giao nhau.

            // Giả sử hệ tọa độ: Y tăng lên trên (Toán học)
            // TopLeft.X < BottomRight.X
            // TopLeft.Y > BottomRight.Y

            // Kiểm tra ranh giới
            if (this.TopLeft.X > other.BottomRight.X || other.TopLeft.X > this.BottomRight.X)
                return false; // Nằm lệch hẳn theo chiều ngang

            if (this.BottomRight.Y > other.TopLeft.Y || other.BottomRight.Y > this.TopLeft.Y)
                return false; // Nằm lệch hẳn theo chiều dọc

            return true;
        }
    }

    class Program
    {
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
            Console.WriteLine("--- BAT DAU TEST BAI 4: HINH CHU NHAT ---\n");

            // Setup dữ liệu cho TC15: Hình 1 (0,10) tới (10,0) -> Vuông 10x10
            Diem d1 = new Diem(0, 10);
            Diem d2 = new Diem(10, 0);
            HinhChuNhat hcn1 = new HinhChuNhat(d1, d2);

            // TC15: Test Diện tích
            TestCase("TC15_DienTich", hcn1.TinhDienTich().ToString(), "100");

            // Setup hình 2 cho TC16: (5,15) tới (15,5) -> Giao nhau với hcn1
            HinhChuNhat hcn2 = new HinhChuNhat(new Diem(5, 15), new Diem(15, 5));
            // TC16: Test Giao nhau (Có)
            TestCase("TC16_CoGiaoNhau", hcn1.KiemTraGiaoNhau(hcn2).ToString(), "True");

            // Setup hình 3 cho TC17: (20,20) tới (30,10) -> Nằm tít đằng xa
            HinhChuNhat hcn3 = new HinhChuNhat(new Diem(20, 20), new Diem(30, 10));
            // TC17: Test Giao nhau (Không)
            TestCase("TC17_KhongGiao", hcn1.KiemTraGiaoNhau(hcn3).ToString(), "False");

            // Setup hình 4: (2,8) tới (8,2) -> Nằm lọt thỏm bên trong hcn1
            HinhChuNhat hcn4 = new HinhChuNhat(new Diem(2, 8), new Diem(8, 2));
            // TC18: Test Giao nhau (Lồng nhau cũng tính là giao)
            TestCase("TC18_LongNhau", hcn1.KiemTraGiaoNhau(hcn4).ToString(), "True");

            Console.WriteLine("\n--- XONG BAI 4 ---");
            Console.ReadKey();
        }
    }
}