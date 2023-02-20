namespace Key
{


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
            KeyActionController actorObject,
            float actionSpeed);
    }

}