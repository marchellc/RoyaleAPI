using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Analytics
{
    public class AnalyticsData
    {
        private AnalyticsFrame[] _frames;

        [JsonPropertyName("interval")]
        public int intervalMinutes { get; set; }

        [JsonPropertyName("traffic")]
        public long[][] unparsedFrames { get; set; }

        [JsonIgnore]
        public TimeSpan Interval
        {
            get => TimeSpan.FromMinutes(intervalMinutes);
            set => intervalMinutes = (int)Math.Floor((double)value.TotalMinutes);
        }

        [JsonIgnore]
        public AnalyticsFrame[] Frames
        {
            get
            {
                if (_frames is null)
                {
                    var frames = new List<AnalyticsFrame>();

                    for (int i = 0; i < unparsedFrames.Length; i++)
                    {
                        var stupidFrame = unparsedFrames[i];

                        if (stupidFrame is null || stupidFrame.Length != 3)
                            continue;

                        var frame = new AnalyticsFrame();

                        frame.Time = new DateTime(stupidFrame[0]);

                        frame.AllowedTraffic = stupidFrame[1];
                        frame.DroppedTraffic = stupidFrame[2];
                    }

                    _frames = frames.ToArray();
                }

                return _frames;
            }
        }
    }
}