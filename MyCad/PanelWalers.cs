using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad
{
    class PanelWalers
    {
        public PanelWalers(double[] plywoodDimensions)
        {
            WalerValues(plywoodDimensions);
        }

        private string walerName = "";
        private double walerLength = 0.00;
        private double[] walersPositions;

        public string Type
        {
            get { return walerName; }
        }

        public double Length
        {
            get { return walerLength; }
        }

        public double[] Positions
        {
            get { return walersPositions; }
        }

        internal void WalerValues(double[] plywoodDimensions)
        {
            double module = 300;
            double girderHeight = 0;
            double girderModule = 300;

            this.walerLength = ((int)(plywoodDimensions[0] / module)) * module;
            
            this.walerLength -= 50;
            this.walerName = String.Concat("waler_", walerLength.ToString());

            if (plywoodDimensions[1] % girderModule < 0.1)
            {
                girderHeight = ((int)(plywoodDimensions[1] / girderModule)) * module;
            }
            else
            {
                girderHeight = ((int)(plywoodDimensions[1] / girderModule) + 1) * module;
            }

            double[] walers;
            if (girderHeight <= 4500)
            {
                walers = new double[] { 500, girderHeight * 0.5, girderHeight - 500 };
            }
            else
            {
                walers = new double[] { 500, 500 + (girderHeight - 1000) / 3, 500 + 2 * ((girderHeight - 1000) / 3), girderHeight - 500 };
            }
            this.walersPositions = walers;
        }
    }
}
