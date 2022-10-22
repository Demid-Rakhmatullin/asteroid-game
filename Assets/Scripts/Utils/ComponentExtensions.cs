using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class ComponentExtensions
    {
        public static bool CompareTagEnum(this Component component, params object[] enumValue)
            => (bool)enumValue?.Any(ev => component.CompareTag(ev?.ToString()));
        
    }
}
