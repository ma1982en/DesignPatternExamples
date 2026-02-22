namespace AutoUpdateChoco.Contracts
{
    public interface IAutostartService
    {
        void RegisterAutostart(string appPath);
        void UnregisterAutostart();
    }
}
