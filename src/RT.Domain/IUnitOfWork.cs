using System;
using System.Threading;
using System.Threading.Tasks;

namespace RT.Domain
{
    public interface IUnitOfWork : IDisposable
	{
		Task<int> CommitAsync(CancellationToken cancellationToken = default);
	}
}
