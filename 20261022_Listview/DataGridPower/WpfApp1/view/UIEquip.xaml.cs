using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.model;

namespace WpfApp1.view
{
    /// <summary>
    /// Lógica de interacción para UIEquip.xaml
    /// </summary>
    public partial class UIEquip : UserControl
    {
        public UIEquip()
        {
            InitializeComponent();
        }

        // Propietat Equip
        public Equip UnEquip
        {
            get { return (Equip)GetValue(UnEquipProperty); }
            set { SetValue(UnEquipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnEquip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnEquipProperty =
            DependencyProperty.Register(nameof(UnEquip), typeof(Equip), typeof(UIEquip), new PropertyMetadata(null));




    }
}
