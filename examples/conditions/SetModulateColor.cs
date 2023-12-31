using Beehave.Nodes.Leaves;
using Godot;

namespace Beehave.Examples {
    public partial class SetModulateColor : ActionLeaf {

        [Export]
        public float Speed = 3;
        [Export]
        public Color TargetColor = Colors.White;

        private Color currentColor;
        private Tween tween;
        private Color origColor;
        private bool isRunning = false;


        public override int Tick(Node actor, Blackboard board) {
            Sprite2D sp = actor as Sprite2D;

            if (!this.isRunning && this.currentColor != TargetColor)
                this.origColor = sp.Modulate;

            if (this.currentColor != TargetColor && sp.Modulate != TargetColor) {
                if (this.tween != null)
                    this.tween.Stop();
                this.currentColor = TargetColor;
                this.tween = CreateTween()
                    .SetEase(Tween.EaseType.InOut)
                    .SetTrans(Tween.TransitionType.Cubic);
                this.tween.TweenProperty(actor, "modulate", currentColor, Speed);
                this.tween.Finished += onComplete;
                this.isRunning = true;
            }

            if (this.isRunning)
                return (int)BeehaveStatus.RUNNING;
            else
                return (int)BeehaveStatus.SUCCESS;
        } // Tick


        private void onComplete() {
            this.isRunning = false;
            this.tween = null;
        }

    } // class
} // namespace