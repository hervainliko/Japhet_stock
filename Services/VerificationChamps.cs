using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Services
{
    static class VerificationChamps
    {
        public static void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Autoriser le caractère de contrôle (comme retour arrière)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Autoriser les chiffres et le point décimal
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Vérifier s'il y a déjà un point décimal dans le TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

    }
}
