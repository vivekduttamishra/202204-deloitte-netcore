using System;

namespace WebApp01.Services
{
    public  class TimeUtil
    {
        public string TimedGreet
        {
            get
            {
                var time = DateTime.Now;
                if (time.Hour < 12)
                    return "Good Morning";
                else if (time.Hour < 18)
                    return "Good Afternoon";
                else
                    return "Good Evening";
            }
        }
    }
}