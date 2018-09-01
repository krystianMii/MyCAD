using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyCad
{
    static class Plywood
    {
        public static void Draw(double width, double height, double thickness = 2.1)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock acDocLock = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    /*BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;*/
                    BlockTableRecord currentSpace = acTrans.GetObject(acCurDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                    SubDMesh plywood = new SubDMesh();
                    plywood.Setbox(width, thickness, height, 1, 1, 1, 0);
                    plywood.TransformBy(Matrix3d.Displacement(new Vector3d(width * 0.5, thickness * 0.5, height * 0.5)));

                    currentSpace.AppendEntity(plywood);
                    acTrans.AddNewlyCreatedDBObject(plywood, true);
                    acTrans.Commit();

                }
            }
        }
    }
}
