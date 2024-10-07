using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FakeRepo.Core;
using FakeRepo.Test.GuidDtoTests.Domain;

namespace FakeRepo.Test.GuidDtoTests.AutomationLayer;

public interface IUserGuidDtoRepository
{
    Task<UserGuidDto> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<UserGuidDto>> GetAllByNameAsync(string name, CancellationToken ct);
    Task InsertAsync(UserGuidDto entity, CancellationToken ct);
    Task UpdateAsync(UserGuidDto entity, CancellationToken ct);
    Task UpsertAsync(UserGuidDto entity, CancellationToken ct);
}

public class UserGuidDtoRepositoryFake : GuidRepositoryFake<UserGuidDto>, IUserGuidDtoRepository
{
    public UserGuidDtoRepositoryFake(ISerializer serializer)
        : base(serializer, cfg => cfg.GetIdFunc = entity => entity.Id)
    {
    }

    public Task<IReadOnlyList<UserGuidDto>> GetAllByNameAsync(string name, CancellationToken ct)
        => GetAllByAsync(e => e.Name == name, ct);

    public UserGuidDto Add(Action<UserGuidDto> customizeAction = null)
        => base.Add(_ => DomainBuilder.Create(customizeAction));
}