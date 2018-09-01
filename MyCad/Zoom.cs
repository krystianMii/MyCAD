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
    public static class Zoom
    {
        public static void Use(Point3d pMin, Point3d pMax, Point3d pCenter, double dFactor)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDB = acDoc.Database;

            int nCurVport = System.Convert.ToInt32(Application.GetSystemVariable("CVPORT"));

            if (acCurDB.TileMode == true) //model space
            {
                if (pMin.Equals(new Point3d()) == true && pMax.Equals(new Point3d()) == true)
                {
                    pMin = acCurDB.Extmin; //Get the extends of model space
                    pMax = acCurDB.Extmax;
                }
            }
            else
            {
                if (nCurVport == 1) //paper space
                {
                    if (pMin.Equals(new Point3d()) == true && pMax.Equals(new Point3d()) == true)
                    {
                        pMin = acCurDB.Pextmin; //Get the extends of paper space
                        pMax = acCurDB.Pextmax;
                    }
                }
                else
                {
                    if (pMin.Equals(new Point3d()) == true && pMax.Equals(new Point3d()) == true)
                    {
                        pMin = acCurDB.Extmin; //Get the extends of model space
                        pMax = acCurDB.Extmax;
                    }
                }
            }
            using (Transaction acTrans = acCurDB.TransactionManager.StartTransaction())
            {
                using (ViewTableRecord acView = acDoc.Editor.GetCurrentView())
                {
                    Extents3d eExtents;

                    Matrix3d matWCStoDCS;
                    matWCStoDCS = Matrix3d.PlaneToWorld(acView.ViewDirection);
                    matWCStoDCS = Matrix3d.Displacement(acView.Target - Point3d.Origin) * matWCStoDCS;
                    matWCStoDCS = Matrix3d.Rotation(-acView.ViewTwist, acView.ViewDirection, acView.Target) * matWCStoDCS;

                    if (pCenter.DistanceTo(Point3d.Origin) != 0)
                    {
                        pMin = new Point3d(pCenter.X - (acView.Width / 2), pCenter.Y - (acView.Height / 2), 0);
                        pMax = new Point3d((acView.Width / 2) + pCenter.X, (acView.Height / 2) + pCenter.Y, 0);
                    }
                    using (Line acLine = new Line(pMin, pMax))
                    {
                        eExtents = new Extents3d(acLine.Bounds.Value.MinPoint, acLine.Bounds.Value.MaxPoint);
                    }
                    double dViewRatio;
                    dViewRatio = (acView.Width / acView.Height);

                    matWCStoDCS = matWCStoDCS.Inverse();
                    eExtents.TransformBy(matWCStoDCS);

                    double dWidth;
                    double dHeight;
                    Point2d pNewCentPt;

                    if(pCenter.DistanceTo(Point3d.Origin) != 0)
                    {
                        dWidth = acView.Width;
                        dHeight = acView.Height;

                        if (dFactor == 0)
                        {
                            pCenter = pCenter.TransformBy(matWCStoDCS);
                        }
                        pNewCentPt = new Point2d(pCenter.X, pCenter.Y);
                    }
                    else
                    {
                        dWidth = eExtents.MaxPoint.X - eExtents.MinPoint.X;
                        dHeight = eExtents.MaxPoint.Y - eExtents.MinPoint.Y;

                        pNewCentPt = new Point2d((eExtents.MaxPoint.X + eExtents.MinPoint.X) * 0.5, (eExtents.MaxPoint.Y + eExtents.MinPoint.Y) * 0.5);
                    }
                    if (dWidth > (dHeight * dViewRatio))
                    {
                        dHeight = dWidth / dViewRatio;
                    }
                    if (dFactor != 0)
                    {
                        acView.Height = dHeight * dFactor;
                        acView.Width = dWidth * dFactor;
                    }
                    acView.CenterPoint = pNewCentPt;
                    acDoc.Editor.SetCurrentView(acView);
                }
                acTrans.Commit();
            }
        }
    }
}
