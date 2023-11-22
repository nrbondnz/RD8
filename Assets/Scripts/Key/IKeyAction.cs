namespace Key
{


    /// <summary>
    /// Interface defining the action that happen on the KeyActionController when the action is triggered
    /// This is usually to make another object do something so the player can move on
    /// </summary>
    public interface IKeyAction
    {
        public enum KeyActionEnum
        {
            Open,
            Reveal,
            WalkThrough
        }

        /// <summary>
        /// Carries out/continues the key induced action on the game object
        /// </summary>
        /// <param name="isActionStarted"></param>
        /// <param name="actionTime"></param>
        /// <param name="actionSpeed"></param>
        /// <returns></returns>
        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed);
    }

}