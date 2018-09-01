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

namespace MyCad
{
    public static class Blocks
    {
        public static void load(string dwgFilePath)
        {
            DocumentCollection dm = Application.DocumentManager;
            Editor ed = dm.MdiActiveDocument.Editor;

            Database destDb = dm.MdiActiveDocument.Database;
            Database sourceDb = new Database(false, true);
            try
            {
                sourceDb.ReadDwgFile(dwgFilePath, System.IO.FileShare.Read, true, "");

                ObjectIdCollection blockIds = new ObjectIdCollection();

                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = sourceDb.TransactionManager;

                using (Transaction myT = tm.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tm.GetObject(sourceDb.BlockTableId, OpenMode.ForRead, false);

                    foreach (ObjectId btrId in bt)
                    {
                        BlockTableRecord btr = (BlockTableRecord)tm.GetObject(btrId, OpenMode.ForRead, false);

                        if (!btr.IsAnonymous && !btr.IsLayout)
                        {
                            blockIds.Add(btrId);
                        }
                        btr.Dispose();
                    }
                }
                IdMapping mapping = new IdMapping();
                sourceDb.WblockCloneObjects(blockIds, destDb.BlockTableId, mapping, DuplicateRecordCloning.Replace, false);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage("\nError during loading blocks dwg file: " + ex.Message);
            }
        }
    }
}
