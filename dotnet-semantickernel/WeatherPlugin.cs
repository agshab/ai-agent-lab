using System;
using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelPlugin
{
    public class WeatherPlugin
    {
        [KernelFunction, Description("Returns a mock weather report for the specified date.")]
        public string GetWeatherForDate([Description("The date to get the weather for.")] string date)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string inputDate = date.Split(' ')[0];

            if (inputDate == today)
            {
                return "Sunny, 80°F";
            }
            else
            {
                return "Rainy, 45°F";
            }
        }
    }
}
