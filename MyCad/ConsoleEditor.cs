using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace MyCad
{
    public static class ConsoleEditor
    {
        public static double[] GetSize(out bool hasInput)
        {
            hasInput = true;
            double[] widthHeight = new double[] { 0.00, 0.00 }; //double[] width_Height_ConcrPress = new double[] { 0.00, 0.00, 0.00 };


            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");
            Point3d inputValues = new Point3d(0.00, 0.00, 0.00);



            pPtOpts.Message = "\nEnter panel width[mm], height[mm] : "; //pPtOpts.Message = "\nEnter width[mm], height[mm], permissible fresh concrete pressure[kN/m2] : ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            inputValues = pPtRes.Value;

            widthHeight[0] = inputValues.X;
            widthHeight[1] = inputValues.Y;
            //width_Height_ConcrPress[2] = inputValues.Z;


            if (pPtRes.Status == PromptStatus.Cancel)
            {
                hasInput = false;
            }

            return widthHeight;
        }
    }
}
