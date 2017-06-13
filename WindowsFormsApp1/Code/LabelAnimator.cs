using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class LabelAnimator
    {
        private Timer tmrMoveLabels = new Timer();

        // The Label controls we will animate and their properties.
        private List<Label> AnimateLabels = new List<Label>();
        private List<int> AnimateStartXs = new List<int>();
        private List<int> AnimateStartYs = new List<int>();
        private List<float> AnimateDxs = new List<float>();
        private List<float> AnimateDys = new List<float>();
        private List<float> AnimateXs = new List<float>();
        private List<float> AnimateYs = new List<float>();
        private List<int> AnimateTotalTicks = new List<int>();
        private List<int> AnimateTicksToGo = new List<int>();

        public LabelAnimator()
        {
            tmrMoveLabels.Interval = 100;

            tmrMoveLabels.Tick += tmrMoveLabels_Tick;
        }

        // Store information to move a label.
        public void StoreAnimationInfo(
            Label lbl, float dx, float dy, float milliseconds)
        {
            // Calculate the number of times the Timer will tick.
            int ticks = (int)(milliseconds / tmrMoveLabels.Interval);

            // Add the values.
            AnimateLabels.Add(lbl);
            AnimateStartXs.Add((int)(lbl.Location.X - dx));
            AnimateStartYs.Add((int)(lbl.Location.Y - dy));
            AnimateDxs.Add(dx / ticks);
            AnimateDys.Add(dy / ticks);
            AnimateTotalTicks.Add(ticks);
        }

        // Move the labels to the start positions and start animating them.
        public void btnAnimate_Click()
        {
            AnimateTicksToGo = new List<int>();
            AnimateXs = new List<float>();
            AnimateYs = new List<float>();

            for (int i = 0; i < AnimateLabels.Count; i++)
            {
                AnimateXs.Add(AnimateStartXs[i]);
                AnimateYs.Add(AnimateStartYs[i]);
                AnimateLabels[i].Location =
                    new System.Drawing.Point((int)AnimateXs[i], (int)AnimateYs[i]);
                AnimateLabels[i].Visible = true;
                AnimateTicksToGo.Add(AnimateTotalTicks[i]);
            }
            tmrMoveLabels.Enabled = true;
        }

        // Move the labels.
        public void tmrMoveLabels_Tick(object sender, EventArgs e)
        {
            bool done_moving = true;
            DateTime now = DateTime.Now;
            for (int i = 0; i < AnimateLabels.Count; i++)
            {
                if (AnimateTicksToGo[i]-- > 0)
                {
                    done_moving = false;
                    AnimateXs[i] += AnimateDxs[i];
                    AnimateYs[i] += AnimateDys[i];
                    AnimateLabels[i].Location =
                        new System.Drawing.Point((int)AnimateXs[i], (int)AnimateYs[i]);
                }
            }

            // If all labels are done moving, disable the Timer.
            if (done_moving)
            {
                tmrMoveLabels.Enabled = false;
            }
        }
    }
}
