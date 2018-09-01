using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Colors;

namespace MyCad
{
    static class Layer
    {
        public static void Add(string layerName, Color layerColor, bool setActive)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock acDocLock = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    if (!acLyrTbl.Has(layerName))
                    {
                        LayerTableRecord acLyrTblRec = new LayerTableRecord();
                        acLyrTblRec.Name = layerName;
                        acLyrTblRec.Color = layerColor;
                        acLyrTbl.Add(acLyrTblRec);
                        acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                    }

                    if (setActive == true) // set active
                    {
                        acCurDb.Clayer = acLyrTbl[layerName];
                    }
                    acTrans.Commit();
                }
            }
        }
    }
}