namespace RecordParse.CLI.Interfaces
{
    public interface IArgumentValidator
    {
        void Validate(string[] args);
        bool IsHelpCommand(string[] args);
    }
}