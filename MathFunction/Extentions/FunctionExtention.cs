using org.mariuszgromada.math.mxparser;

namespace MathFunction
{
    public static class FunctionExtention
    {
        public static Argument[] GetAllArguments(this Function function)
        {
            Argument[] arguments = new Argument[function.getArgumentsNumber()];
            for (int i = 0; i < function.getArgumentsNumber(); i++)
            {
                arguments[i] = function.getArgument(i);
            }
            return arguments;
        }
    }
}
