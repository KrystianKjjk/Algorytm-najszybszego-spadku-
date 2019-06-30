using ChartsCreator;

namespace FastestFallProgramExample.Model
{
    public class LineItem
    {
        public Coordinates BeginLine { get; set; }
        public Coordinates EndLine { get; set; }

        public LineItem( Coordinates begin, Coordinates end)
        {
            BeginLine = begin;
            EndLine = end;
        }
    }
}
