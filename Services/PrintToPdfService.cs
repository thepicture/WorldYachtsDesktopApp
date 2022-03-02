using System;
using System.Diagnostics;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Media;

namespace WorldYachtsDesktopApp.Services
{
    public class PrintToPdfService : IPrintService<Visual>
    {
        public bool Print(Visual target, string description)
        {
            try
            {
                using (PrintServer server = new PrintServer())
                using (PrintQueue queue =
                    new PrintQueue(server,
                                   "Microsoft Print to PDF"))
                {
                    PrintDialog dialog = new PrintDialog
                    {
                        PrintQueue = queue,
                    };
                    bool? wantsToPrint = dialog.ShowDialog();
                    if (wantsToPrint != null && (bool)wantsToPrint)
                    {
                        dialog.PrintVisual(target, description);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return false;
        }
    }
}
