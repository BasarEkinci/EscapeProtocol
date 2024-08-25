using System.Threading;
using Cysharp.Threading.Tasks;

namespace Controllers
{
    public interface IGunController
    {
        float FireRate { get; set; }
        void Fire();
        UniTaskVoid FireRepeatedly(CancellationToken token);
    }
}