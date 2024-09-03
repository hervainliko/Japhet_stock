using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Services
{
    public static class RoundedPanel
    {
        public static void SetRoundedPanel(Panel panel, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = cornerRadius;
            int diameter = radius * 2;

            Rectangle arc = new Rectangle(0, 0, diameter, diameter);

            // Coin supérieur gauche
            path.AddArc(arc, 180, 90);

            // Ligne horizontale supérieure
            path.AddLine(radius, 0, panel.Width - radius, 0);

            // Coin supérieur droit
            arc.X = panel.Width - diameter;
            path.AddArc(arc, 270, 90);

            // Ligne verticale droite
            path.AddLine(panel.Width, radius, panel.Width, panel.Height - radius);

            // Coin inférieur droit
            arc.Y = panel.Height - diameter;
            path.AddArc(arc, 0, 90);

            // Ligne horizontale inférieure
            path.AddLine(panel.Width - radius, panel.Height, radius, panel.Height);

            // Coin inférieur gauche
            arc.X = 0;
            path.AddArc(arc, 90, 90);

            // Fermeture du chemin
            path.CloseFigure();

            // Applique le chemin comme région du panneau
            panel.Region = new Region(path);
        }


        public static void SetRoundedButton(Button button, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = cornerRadius;
            int diameter = radius * 2;

            Rectangle arc = new Rectangle(0, 0, diameter, diameter);

            // Coin supérieur gauche
            path.AddArc(arc, 180, 90);

            // Ligne horizontale supérieure
            path.AddLine(radius, 0, button.Width - radius, 0);

            // Coin supérieur droit
            arc.X = button.Width - diameter;
            path.AddArc(arc, 270, 90);

            // Ligne verticale droite
            path.AddLine(button.Width, radius, button.Width, button.Height - radius);

            // Coin inférieur droit
            arc.Y = button.Height - diameter;
            path.AddArc(arc, 0, 90);

            // Ligne horizontale inférieure
            path.AddLine(button.Width - radius, button.Height, radius, button.Height);

            // Coin inférieur gauche
            arc.X = 0;
            path.AddArc(arc, 90, 90);

            // Fermeture du chemin
            path.CloseFigure();

            // Appliquer le chemin comme région du bouton
            button.Region = new Region(path);
        }

        public static void SetRoundedPict(PictureBox button, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = cornerRadius;
            int diameter = radius * 2;

            Rectangle arc = new Rectangle(0, 0, diameter, diameter);

            // Coin supérieur gauche
            path.AddArc(arc, 180, 90);

            // Ligne horizontale supérieure
            path.AddLine(radius, 0, button.Width - radius, 0);

            // Coin supérieur droit
            arc.X = button.Width - diameter;
            path.AddArc(arc, 270, 90);

            // Ligne verticale droite
            path.AddLine(button.Width, radius, button.Width, button.Height - radius);

            // Coin inférieur droit
            arc.Y = button.Height - diameter;
            path.AddArc(arc, 0, 90);

            // Ligne horizontale inférieure
            path.AddLine(button.Width - radius, button.Height, radius, button.Height);

            // Coin inférieur gauche
            arc.X = 0;
            path.AddArc(arc, 90, 90);

            // Fermeture du chemin
            path.CloseFigure();

            // Appliquer le chemin comme région du bouton
            button.Region = new Region(path);
        }

        public static void SetRoundedForm(Form button, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = cornerRadius;
            int diameter = radius * 2;

            Rectangle arc = new Rectangle(0, 0, diameter, diameter);

            // Coin supérieur gauche
            path.AddArc(arc, 180, 90);

            // Ligne horizontale supérieure
            path.AddLine(radius, 0, button.Width - radius, 0);

            // Coin supérieur droit
            arc.X = button.Width - diameter;
            path.AddArc(arc, 270, 90);

            // Ligne verticale droite
            path.AddLine(button.Width, radius, button.Width, button.Height - radius);

            // Coin inférieur droit
            arc.Y = button.Height - diameter;
            path.AddArc(arc, 0, 90);

            // Ligne horizontale inférieure
            path.AddLine(button.Width - radius, button.Height, radius, button.Height);

            // Coin inférieur gauche
            arc.X = 0;
            path.AddArc(arc, 90, 90);

            // Fermeture du chemin
            path.CloseFigure();

            // Appliquer le chemin comme région du bouton
            button.Region = new Region(path);
        }
    }
}
