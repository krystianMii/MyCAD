using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Colors;
using System.IO;
using System.Reflection;

namespace MyCad
{
    public class MyCad
    {
        [CommandMethod("createPanel")]
        public static void createPanel()
        {
            double[] widthHeight;
            bool hasInput = false;

            widthHeight = ConsoleEditor.GetSize(out hasInput);

            /*PanelOnRibbon myCadPanel = new PanelOnRibbon();
            myCadPanel.Start();*/

            try
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase; //get current dll path - start
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                path = Path.GetDirectoryName(path);                         //get current dll path - end

                Blocks.load(path + "\\MyCadBlocks_girders.dwg");
                Blocks.load(path + "\\MyCadBlocks_walers.dwg");
            }
            catch (System.Exception)
            {
                hasInput = false;
            }

            if (hasInput)
            {
                AssembleElement.Run(widthHeight);

                Point3d pMin = new Point3d(-500, -361, -500);
                Point3d pMax = new Point3d(widthHeight[0] + 500, 21, widthHeight[1]+500);
                Zoom.Use(pMin, pMax, new Point3d(), 1);
            }
        }
    }
}
