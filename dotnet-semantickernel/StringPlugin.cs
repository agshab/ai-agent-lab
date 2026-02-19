using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelPlugin
{
    public class StringPlugin
    {
        [KernelFunction, Description("Reverses the given string and returns the result.")]
        public string ReverseString([Description("The string to reverse.")] string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "Error: Input string is null or empty.";
            }

          
           return new string(input.Reverse().ToArray());

        }
    }
}