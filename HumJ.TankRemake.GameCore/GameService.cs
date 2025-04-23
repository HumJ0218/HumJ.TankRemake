using HumJ.TankRemake.GameCore.Tank;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace HumJ.TankRemake.GameCore
{
    public class GameService(ILogger<GameService> logger, ILogger<Playground> pgLogger, ILogger<PlayerTank> ptLogger)
    {
        private readonly ConcurrentDictionary<Guid, Playground> playgrounds = [];

        public Guid CreatePlayground(Guid? cid = null)
        {
            var id = cid ?? Guid.NewGuid();
            var playground = new Playground(id, pgLogger, ptLogger);
            if (playgrounds.TryAdd(id, playground))
            {
                logger.LogInformation("Playground {id} created", id);
                return id;
            }
            else
            {
                return CreatePlayground();
            }
        }

        public Playground GetPlayground(Guid id)
        {
            if (playgrounds.TryGetValue(id, out var result))
            {
                return result;
            }
            else
            {
                CreatePlayground(id);
                return GetPlayground(id);
            }
        }
    }
}