using System;

namespace RoyaleAPI.Objects.Analytics
{
    public class AnalyticsFrame
    {
        public DateTime Time { get; internal set; }

        public long AllowedTraffic { get; internal set; }
        public long DroppedTraffic { get; internal set; }
    }
}