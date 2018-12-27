using System.Collections.Generic;

namespace LearningPlus.Web
{
    public static class GlobalConstants
    {
        public static List<string> DaysOfWeek = new List<string>
            {
                "Понеделник", "Вторник", "Сряда", "Четвъртък", "Петък", "Събота", "Неделя"
            };

        public static List<string> StartHours = new List<string>()
            {
                "08:00",  "09:30", "11:00", "13:30", "15:00", "16:30", "18:00"
            };

        public static List<string> FullHours = new List<string>()
            {
                "08:00 - 09:30",  "09:30 - 11:00", "11:00 - 12:30", "13:30 - 15:00", "15:00 - 16:30", "16:30 - 18:00", "18:00 - 19:30"
            };

        public static string BlobStorageUrl = @"https://lpwebstorage.blob.core.windows.net/lpblobs/";

        public static int ChatMessagesToShow = 5;
    }
}
