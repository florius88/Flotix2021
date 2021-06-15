using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para DialogImageView.xaml
    /// </summary>
    public partial class DialogImageView : Window
    {
        public string HeadPortrait
        {
            get { return (string)GetValue(HeadPortraitProperty); }
            set { SetValue(HeadPortraitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeadPortrait.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadPortraitProperty =
            DependencyProperty.Register("HeadPortrait", typeof(string), typeof(DialogImageView), new PropertyMetadata(null));

        public DialogImageView(BitmapSource imgpath)
        {
            InitializeComponent();
            imagenPermiso.Source = imgpath;
        }
    }
}