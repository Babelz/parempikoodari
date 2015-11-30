using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VideoStoreApplication
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        #region Properties
        /// <summary>
        /// Gets or sets the response text.
        /// </summary>
        public string ResponseText
        {
            get
            {
                return dialogInputTextBox.Text;
            }
            set
            {
                dialogInputTextBox.Text = value;
            }
        }
        /// <summary>
        /// Gets or sets the header of the dialog window.
        /// </summary>
        public string Header
        {
            get
            {
                return (string)headerLabel.Content;
            }
            set
            {
                headerLabel.Content = value;
            }
        }
        /// <summary>
        /// Gets or sets what to enter string.
        /// Used to tell the user what he is supposed to enter to
        /// the field.
        /// </summary>
        public string WhatToEnter
        {
            get
            {
                return (string)whatToEnterLabel.Content;
            }
            set
            {
                whatToEnterLabel.Content = value;
            }
        }
        #endregion

        public DialogWindow()
        {
            Topmost = true;

            InitializeComponent();
        }

        #region Event handlers
        private void dialogButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        #endregion
    }
}
