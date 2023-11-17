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

        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed);
    }

}