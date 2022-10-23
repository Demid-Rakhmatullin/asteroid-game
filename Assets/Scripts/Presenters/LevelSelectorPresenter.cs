using Data;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Presenters
{
    public class LevelSelectorPresenter : BasePresenter
    {
        [SerializeField] LevelSelectorPresenter previousSelector;
        [SerializeField] LevelData levelData;

        public LevelDataState State => levelData.State;

        void Awake()
        {
            
        }
    }
}
