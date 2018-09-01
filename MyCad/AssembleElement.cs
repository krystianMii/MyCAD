using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;

namespace MyCad
{
    public static class AssembleElement
    {
        public static void Run(double[] widthHeight)
        {
            Layer.Add("plywoodMyCad", Color.FromRgb(105, 51, 0), true);

            Plywood.Draw(widthHeight[0],widthHeight[1], 21);

            Layer.Add("girdersMyCad", Color.FromRgb(204, 204, 0), true);

            PanelWalers acPanWalers = new PanelWalers(widthHeight);

            PanelGirders acPanGirders = new PanelGirders(widthHeight[1], acPanWalers.Length);

            double offsetFromZero = (widthHeight[0] - acPanWalers.Length)*0.5;
            string girderTypeName = acPanGirders.Type;

            foreach (double position in acPanGirders.Positions)
            {
                Point3d insertionPoint = new Point3d(position + offsetFromZero, 0, 0);
                Girder.insert(girderTypeName, insertionPoint);
            }

            Layer.Add("walersMyCad", Color.FromRgb(192, 192, 192), true);

            string walerTypeName = acPanWalers.Type;

            foreach (double position in acPanWalers.Positions)
            {
                Point3d insertionPoint = new Point3d(offsetFromZero, -200, position);
                Waler.Insert(walerTypeName, insertionPoint);
            }

        }
    }
}
