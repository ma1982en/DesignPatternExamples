namespace AutoUpdateChoco.Contracts
{
    public interface IChocoService
    {
        Task<bool> UpgradeAllAsync();
    }
}
