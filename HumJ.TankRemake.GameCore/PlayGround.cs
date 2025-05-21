using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.MapStage.Map;
using HumJ.TankRemake.GameCore.MapStage.Tile;
using HumJ.TankRemake.GameCore.Tank;
using HumJ.TankRemake.GameCore.Weapon;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Drawing;

namespace HumJ.TankRemake.GameCore
{
    public class Playground(Guid id, ILogger<Playground> logger, ILogger<PlayerTank> ptLogger) : IWillPlaySound
    {
        public Guid Id { get; } = id;

        public int StageIndex { get; private set; } = 0;
        public TimeSpan StageInterval { get; private set; } = TimeSpan.Zero;

        public Difficulty StageDifficulty { get; private set; } = Difficulty.Tourist;
        public string StageName { get; private set; } = "";
        public Color BackgroundColor { get; private set; } = Color.Black;
        public string BackgroundImage { get; private set; } = "";

        public int Tick { get; private set; } = 0;

        public ConcurrentBag<TileBase> BackgroundTile { get; } = [];
        public ConcurrentBag<TileBase> ForegroundTile { get; } = [];

        public ConcurrentBag<TankBase> EnemyTank { get; } = [];
        public TankBase? PlayerTank { get; private set; }

        public ConcurrentBag<BulletBase> Bullet { get; } = [];

        public event EventHandler? OnTick;
        public event EventHandler<string>? OnPlaySound;

        private Timer? timer;

        public void Start(int stageIndex)
        {
            logger.LogDebug($"{nameof(Start)} {stageIndex}");

            timer?.Dispose();

            while (stageIndex < 0)
            {
                stageIndex += Stage.AllStages.Length;
            }

            StageIndex = stageIndex % Stage.AllStages.Length;

            LoadStage();
            StartGameTick();
        }

        public void Stop()
        {
            logger.LogDebug($"{nameof(Stop)}");

            timer?.Dispose();
        }

        public void Pause(bool enabled)
        {
            logger.LogDebug($"{nameof(Pause)} {enabled}");

            if (enabled)
            {
                timer?.Change(0, 0);
                OnPlaySound?.Invoke(this, "Select");
            }
            else
            {
                timer?.Change(StageInterval, StageInterval);
                OnPlaySound?.Invoke(this, "SignIn");
            }
        }

        private void LoadStage()
        {
            logger.LogDebug($"{nameof(LoadStage)}");

            (StageDifficulty, var stage) = Stage.AllStages[StageIndex];

            StageName = stage.Caption;
            BackgroundColor = stage.BackgroundColor;
            BackgroundImage = stage.BackgroundImage;

            BackgroundTile.Clear();
            foreach (var tile in stage.BackgroundTile)
            {
                BackgroundTile.Add(tile);
            }

            ForegroundTile.Clear();
            foreach (var tile in stage.ForeGroundTile)
            {
                var inPlayer1sPlace = tile.GridPosition.X >= 16 && tile.GridPosition.X <= 17 && tile.GridPosition.Y >= 27 && tile.GridPosition.Y <= 29;
                var inPlayer2sPlace = tile.GridPosition.X >= 22 && tile.GridPosition.X <= 23 && tile.GridPosition.Y >= 27 && tile.GridPosition.Y <= 29;
                var flagPlage = tile.GridPosition.X >= 19 && tile.GridPosition.X <= 20 && tile.GridPosition.Y >= 28 && tile.GridPosition.Y <= 29;

                if (!(inPlayer1sPlace || inPlayer2sPlace || flagPlage))
                {
                    ForegroundTile.Add(tile);
                }
            }

            EnemyTank.Clear();
            Bullet.Clear();

            if (PlayerTank is not null)
            {
                PlayerTank.OnPlaySound -= PlaySound;
            }
            PlayerTank = new PlayerTank(ptLogger);
            PlayerTank.OnPlaySound += PlaySound;
        }

        private void PlaySound(object? sender, string e)
        {
            OnPlaySound?.Invoke(sender, e);
        }

        private void StartGameTick(int tps = 40)
        {
            logger.LogDebug($"{nameof(StartGameTick)} {tps}");

            StageInterval = TimeSpan.FromMilliseconds(1000.0 / tps);
            timer = new Timer(state =>
            {
                GoTick();
            }, null, 0, 0);
        }

        private void GoTick()
        {
            logger.LogTrace($"{nameof(GoTick)} {Tick}");

            var bulletToRemove = new List<BulletBase>();
            void RemoveBullet()
            {
                if (bulletToRemove.Count > 0)
                {
                    var lived = Bullet.Except(bulletToRemove).ToArray();
                    Bullet.Clear();
                    foreach (var b in lived)
                    {
                        Bullet.Add(b);
                    }
                    bulletToRemove.Clear();
                }
            }

            var foregroundTileToRemove = new List<TileBase>();
            void RemoveForegroundTile()
            {
                if (foregroundTileToRemove.Count > 0)
                {
                    var lived = ForegroundTile.Except(foregroundTileToRemove).ToArray();
                    ForegroundTile.Clear();
                    foreach (var t in lived)
                    {
                        ForegroundTile.Add(t);
                    }
                    foregroundTileToRemove.Clear();
                }
            }

            try
            {
                Tick++;

                // 玩家
                PlayerTank?.GoTick(this);

                // 敌人

                // 子弹
                {
                    foreach (var bullet in Bullet)
                    {
                        bullet.GoTick(this);
                        if (bullet.TimeToLive < 0)
                        {
                            bulletToRemove.Add(bullet);
                        }
                    }
                    RemoveBullet();

                    foreach (var bullet in Bullet)
                    {
                        var hits = ForegroundTile.Where(tile => tile.HitWith(bullet)).ToArray();
                        var changed = bullet.HitWith(hits);
                        if (bullet.TimeToLive < 1)
                        {
                            bulletToRemove.Add(bullet);
                        }

                        foreach (var kv in changed)
                        {
                            foregroundTileToRemove.Add(kv.Key);
                            if (kv.Value != null)
                            {
                                ForegroundTile.Add(kv.Value);
                            }
                        }
                    }
                    RemoveBullet();
                    RemoveForegroundTile();
                }

                OnTick?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}