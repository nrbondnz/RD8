using System;

namespace DefaultNamespace
{
    public sealed class Singleton    
    {    
        private Singleton()    
        {    
        }    
        private static readonly Lazy<Singleton> lazy = new Lazy<Singleton>(() => new Singleton());    
        public static Singleton Instance    
        {    
            get    
            {    
                return lazy.Value;    
            }    
        }    
    }
}