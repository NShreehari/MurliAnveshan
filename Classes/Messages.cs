using System.Runtime.InteropServices;
using System.Windows.Forms;

using SelfControls.Controls;

namespace MurliAnveshan
{
    class Messages
    {
        public static DialogResult LoginErrorMessage(string msg)
        {
            DialogResult userChoice = MessageBox.Show(msg, "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            return userChoice;
        }

        public static DialogResult ErrorMessage(string msg)
        {
            DialogResult userChoice = MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            return userChoice;
        }

        public static DialogResult InfoMessage(string msg)
        {
            DialogResult userChoice = MessageBox.Show(msg, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            return userChoice;
        }

        public static DialogResult ConfirmMessage(string msg)
        {
            DialogResult userChoice = MessageBox.Show(msg, "DELETE?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return userChoice;
        }

        public static DialogResult WarningMessage(string msg)
        {
            DialogResult userChoice = MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            return userChoice;
        }

        public static DialogResult ExceptionMessage(string msg)
        {
            DialogResult userChoice = MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            return userChoice;
        }
    }



    class SelfMessageBoxWrapper
    {
        public static DialogResult ShowInfoMessage(string msg, [Optional] string toBeBoldText)
        {
            DialogResult userChoice = SelfMessageBox3.Show(msg, toBeBoldText, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            return userChoice;
        }

        public static DialogResult ShowConfirmMessage(string msg, [Optional] string toBeBoldText)
        {
            DialogResult userChoice = SelfMessageBox3.Show(msg, toBeBoldText, "DELETE?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return userChoice;
        }

        public static DialogResult ShowWarningMessage(string msg, [Optional] string toBeBoldText)
        {
            DialogResult userChoice = SelfMessageBox3.Show(msg, toBeBoldText, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            return userChoice;
        }

        public static DialogResult ShowLoginErrorMessage(string msg, [Optional] string toBeBoldText)
        {
            DialogResult userChoice = SelfMessageBox3.Show(msg, toBeBoldText, "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            return userChoice;
        }

        public static DialogResult ShowErrorMessage(string msg, [Optional] string toBeBoldText)
        {
            DialogResult userChoice = SelfMessageBox3.Show(msg, toBeBoldText, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            return userChoice;
        }

        
        public static DialogResult ShowExceptionMessage(string msg, [Optional] string toBeBoldText)
        {
            DialogResult userChoice = SelfMessageBox3.Show(msg, toBeBoldText, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            return userChoice;
        }
    }
}
