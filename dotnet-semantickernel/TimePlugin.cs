using System;
using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelPlugin
{
    public class TimePlugin
    {
        [KernelFunction, Description("Returns the current date and time formatted as 'yyyy-MM-dd HH:mm:ss'.")]
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
