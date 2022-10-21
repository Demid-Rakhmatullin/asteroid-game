using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace States
{
    public static class GameState
    {
        public static BoolReactiveProperty GameStarted = new BoolReactiveProperty();
    }
}
