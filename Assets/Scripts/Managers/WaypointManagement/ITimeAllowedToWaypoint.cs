namespace Key
{
    /// <summary>
    /// Optional interface causing the time allowed to the next waypoint to be measured
    /// </summary>
    public interface ITimeAllowedToWaypoint
    {
        /// <summary>
        /// Get the time allowed to waypoint
        /// </summary>
        /// <returns>float</returns>
        public float TimeAllowedToWaypoint();
    }
}