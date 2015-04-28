using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace HyperBackup.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<HyperBackupService>(s =>
                {
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                }); ;

                c.SetServiceName("HyperBackup");
                c.SetDisplayName("HyperBackup Service");
                c.SetDescription("HyperBackup as a Service");
            });
        }
    }
}
