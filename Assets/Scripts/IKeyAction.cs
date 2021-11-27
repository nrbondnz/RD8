
    public interface IKeyAction
    {
        public enum KeyAction {Open, Reveal}
    
        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            KeyActionController actorObject,
            float actionSpeed);
    }

