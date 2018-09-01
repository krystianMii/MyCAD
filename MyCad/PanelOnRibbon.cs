using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;


namespace MyCad
{
    public class PanelOnRibbon
    {
        public void Start()
        {
            RibbonControl rbCont = ComponentManager.Ribbon;

            if (rbCont != null)
            {
                RibbonTab rbTab = rbCont.FindTab("MyCad");

                if (rbTab != null)
                {
                    rbCont.Tabs.Remove(rbTab);
                }
                rbTab = new RibbonTab();
                rbTab.Title = "MyCad";
                rbTab.Id = "MyCad";
                rbCont.Tabs.Add(rbTab);
                addContent(rbTab);
            }
        }

        public static void addContent(RibbonTab rbTab)
        {
            rbTab.Panels.Add(AddOnePanel());
        }

        public static RibbonPanel AddOnePanel()
        {
            RibbonButton rbBut;
            RibbonPanelSource rbPanSr = new RibbonPanelSource();
            rbPanSr.Title = "Panels";
            RibbonPanel rbPan = new RibbonPanel();
            rbPan.Source = rbPanSr;

            RibbonButton rci = new RibbonButton();
            rci.Name = "MyCad2";

            rbPanSr.DialogLauncher = rci;

            rbBut = new RibbonButton();
            rbBut.Name = "Create Panel";
            rbBut.ShowText = true;
            rbBut.Text = "Create Panel";
            rbPanSr.Items.Add(rbBut);
            return rbPan;

        }
    }
}
