namespace EternalBlueWebApplication.Contracts
{
    public interface IEternalBlueService
    {
        string FirstPassword { get; }
        string FirstPasswordASCIIForm { get; }
        string SecondPassword { get; }
    }
}