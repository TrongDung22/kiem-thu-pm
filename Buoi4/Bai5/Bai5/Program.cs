using System;
using System.Collections.Generic;

namespace Bai5_HocBong
{
    // LỚP HỌC VIÊN
    class HocVien
    {
        public string MaHV { get; set; }
        public string HoTen { get; set; }
        // Điểm 3 môn
        public double Diem1 { get; set; }
        public double Diem2 { get; set; }
        public double Diem3 { get; set; }

        public HocVien(string ma, string ten, double d1, double d2, double d3)
        {
            this.MaHV = ma;
            this.HoTen = ten;
            this.Diem1 = d1;
            this.Diem2 = d2;
            this.Diem3 = d3;
        }

        // HÀM KIỂM TRA HỌC BỔNG (Logic chính cần test)
        public bool CheckHocBong()
        {
            // 1. Tính điểm trung bình
            double dtb = (Diem1 + Diem2 + Diem3) / 3;

            // 2. Điều kiện 1: ĐTB >= 8.0
            if (dtb < 8.0)
                return false;

            // 3. Điều kiện 2: Không môn nào dưới 5.0
            if (Diem1 < 5.0 || Diem2 < 5.0 || Diem3 < 5.0)
                return false;

            // Nếu qua hết các ải trên -> ĐẬU
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
            Console.WriteLine("--- BAT DAU TEST BAI 5: XET HOC BONG ---\n");

            // TC19: Học đều 8 điểm -> ĐTB 8.0, ko môn nào < 5 -> ĐẬU
            HocVien hv1 = new HocVien("HV01", "Nguyen Van A", 8, 8, 8);
            TestCase("TC19_DatChuan", hv1.CheckHocBong().ToString(), "True");

            // TC20: Học đều 7 điểm -> ĐTB 7.0 -> RỚT
            HocVien hv2 = new HocVien("HV02", "Nguyen Van B", 7, 7, 7);
            TestCase("TC20_RotDoDiemThap", hv2.CheckHocBong().ToString(), "False");

            // TC21: Học giỏi nhưng dính điểm liệt (10, 10, 4) -> ĐTB 8.0 nhưng Hóa 4 -> RỚT
            HocVien hv3 = new HocVien("HV03", "Nguyen Van C", 10, 10, 4);
            TestCase("TC21_RotDoDiemLiet", hv3.CheckHocBong().ToString(), "False");

            // TC22: Học lệch nhưng vừa đủ thoát liệt (10, 9, 5) -> ĐTB 8.0, Thấp nhất 5 -> ĐẬU
            HocVien hv4 = new HocVien("HV04", "Nguyen Van D", 10, 9, 5);
            TestCase("TC22_VuaDuDau", hv4.CheckHocBong().ToString(), "True");

            Console.WriteLine("\n--- HOAN THANH TOAN BO 5 BAI ---");
            Console.ReadKey();
        }
    }
}