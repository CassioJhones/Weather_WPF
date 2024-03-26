using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;
using System.Windows;
using System.Windows.Media;

namespace TimeVersion
{
    /// <summary>
    ///JANELA FILHA = NOTIFICACAO
    /// </summary>
    public partial class Mensagem : ChildWindow
    {

        public Mensagem()
        {
            InitializeComponent();
            AllowMove = true;
            ShowTitleBar = true;
            TitleBarBackground = Brushes.IndianRed;
            TitleForeground = Brushes.White;
            NonActiveGlowBrush = Brushes.IndianRed;
            TitleBarNonActiveBackground = Brushes.IndianRed;
            TitleHorizontalAlignment = HorizontalAlignment.Center;
            TitleVerticalAlignment = VerticalAlignment.Center;
            CloseByEscape = true;

        }

        private void confirmar_click(object sender, RoutedEventArgs e) => this.Close();

        private void teste_fechar(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MetroDialogSettings config = new MetroDialogSettings
            {
                AffirmativeButtonText = "SIM",
                NegativeButtonText = "NAO",

            };

            //MessageDialogResult resultado =
            //    DialogCoordinator.Instance.ShowModalMessageExternal(Application.Current.MainWindow, "TITULO", "MENSAGEM", MessageDialogStyle.AffirmativeAndNegative);

            //if (resultado == MessageDialogResult.Negative)
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
