namespace Collision
{
   
    public interface ICollisionAction
    {
        public enum CollisionActionEnum
        {
            Bounce,
            SpeedChange
        }

        public enum CollisionEffectStrengthEnum
        {
            Full = 15,
            Strong = 10,
            MedStrong = 7,
            Normal = 5,
            Half = 3,
            Low = 2,
            Lowest = 1
        }

        public void DoCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum);
    }
}