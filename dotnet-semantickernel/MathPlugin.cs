using System.ComponentModel;
using System.Data;
using Microsoft.SemanticKernel;

namespace SemanticKernelPlugin
{
    public class MathPlugin
    {
        [KernelFunction, Description("Evaluates a mathematical expression and returns the result.")]
        public string Calculate([Description("The mathematical expression to evaluate.")] string expression)
        {
            try
            {
                // Use DataTable to evaluate the expression
                var dataTable = new DataTable();
                var result = dataTable.Compute(expression, null);

                return result?.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}