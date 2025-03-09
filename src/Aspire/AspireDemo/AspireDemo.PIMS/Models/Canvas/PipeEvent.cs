using AspireDemo.ApiService.Client.Models;
using AspireDemo.PIMS.Models.Clock;
using AspireDemo.PIMS.Models.Range;
using System.Drawing;

namespace AspireDemo.PIMS.Models.Canvas
{
    public record PipeEvent(
        int Id,
        VPMEventGroup Group,
        PipeRect Bottom,
        PipeRect Top,
        bool IsSelected)
    {
        public string Name { get; } = Group.ToString();
        public int ZIndex { get; } = IsSelected ? 100 : Group.ToZIndex();
    }

    internal static class PipeEventExtensions
    {
        public static PipeEvent ToPipeEvent(
            this VPMEventVM pipeEvent,
            Size canvasSize,
            ClockTime clock,
            PipeRange range,
            double pixelsPerClock,
            double pixelsPerUnit)
        {
            //If this event does not fall within the range then hide 
            //rectangles and return;
            if (!pipeEvent.Range.IntersectsWith(range))
            {
                return new PipeEvent(
                    pipeEvent.Id,
                    pipeEvent.Group,
                    new PipeRect(0, 0, 0, 0, false),
                    new PipeRect(0, 0, 0, 0, false),
                    pipeEvent.IsSelected);
            }

            // TODO:  create resource? to store the min defect pixel width/height;
            var minPixelSize = 15.0;

            var fullPipe = pipeEvent.Group.IsFullPipe();
            double defectHeight;
            double defectWidth;
            bool defTopVis = true;
            bool defBotVis = false;

            if (fullPipe)
            {
                defectHeight = canvasSize.Height;
                defectWidth = pipeEvent.Range.Width * pixelsPerUnit;

                if (defectWidth < minPixelSize)
                    defectWidth = minPixelSize;
            }
            else
            {
                //Calculate the height of this defect
                defectHeight = pipeEvent.Length / PipeClocks.DEGREES_PER_CLOCK * pixelsPerClock;
                if (defectHeight < minPixelSize)
                    defectHeight = minPixelSize;

                //Calculate the width of this defect
                defectWidth = pipeEvent.Range.Width * pixelsPerUnit;
                if (defectWidth < minPixelSize)
                    defectWidth = minPixelSize;
            }

            //Calculate X1 position of this defect
            var cx1 = range.X1;
            var x1 = pipeEvent.Range.X1;
            var xAbs = Math.Abs(cx1 - x1);
            if (x1 < cx1)
            {
                x1 = -(xAbs * pixelsPerUnit);
            }
            else
            {
                x1 = xAbs * pixelsPerUnit;
            }

            if (fullPipe)
            {
                x1 -= defectWidth / 2.0;
            }

            var peClock = pipeEvent.Clock;
            var yTop = 0.0;
            var yBot = 0.0;

            if (!fullPipe)
            {
                var hrCW = clock.DistanceFrom(peClock, ClockDirection.Clockwise);
                var hrACW = clock.DistanceFrom(peClock, ClockDirection.AntiClockwise);

                var cenCanvas = canvasSize.Height / 2.0;

                yTop = cenCanvas + hrCW * pixelsPerClock;
                yBot = cenCanvas - hrACW * pixelsPerClock;

                defBotVis = yBot != yTop;
            }
            else
            {
                //Place all defects always at the top of the canvas if no valid clock
                //yTop = 0;
                //yBot = 0;

                //no need to show bottom defect as the clocks would be the same
                //defBotVis = false;
            }

            var rcCanvas = new RectangleF(0, 0, canvasSize.Width, canvasSize.Height);

            if (defTopVis)
            {
                var rcTop = new RectangleF((float)x1, (float)yTop, (float)(x1 + defectWidth), (float)(yTop + defectHeight));
                defTopVis = rcCanvas.IntersectsWith(rcTop);
            }

            if (defBotVis)
            {
                var rcBot = new RectangleF((float)x1, (float)yBot, (float)(x1 + defectWidth), (float)(yBot + defectHeight));
                defBotVis = rcCanvas.IntersectsWith(rcBot);
            }

            var top = new PipeRect(x1, yTop, x1 + defectWidth, yTop + defectHeight, defTopVis);
            var bottom = new PipeRect(x1, yBot, x1 + defectWidth, yBot + defectHeight, defBotVis);
            var style = new PipeEvent(
                pipeEvent.Id,
                pipeEvent.Group,
                bottom,
                top,
                pipeEvent.IsSelected);

            return style;
        }
    }
}
