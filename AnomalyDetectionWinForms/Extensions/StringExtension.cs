using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDetectionWinForms
{
    static partial class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string that) => string.IsNullOrWhiteSpace(that);
    }
}
