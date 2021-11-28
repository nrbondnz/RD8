using System;

namespace GameObjectCollision
{
    public static class GameObjectCollisionActionFactory
    {
        public static void setGameObjectCollisionAction(GameObjectCollisionActionController controller,
            IGameObjectCollisionAction.GameObjectCollisionActionEnum gameObjectCollisionActionEnum)
        {
            if ( gameObjectCollisionActionEnum == IGameObjectCollisionAction.GameObjectCollisionActionEnum.Bounce)
            {
                controller.gameObject.AddComponent<BounceGameObjectCollisionAction>();
            }
            else
            {
                throw new NotImplementedException("Non implemented IKeyAction used in factory");
            }
        }
    }
}