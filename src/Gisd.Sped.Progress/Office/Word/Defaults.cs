using Gisd.Sped.Office.Word;
using Independentsoft.Office;
using Independentsoft.Office.Word;

namespace Gisd.Sped.Progress
{
    public static class Defaults
    {
        public const int LargeFontSize = 0;
        public const int FontSize = 24;
        public const int SmallFontSize = 20;
        public const string FontFamily = FontFamilies.Arial;
        public const int LargeCellWidth = 12000;
        public const int SmallCellWidth = 2000;

        public const ExtendedBoolean True = ExtendedBoolean.True;
        public const ExtendedBoolean False = ExtendedBoolean.False;
        public const ExtendedBoolean Off = ExtendedBoolean.Off;

        public static readonly Underline SingleUnderline = new Underline(UnderlinePattern.Single);


        public static readonly Independentsoft.Office.Word.Sections.PageMargins PageMargins = new Independentsoft.Office.Word.Sections.PageMargins
        {
            Bottom = 360,
            Left = 360,
            Right = 360,
            Top = 360
        };
    }
}
