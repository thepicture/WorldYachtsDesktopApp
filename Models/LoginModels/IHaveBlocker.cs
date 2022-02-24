using System;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp
{
    public interface IHaveBlocker<TTarget>
    {
        TimeSpan BlockTime { get; }
        TimeSpan BlockIncrementTime { get; }
        ITimeoutBlocker<TTarget> Blocker { get; }
    }
}