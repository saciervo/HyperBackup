namespace HyperBackup.Core.Interfaces
{
    public interface IBackupStorage
    {
        string CreateProgressDirectory();
        void DeleteProgressDirectory();
        void FinalizeExport(string name);
    }
}