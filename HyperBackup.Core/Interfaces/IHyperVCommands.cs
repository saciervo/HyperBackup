using System.Collections.Generic;

namespace HyperBackup.Core.Interfaces
{
    public interface IHyperVCommands
    {
        List<string> GetVirtualMachines();
        void ExportVirtualMachine(string name, string path);
    }
}
