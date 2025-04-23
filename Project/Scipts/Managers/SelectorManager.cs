using Godot;
using System;

// Author : Mathys Moles

namespace Com.IsartDigital.Manager
{

    public partial class SelectorManager : Manager
    {
        #region Singleton
        static private SelectorManager instance;

        private SelectorManager() { }

        static public SelectorManager GetInstance()
        {
            if (instance == null) instance = new SelectorManager();
            return instance;

        }
        #endregion

        [Export] private float durationToStayMaintained = 50;
        [Export] public CanvasItem[] selectesZone;

        private CanvasItem currentSelected;
        private int _currentIndexSelected;

        private Action processAction;
        private int count;
        private bool onSelect;
        private int CurrentIndexSelected
        {
            get { return _currentIndexSelected; }
            set
            {
                int lIndex = Mathf.Wrap(value, 0, selectesZone.Length);
                currentSelected.Modulate = Colors.White;
                _currentIndexSelected = lIndex;
                currentSelected = selectesZone[lIndex];
                currentSelected.Modulate = Colors.Black;
            }
        }
        public override void _EnterTree()
        {
            base._EnterTree();
            #region Singleton Ready
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(SelectorManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
            GD.Print("Select");
        }
        public override void Init()
        {
            base.Init();
            
            currentSelected = selectesZone[Prop.ZERO];
            InputManager.GetInstance().Connect(nameof(InputManager.EnterClick), Callable.From(Enter));
            InputManager.GetInstance().Connect(nameof(InputManager.ExitClick), Callable.From(Exit));
        }
        private void Enter()
        {
            InitCoolDownToSelect();
        }
        private void Exit()
        {
            if (processAction.Contain(nameof(CoolDownToSelect), out Action lMethod))
            {
                processAction -= lMethod;
                
                onSelect = false;
            }
            if(!onSelect) CurrentIndexSelected++;
            else currentSelected.Modulate = Colors.Black;
            GD.Print("Exite");
           
        }
        private void InitCoolDownToSelect()
        {
            if (processAction.Contain(nameof(CoolDownToSelect), out Action lMethod)) processAction -= lMethod;
            count = 0;
            
            processAction += CoolDownToSelect;
        }
        private void CoolDownToSelect()
        {
            count++;
            if (count >= durationToStayMaintained)
            {
                onSelect = true;
                currentSelected.Modulate = Colors.Bisque;
                processAction -= CoolDownToSelect;
            }
        }
        public override void _Process(double delta)
        {
            base._Process(delta);
            processAction?.Invoke();
        }
        protected override void Dispose(bool pDisposing)
        {
            instance = null;
            base.Dispose(pDisposing);
        }
    }
}
