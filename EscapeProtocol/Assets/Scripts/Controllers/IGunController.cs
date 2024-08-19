using System.Threading;
using Cysharp.Threading.Tasks;

namespace Controllers
{
    public interface IGunController
    {
        void Fire();
        UniTaskVoid FireRepeatedly(CancellationToken token);
    }
}