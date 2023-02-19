namespace Collision
{
   
   /// <summary>
   /// 
   /// </summary>
        public enum CollisionActionEnum
        {
            Bounce = 0,
            SpeedChange = 1,
            Death = 2,
            Attract = 3
        }

   /// <summary>
   /// 
   /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public interface ICollisionAction
        {   
            /// <summary>
            /// 
            /// </summary>
            /// <param name="controller"></param>
            /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum);
    }
}