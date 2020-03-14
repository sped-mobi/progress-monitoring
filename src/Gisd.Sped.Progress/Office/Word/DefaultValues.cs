using Independentsoft.Office;
using Independentsoft.Office.Word;

namespace Gisd.Sped.Office.Word
{
    public static class DefaultValues
    {
        public const int FontSize = 24;
        public const string FontFamily = "Arial";
        public const int LargeCellWidth = 12000;
        public const int SmallCellWidth = 2000;

        public const ExtendedBoolean True = ExtendedBoolean.True;
        public const ExtendedBoolean False = ExtendedBoolean.False;
        public const ExtendedBoolean Off = ExtendedBoolean.Off;

        public static readonly Underline SingleUnderline = new Underline(UnderlinePattern.Single);
    }
}
