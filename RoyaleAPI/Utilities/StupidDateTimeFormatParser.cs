using System;

namespace RoyaleAPI.Utilities
{
    public static class StupidDateTimeFormatParser
    {
        public static DateTime ParseDateTime(string dateTime)
        {
            var split = dateTime.Split(' ');

            var date = split[0];
            var dateSplit = date.Split('-');

            var time = split[1];
            var timeSplit = time.Split(':');

            return new DateTime(
                int.Parse(dateSplit[0]),
                int.Parse(dateSplit[1]),
                int.Parse(dateSplit[2]),

                int.Parse(timeSplit[0]),
                int.Parse(timeSplit[1]),
                int.Parse(timeSplit[2]));
        }
    }
}