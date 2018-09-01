using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad
{
    class PanelGirders
    {
        public PanelGirders(double plywoodHeight, double walerLength)
        {
            girderValues(plywoodHeight, walerLength);
        }

        private string girderName = "";
        private int girdersNumb = 0;
        private double[] girdersPositions;

        public string Type
        {
            get { return girderName; }
        }

        public int Number
        {
            get { return girdersNumb; }
        }

        public double[] Positions
        {
            get { return girdersPositions; }
        }

        internal void girderValues(double plywoodHeight, double walerLength)
        {
            int i = 0;
            double girderSpacing = 300;
            double firstGirderPos = 50;
            double startGirderSpacing = 275;
            double middleGirderSpacing = 250;

            double girderHeight = 0;
            double module = 300;
            if (plywoodHeight % module < 0.1)
            {
                girderHeight = ((int)(plywoodHeight / module)) * module;
            }
            else
            {
                girderHeight = ((int)(plywoodHeight / module) + 1) * module;
            }
            this.girderName = String.Concat("girder_", girderHeight.ToString());

            walerLength = walerLength + 50;
            this.girdersNumb = (int)(walerLength / girderSpacing) + 1;     

            double[] girdersPos = new double[girdersNumb];
            
            if (girdersNumb == 6)
            {
                girdersPos[0] = firstGirderPos;
                girdersPos[1] = girdersPos[0] + startGirderSpacing;
                girdersPos[2] = girdersPos[1] + startGirderSpacing;
                girdersPos[3] = girdersPos[2] + middleGirderSpacing;
                girdersPos[4] = girdersPos[3] + startGirderSpacing;
                girdersPos[5] = girdersPos[4] + startGirderSpacing;
            }
            else if (girdersNumb == 7)
            {
                girdersPos[0] = firstGirderPos;
                for(i = 1; i < girdersNumb; i++)
                {
                    girdersPos[i] = girdersPos[i-1] + startGirderSpacing;
                }
            }
            else
            {
                girdersPos[0] = firstGirderPos;
                girdersPos[1] = girdersPos[0] + startGirderSpacing;
                girdersPos[2] = girdersPos[1] + startGirderSpacing;
                girdersPos[3] = girdersPos[2] + startGirderSpacing;
                for (i = 4; i <(girdersNumb - 2); i++)
                {
                    girdersPos[i] = girdersPos[i - 1] + girderSpacing;
                }
                girdersPos[girdersNumb - 3] = girdersPos[girdersNumb - 4] + startGirderSpacing;
                girdersPos[girdersNumb - 2] = girdersPos[girdersNumb - 3] + startGirderSpacing;
                girdersPos[girdersNumb - 1] = girdersPos[girdersNumb - 2] + startGirderSpacing;
            }
            this.girdersPositions = girdersPos;
        }
        /*public double[] girdersPositionsOnPlywood()
        {
            int girdNumb = girdersNumber();
            double[] girdersPos = new double[girdersNumb];
            int i = 0;

            double sideDistance = (panelDimension[0] - (girdersNumb - 1) * girdersSpacing) * 0.5;

            if (sideDistance >= 50 && sideDistance <= 175)
            {
                girdersPos[0] = sideDistance;

                for (i = 1; i < girdersNumb; i++)
                {
                    girdersPos[i] = girdersPos[i - 1] + girdersSpacing;
                }
            }
            else if (sideDistance >= 25)
            {
                girdersPos[0] = sideDistance + 25;
                girdersPos[1] = girdersPos[0] + girdersSpacing - 25;

                for (i = 2; i < girdersNumb - 1; i++)
                {
                    girdersPos[i] = girdersPos[i - 1] + girdersSpacing;
                }
                girdersPos[girdersNumb - 1] = girdersPos[girdersNumb - 2] + girdersSpacing - 25;
            }
            else
            {
                girdersPos[0] = sideDistance + 50;
                girdersPos[1] = girdersPos[0] - 25;
                girdersPos[2] = girdersPos[1] - 25;

                for (i = 3; i < girdersNumb - 2; i++)
                {
                    girdersPos[i] = girdersPos[i - 1] + girdersSpacing;
                }
                girdersPos[girdersNumb - 2] = girdersPos[girdersNumb - 3] + girdersSpacing - 25;
                girdersPos[girdersNumb - 1] = girdersPos[girdersNumb - 2] + girdersSpacing - 25;
            }

            return girdersPos;
        }*/
    }
}
