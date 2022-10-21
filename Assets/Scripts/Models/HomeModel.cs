using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Models
{
    public class HomeModel
    {
        public ReactiveProperty<bool> GameStarted { get; private set; }
    }
}
