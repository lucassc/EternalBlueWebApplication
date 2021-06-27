namespace EternalBlueWebApplication.Contracts
{
    public interface IPasswordService
    {
        bool IsFirstPassword(string pass);
        bool IsSecondPassword(string pass);
        string GetFirstPasswordAscii();
    }
}