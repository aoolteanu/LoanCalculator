using CommandLine;
using System.IO;

namespace LoanCalculator
{
    public abstract class ProcessorTemplateBase<TOptions> where TOptions : new()
    {
        protected TextWriter Error;
        protected TextWriter Output;
        protected TOptions Options;

        public void Process(string[] args,
                            TextWriter output,
                            TextWriter error)
        {
            Error = error;
            Output = output;

            ParseOptions(args);

            var isValidArguments = ValidateArguments();

            if (isValidArguments)
            {
                ProcessFile();
            }
        }

        private void ParseOptions(string[] args)
        {
            var result = Parser.Default.ParseArguments<TOptions>(args)
                .WithParsed(options => Options = options)
                .WithNotParsed(error => Error.WriteLine(error));
        }

        protected abstract void ProcessFile();


        protected abstract bool ValidateArguments();

        //protected abstract void ProcessLine(string line);

    }
}