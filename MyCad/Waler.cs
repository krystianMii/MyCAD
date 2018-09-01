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
    public static class Waler
    {
        public static void Insert(string blockName, Point3d insertionPoint)
        {
            Database acCurDb = Application.DocumentManager.MdiActiveDocument.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTb = acCurDb.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                BlockTableRecord blockDef = acBlkTb[blockName].GetObject(OpenMode.ForRead) as BlockTableRecord;
                BlockTableRecord ms = acBlkTb[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;

                using (BlockReference blockRef = new BlockReference(insertionPoint, blockDef.ObjectId))
                {
                    ms.AppendEntity(blockRef);
                    acTrans.AddNewlyCreatedDBObject(blockRef, true);

                    foreach (ObjectId id in blockDef)
                    {
                        DBObject obj = id.GetObject(OpenMode.ForRead);
                        AttributeDefinition attDef = obj as AttributeDefinition;

                        if ((attDef != null) && (!attDef.Constant))
                        {
                            using (AttributeReference attRef = new AttributeReference())
                            {
                                attRef.SetAttributeFromBlock(attDef, blockRef.BlockTransform);
                                /*attRef.TextString = "test";
                                blockRef.AttributeCollection.AppendAttribute(attRef);
                                acTrans.AddNewlyCreatedDBObject(attRef, true);*/
                            }
                        }
                    }
                }
                acTrans.Commit();
            }

        }
    }
}
