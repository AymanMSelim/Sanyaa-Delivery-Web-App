using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global
{
    public static class Generator
    {
        public static string GenerateOTPCode(int length = 4)
        {
            return new Random(DateTime.Now.Millisecond).Next(1000, 9999).ToString($"D{length}");
        }
    }
}
